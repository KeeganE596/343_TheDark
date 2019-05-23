using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class playerArtifactState : MonoBehaviour
{

	public bool isHoldingArtifact;

	GameObject[] dreamObjects;
	public GameObject artifactInHand;

    GameObject[] collectedArtifacts;

	PostProcessVolume testVolume;
	Vignette testVignette;
	ChromaticAberration testCA;
	ColorGrading testCG;
	LensDistortion testLD;

    public GameObject greyScaleCG;
    public GameObject pedLight1;
    public GameObject pedLight2;

    // Start is called before the first frame update
    void Start()
    {
        isHoldingArtifact = false;
        dreamObjects = GameObject.FindGameObjectsWithTag("DreamObject");
        artifactInHand.SetActive(false);
        collectedArtifacts = GameObject.FindGameObjectsWithTag("CollectedArtifact");
    }

    // Update is called once per frame
    void Update()
    {
    	//var sinTest = (Mathf.Sin(Time.realtimeSinceStartup));
    	//Debug.Log(sinTest);

    	if(isHoldingArtifact) {
    		for(int i=0; i<dreamObjects.Length; i++) {
    			dreamObjects[i].SetActive(true);
    		}

    		var sinWave = (Mathf.Sin(Time.realtimeSinceStartup*2f));
    		var cosWave = (Mathf.Cos(Time.realtimeSinceStartup*2f));

    		//testVignette.intensity.value = Mathf.Lerp(0.3f, 0.45f, sinWave);
    		testCA.intensity.value = Mathf.Lerp(0.3f, 1.0f, sinWave);
    		testCG.hueShift.value = Mathf.Lerp(-50f, 50f, sinWave);
    		testLD.intensity.value = Mathf.Lerp(-30f, 30f, sinWave);
    		testLD.centerX.value = Mathf.Lerp(-0.3f, 0.3f, (Mathf.Sin(Time.realtimeSinceStartup*2.2f)));
    		testLD.centerY.value = Mathf.Lerp(-0.3f, 0.3f, (Mathf.Cos(Time.realtimeSinceStartup*3f)));
    		//Debug.Log(testLD.centerY.value);
    	}
    	else if(dreamObjects[0].activeSelf) {
    		for(int i=0; i<dreamObjects.Length; i++) {
    			dreamObjects[i].SetActive(false);
    		}
    	}

        collectedArtifacts = GameObject.FindGameObjectsWithTag("CollectedArtifact");
        //Debug.Log(collectedArtifacts.Length);
    }

    public void changeIsHolding() {
    	isHoldingArtifact = !isHoldingArtifact;
    	
    	if(isHoldingArtifact) {
    		artifactInHand.SetActive(true);
    		doEffects();
            greyScaleCG.SetActive(false);
            pedLight1.SetActive(false);
            pedLight2.SetActive(false);
    	}
    	else {
    		artifactInHand.SetActive(false);
    		RuntimeUtilities.DestroyVolume(testVolume, true, true);
            greyScaleCG.SetActive(true);
    	}
    }

    //enable the post processing volume
    void doEffects() {
        Debug.Log("doing effects");
    	testVignette = ScriptableObject.CreateInstance<Vignette>();
    	testVignette.enabled.Override(true);
        testVignette.intensity.Override(0.35f);
		testCA = ScriptableObject.CreateInstance<ChromaticAberration>();
		testCA.enabled.Override(true);
        testCA.intensity.Override(1f);
		testCG = ScriptableObject.CreateInstance<ColorGrading>();
		testCG.enabled.Override(true);
        testCG.hueShift.Override(1f);
        testCG.saturation.Override(-10f);
		testLD = ScriptableObject.CreateInstance<LensDistortion>();
		testLD.enabled.Override(true);
        testLD.intensity.Override(1f);
        testLD.centerX.Override(0f);
        testLD.centerY.Override(0f);

		testVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, testVignette, testCA, testCG, testLD);
    }
}
