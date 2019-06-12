using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHeightRaycast : MonoBehaviour
{
    int layerMask = 1 << 9;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray downRay = new Ray(transform.position, Vector3.down);
        RaycastHit downHit;
        

        if(Physics.Raycast(downRay, out downHit, 200, layerMask)) {
            Debug.Log("Hit");
            if(downHit.collider.tag == "Terrain") {
                Debug.Log("Hit terrain");
                distance = downHit.distance;
            }
        }
        Debug.Log("doing something " + distance);
        transform.position += Vector3.down * distance;
        distance = 0;

        Debug.DrawRay(downRay.origin, downRay.direction * 100, Color.green);

    }
}
