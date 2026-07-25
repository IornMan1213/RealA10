// Decompiled with JetBrains decompiler
// Type: RealA10.JetNozzleUtils
// Assembly: RealA10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 532C87AC-4BC4-4B3D-ACF1-A050F52D51FF
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Nuclear Option\BepInEx\plugins\RealA10.dll

using UnityEngine;

#nullable disable
namespace RealA10;

public static class JetNozzleUtils
{
  public static void InitializeFromTemplate(JetNozzle template, JetNozzle target)
  {
    if ((template == null) || (target == null))
    {
      Debug.LogWarning((object) "[JetNozzleUtils] Template or target is null.");
    }
    else
    {
      target.part = (UnitPart) ((Component) target).GetComponent<AeroPart>();
      if ((target.part == null))
        Debug.LogWarning((object) ("[JetNozzleUtils] AeroPart not found on " + ((Object) ((Component) target).gameObject).name));
      target.turbojet = (Turbojet) null;
      target.failureEffect = template.failureEffect;
      target.heatHaze = template.heatHaze;
      target.glow = template.glow;
      target.thrustProportion = template.thrustProportion;
      target.swivelSpeed = template.swivelSpeed;
      target.pitchThrust = template.pitchThrust;
      target.rollThrust = template.rollThrust;
      target.yawSwivel = template.yawSwivel;
      target.angleMax = template.angleMax;
      target.angleMin = template.angleMin;
      target.thrustMaxVolume = template.thrustMaxVolume;
      target.IRMin = template.IRMin;
      target.IRMax = template.IRMax;
      target.volumeMultiplier = template.volumeMultiplier;
      target.swivelPitchBase = template.swivelPitchBase;
      target.swivelPitchFactor = template.swivelPitchFactor;
      target.afterburners = (JetNozzle.Afterburner[]) null;
      target.vectorTransforms = (Transform[]) null;
      target.swivelAudio = (AudioSource) null;
      target.thrustTransform = ((Component) target).transform;
      target.thrustAudio = JetNozzleUtils.CreateAudioSourceFromTemplate(template.thrustAudio, ((Component) target).transform);
    }
  }

  private static AudioSource CreateAudioSourceFromTemplate(AudioSource template, Transform parent)
  {
    if ((template == null) || (parent == null))
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

