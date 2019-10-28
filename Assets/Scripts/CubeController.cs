using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    float x=1.0f;
    float y=1.0f;
    float z=1.0f;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z")) {
            StartCoroutine(RotateCube());
        }
        transform.Rotate(x, y, z);
    }
    IEnumerator RotateCube() {
        int x = 10;
     for(int i = 0; i < 3; i++) {
            y += 1.0f;
            yield return new WaitForSeconds(1.0f);
        }
        x--;
    }
}
