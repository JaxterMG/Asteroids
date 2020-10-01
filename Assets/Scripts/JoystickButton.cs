using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool _isPressing;
    public void OnPointerDown(PointerEventData eventData)
    {

        _isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        _isPressing = false;
    }


}
