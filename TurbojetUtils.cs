// Decompiled with JetBrains decompiler
// Type: RealA10.TurbojetUtils
// Assembly: RealA10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 532C87AC-4BC4-4B3D-ACF1-A050F52D51FF
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\BepInEx\plugins\RealA10.dll

using UnityEngine;

#nullable disable
namespace RealA10;

public static class TurbojetUtils
{
  public static void InitializeFromTemplate(Turbojet template, Turbojet target)
  {
    if (Object.op_Equality((Object) template, (Object) null) || Object.op_Equality((Object) target, (Object) null))
    {
      Debug.LogWarning((object) "[TurbojetUtils] Template or target is null.");
    }
    else
    {
      target.maxThrust = 600000f;
      target.damageFactor = template.damageFactor;
      target.turbineAudio = TurbojetUtils.CreateAudioSourceFromTemplate(template.turbineAudio, ((Component) target).transform);
      target.thrustVectoring = template.thrustVectoring;
      target.thrustVectoringGain = template.thrustVectoringGain;
      target.throttleRemap = template.throttleRemap;
      target.thrustVectoringMaxAirspeed = template.thrustVectoringMaxAirspeed;
      target.minDensity = template.minDensity;
      target.splitThrustFactor = template.splitThrustFactor;
      target.altitudeThrust = template.altitudeThrust;
      target.nozzles = (JetNozzle[]) null;
      target.vectoringTransforms = (Transform[]) null;
      target.turbineMaxPitch = template.turbineMaxPitch;
      target.minRPM = template.minRPM;
      target.maxRPM = template.maxRPM;
      target.maxSpeed = template.maxSpeed;
      target.spoolRate = template.spoolRate;
      target.startupRate = template.startupRate;
      target.fuelConsumptionMin = template.fuelConsumptionMin;
      target.fuelConsumptionMax = template.fuelConsumptionMax;
      target.damageThreshold = template.damageThreshold;
      AeroPart component = ((Component) target).GetComponent<AeroPart>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        target.criticalParts = new UnitPart[1]
        {
          (UnitPart) component
        };
      }
      else
      {
        target.criticalParts = (UnitPart[]) null;
        Debug.LogWarning((object) ("[TurbojetUtils] AeroPart not found on " + ((Object) ((Component) target).gameObject).name));
      }
      target.failureMessage = template.failureMessage;
      target.failureMessageAudio = template.failureMessageAudio;
    }
  }

  private static AudioSource CreateAudioSourceFromTemplate(AudioSource template, Transform parent)
  {
    if (Object.op_Equality((Object) template, (Object) null) || Object.op_Equality((Object) parent, (Object) null))
      return (AudioSource) null;
    AudioSource sourceFromTemplate = ((Component) parent).gameObject.AddComponent<AudioSource>();
    sourceFromTemplate.clip = template.clip;
    sourceFromTemplate.outputAudioMixerGroup = template.outputAudioMixerGroup;
    sourceFromTemplate.playOnAwake = template.playOnAwake;
    sourceFromTemplate.loop = template.loop;
    sourceFromTemplate.priority = template.priority;
    sourceFromTemplate.volume = template.volume;
    sourceFromTemplate.pitch = template.pitch;
    sourceFromTemplate.spatialBlend = template.spatialBlend;
    sourceFromTemplate.dopplerLevel = template.dopplerLevel;
    sourceFromTemplate.spread = template.spread;
    sourceFromTemplate.rolloffMode = template.rolloffMode;
    sourceFromTemplate.minDistance = template.minDistance;
    sourceFromTemplate.maxDistance = template.maxDistance;
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType) 0, template.GetCustomCurve((AudioSourceCurveType) 0));
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType) 1, template.GetCustomCurve((AudioSourceCurveType) 1));
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType) 2, template.GetCustomCurve((AudioSourceCurveType) 2));
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType) 3, template.GetCustomCurve((AudioSourceCurveType) 3));
    return sourceFromTemplate;
  }
}
