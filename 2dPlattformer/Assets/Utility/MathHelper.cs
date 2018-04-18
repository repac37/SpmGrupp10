using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    public const float FloatEpsilon = 0.001f;

    public static int Sign(float value)
    {
        return value > 0.0f ? 1 : Mathf.Abs(value) < FloatEpsilon ? 0 : -1;
    }

    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        if (vector.magnitude < FloatEpsilon) return Vector2.zero;
        float radian = Mathf.Atan2(vector.y, vector.x) + (angle * Mathf.Deg2Rad);
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * vector.magnitude;
    }

    public static Vector2 GetNormalForce(Vector2 velocity, Vector2 normal)
    {
        Vector2 velocityInDirection = GetVectorInDirection(velocity, normal);
        float dot = Vector2.Dot(velocityInDirection.normalized, normal.normalized);
        return dot > 0.0f ? Vector2.zero : -velocityInDirection;
    }

    public static Vector2 GetVectorInDirection(Vector2 originalVector, Vector2 direction)
    {
        return direction *
            Vector2.Dot(direction.normalized, originalVector.normalized) *
            originalVector.magnitude;
    }

    public static bool CheckAllowedSlope(MinMaxFloat allowedAngles, Vector2 surfaceNormal)
    {
        float angle = Mathf.Atan2(surfaceNormal.y, surfaceNormal.x) * Mathf.Rad2Deg - 90f;
        return angle >= allowedAngles.Min && angle <= allowedAngles.Max;
    }

    public static Vector2 PointOnRectangle(Vector2 dirr, Vector2 size)
    {
        Vector2 halfSize = size * 0.5f;
        float radian = Mathf.Atan2(dirr.y, dirr.x);
        float distanceX = Mathf.Abs(halfSize.x / Mathf.Cos(radian));
        float distanceY = Mathf.Abs(halfSize.y / Mathf.Sin(radian));

        return dirr.normalized * Mathf.Min(distanceY, distanceX);
    }

    public static float GetWallAngleDelta(Vector2 normal)
    {
        float angle = Vector2.Angle(normal, Vector2.right);
        float delta = Mathf.Abs(angle < 90.0f ? angle : angle - 180);
        return delta;
    }

}