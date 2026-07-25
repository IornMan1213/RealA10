using UnityEngine;

#nullable disable
namespace RealA10;

public static class JetEngineTemplateHelper
{
  /// <summary>
  /// Locates a Turbojet/JetNozzle pair on a source plane's prefab to use as a template.
  /// enginePath/nozzlePath default to the T/A-30's rig ("engine_R" / "nozzle_R" under it),
  /// but can be overridden to pull a template engine from any other plane's prefab.
  /// </summary>
  public static bool TryGetJetEngineTemplates(
    GameObject root,
    out Turbojet turbojetTemplate,
    out JetNozzle nozzleTemplate,
    string enginePath = "engine_R",
    string nozzlePath = "nozzle_R")
  {
    turbojetTemplate = null;
    nozzleTemplate = null;

    if (root == null)
    {
      Debug.LogWarning("[JetEngineTemplateHelper] Root GameObject is null.");
      return false;
    }

    Debug.Log($"[JetEngineTemplateHelper] Looking for template engine at '{enginePath}' under '{root.name}'.");
    Transform engineTransform = root.transform.Find(enginePath);
    if (engineTransform == null)
    {
      Debug.LogWarning($"[JetEngineTemplateHelper] Engine path not found: '{enginePath}' (searched under '{root.name}').");
      return false;
    }
    Debug.Log($"[JetEngineTemplateHelper] Found engine transform '{engineTransform.name}'.");

    turbojetTemplate = engineTransform.GetComponent<Turbojet>();
    if (turbojetTemplate == null)
    {
      Debug.LogWarning($"[JetEngineTemplateHelper] No Turbojet component found on '{engineTransform.name}'.");
      return false;
    }
    Debug.Log($"[JetEngineTemplateHelper] Found Turbojet template on '{engineTransform.name}' (maxThrust={turbojetTemplate.maxThrust}, maxRPM={turbojetTemplate.maxRPM}).");

    Debug.Log($"[JetEngineTemplateHelper] Looking for template nozzle at '{nozzlePath}' under '{engineTransform.name}'.");
    Transform nozzleTransform = engineTransform.Find(nozzlePath);
    if (nozzleTransform == null)
    {
      Debug.LogWarning($"[JetEngineTemplateHelper] Nozzle path not found: '{nozzlePath}' (searched under '{engineTransform.name}').");
      turbojetTemplate = null;
      return false;
    }
    Debug.Log($"[JetEngineTemplateHelper] Found nozzle transform '{nozzleTransform.name}'.");

    nozzleTemplate = nozzleTransform.GetComponent<JetNozzle>();
    if (nozzleTemplate == null)
    {
      Debug.LogWarning($"[JetEngineTemplateHelper] No JetNozzle component found on '{nozzleTransform.name}'.");
      turbojetTemplate = null;
      return false;
    }

    Debug.Log($"[JetEngineTemplateHelper] Successfully collected templates from '{root.name}': Turbojet='{turbojetTemplate.name}', JetNozzle='{nozzleTemplate.name}'.");
    return true;
  }
}
