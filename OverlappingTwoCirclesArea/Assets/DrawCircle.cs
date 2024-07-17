using System.Linq;
using TMPro;
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
        location = Vector2.zero,
        radius = 1f
    };

    [Header("UI")]
    public TMP_InputField xField;
    public TMP_InputField yField;
    public TMP_InputField radiusField;

    private void Awake()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        TMP_InputField[] fields = GetComponentsInChildren<TMP_InputField>();
        if (!xField)
        {
            xField = fields.Single(f => f.transform.parent.name == "X");
        }

        if (!yField)
        {
            yField = fields.Single(f => f.transform.parent.name == "Y");
        }

        if (!radiusField)
        {
            radiusField = fields.Single(f => f.transform.parent.name == "Radius");
        }
    }

    private void Start()
    {
        xField.text = circle.location.x.ToString();
        xField.onEndEdit.AddListener(str =>
        {
            if (float.TryParse(str, out float x))
            {
                circle.location.x = x;
            }
            else
            {
                xField.text = circle.location.x.ToString();
            }
        });

        yField.text = circle.location.y.ToString();
        yField.onEndEdit.AddListener(str =>
        {
            if (float.TryParse(str, out float y))
            {
                circle.location.y = y;
            }
            else
            {
                yField.text = circle.location.y.ToString();
            }
        });

        radiusField.text = circle.radius.ToString();
        radiusField.onEndEdit.AddListener(str =>
        {
            if (float.TryParse(str, out float radius))
            {
                circle.radius = radius;
            }
            else
            {
                radiusField.text = circle.radius.ToString();
            }
        });
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

    public void AddLocateX(float additional)
    {
        circle.location.x += additional;
        xField.text = circle.location.x.ToString();
    }

    public void AddLocateY(float additional)
    {
        circle.location.y += additional;
        yField.text = circle.location.y.ToString();
    }

    public void AddRadius(float additional)
    {
        circle.radius += additional;
        radiusField.text = circle.radius.ToString();
    }
}