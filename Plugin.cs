using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System;
using UnityEngine;

#nullable disable
namespace RealA10;

[BepInPlugin("fun.assassin1076.realA10", "RealA10", "0.0.1")]
public class Plugin : BaseUnityPlugin
{
  internal static ManualLogSource Logger;
  public bool done;

  // Which plane's engine to copy the template from, and which plane to install it on.
  // Change these in the BepInEx config file to swap engines between any two planes
  // without recompiling. Codes correspond to AircraftDefinition.code (e.g. "T/A-30", "A-19").
  private ConfigEntry<string> sourceAircraftCode;
  private ConfigEntry<string> targetAircraftCode;
  private ConfigEntry<string> sourceEnginePath;
  private ConfigEntry<string> sourceNozzlePath;
  private ConfigEntry<bool> stripTargetProps;

  private void Awake()
  {
    Plugin.Logger = base.Logger;
    Plugin.Logger.LogInfo("Plugin RealA10 is loaded!");

    sourceAircraftCode = Config.Bind("EngineSwap", "SourceAircraftCode", "T/A-30",
        "AircraftDefinition.code of the plane whose engine will be used as the template.");
    targetAircraftCode = Config.Bind("EngineSwap", "TargetAircraftCode", "A-19",
        "AircraftDefinition.code of the plane that will receive the swapped-in engine.");
    sourceEnginePath = Config.Bind("EngineSwap", "SourceEnginePath", "engine_R",
        "Transform path (relative to the source plane's root) where the template Turbojet lives.");
    sourceNozzlePath = Config.Bind("EngineSwap", "SourceNozzlePath", "nozzle_R",
        "Transform path (relative to SourceEnginePath) where the template JetNozzle lives.");
    stripTargetProps = Config.Bind("EngineSwap", "StripTargetProps", true,
        "If true, disables the target plane's propellers and removes its TurbineEngine components before installing jet engines.");

    Plugin.Logger.LogInfo($"[Plugin] Config loaded: SourceAircraftCode='{sourceAircraftCode.Value}', " +
        $"TargetAircraftCode='{targetAircraftCode.Value}', SourceEnginePath='{sourceEnginePath.Value}', " +
        $"SourceNozzlePath='{sourceNozzlePath.Value}', StripTargetProps={stripTargetProps.Value}");
  }

  private void Update()
  {
    if (this.done)
      return;

    if (Encyclopedia.i == null)
    {
      Plugin.Logger.LogInfo("[Plugin] Waiting for target prefabs to load...");
      return;
    }

    string sourceCode = sourceAircraftCode.Value;
    string targetCode = targetAircraftCode.Value;

    Plugin.Logger.LogInfo($"[Plugin] Encyclopedia loaded. Looking up aircraft definitions for source='{sourceCode}' and target='{targetCode}'.");

    AircraftDefinition sourceDef = Encyclopedia.i.aircraft.Find(def => def.code == sourceCode);
    AircraftDefinition targetDef = Encyclopedia.i.aircraft.Find(def => def.code == targetCode);

    if (sourceDef == null)
    {
      Plugin.Logger.LogWarning($"[Plugin] Could not find AircraftDefinition with code '{sourceCode}'. Aborting engine swap.");
      this.done = true;
      return;
    }
    if (targetDef == null)
    {
      Plugin.Logger.LogWarning($"[Plugin] Could not find AircraftDefinition with code '{targetCode}'. Aborting engine swap.");
      this.done = true;
      return;
    }

    Plugin.Logger.LogInfo($"[Plugin] Found source aircraft '{sourceDef.code}' (prefab='{sourceDef.unitPrefab.name}') " +
        $"and target aircraft '{targetDef.code}' (prefab='{targetDef.unitPrefab.name}').");

    if (stripTargetProps.Value)
    {
      Plugin.Logger.LogInfo($"[Plugin] Stripping propellers/TurbineEngine from target prefab '{targetDef.unitPrefab.name}'.");
      PlanePrefabModifier.ModifyPlanePrefab(targetDef.unitPrefab);
    }
    else
    {
      Plugin.Logger.LogInfo("[Plugin] StripTargetProps is disabled; skipping prop/TurbineEngine removal on target.");
    }

    Plugin.Logger.LogInfo($"[Plugin] Extracting engine templates from '{sourceDef.unitPrefab.name}' " +
        $"(enginePath='{sourceEnginePath.Value}', nozzlePath='{sourceNozzlePath.Value}').");

    if (JetEngineTemplateHelper.TryGetJetEngineTemplates(
            sourceDef.unitPrefab,
            out Turbojet turbojetTemplate,
            out JetNozzle nozzleTemplate,
            sourceEnginePath.Value,
            sourceNozzlePath.Value))
    {
      Plugin.Logger.LogInfo($"[Plugin] Templates found. Assembling jet engines onto '{targetDef.unitPrefab.name}'.");
      JetEngineAssembler.AssembleJetEngines(targetDef.unitPrefab, turbojetTemplate, nozzleTemplate);
      Plugin.Logger.LogInfo($"[Plugin] Engine swap complete: '{sourceDef.code}' engine -> '{targetDef.code}'.");
    }
    else
    {
      Plugin.Logger.LogWarning($"[Plugin] Failed to extract engine templates from '{sourceDef.unitPrefab.name}'. Engine swap aborted.");
    }

    this.done = true;
  }
}
