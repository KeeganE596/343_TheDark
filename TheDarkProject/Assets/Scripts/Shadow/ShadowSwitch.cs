using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ShadowSwitch : MonoBehaviour
{
    bool nearPlayer;
	ShadowDrift shadowDriftScript;
    ShadowRandomMove shadowRandomMoveScript;
    checkInArea checkPlayerInArea;
    GameObject currentArea;
    int areaIndex;
    bool moveRandom;
    public GameObject player;
    //GameObject playerCharacter;
    //playerHealthScript playerHealthSc;

    float timer;

    float playerHealth;
    bool dying;

    PostProcessVolume ppVolume;
    Vignette vignettePP;
    float vIntensity;

    // Start is called before the first frame update
    void Start()
    {
        nearPlayer = false;
        areaIndex = 0;
        shadowDriftScript = GetComponent<ShadowDrift>();
        shadowRandomMoveScript = GetComponent<ShadowRandomMove>();
        currentArea = shadowRandomMoveScript.currentArea(areaIndex);
        checkPlayerInArea = currentArea.GetComponent<checkInArea>();
        moveRandom = true;
        //playerCharacter = player.GetComponent<FirstPersonCharacter>();
        //playerHealthSc = player.GetComponentInChildren<playerHealthScript>();

        timer = 0;

        playerHealth = 50;
        dying = false;
        vIntensity = 0.05f;

        doEffects();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerHealthSc.getHealth());

        currentArea = shadowRandomMoveScript.currentArea(areaIndex);
        checkPlayerInArea = currentArea.GetComponent<checkInArea>();
        //Debug.Log(currentArea);
        //Debug.Log(checkPlayerInArea.getIfInArea());
        //Debug.Log(checkPlayerInArea.getIfInArea());
        if(!moveRandom && nearPlayer && checkPlayerInArea.getIfInArea()) { 
        	shadowDriftScript.Move();
        }
        else {
            timer += Time.deltaTime;
        }

        if(moveRandom && checkPlayerInArea.getIfInArea() && timer > 1) {
            transform.position = shadowRandomMoveScript.pickArea(areaIndex);
            timer = 0;
        }

        if(moveRandom && !nearPlayer && timer > 2) {
            areaIndex = Random.Range(0, 6);
            currentArea = shadowRandomMoveScript.currentArea(areaIndex);
            checkPlayerInArea = currentArea.GetComponent<checkInArea>();
            transform.position = shadowRandomMoveScript.pickArea(areaIndex);

            timer = 0;
        }

       if(dying && playerHealth >= 0) {
        playerHealth -= 0.1f;
        //if(vIntensity < 0.6) {
        //    vIntensity += 0.01f;
        //}
        
        //if(ppVolume = null) {
            
        //}
        }
        else if(!dying && playerHealth <= 50) {
            playerHealth += 0.1f;
            //if(vIntensity > 0) {
            //    vIntensity -= 0.1f;
            //}
            
        }
        
        if(playerHealth < 50) {
            vignettePP.intensity.value = vIntensity;
        }
        if(playerHealth >= 50 && ppVolume != null) {
            RuntimeUtilities.DestroyVolume(ppVolume, true, true);
        }
        Debug.Log(vIntensity + ",  " + playerHealth);
    }

    void LateUpdate() {
        vIntensity = Mathf.Lerp(1.0f, 0.0f, playerHealth);
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		nearPlayer = true;
            moveRandom = false;
    		transform.LookAt(GameObject.FindWithTag("Player").transform.position);
            dying = true;
            //playerHealthSc.changeInDyingRange();
            //Debug.Log(playerHealthSc.getIfDying());
    	}
    }
    void OnTriggerExit(Collider other) {
        //if(other.gameObject.tag == "Player") {
        Debug.Log("exit");
            nearPlayer = false;
            moveRandom = true;
            dying = false;
            //playerHealthSc.changeInDyingRange();
            //Debug.Log(playerHealthSc.getIfDying());
        //}
    }

    void doEffects() {
        vignettePP = ScriptableObject.CreateInstance<Vignette>();
        vignettePP.enabled.Override(true);
        vignettePP.intensity.Override(0.05f);

        ppVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignettePP);
    }
}
