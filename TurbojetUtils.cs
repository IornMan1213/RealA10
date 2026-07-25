using UnityEngine;

#nullable disable
namespace RealA10;

public static class TurbojetUtils
{
  public static void InitializeFromTemplate(Turbojet template, Turbojet target)
  {
    if (template == null || target == null)
    {
      Debug.LogWarning($"[TurbojetUtils] InitializeFromTemplate called with null argument(s). template={(template != null ? template.name : "NULL")}, target={(target != null ? target.name : "NULL")}");
      return;
    }

    Debug.Log($"[TurbojetUtils] Initializing '{target.name}' from template '{template.name}'.");

    target.maxThrust = 600000f;
    Debug.Log($"[TurbojetUtils]   maxThrust = {target.maxThrust} (hardcoded override, template had {template.maxThrust})");

    target.damageFactor = template.damageFactor;
    target.turbineAudio = CreateAudioSourceFromTemplate(template.turbineAudio, target.transform);
    target.thrustVectoring = template.thrustVectoring;
    target.thrustVectoringGain = template.thrustVectoringGain;
    target.throttleRemap = template.throttleRemap;
    target.thrustVectoringMaxAirspeed = template.thrustVectoringMaxAirspeed;
    target.minDensity = template.minDensity;
    target.splitThrustFactor = template.splitThrustFactor;
    target.altitudeThrust = template.altitudeThrust;
    target.nozzles = null;
    target.vectoringTransforms = null;
    target.turbineMaxPitch = template.turbineMaxPitch;
    target.minRPM = template.minRPM;
    target.maxRPM = template.maxRPM;
    target.maxSpeed = template.maxSpeed;
    target.spoolRate = template.spoolRate;
    target.startupRate = template.startupRate;
    target.fuelConsumptionMin = template.fuelConsumptionMin;
    target.fuelConsumptionMax = template.fuelConsumptionMax;
    target.damageThreshold = template.damageThreshold;

    Debug.Log($"[TurbojetUtils]   damageFactor={target.damageFactor}, thrustVectoring={target.thrustVectoring}, " +
              $"minRPM={target.minRPM}, maxRPM={target.maxRPM}, maxSpeed={target.maxSpeed}, " +
              $"spoolRate={target.spoolRate}, startupRate={target.startupRate}, " +
              $"fuelConsumptionMin={target.fuelConsumptionMin}, fuelConsumptionMax={target.fuelConsumptionMax}, " +
              $"damageThreshold={target.damageThreshold}");

    AeroPart component = target.GetComponent<AeroPart>();
    if (component != null)
    {
      target.criticalParts = new UnitPart[1] { component };
      Debug.Log($"[TurbojetUtils]   criticalParts set to AeroPart on '{target.name}'.");
    }
    else
    {
      target.criticalParts = null;
      Debug.LogWarning($"[TurbojetUtils] AeroPart not found on {target.gameObject.name}; criticalParts left null.");
    }

    target.failureMessage = template.failureMessage;
    target.failureMessageAudio = template.failureMessageAudio;

    Debug.Log($"[TurbojetUtils] Finished initializing '{target.name}' from template '{template.name}'.");
  }

  private static AudioSource CreateAudioSourceFromTemplate(AudioSource template, Transform parent)
  {
    if (template == null || parent == null)
    {
      Debug.LogWarning($"[TurbojetUtils] CreateAudioSourceFromTemplate called with null argument(s). template={(template != null ? template.name : "NULL")}, parent={(parent != null ? parent.name : "NULL")}");
      return null;
    }

    Debug.Log($"[TurbojetUtils] Creating AudioSource on '{parent.name}' from template '{template.name}'.");

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

    Debug.Log($"[TurbojetUtils] AudioSource created on '{parent.name}': clip='{(template.clip != null ? template.clip.name : "NULL")}', volume={sourceFromTemplate.volume}, loop={sourceFromTemplate.loop}.");

    return sourceFromTemplate;
  }
}
