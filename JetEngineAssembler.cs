using UnityEngine;

#nullable disable
namespace RealA10;

public static class JetEngineAssembler
{
  public static void AssembleJetEngines(
    GameObject root,
    Turbojet turbojetTemplate,
    JetNozzle nozzleTemplate)
  {
    if (root == null)
    {
      Debug.LogWarning("[JetEngineAssembler] AssembleJetEngines called with null root.");
      return;
    }

    Debug.Log($"[JetEngineAssembler] Assembling jet engines on '{root.name}' " +
               $"using turbojetTemplate='{(turbojetTemplate != null ? turbojetTemplate.name : "NULL")}', " +
               $"nozzleTemplate='{(nozzleTemplate != null ? nozzleTemplate.name : "NULL")}'");

    AssembleSingleEngine(root, "wing1_L/engineMount_L/engine_L", "wing1_L/engineMount_L/engine_L/hub_L", turbojetTemplate, nozzleTemplate);
    AssembleSingleEngine(root, "wing1_R/engineMount_R/engine_R", "wing1_R/engineMount_R/engine_R/hub_R", turbojetTemplate, nozzleTemplate);

    Debug.Log($"[JetEngineAssembler] Finished assembling jet engines on '{root.name}'.");
  }

  private static void AssembleSingleEngine(
    GameObject root,
    string enginePath,
    string nozzlePath,
    Turbojet turbojetTemplate,
    JetNozzle nozzleTemplate)
  {
    Debug.Log($"[JetEngineAssembler] Looking for engine transform at '{enginePath}' under '{root.name}'.");

    Transform engineTransform = root.transform.Find(enginePath);
    if (engineTransform == null)
    {
      Debug.LogWarning($"[JetEngineAssembler] Engine path not found: '{enginePath}' (searched under '{root.name}').");
      return;
    }
    Debug.Log($"[JetEngineAssembler] Found engine transform '{engineTransform.name}' at '{enginePath}'.");

    Turbojet newTurbojet = engineTransform.gameObject.AddComponent<Turbojet>();
    Debug.Log($"[JetEngineAssembler] Added Turbojet component to '{engineTransform.name}'. Initializing from template '{(turbojetTemplate != null ? turbojetTemplate.name : "NULL")}'.");
    TurbojetUtils.InitializeFromTemplate(turbojetTemplate, newTurbojet);

    Debug.Log($"[JetEngineAssembler] Looking for nozzle transform at '{nozzlePath}' under '{root.name}'.");
    Transform nozzleTransform = root.transform.Find(nozzlePath);
    if (nozzleTransform == null)
    {
      Debug.LogWarning($"[JetEngineAssembler] Nozzle path not found: '{nozzlePath}' (searched under '{root.name}'). Turbojet was added but has no nozzle linked.");
      return;
    }
    Debug.Log($"[JetEngineAssembler] Found nozzle transform '{nozzleTransform.name}' at '{nozzlePath}'.");

    JetNozzle newNozzle = nozzleTransform.gameObject.AddComponent<JetNozzle>();
    Debug.Log($"[JetEngineAssembler] Added JetNozzle component to '{nozzleTransform.name}'. Initializing from template '{(nozzleTemplate != null ? nozzleTemplate.name : "NULL")}'.");
    JetNozzleUtils.InitializeFromTemplate(nozzleTemplate, newNozzle);

    newTurbojet.nozzles = new JetNozzle[1] { newNozzle };
    newNozzle.turbojet = newTurbojet;

    Debug.Log($"[JetEngineAssembler] Jet engine assembled: '{engineTransform.name}' -> '{nozzleTransform.name}' (linked Turbojet<->JetNozzle).");
  }

  /// <summary>
  /// Copies a Turbojet/JetNozzle setup from a source plane's existing engine onto
  /// a different target plane, using the source's live component values as the template.
  /// Useful for swapping one plane's engine config onto another plane.
  /// </summary>
  public static void TransferJetEngine(
    GameObject sourcePlane,
    GameObject targetPlane,
    string sourceEnginePath,
    string sourceNozzlePath,
    string targetEnginePath,
    string targetNozzlePath)
  {
    if (sourcePlane == null || targetPlane == null)
    {
      Debug.LogWarning($"[JetEngineAssembler] TransferJetEngine called with null plane(s). sourcePlane={(sourcePlane != null ? sourcePlane.name : "NULL")}, targetPlane={(targetPlane != null ? targetPlane.name : "NULL")}");
      return;
    }

    Debug.Log($"[JetEngineAssembler] Transferring engine from '{sourcePlane.name}' ({sourceEnginePath}) to '{targetPlane.name}' ({targetEnginePath}).");

    Transform sourceEngineTransform = sourcePlane.transform.Find(sourceEnginePath);
    if (sourceEngineTransform == null)
    {
      Debug.LogWarning($"[JetEngineAssembler] Source engine path not found: '{sourceEnginePath}' on '{sourcePlane.name}'.");
      return;
    }

    Turbojet sourceTurbojet = sourceEngineTransform.GetComponent<Turbojet>();
    if (sourceTurbojet == null)
    {
      Debug.LogWarning($"[JetEngineAssembler] No Turbojet component found on source transform '{sourceEngineTransform.name}'.");
      return;
    }
    Debug.Log($"[JetEngineAssembler] Found source Turbojet on '{sourceEngineTransform.name}'.");

    Transform sourceNozzleTransform = sourcePlane.transform.Find(sourceNozzlePath);
    JetNozzle sourceNozzle = sourceNozzleTransform != null ? sourceNozzleTransform.GetComponent<JetNozzle>() : null;
    if (sourceNozzle == null)
    {
      Debug.LogWarning($"[JetEngineAssembler] No JetNozzle component found at source path '{sourceNozzlePath}' on '{sourcePlane.name}'.");
      return;
    }
    Debug.Log($"[JetEngineAssembler] Found source JetNozzle on '{sourceNozzleTransform.name}'.");

    Debug.Log($"[JetEngineAssembler] Applying source engine config to target '{targetPlane.name}'.");
    AssembleSingleEngine(targetPlane, targetEnginePath, targetNozzlePath, sourceTurbojet, sourceNozzle);

    Debug.Log($"[JetEngineAssembler] Transfer complete: '{sourcePlane.name}' engine -> '{targetPlane.name}'.");
  }
}