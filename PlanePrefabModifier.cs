// Decompiled with JetBrains decompiler
// Type: RealA10.PlanePrefabModifier
// Assembly: RealA10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 532C87AC-4BC4-4B3D-ACF1-A050F52D51FF
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\BepInEx\plugins\RealA10.dll

using UnityEngine;

#nullable disable
namespace RealA10;

public static class PlanePrefabModifier
{
  public static void ModifyPlanePrefab(GameObject root)
  {
    if ((root == null))
    {
      Debug.LogWarning((object) "[PlanePrefabModifier] Root GameObject is null.");
    }
    else
    {
      string[] strArray1 = new string[4]
      {
        "wing1_L/engineMount_L/engine_L/hub_L/hub1_L/prop1_L",
        "wing1_L/engineMount_L/engine_L/hub_L/hub2_L/prop2_L",
        "wing1_R/engineMount_R/engine_R/hub_R/hub1_R/prop1_R",
        "wing1_R/engineMount_R/engine_R/hub_R/hub2_R/prop2_R"
      };
      foreach (string str in strArray1)
      {
        Transform transform = root.transform.Find(str);
        if ((transform != null))
        {
          ((Component) transform).gameObject.SetActive(false);
          Debug.Log((object) ("[PlanePrefabModifier] Disabled prop: " + str));
        }
        else
          Debug.LogWarning((object) ("[PlanePrefabModifier] Prop path not found: " + str));
      }
      string[] strArray2 = new string[2]
      {
        "wing1_L/engineMount_L/engine_L",
        "wing1_R/engineMount_R/engine_R"
      };
      foreach (string str in strArray2)
      {
        Transform transform = root.transform.Find(str);
        if ((transform == null))
        {
          Debug.LogWarning((object) ("[PlanePrefabModifier] Engine path not found: " + str));
        }
        else
        {
          TurbineEngine component = ((Component) transform).GetComponent<TurbineEngine>();
          if ((component != null))
          {
            Object.Destroy((Object) component);
            Debug.Log((object) ("[PlanePrefabModifier] Removed TurbineEngine from: " + str));
          }
          else
            Debug.Log((object) ("[PlanePrefabModifier] TurbineEngine not found on: " + str));
        }
      }
    }
  }
}

