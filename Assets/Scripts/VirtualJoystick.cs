using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Vector3 InputDirection { set; get; }

    private Image bgImg;
    private Image joystickImg;
   

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);


            float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 : pos.x * 2;
            float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 : pos.y * 2;

            //float x = pos.x;
            //float y = pos.y;

            InputDirection = new Vector3(x, 0, y);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            //move Joystick IMG
            joystickImg.rectTransform.anchoredPosition =
                new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3)
                    , InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        // Reset Joystick Img position
        InputDirection = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;

    }


}
