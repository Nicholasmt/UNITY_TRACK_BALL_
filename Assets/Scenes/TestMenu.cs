using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenu : MonoBehaviour {
   private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

   private void Start()
    {
        //grap the canvasgruop in the Scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        //start with a white Screen
        fadeGroup.alpha = 1;
        }


     private void Update() {

        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

    }
}
