using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraMotorScript : MonoBehaviour {

    public Transform LookAt;
    public RectTransform virtualJoystickspace;


    private Vector3 desiredPosition;
    private Vector3 offset;

    private Vector2 touchposition;
    private float swipeReistance = 200.0f;


    private float distance = 6.0f;
    private float yOffset = 3.5f;
    private float smoothSpeed = 7.5f;
    private bool IsinsideVirtualJoystickSpace = false;

    private float StartTime = 0f;
    private float Time_before_start = 2.5f;

    private void Start() {
        offset = new Vector3 (0, yOffset, -2f * distance);
        StartTime = Time.time;
    }

	private void Update() {

        if (Time.time - StartTime < Time_before_start)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SlideCamera(true);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SlideCamera(false);

       if (Input.GetMouseButtonDown(0) 
            || Input.GetMouseButtonDown(1)) {
            if (RectTransformUtility.RectangleContainsScreenPoint(virtualJoystickspace, Input.mousePosition))
               IsinsideVirtualJoystickSpace = true;
           else
            touchposition = Input.mousePosition;

        }

        if (Input.GetMouseButtonUp(0)
             || Input.GetMouseButtonUp(1))
        {
            if (IsinsideVirtualJoystickSpace)
            {
                IsinsideVirtualJoystickSpace = false;
                return;
            }
            float swipeForce = touchposition.x - Input.mousePosition.x;
            
            if (Mathf.Abs (swipeForce) > swipeReistance)
            {
                if (swipeForce < 0)
                    SlideCamera(true);
                else
                    SlideCamera(false);
            }


        }

    }

    private void FixedUpdate()
    {
        if (Time.time - StartTime < Time_before_start)
            return;

        desiredPosition = LookAt.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(LookAt.position + Vector3.up );
    }

    public void SlideCamera(bool left) {
        if (left)
            offset = Quaternion.Euler(0, 90, 0) * offset;
        else offset = Quaternion.Euler(0, 90, 0) * offset;
    }
}
