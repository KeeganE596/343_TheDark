using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class podiumCheck : MonoBehaviour
{
	Collider artifactCol;
    playerArtifactState playerArtifactStateScript;

    // Start is called before the first frame update
    void Start() {
        playerArtifactStateScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<playerArtifactState>();
    }

    // Update is called once per frame
    void Update() {
        if(!artifactCol) {
            gameObject.GetComponent<Collider>().enabled = false;
            playerArtifactStateScript.nearPodium = false;
        }
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "ActiveArtifact") {
            artifactCol = other;
        }
        if(other.gameObject.tag == "Player") {
            playerArtifactStateScript.nearPodium = true;
        }
    }

    void OnTriggerStay(Collider other) {
    	if(other.gameObject.tag == "ActiveArtifact") {
            artifactCol = other;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            playerArtifactStateScript.nearPodium = false;
        }
    }
}
