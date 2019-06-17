﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternRaycast : MonoBehaviour
{

	Camera cam;
    int layerMask = 1 << 11;

    public Light playerLight;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 400, Color.green);

        if(Physics.Raycast(ray, out hit, 400, layerMask)) {
        	if(hit.collider.tag == "PodiumBase") {
                if(playerLight.intensity < 2.8f) {
                    playerLight.intensity += 0.1f;
                }
                if(playerLight.range < 8.5f) {
                    playerLight.range += 0.1f;
                }
        	}
	    }
	    else {
            if(playerLight.intensity > 1.8f) {
                playerLight.intensity -= 0.1f;
            }
            if(playerLight.range > 8f) {
                playerLight.range -= 0.1f;
            }
	    }
    }
}
