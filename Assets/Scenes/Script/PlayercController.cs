using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayercController : MonoBehaviour {
    public float speed;
    public Text countText;
    public Text WinText;

    private Rigidbody rb;
    private int count;

    void Start () {

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        WinText.text = "";

    }

    void FixedUpdate()  {

       float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movemnt = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce (movemnt * speed);
         
    }
      void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("player")) {

            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText ();

        }
            
    }

    void SetCountText () {


        countText.text = "count: " + count.ToString();
        if  (count >= 10) {

            WinText.text = "You Win";

        }
    }

}

 