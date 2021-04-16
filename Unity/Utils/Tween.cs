using System;
using UnityEngine;

namespace Utils
{
    public enum EaseFunctions
    {
        EASE_IN_SINE,
        EASE_OUT_SINE,
        EASE_IN_OUT_SINE,
        EASE_IN_QUAD,
        EASE_OUT_QUAD,
        EASE_IN_OUT_QUAD,
        EASE_IN_CUBIC,
        EASE_OUT_CUBIC,
        EASE_IN_OUT_CUBIC,
        EASE_IN_QUART,
        EASE_OUT_QUART,
        EASE_IN_OUT_QUART,
        EASE_IN_QUINT,
        EASE_OUT_QUINT,
        EASE_IN_OUT_QUINT,
        EASE_IN_EXPO,
        EASE_OUT_EXPO,
        EASE_IN_OUT_EXPO,
        EASE_IN_CIRC,
        EASE_OUT_CIRC,
        EASE_IN_OUT_CIRC,
        EASE_IN_BACK,
        EASE_OUT_BACK,
        EASE_IN_OUT_BACK,
        EASE_IN_ELASTIC,
        EASE_OUT_ELASTIC,
        EASE_IN_OUT_ELASTIC,
        EASE_IN_BOUNCE,
        EASE_OUT_BOUNCE,
        EASE_IN_OUT_BOUNCE,
    }

    public static class Tween
    {
        public static Vector3 Ease(Vector3 a, Vector3 b, EaseFunctions easeFunction, float t)
        {
            var tweened = Interpolate(easeFunction, Mathf.Clamp01(t));
            return Vector3.LerpUnclamped(a, b, tweened);
        }

        public static Quaternion Ease(Quaternion a, Quaternion b, EaseFunctions easeFunction, float t)
        {
            var tweened = Interpolate(easeFunction, Mathf.Clamp01(t));
            return Quaternion.LerpUnclamped(a, b, tweened);
        }

        public static float Ease(float a, float b, EaseFunctions easeFunction, float t)
        {
            var tweened = Interpolate(easeFunction, Mathf.Clamp01(t));
            return Mathf.LerpUnclamped(a, b, tweened);
        }
        
        private static float Interpolate(EaseFunctions easeFunction, float t)
        {
            switch (easeFunction)
            {
                case EaseFunctions.EASE_IN_SINE:
                    return 1 - Mathf.Cos((t * Mathf.PI) / 2);
                case EaseFunctions.EASE_OUT_SINE:
                    return Mathf.Sin((t * Mathf.PI) / 2);
                case EaseFunctions.EASE_IN_OUT_SINE:
                    return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
                case EaseFunctions.EASE_IN_QUAD:
                    return t * t;
                case EaseFunctions.EASE_OUT_QUAD:
                    return 1 - (1 - t) * (1 - t);
                case EaseFunctions.EASE_IN_OUT_QUAD:
                    return t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
                case EaseFunctions.EASE_IN_CUBIC:
                    return t * t * t;
                case EaseFunctions.EASE_OUT_CUBIC:
                    return 1 - Mathf.Pow(1 - t, 3);
                case EaseFunctions.EASE_IN_OUT_CUBIC:
                    return t < 0.5 ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
                case EaseFunctions.EASE_IN_QUART:
                    return t * t * t * t;
                case EaseFunctions.EASE_OUT_QUART:
                    return 1 - Mathf.Pow(1 - t, 4);
                case EaseFunctions.EASE_IN_OUT_QUART:
                    return t < 0.5 ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
                case EaseFunctions.EASE_IN_QUINT:
                    return t * t * t * t * t;
                case EaseFunctions.EASE_OUT_QUINT:
                    return 1 - Mathf.Pow(1 - t, 5);
                case EaseFunctions.EASE_IN_OUT_QUINT:
                    return t < 0.5 ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
                case EaseFunctions.EASE_IN_EXPO:
                    return t <= 0.001f ? 0 : Mathf.Pow(2, 10 * t - 10);
                case EaseFunctions.EASE_OUT_EXPO:
                    return t >= 0.999f ? 1f : 1f - Mathf.Pow(2, -10 * t);
                case EaseFunctions.EASE_IN_OUT_EXPO:
                    if (t <= 0.001f) return 0f;
                    if (t >= 0.999f) return 1f;
                    if (t < 0.5f) return Mathf.Pow(2f, 20f * t - 10f) / 2f;
                    return (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;
                case EaseFunctions.EASE_IN_CIRC:
                    return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
                case EaseFunctions.EASE_OUT_CIRC:
                    return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
                case EaseFunctions.EASE_IN_OUT_CIRC:
                    return t < 0.5f
                        ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * t, 2f))) / 2f
                        : (Mathf.Sqrt(1f - Mathf.Pow(-2f * t + 2f, 2f)) + 1f) / 2f;
                case EaseFunctions.EASE_IN_BACK:
                    var a1 = 1.70158f;
                    var a3 = a1 + 1f;
                    return a3 * t * t * t - a1 * t * t;
                case EaseFunctions.EASE_OUT_BACK:
                    var b1 = 1.70158f;
                    var b3 = b1 + 1f;
                    return 1f + b3 * Mathf.Pow(t - 1f, 3f) + b1 * Mathf.Pow(t - 1f, 2f);
                case EaseFunctions.EASE_IN_OUT_BACK:
                    var c1 = 1.70158f;
                    var c2 = c1 * 1.525f;
                    return t < 0.5
                        ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2
                        : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
                case EaseFunctions.EASE_IN_ELASTIC:
                    var d4 = (float)(2f * Math.PI) / 3f;
                    return t <= 0.001f
                        ? 0f
                        : t >= 0.999f
                            ? 1f
                            : -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * d4);
                case EaseFunctions.EASE_OUT_ELASTIC:
                    var e4 = (float)(2f * Math.PI) / 3f;
                    return t <= 0.001f
                        ? 0f
                        : t >= 0.999f
                            ? 1f
                            : Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * e4) + 1f;
                case EaseFunctions.EASE_IN_OUT_ELASTIC:
                    var f5 = (float)(2f * Math.PI) / 4.5f;
                    return t <= 0.001f
                        ? 0f
                        : t >= 0.999f
                            ? 1f
                            : t < 0.5f
                                ? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * f5)) / 2f
                                : (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * f5)) / 2f + 1f;
                case EaseFunctions.EASE_IN_BOUNCE:
                    return 1f - Interpolate(EaseFunctions.EASE_OUT_BOUNCE, 1f - t);
                case EaseFunctions.EASE_OUT_BOUNCE:
                    var n1 = 7.5625f;
                    var d1 = 2.75f;
                    if (t < 1f / d1) {
                        return n1 * t * t;
                    } else if (t < 2f / d1) {
                        return n1 * (t -= 1.5f / d1) * t + 0.75f;
                    } else if (t < 2.5f / d1) {
                        return n1 * (t -= 2.25f / d1) * t + 0.9375f;
                    } else {
                        return n1 * (t -= 2.625f / d1) * t + 0.984375f;
                    }
                case EaseFunctions.EASE_IN_OUT_BOUNCE:
                    return t < 0.5f
                        ? (1 - Interpolate(EaseFunctions.EASE_OUT_BOUNCE, 1f - 2f * t)) / 2f
                        : (1 + Interpolate(EaseFunctions.EASE_OUT_BOUNCE, 2f * t - 1f)) / 2f;
                default:
                    throw new ArgumentOutOfRangeException(nameof(easeFunction), easeFunction, null);
            }
        }

    }
}