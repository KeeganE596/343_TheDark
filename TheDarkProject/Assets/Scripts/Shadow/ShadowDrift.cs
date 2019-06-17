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
    	transform.Translate(Vector3.forward * Time.deltaTime*4);
        transform.position += Vector3.up * 0.01f;
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
        //Debug.Log("normal drift");
    }

    public void stopMove()  {
        transform.LookAt(GameObject.FindWithTag("Player").transform.position);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
        //Debug.Log("stop drift");
    }

    public void artifactMove() {
        transform.LookAt(GameObject.FindWithTag("Player").transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime*5f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
        //Debug.Log("finding");
    }
}
