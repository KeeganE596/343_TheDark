using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDrift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void Move() {
    	transform.LookAt(GameObject.FindWithTag("Player").transform.position);
    	transform.Translate(Vector3.forward * Time.deltaTime*3);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void stopMove()  {
        transform.LookAt(GameObject.FindWithTag("Player").transform.position);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
