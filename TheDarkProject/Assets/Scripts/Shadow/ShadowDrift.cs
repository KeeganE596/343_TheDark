using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDrift : MonoBehaviour
{
    int collectedArtifacts;
    float speedMult;

    // Start is called before the first frame update
    void Start() {
        collectedArtifacts = 0;
        speedMult = 0;
    }

    // Update is called once per frame
    void Update() {
        speedMult = 0.3f*collectedArtifacts;
    }

    public void Move() {
    	transform.LookAt(GameObject.FindWithTag("Player").transform.position);
    	transform.Translate(Vector3.forward * Time.deltaTime*(3+speedMult));
        transform.position += Vector3.up * 0.01f;
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void stopMove()  {
        transform.LookAt(GameObject.FindWithTag("Player").transform.position);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void artifactMove() {
        transform.LookAt(GameObject.FindWithTag("Player").transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime*(4+speedMult));
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void addArtifact() {
        collectedArtifacts++;
    }
}
