using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class playerArtifactState : MonoBehaviour
{

	public bool isHoldingArtifact;

	GameObject[] dreamObjects;
	public GameObject artifactInHand;
	public GameObject ppVolume;

	

    // Start is called before the first frame update
    void Start()
    {
        isHoldingArtifact = false;
        dreamObjects = GameObject.FindGameObjectsWithTag("DreamObject");
        artifactInHand.SetActive(false);
        PostProcessVolume volume = ppVolume.GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(isHoldingArtifact) {
    		for(int i=0; i<dreamObjects.Length; i++) {
    			dreamObjects[i].SetActive(true);
    		}
    		ppVolume.SetActive(true);
    	}
    	else if(dreamObjects[0].activeSelf) {
    		for(int i=0; i<dreamObjects.Length; i++) {
    			dreamObjects[i].SetActive(false);
    		}
    		ppVolume.SetActive(false);
    	}
    }

    public void changeIsHolding() {
    	isHoldingArtifact = !isHoldingArtifact;
    	
    	if(isHoldingArtifact) {
    		artifactInHand.SetActive(true);
    	}
    	else {
    		artifactInHand.SetActive(false);
    	}
    }
}
