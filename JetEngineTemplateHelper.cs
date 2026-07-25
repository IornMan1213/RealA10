// Decompiled with JetBrains decompiler
// Type: RealA10.JetEngineTemplateHelper
// Assembly: RealA10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 532C87AC-4BC4-4B3D-ACF1-A050F52D51FF
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\BepInEx\plugins\RealA10.dll

using UnityEngine;

#nullable disable
namespace RealA10;

public static class JetEngineTemplateHelper
{
  public static bool TryGetJetEngineTemplates(
    GameObject root,
    out Turbojet turbojetTemplate,
    out JetNozzle nozzleTemplate)
  {
    turbojetTemplate = (Turbojet) null;
    nozzleTemplate = (JetNozzle) null;
    if (Object.op_Equality((Object) root, (Object) null))
    {
      Debug.LogWarning((object) "[JetEngineTemplateHelper] Root is null.");
      return false;
    }
    Transform transform1 = root.transform.Find("engine_R");
    if (Object.op_Equality((Object) transform1, (Object) null))
    {
      Debug.LogWarning((object) "[JetEngineTemplateHelper] engine_R not found.");
      return false;
    }
    turbojetTemplate = ((Component) transform1).GetComponent<Turbojet>();
    if (Object.op_Equality((Object) turbojetTemplate, (Object) null))
    {
      Debug.LogWarning((object) "[JetEngineTemplateHelper] Turbojet not found on engine_R.");
      return false;
    }
    Transform transform2 = transform1.Find("nozzle_R");
    if (Object.op_Equality((Object) transform2, (Object) null))
    {
      Debug.LogWarning((object) "[JetEngineTemplateHelper] nozzle_R not found under engine_R.");
      return false;
    }
    nozzleTemplate = ((Component) transform2).GetComponent<JetNozzle>();
    if (!Object.op_Equality((Object) nozzleTemplate, (Object) null))
      return true;
    Debug.LogWarning((object) "[JetEngineTemplateHelper] JetNozzle not found on nozzle_R.");
    return false;
  }
}
