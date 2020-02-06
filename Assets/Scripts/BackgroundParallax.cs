using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class BackgroundParallax : MonoBehaviour
{
    public Vector3 pz;
    public Vector3 StartPos;
    public Vector3 StartSize;

    public float moveModifier;

    // Use this for initialization
    void Start()
    {
        StartPos = transform.position;
        StartSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        pz.z = 0;
        gameObject.transform.position = pz;
        //Debug.Log("Mouse Position: " + pz);

        transform.position = new Vector3(StartPos.x + (pz.x * moveModifier), StartPos.y + (pz.y * moveModifier), StartPos.z);
        if (gameObject.name == "background(3)")
        {
            //transform.localScale = new Vector3(StartSize.x + (pz.x * moveModifier), StartSize.y + (pz.y * moveModifier), 0);
        }

        
    }
}
