using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHeightRaycast : MonoBehaviour
{
    int layerMask = 1 << 9;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray downRay = new Ray(transform.position, Vector3.down);
        Ray upRay = new Ray(transform.position, Vector3.up);
        RaycastHit downHit;
        RaycastHit upHit;
        float distance = 0;

        /*if(Physics.Raycast(upRay, out upHit, 200, layerMask)) {
            Debug.Log("up: " + upHit.collider.name);
            Debug.Log(upHit.distance);
            if(upHit.collider.name == "Terrain") {
                Debug.Log("go up");
                transform.position += Vector3.up * upHit.distance;
                //Vector3 newY = new Vector3(0, 0, 0);
                //transform.position = newY;
            }
        }*/
        if(Physics.Raycast(downRay, out downHit, 200, layerMask)) {
            //Debug.Log("down: " + downHit.collider.name);
            //Debug.Log(downHit.distance);
        	if(downHit.collider.name == "Terrain") {
        		//Debug.Log("go down");
                distance = downHit.distance;
                //Vector3 newY = new Vector3(0, 0, 0);
                //transform.position = newY;
        	}
	    }
        //transform.position += Vector3.up * distance;
	    /*else {
	    	Debug.Log("something inbetween");
            gameObject.transform.position += Vector3.up * 0.5f;
	    }*/
        //Debug.Log(transform.position.y);

        Debug.DrawRay(downRay.origin, downRay.direction * 100, Color.green);
        //Debug.DrawRay(upRay.origin, upRay.direction * 100, Color.yellow);
    }
}
