// Decompiled with JetBrains decompiler
// Type: RealA10.Plugin
// Assembly: RealA10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 532C87AC-4BC4-4B3D-ACF1-A050F52D51FF
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\BepInEx\plugins\RealA10.dll

using BepInEx;
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

  private void Awake()
  {
    Plugin.Logger = base.Logger;
    Plugin.Logger.LogInfo((object) "Plugin RealA10 is loaded!");
  }

  private void Update()
  {
    if (this.done)
      return;
    if ((Encyclopedia.i == null))
    {
      Plugin.Logger.LogInfo((object) "Waiting for target prefabs to load...");
    }
    else
    {
      AircraftDefinition aircraftDefinition1 = Encyclopedia.i.aircraft.Find((Predicate<AircraftDefinition>) (def => ((UnitDefinition) def).code == "T/A-30"));
      AircraftDefinition aircraftDefinition2 = Encyclopedia.i.aircraft.Find((Predicate<AircraftDefinition>) (def => ((UnitDefinition) def).code == "A-19"));
      PlanePrefabModifier.ModifyPlanePrefab(((UnitDefinition) aircraftDefinition2).unitPrefab);
      Turbojet turbojetTemplate;
      JetNozzle nozzleTemplate;
      if (JetEngineTemplateHelper.TryGetJetEngineTemplates(((UnitDefinition) aircraftDefinition1).unitPrefab, out turbojetTemplate, out nozzleTemplate))
        JetEngineAssembler.AssembleJetEngines(((UnitDefinition) aircraftDefinition2).unitPrefab, turbojetTemplate, nozzleTemplate);
      this.done = true;
    }
  }
}

