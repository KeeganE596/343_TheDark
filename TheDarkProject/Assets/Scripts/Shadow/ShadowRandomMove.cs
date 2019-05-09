using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRandomMove : MonoBehaviour
{
	GameObject area0Plane;
	GameObject area1Plane;
	GameObject area2Plane;
	GameObject area3Plane;
	GameObject area4Plane;
	GameObject area5Plane;
	GameObject[] planes;

	Vector3 offset0;
	Vector3 offset1;
	Vector3 offset2;
	Vector3 offset3;
	Vector3 offset4;
	Vector3[] offsets;

    // Start is called before the first frame update
    void Start()
    {
		area0Plane = GameObject.FindWithTag("plane0");
		area1Plane = GameObject.FindWithTag("plane1");
		area2Plane = GameObject.FindWithTag("plane2");
		area3Plane = GameObject.FindWithTag("plane3");
		area4Plane = GameObject.FindWithTag("plane4");
		area5Plane = GameObject.FindWithTag("plane5");
		planes = new GameObject[]{area0Plane, area1Plane, area2Plane, area3Plane, area4Plane, area5Plane};

		Vector3 offset0 = new Vector3(0, 0, 0);
    	Vector3 offset1 = new Vector3(15, 0, 0);
    	Vector3 offset2 = new Vector3(-15, 0, 0);
    	Vector3 offset3 = new Vector3(0, 0, 15);
    	Vector3 offset4 = new Vector3(0, 0, -15);
    	offsets = new Vector3[]{offset0, offset1, offset2, offset3, offset4};
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 pickArea(int i) {
    	Vector3 newPosition = planes[i].transform.position + pickAreaPoint();

    	return newPosition;
    }

    Vector3 pickAreaPoint() {
    	int r = Random.Range(0, 5);

    	return offsets[r];

    }

    public GameObject currentArea(int i) {
    	return planes[i];
    }
}
