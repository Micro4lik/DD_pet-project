using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    public AnimationCurve curveX;
    public AnimationCurve curveY;
    // Start is called before the first frame update
    void Start()
    {
        curveX = AnimationCurve.EaseInOut(0, 600, 1, 0);
        curveX.AddKey(0.5f, -100);
        curveY = AnimationCurve.EaseInOut(0, 600, 1, 0);
        StartCoroutine(RunYouFuckers());
    }

    IEnumerator RunYouFuckers()
    {
        float curTime = Time.time;
        while (Time.time - curTime < 1f)
        {
            transform.position = new Vector3(curveX.Evaluate(Time.time), curveY.Evaluate(Time.time), transform.position.z);
            yield return null;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
