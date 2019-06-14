using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowInnerTrigger : MonoBehaviour
{
    bool innerTrigger;

    // Start is called before the first frame update
    void Start() {
        innerTrigger = false;
    }

    // Update is called once per frame
    void Update() {    
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
            innerTrigger = true;
    	}
    }

    void OnTriggerExit(Collider other) {
    	if(other.gameObject.tag == "Player") {
            innerTrigger = false;
    	}
    }

    public bool getInnerTrigger() {
        return innerTrigger;
    }
}
