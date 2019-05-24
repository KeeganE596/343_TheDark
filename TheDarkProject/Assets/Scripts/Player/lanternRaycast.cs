using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternRaycast : MonoBehaviour
{
	Camera cam;
	Ray ray;
    RaycastHit hit;

    public Light playerLight;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

        if(Physics.Raycast(ray, out hit)) {
        	if(hit.collider.name == "test") {
        		playerLight.intensity = 3f;
        		playerLight.range = 8;
        	}
	    }
	    else {
	    	playerLight.intensity = 1.4f;
	    	playerLight.range = 7;
	    }
    }
}
