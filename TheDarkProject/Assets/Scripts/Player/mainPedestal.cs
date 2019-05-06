using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainPedestal : MonoBehaviour
{
	public GameObject pedestalArtifact;
	public bool artifactPlaced = false;


    // Start is called before the first frame update
    void Start()
    {
        pedestalArtifact.SetActive(false);
        artifactPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void makePlaced() {
    	artifactPlaced = true;
    	pedestalArtifact.SetActive(true);
    }
}
