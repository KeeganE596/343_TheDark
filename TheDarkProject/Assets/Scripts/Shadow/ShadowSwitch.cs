using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ShadowSwitch : MonoBehaviour
{
	ShadowDrift shadowDriftScript;
    ShadowRandomMove shadowRandomMoveScript;
    ShadowInnerTrigger shadowInnerTriggerScript;
    ShadowAreaSearch shadowAreaSearchScript;
    playerArtifactState playerArtifactStateScript;

    GameObject player;
    bool nearPlayer;
    int areaIndex;
    bool hasPlayerInArea;
    float timer;
    public bool innerTrigger;
    bool playerHasArtifact;

    //Animation control vars
    Animator shAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nearPlayer = false;
        areaIndex = 0;
        shadowDriftScript = GetComponent<ShadowDrift>();
        shadowRandomMoveScript = GetComponent<ShadowRandomMove>();
        shadowInnerTriggerScript = GetComponentInChildren<ShadowInnerTrigger>();
        shadowAreaSearchScript = GetComponent<ShadowAreaSearch>();
        playerArtifactStateScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<playerArtifactState>();
        hasPlayerInArea = false;

        //Getting animation components
        shAnim = GetComponentInChildren<Animator>();

        timer = 0;

        innerTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        hasPlayerInArea = shadowRandomMoveScript.getIfHasPlayer(areaIndex);
        innerTrigger = shadowInnerTriggerScript.getInnerTrigger();

        if(playerHasArtifact && !nearPlayer && !innerTrigger) { //if player has artifact go to them
            shadowDriftScript.artifactMove();
        }
        
        if(innerTrigger) { //if right at player
            shadowAreaSearchScript.switchSearch(false);
            shadowDriftScript.stopMove();
        }
        else if(nearPlayer) { //if in area and close to player
        	shadowAreaSearchScript.switchSearch(false);
            shadowDriftScript.Move();
        }
        else {
            timer += Time.deltaTime;
        }

        if(hasPlayerInArea && !nearPlayer && !playerHasArtifact) {  //if in same area as player but not close
            shadowAreaSearchScript.switchSearch(true);
            timer = 0;
        }

        if(!hasPlayerInArea && timer > 1.8 && !playerHasArtifact) {  //if not in same area as player
            shadowAreaSearchScript.switchSearch(false);
            areaIndex = Random.Range(0, 100);
            transform.position = shadowRandomMoveScript.pickArea(areaIndex);

            timer = 0;
        }

        //Running the Shadows animations
        ShadowAnimate();
    }


    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		nearPlayer = true;
    		transform.LookAt(other.gameObject.transform.position);
            playerArtifactStateScript.nearShadow = true;
    	}
    }
    void OnTriggerStay(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		nearPlayer = true;
    		transform.LookAt(other.gameObject.transform.position);
    	}
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            nearPlayer = false;
            playerArtifactStateScript.nearShadow = false;
        }
    }

    void ShadowAnimate() {
        //Calling on player animator
        if (nearPlayer) {
            shAnim.SetBool("Spot", true);
        }
        if (!nearPlayer) {
            shAnim.SetBool("Spot", false);
        }

        //Testing, can be deleted
        if (Input.GetKeyDown(KeyCode.L)) {
            shAnim.SetBool("Spot", true);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            shAnim.SetBool("Spot", false);
        }

//        print(shAnim.GetBool("Spot"));
    }

    public int getArea() {
        return areaIndex;
    }

    public void changeHoldingArtifact() {
        playerHasArtifact =! playerHasArtifact;
    }
}
