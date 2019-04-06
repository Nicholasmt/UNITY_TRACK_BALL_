using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Visualjoystick : MonoBehaviour, IDragHandler ,IPointerUpHandler, IPointerDownHandler {
    private Image bgimg;
    private Image Joystickimg;
    public  Vector3 InputDirection { set; get; }

    private void Start()
    {
        bgimg = GetComponent <Image> ();
        Joystickimg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped){
        Vector2 pos = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
           ( bgimg.rectTransform, 
           ped.position,
           ped.pressEventCamera,
           out pos ))
        {
            pos.x = (pos.x / bgimg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgimg.rectTransform.sizeDelta.y);
            float x = (bgimg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (bgimg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputDirection = new Vector3(x, 0, y);
            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;
            Joystickimg.rectTransform.anchoredPosition =
                new Vector3 (InputDirection.x * (bgimg.rectTransform.sizeDelta.x / 3)
                ,InputDirection.z * (bgimg.rectTransform.sizeDelta.y / 3));

          
        }

    }
    public virtual void  OnPointerDown(PointerEventData ped)
    {
        OnDrag (ped);
   
    } 

    public virtual void OnPointerUp(PointerEventData ped)
    {

        InputDirection = Vector3.zero;
        Joystickimg.rectTransform.anchoredPosition = Vector3.zero; 
    }
}
