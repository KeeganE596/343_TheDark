using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKey("left")) {
			transform.Translate(Vector3.left * Time.deltaTime*5);
    	}
    	if(Input.GetKey("right")) {
			transform.Translate(Vector3.right * Time.deltaTime*5);
    	}
    	if(Input.GetKey("up")) {
			transform.Translate(Vector3.forward * Time.deltaTime*5);
    	}
    	if(Input.GetKey("down")) {
			transform.Translate(Vector3.back * Time.deltaTime*5);
    	}
    }
}
