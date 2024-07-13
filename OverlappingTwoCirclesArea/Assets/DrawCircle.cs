using UnityEngine;

[System.Serializable]
public struct Circle
{
    public Vector2 location;
    public float radius;
}

public class DrawCircle : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [Range(3, 360)] public int repeat = 180;
    public Circle circle = new()
    {
        radius = 1f
    };

    private void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }
    }

    private void Update()
    {
        lineRenderer.positionCount = repeat;
        for (int i = 0; i < repeat; i++)
        {
            float radian = Mathf.PI * 2 * i / repeat;
            lineRenderer.SetPosition(i, new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * circle.radius + circle.location);
        }
    }
}