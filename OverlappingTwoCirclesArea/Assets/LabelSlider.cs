using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasRenderer))]
public class LabelSlider : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public UnityEvent<Vector2> onChangePosition;
    public UnityEvent<float> onChangePositionX;
    public UnityEvent<float> onChangePositionY;

    private Vector2 lastMousePosition;

    private const float Speed = 0.1f;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 change = (eventData.position - lastMousePosition) * Speed;
        onChangePosition.Invoke(change);
        onChangePositionX.Invoke(change.x);
        onChangePositionY.Invoke(change.y);

        lastMousePosition = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }
}
