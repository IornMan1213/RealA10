// Decompiled with JetBrains decompiler
// Type: RealA10.JetEngineAssembler
// Assembly: RealA10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 532C87AC-4BC4-4B3D-ACF1-A050F52D51FF
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\BepInEx\plugins\RealA10.dll

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
    if (Object.op_Equality((Object) root, (Object) null))
    {
      Debug.LogWarning((object) "[JetEngineAssembler] Root is null.");
    }
    else
    {
      JetEngineAssembler.AssembleSingleEngine(root, "wing1_L/engineMount_L/engine_L", "wing1_L/engineMount_L/engine_L/hub_L", turbojetTemplate, nozzleTemplate);
      JetEngineAssembler.AssembleSingleEngine(root, "wing1_R/engineMount_R/engine_R", "wing1_R/engineMount_R/engine_R/hub_R", turbojetTemplate, nozzleTemplate);
    }
  }

  private static void AssembleSingleEngine(
    GameObject root,
    string enginePath,
    
    string nozzlePath,
    Turbojet turbojetTemplate,
    JetNozzle nozzleTemplate

  
    )
  {
    Transform transform1 = root.transform.Find(enginePath);
    if (Object.op_Equality((Object) transform1, (Object) null))
    {
      Debug.LogWarning((object) ("[JetEngineAssembler] Engine path not found: " + enginePath));
    }
    else
    {
      Turbojet target1 = ((Component) transform1).gameObject.AddComponent<Turbojet>();
      TurbojetUtils.InitializeFromTemplate(turbojetTemplate, target1);
      Transform transform2 = root.transform.Find(nozzlePath);
      if (Object.op_Equality((Object) transform2, (Object) null))
      {
        Debug.LogWarning((object) ("[JetEngineAssembler] Nozzle path not found: " + nozzlePath));
      }
      else
      {
        JetNozzle target2 = ((Component) transform2).gameObject.AddComponent<JetNozzle>();
        JetNozzleUtils.InitializeFromTemplate(nozzleTemplate, target2);
        target1.nozzles = new JetNozzle[1]{ target2 };
        target2.turbojet = target1;
        Debug.Log((object) $"[JetEngineAssembler] Jet engine assembled at {((Object) transform1).name} -> {((Object) transform2).name}");
      }
    }
  }
}


public class Plugin : BaseUnityPlugin
{
  internal static ManualLogSource Logger;
  public bool done;

  private void Awake()
  {
    Plugin.Logger = this.Logger;
    Plugin.Logger.LogInfo((AssembleJetEngines) (enginePath, nozzlePath, turbojetTemplate, nozzleTemplate));
  }
}
