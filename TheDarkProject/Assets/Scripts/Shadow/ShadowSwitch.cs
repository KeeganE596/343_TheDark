using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSwitch : MonoBehaviour
{
    bool nearPlayer;
	ShadowDrift shadowDriftScript;
    ShadowRandomMove shadowRandomMoveScript;
    checkInArea checkPlayerInArea;
    GameObject currentArea;
    int areaIndex;
    bool moveRandom;

    float timer;

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

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(nearPlayer);
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
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		nearPlayer = true;
            moveRandom = false;
    		transform.LookAt(GameObject.FindWithTag("Player").transform.position);
    	}
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            nearPlayer = false;
            moveRandom = true;
        }
    }
}
