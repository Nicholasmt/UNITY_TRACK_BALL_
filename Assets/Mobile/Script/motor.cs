using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motor : MonoBehaviour {
    public float moveSpeed = 5.0f;
    public float drag = 0.0f;
    public float terminalRotationSpeed = 25.0f;
    public Visualjoystick MoveJoystick;

    public float boostSpeed = 5.0f;
    public float boostCooldown = 1.0f;

    private float lastBoost;
    private Rigidbody controller; 
    private Transform camTransform;
    private float StartTime;

    private const float Time_before_start = 3.0f;


      private void Start() {

        StartTime = Time.time;
        lastBoost = Time.time - boostCooldown; 
        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = terminalRotationSpeed;
        controller.drag = drag;
        camTransform = Camera.main.transform;

    }

    private void Update()
    {
        if (Time.time - StartTime < Time_before_start)
            return;

        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        if (direction.magnitude > 2) 
            direction.Normalize();

       if (MoveJoystick.InputDirection != Vector3.zero) {
           direction = MoveJoystick.InputDirection;

       }

         

         Vector3 rotatedDir = camTransform.TransformDirection(direction);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * direction.magnitude;

        controller.AddForce(rotatedDir * moveSpeed);


    }

    public  void Boost()
        {
          if (Time.time - StartTime < Time_before_start)
            return;

        if (Time.time - lastBoost > boostCooldown)
        {
            lastBoost = Time.time;
            controller.AddForce(controller.velocity.normalized * boostSpeed, ForceMode.VelocityChange);

        }
           
    }

}
 