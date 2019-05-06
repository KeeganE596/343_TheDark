using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRaycastHit : MonoBehaviour
{
	Camera cam;
	Ray ray;
    RaycastHit hit;
    
    GameObject[] artifacts;
    playerArtifactState pickUpArtifactScript;
    mainPedestal mainPedestalScript;

    public GameObject pedestalArtifact;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        artifacts = GameObject.FindGameObjectsWithTag("Artifact");
        pickUpArtifactScript = GetComponent<playerArtifactState>();
        mainPedestalScript = GetComponent<mainPedestal>();

        pedestalArtifact.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if(Physics.Raycast(ray, out hit))
	    {
            //print(hit.collider.name);
            if (Input.GetMouseButtonDown(0)) {
                if(hit.collider.name == "mainPedestal" && pickUpArtifactScript.isHoldingArtifact) {
                    pickUpArtifactScript.changeIsHolding();
                    //mainPedestalScript.makePlaced();
                    pedestalArtifact.SetActive(true);
                    Debug.Log("put down");
                }
                for(int i=0; i<artifacts.Length; i++){
                    if(hit.collider.name == "artifact"){
                        Debug.Log("pick up");
                        pickUpArtifactScript.changeIsHolding();
                        hit.collider.gameObject.SetActive(false);
                    }   
                }
                           
            }
        }
    }
}
