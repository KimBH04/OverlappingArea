using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class Calculator : MonoBehaviour
{
    public TMP_Text display;
    public DrawCircle[] circles;

    [Header("Draw")]
    public LineRenderer lineRenderer;
    [Range(0, 360)] public int repeat = 36;

    private void Update()
    {
        display.text = GetOverlappingAreaSize(circles[0].circle, circles[1].circle, out float radian1, out float radian2).ToString();

        Vector3[] result = GetDrawCorners(circles[0].circle, circles[1].circle, radian1, radian2, repeat);

        lineRenderer.positionCount = result.Length;
        lineRenderer.SetPositions(result.ToArray());
    }

    private static Vector3[] GetDrawCorners(Circle lhs, Circle rhs, float lhsRadian, float rhsRadian, int repeat)
    {
        List<Vector3> result = new();

        if (lhsRadian != 0f)
        {
            float direction = Mathf.Atan2(rhs.location.y - lhs.location.y, rhs.location.x - lhs.location.x) - lhsRadian / 2f;
            for (int i = 0; i < repeat; i++)
            {
                float radian = direction + lhsRadian * i / repeat;
                result.Add(lhs.location + new Vector2(Mathf.Cos(radian) * lhs.radius, Mathf.Sin(radian) * lhs.radius));
            }
        }

        if (rhsRadian != 0f)
        {
            float direction = Mathf.Atan2(lhs.location.y - rhs.location.y, lhs.location.x - rhs.location.x) - rhsRadian / 2f;
            for (int i = 0; i < repeat; i++)
            {
                float radian = direction + rhsRadian * i / repeat;
                result.Add(rhs.location + new Vector2(Mathf.Cos(radian) * rhs.radius, Mathf.Sin(radian) * rhs.radius));
            }
        }

        return result.ToArray();
    }

    public static float GetOverlappingAreaSize(Circle lhs, Circle rhs, out float lhsRadian, out float rhsRadian)
    {
        lhsRadian = rhsRadian = 0f;

        float sqrDistance = (lhs.location - rhs.location).sqrMagnitude;
        float distance = Mathf.Sqrt(sqrDistance);

        // 두 원이 겹치치 않음
        if (distance >= lhs.radius + rhs.radius)
        {
            return 0f;
        }

        // 큰 원 안에 작은 원이 포함됨
        if (lhs.radius >= distance + rhs.radius)
        {
            rhsRadian = Mathf.PI * 2f;
            return rhs.radius * rhs.radius * Mathf.PI;
        }
        if (rhs.radius >= distance + lhs.radius)
        {
            lhsRadian = Mathf.PI * 2f;
            return lhs.radius * lhs.radius * Mathf.PI;
        }

        float a = distance;
        float b = rhs.radius;
        float c = lhs.radius;

        float squareA = sqrDistance;
        float squareB = b * b;
        float squareC = c * c;

        float halfCetaC = Mathf.Acos((squareA + squareB - squareC) / (2f * a * b));   // ceta / 2
        float halfCetaB = Mathf.Acos((squareA + squareC - squareB) / (2f * a * c));   // ceta / 2

        lhsRadian = halfCetaB * 2f;
        rhsRadian = halfCetaC * 2f;

        float s = (a + b + c) / 2f;   // 헤론의 공식

        // 두 부채꼴의 넓이의 합 - 사각형의 넓이
        return squareB * halfCetaC + squareC * halfCetaB - Mathf.Sqrt(s * (s - a) * (s - b) * (s - c)) * 2f;

        // 라디안 기준 부채꼴의 넓이 공식 : r * r * ceta / 2
        // r    : 반지름
        // ceta : 라디안 각
    }
}