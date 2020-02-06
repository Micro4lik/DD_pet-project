using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enlarge()
    {
        gameObject.GetComponent<Transform>().localScale= new Vector3(1.2f,1.2f,1.2f);
    }
}
