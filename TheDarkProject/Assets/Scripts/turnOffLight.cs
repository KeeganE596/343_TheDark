using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffLight : MonoBehaviour
{

	public GameObject editingLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(editingLight.activeSelf) {
        	editingLight.SetActive(false);
        }
    }
}
