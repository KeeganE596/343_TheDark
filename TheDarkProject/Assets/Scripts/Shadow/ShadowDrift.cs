using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDrift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     	  
    }

    public void Move() {
    	Debug.Log("moving");
    	transform.LookAt(GameObject.FindWithTag("Player").transform.position);
    	transform.Translate(Vector3.forward * Time.deltaTime*3); 
    }
}
