using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwall : MonoBehaviour {
    public float ForceReguired = 15.0f;
    public GameObject burstprefab;


     


    private void OnCollisionEnter(Collision col)
    {
        if (col.impulse.magnitude > ForceReguired)
        {
            Destroy(gameObject);
            Instantiate(burstprefab, col.contacts[0].point, Quaternion.identity); 
        }  
    }
	 
}
