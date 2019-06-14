using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ShadowSwitch : MonoBehaviour
{
    bool nearPlayer;
	ShadowDrift shadowDriftScript;
    ShadowRandomMove shadowRandomMoveScript;
    ShadowInnerTrigger shadowInnerTriggerScript;
    ShadowAreaSearch shadowAreaSearchScript;
    int areaIndex;
    bool hasPlayerInArea;

    public GameObject player;

    float timer;

    float playerHealth;
    bool dying;

    PostProcessVolume ppVolume;
    Vignette vignettePP;
    Color vignetteCol = new Color(0,0,0,150);
    Grain grainPP;

    public bool innerTrigger;
    bool playerHasArtifact;

    //Animation control vars
    Animator shAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        nearPlayer = false;
        areaIndex = 0;
        shadowDriftScript = GetComponent<ShadowDrift>();
        shadowRandomMoveScript = GetComponent<ShadowRandomMove>();
        shadowInnerTriggerScript = GetComponentInChildren<ShadowInnerTrigger>();
        shadowAreaSearchScript = GetComponent<ShadowAreaSearch>();
        hasPlayerInArea = false;

        //Getting animation components
        shAnim = GetComponentInChildren<Animator>();

        timer = 0;

        playerHealth = 50;
        dying = false;

        innerTrigger = false;

        doEffects();
    }

    // Update is called once per frame
    void Update()
    {
        hasPlayerInArea = shadowRandomMoveScript.getIfHasPlayer(areaIndex);
        innerTrigger = shadowInnerTriggerScript.getInnerTrigger();

        if(playerHasArtifact && !nearPlayer && !innerTrigger) {
            shadowDriftScript.artifactMove();
        }
        
        if(innerTrigger) {
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
        //Debug.Log(areaIndex);

        //Running the Shadows animations
        ShadowAnimate();
        
        if (dying && playerHealth >= 0) {
       		playerHealth -= 0.1f;
       	}
        else if(!dying && playerHealth <= 50) {
            playerHealth += 0.1f;        
        }

        //Post processing for player when close
        vignettePP.intensity.value = map(playerHealth, 0f, 50f, 0.5f, 0f);
        grainPP.intensity.value = map(playerHealth, 0f, 50f, 1f, 0f);
        grainPP.size.value = map(playerHealth, 0f, 50f, 3f, 1f);
    }


    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		nearPlayer = true;
    		transform.LookAt(GameObject.FindWithTag("Player").transform.position);
            dying = true;
    	}
    }
    void OnTriggerStay(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		nearPlayer = true;
    		transform.LookAt(GameObject.FindWithTag("Player").transform.position);
            dying = true;
    	}
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            nearPlayer = false;
            dying = false;
        }
    }

    void doEffects() {
        vignettePP = ScriptableObject.CreateInstance<Vignette>();
        vignettePP.enabled.Override(true);
        vignettePP.intensity.Override(0.05f);
        vignettePP.color.Override(vignetteCol);
        grainPP = ScriptableObject.CreateInstance<Grain>();
        grainPP.enabled.Override(true);
        grainPP.intensity.Override(0.1f);
        grainPP.size.Override(1f);

        ppVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignettePP, grainPP);
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

    float map(float s, float a1, float a2, float b1, float b2) {
    	return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}

    public int getArea() {
        return areaIndex;
    }

    public void changeHoldingArtifact() {
        playerHasArtifact =! playerHasArtifact;
    }
}
