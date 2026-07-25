using UnityEngine;

#nullable disable
namespace RealA10;

public static class JetNozzleUtils
{
  public static void InitializeFromTemplate(JetNozzle template, JetNozzle target)
  {
    if (template == null || target == null)
    {
      Debug.LogWarning($"[JetNozzleUtils] InitializeFromTemplate called with null argument(s). template={(template != null ? template.name : "NULL")}, target={(target != null ? target.name : "NULL")}");
      return;
    }

    Debug.Log($"[JetNozzleUtils] Initializing '{target.name}' from template '{template.name}'.");

    target.part = target.GetComponent<AeroPart>();
    if (target.part == null)
      Debug.LogWarning($"[JetNozzleUtils] AeroPart not found on {target.gameObject.name}.");
    else
      Debug.Log($"[JetNozzleUtils]   part = AeroPart on '{target.gameObject.name}'.");

    target.turbojet = null;
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
    target.afterburners = null;
    target.vectorTransforms = null;
    target.swivelAudio = null;
    target.thrustTransform = target.transform;

    Debug.Log($"[JetNozzleUtils]   thrustProportion={target.thrustProportion}, swivelSpeed={target.swivelSpeed}, " +
              $"pitchThrust={target.pitchThrust}, rollThrust={target.rollThrust}, yawSwivel={target.yawSwivel}, " +
              $"angleMin={target.angleMin}, angleMax={target.angleMax}, thrustMaxVolume={target.thrustMaxVolume}, " +
              $"IRMin={target.IRMin}, IRMax={target.IRMax}, volumeMultiplier={target.volumeMultiplier}");

    target.thrustAudio = CreateAudioSourceFromTemplate(template.thrustAudio, target.transform);

    Debug.Log($"[JetNozzleUtils] Finished initializing '{target.name}' from template '{template.name}'.");
  }

  private static AudioSource CreateAudioSourceFromTemplate(AudioSource template, Transform parent)
  {
    if (template == null || parent == null)
    {
      Debug.LogWarning($"[JetNozzleUtils] CreateAudioSourceFromTemplate called with null argument(s). template={(template != null ? template.name : "NULL")}, parent={(parent != null ? parent.name : "NULL")}");
      return null;
    }

    Debug.Log($"[JetNozzleUtils] Creating AudioSource on '{parent.name}' from template '{template.name}'.");

    AudioSource sourceFromTemplate = parent.gameObject.AddComponent<AudioSource>();
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
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType)0, template.GetCustomCurve((AudioSourceCurveType)0));
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType)1, template.GetCustomCurve((AudioSourceCurveType)1));
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType)2, template.GetCustomCurve((AudioSourceCurveType)2));
    sourceFromTemplate.SetCustomCurve((AudioSourceCurveType)3, template.GetCustomCurve((AudioSourceCurveType)3));

    Debug.Log($"[JetNozzleUtils] AudioSource created on '{parent.name}': clip='{(template.clip != null ? template.clip.name : "NULL")}', volume={sourceFromTemplate.volume}, loop={sourceFromTemplate.loop}.");

    return sourceFromTemplate;
  }
}
