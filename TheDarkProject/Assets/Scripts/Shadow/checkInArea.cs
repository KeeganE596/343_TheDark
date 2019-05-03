using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkInArea : MonoBehaviour
{

	bool playerIn;

    // Start is called before the first frame update
    void Start()
    {
        playerIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		playerIn = true;
    	}
    }
    void OnTriggerExit(Collider other) {
    	if(other.gameObject.tag == "Player") {
    		playerIn = false;
    	}
    }

    public bool getIfInArea() {
    	return playerIn;
    }
}
