using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRectTransform : MonoBehaviour {
    public float padding  = 10.0f;
    public float elementSize = 128.0f;
    public float viewSize = 150.0f;
    public float leftpadding = 5.0f
;
    private RectTransform rt;  
    private int amountElement;
    private float contentSize;


    private void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.localPosition  = new Vector3(100, rt.localPosition.y, rt.localPosition.z);

    }

    private void Update()
    {
        // clamp our rect transform
        amountElement = rt.childCount;
        contentSize = ((amountElement * (elementSize + padding)) - viewSize) * rt.localScale.x;

        if (rt.localPosition.x > padding + leftpadding)
            rt.localPosition = new Vector3(padding + leftpadding, rt.localPosition.y, rt.localPosition.z);
        else if (rt.localPosition.x < -contentSize) 
            rt.localPosition = new Vector3(-contentSize, rt.localPosition.y, rt.localPosition.z);
    }

	 
}
