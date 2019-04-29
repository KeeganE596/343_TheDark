using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSwitch : MonoBehaviour
{
    bool nearPlayer;
	ShadowDrift shadowDriftScript;

    // Start is called before the first frame update
    void Start()
    {
        nearPlayer = false;
        shadowDriftScript = GetComponent<ShadowDrift>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(nearPlayer);
        if(nearPlayer) { 
        	shadowDriftScript.Move();
        }
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		nearPlayer = true;
    		transform.LookAt(GameObject.FindWithTag("Player").transform.position);
    	}
    }
}
