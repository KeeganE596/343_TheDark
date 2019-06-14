using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupRaycast : MonoBehaviour
{
	Camera cam;
	Ray ray;
    RaycastHit hit;
    
    GameObject[] activeArtifacts;
    GameObject[] collectedArtifacts;
    GameObject[] inHandArtifacts;
    playerArtifactState pickUpArtifactScript;
    mainPedestal mainPedestalScript;

    public GameObject pedestalPointLight;
    int numCollectedArtifacts;
    bool gameWon;

    string currentHolding;

    GameObject shadow;
    ShadowRandomMove shadowRandomMoveScript;
    ShadowSwitch shadowSwitchScript;

    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();

        activeArtifacts = GameObject.FindGameObjectsWithTag("ActiveArtifact");
        collectedArtifacts = GameObject.FindGameObjectsWithTag("CollectedArtifact");
        inHandArtifacts = GameObject.FindGameObjectsWithTag("inHandArtifact");
        pickUpArtifactScript = GetComponent<playerArtifactState>();
        mainPedestalScript = GetComponent<mainPedestal>();

        pedestalPointLight.SetActive(false);
        currentHolding = null;

        foreach(GameObject a in collectedArtifacts) {
            a.SetActive(false);
        }
        foreach(GameObject a in inHandArtifacts) {
            a.SetActive(false);
        }
        numCollectedArtifacts = 0;
        gameWon = false;

        shadow = GameObject.FindWithTag("Shadow");
        shadowRandomMoveScript = shadow.GetComponent<ShadowRandomMove>();
        shadowSwitchScript = shadow.GetComponent<ShadowSwitch>();
    }

    // Update is called once per frame
    void Update() {
        ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));

        if(Physics.Raycast(ray, out hit, 3))
	    {
            if(Input.GetMouseButtonDown(0)) {
                if(hit.collider.tag == "Pedestal" && pickUpArtifactScript.isHoldingArtifact) {
                    pickUpArtifactScript.changeIsHolding();
                    pedestalPointLight.SetActive(true);
                    shadowSwitchScript.changeHoldingArtifact();
                    for(int i=0; i<collectedArtifacts.Length; i++) {
                        if(collectedArtifacts[i].name == currentHolding) {
                            collectedArtifacts[i].SetActive(true);
                            numCollectedArtifacts++;
                        }
                        if(inHandArtifacts[i].name == currentHolding) {
                            inHandArtifacts[i].SetActive(false);
                        }
                    }
                    currentHolding = null;
                    shadowRandomMoveScript.updateAreaRanges();
                }
                for(int i=0; i<activeArtifacts.Length; i++){
                    if(hit.collider.tag == "ActiveArtifact" && currentHolding == null){
                        pickUpArtifactScript.changeIsHolding();
                        Destroy(hit.collider.gameObject);
                        currentHolding = hit.collider.name;
                        shadowSwitchScript.changeHoldingArtifact();
                        foreach(GameObject a in inHandArtifacts) {
                            if(a.name == currentHolding) {
                                a.SetActive(true);
                            }
                        }
                    }   
                }
                           
            }
        }

        if(numCollectedArtifacts == 5) {
            gameWon = true;
        }
        if(gameWon) {
            Debug.Log("Yay");
        }
    }
}
