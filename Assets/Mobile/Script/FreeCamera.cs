using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour {

    public Visualjoystick Camerajopystick;
    public Transform LookAt;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 3.0f;
    private float sensivityY = 1.0f;

        
    
    private void Update()
    {
        currentX += Camerajopystick.InputDirection.x * sensivityX;
        currentY += Camerajopystick.InputDirection.z * sensivityY;
    }

     private void LateUpdate()
    {
        Vector3 dir = new Vector3 (0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);
        transform.position = LookAt.position + rotation * dir;
        transform.LookAt(LookAt);
            
 
    }

 
}
