using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAreaSearch : MonoBehaviour
{
	bool doSearch;
	Vector3 newPosition;

	ShadowRandomMove shadowRandomMoveScript;
	ShadowSwitch shadowSwitchScript;

    // Start is called before the first frame update
    void Start() {
        doSearch = false;

        shadowRandomMoveScript = GetComponent<ShadowRandomMove>();
        shadowSwitchScript = GetComponent<ShadowSwitch>();

        newPosition = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update() {
    	if(doSearch) {
    		if((newPosition.x == 0f && newPosition.z == 0f) || checkPos()) {
    			newPosition = shadowRandomMoveScript.getAreaPoint(shadowSwitchScript.getArea());
    			newPosition.y = 0f;
    		}
    		transform.LookAt(newPosition);
    		transform.Translate(Vector3.forward * Time.deltaTime*3);
            transform.position += Vector3.up * 0.02f;
    		transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    	}
    }

    public void switchSearch(bool thisBool) {
    	doSearch = thisBool;
    }

    bool checkPos() {
    	if(transform.position.x > (newPosition.x - 5) && transform.position.x < (newPosition.x + 5) &&
    		transform.position.z > (newPosition.z - 5) && transform.position.z < (newPosition.z + 5)) {
    		return true;
    	}
    	else { return false; }
    }
}
