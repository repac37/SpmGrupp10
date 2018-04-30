using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static float _intensity;

    [Header("Perlin Noise")]
    public AnimationCurve IntensityToMagnitudeCurve;
    public float MaxDuration;
    public Vector2 PerlinSpeed;
    public Vector2 MaxShake;
    public float RollSpeed;
    public float MaxRoll;

    public static void AddIntensity(float intensity)
    {
        _intensity += intensity;
    }

    private void Update()
    {
        AddIntensity(3);
        _intensity -= Time.deltaTime / MaxDuration;
        _intensity = Mathf.Clamp01(_intensity);
        float magnitude = IntensityToMagnitudeCurve.Evaluate(_intensity);
        float xPerlin = Mathf.Lerp(-MaxShake.x, MaxShake.x,
        Mathf.PerlinNoise(Time.time * PerlinSpeed.x, 0f));
        float yPerlin = Mathf.Lerp(-MaxShake.y, MaxShake.y,
        Mathf.PerlinNoise(0.0f, Time.time * PerlinSpeed.y));
        float roll = Mathf.Lerp(-MaxRoll, MaxRoll,
        Mathf.PerlinNoise(Time.time * RollSpeed, Time.time * RollSpeed));
        transform.localPosition = new Vector3(xPerlin, yPerlin, 0.0f) * magnitude;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, roll * magnitude);
    }


}
