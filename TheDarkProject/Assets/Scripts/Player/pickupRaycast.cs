using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupRaycast : MonoBehaviour {
	Camera cam;
	Ray ray;
    RaycastHit hit;
    
    GameObject[] activeArtifacts;
    GameObject[] collectedArtifacts;
    GameObject[] inHandArtifacts;
    playerArtifactState pickUpArtifactScript;
    int numCollectedArtifacts;
    string currentHolding;
    Light podiumLight;

    GameObject shadow;
    ShadowRandomMove shadowRandomMoveScript;
    ShadowSwitch shadowSwitchScript;
    ShadowDrift shadowDriftScript;

    //Gamewon variables
    bool gameWon;
    GameObject[] SceneItemsToHide;
    GameObject Ground;
    float timer = 0.0f;
    

    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();

        activeArtifacts = GameObject.FindGameObjectsWithTag("ActiveArtifact");
        collectedArtifacts = GameObject.FindGameObjectsWithTag("CollectedArtifact");
        inHandArtifacts = GameObject.FindGameObjectsWithTag("inHandArtifact");
        pickUpArtifactScript = GetComponent<playerArtifactState>();

        SceneItemsToHide = GameObject.FindGameObjectsWithTag("ToHide");
        Ground = GameObject.FindGameObjectWithTag("Terrain");

        podiumLight = GameObject.FindGameObjectWithTag("PodiumLight").GetComponent<Light>();
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
        shadowDriftScript = shadow.GetComponent<ShadowDrift>();
    }

    // Update is called once per frame
    void Update() {
        ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));

        if(Physics.Raycast(ray, out hit, 3))
	    {
            if(Input.GetMouseButtonDown(0)) {
                //if clicking on main podium
                if(hit.collider.tag == "Pedestal" && pickUpArtifactScript.isHoldingArtifact) {
                    pickUpArtifactScript.changeIsHolding();
                    shadowSwitchScript.changeHoldingArtifact();
                    pickUpArtifactScript.addArtifact();
                    shadowDriftScript.addArtifact();
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
                    podiumLight.intensity = numCollectedArtifacts*3;
                    shadowRandomMoveScript.updateAreaRanges();
                }
                //if clicking on artifact
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

        if (Input.GetKeyDown(KeyCode.F6)) {
            gameWon = true;
        }

        if(gameWon) {
            GameWonWaitTime();
            Debug.Log("Yay");
        }
    }

    public void GameWonWaitTime() {
        for (int i = 0; i < 8; i++) {
            SceneItemsToHide[i].SetActive(false);
        }

        Ground.SetActive(false);

        timer += Time.deltaTime;

        if (timer > 10.0f) {
            OpenFinalScene(2);
            Debug.Log("TimeComplete");
        }

    }

    public void OpenFinalScene(int scene) {
        Application.LoadLevel(scene);
    }
}
