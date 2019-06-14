using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class podiumCheck : MonoBehaviour
{
	Collider artifactCol;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if(!artifactCol) {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "ActiveArtifact") {
            artifactCol = other;
        }
    }

    void OnTriggerStay(Collider other) {
    	if(other.gameObject.tag == "ActiveArtifact") {
            artifactCol = other;
        }
    }
}
