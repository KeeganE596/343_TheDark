using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAreaSearch : MonoBehaviour
{
	bool doSearch;
	Vector3 newPosition;

	ShadowRandomMove shadowRandomMoveScript;
	ShadowSwitch shadowSwitchScript;

	GameObject player;

    // Start is called before the first frame update
    void Start() {
        doSearch = false;

        shadowRandomMoveScript = GetComponent<ShadowRandomMove>();
        shadowSwitchScript = GetComponent<ShadowSwitch>();

        player = GameObject.FindGameObjectWithTag("Player");

        newPosition = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update() {
    	if(doSearch) {
    		if((newPosition.x == 0f && newPosition.z == 0f) || checkPos() || checkPlayerPos()) {
    			newPosition = pickSearchPoint();
    		}
    		transform.LookAt(newPosition);
    		transform.Translate(Vector3.forward * Time.deltaTime*2.5f);
            transform.position += Vector3.up * 0.02f;
    		transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    	}
    }

    public void switchSearch(bool thisBool) {
    	doSearch = thisBool;
    }

    bool checkPos() { //check if shadow is close to newPosition
    	if(transform.position.x > (newPosition.x - 5) && transform.position.x < (newPosition.x + 5) &&
    		transform.position.z > (newPosition.z - 5) && transform.position.z < (newPosition.z + 5)) {
    		return true;
    	}
    	else { return false; }
    }

    bool checkPlayerPos() { //check if player is far from newPosition
    	Vector3 playerPos = player.transform.position;

    	if(playerPos.x < (newPosition.x - 30) || playerPos.x > (newPosition.x + 30) ||
    		playerPos.z < (newPosition.z - 30) || playerPos.z > (newPosition.z + 30)) {
    		return true;
    	}
    	else { return false; }
    }

    Vector3 pickSearchPoint() { //pick new position close to player
        Vector3 playerPos = player.transform.position;

        float xR = Random.Range(-20, 20);
        float zR = Random.Range(-20, 20);
        return new Vector3(playerPos.x + xR, 0, playerPos.z + zR);
    }
}
