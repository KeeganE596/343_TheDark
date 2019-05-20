using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDrift : MonoBehaviour
{

    float yPos;
    // Start is called before the first frame update
    void Start()
    {
        yPos = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
     	  yPos = gameObject.transform.position.y;
    }

    public void Move() {
//    	Debug.Log("moving");
    	transform.LookAt(GameObject.FindWithTag("Player").transform.position);
    	transform.Translate(Vector3.forward * Time.deltaTime*3); 

        Vector3 vec = transform.position;
        vec.y += (0f-yPos);
        transform.position = vec;
    }

    public void stopMove()  {
        transform.LookAt(GameObject.FindWithTag("Player").transform.position);

        Vector3 vec = transform.position;
        vec.y += (0f-yPos);
        transform.position = vec;
    }
}
