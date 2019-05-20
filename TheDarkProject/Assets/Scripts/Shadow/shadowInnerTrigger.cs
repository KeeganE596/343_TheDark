using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowInnerTrigger : MonoBehaviour
{
	ShadowSwitch shadowSwitchScript;
	public GameObject shadowMain;

    // Start is called before the first frame update
    void Start()
    {
        shadowSwitchScript = shadowMain.GetComponent<ShadowSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		shadowSwitchScript.innerTrigger = true;
    	}
    }

    void OnTriggerExit(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		shadowSwitchScript.innerTrigger = false;;
    	}
    }
}
