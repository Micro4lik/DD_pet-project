using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private int[] randomX;
    private int[] randomZ;

    // Start is called before the first frame update
    void Start()
    {
        randomX = new int[3];
        randomZ = new int[5];

        randomX[0] = 0;
        randomX[1] = 180;
        randomX[2] = -180;

        int rX = Random.Range(0, 3);

        randomZ[0] = 0;
        randomZ[1] = 90;
        randomZ[2] = -90;
        randomZ[3] = 180;
        randomZ[4] = -180;

        int rZ = Random.Range(0, 5);

        transform.rotation = Quaternion.Euler(0, 0, randomZ[rZ]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
