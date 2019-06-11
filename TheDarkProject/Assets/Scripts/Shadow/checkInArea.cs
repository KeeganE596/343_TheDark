using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkInArea : MonoBehaviour
{

	bool hasPlayer;
    bool hasArtifact;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayer = false;
        hasArtifact = false;
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		hasPlayer = true;
    	}
        if(other.gameObject.tag == "ActiveArtifact") {
            hasArtifact = true;
        }
    }

    void OnTriggerExit(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		hasPlayer = false;
    	}
        if(other.gameObject.tag == "ActiveArtifact") {
            hasArtifact = false;
        }
    }

    public bool getHasPlayer() {
    	return hasPlayer;
    }

    public bool getHasArtifact() {
        return hasArtifact;
    }
}
