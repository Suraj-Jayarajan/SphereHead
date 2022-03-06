using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlsAreaController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Image controlArea;
    private Image virtualJoystickImg;
    private VirtualJoystick virtualJoystick;

    // Use this for initialization
    void Start()
    {
        controlArea = GetComponent<Image>();
        virtualJoystickImg = transform.GetChild(0).GetComponent<Image>();
        virtualJoystick = virtualJoystickImg.GetComponent<VirtualJoystick>();

    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        virtualJoystick.transform.position = ped.position;
        
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        virtualJoystick.OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        virtualJoystick.OnPointerUp(ped);
    }
}
