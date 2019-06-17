using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class playerArtifactState : MonoBehaviour
{

	public bool isHoldingArtifact;
    public bool nearPodium;
    public bool nearShadow;

	GameObject[] dreamObjects;

	PostProcessVolume postProcessingVolume;
	Vignette vignettePP;
	ChromaticAberration chromaticAberrationPP;
	ColorGrading colorGradingPP;
	LensDistortion lensDistPP;
    MotionBlur motionBlurPP;
    Grain grainPP;

    float playerHealth;
    float deathRate;

    int collectedArtifacts;
    float ldVar;

    //Animation control vars
    Animator plAnim;

    // Start is called before the first frame update
    void Start()
    {
        isHoldingArtifact = false;
        nearPodium = false;
        dreamObjects = GameObject.FindGameObjectsWithTag("DreamObject");

        //Getting animation components
        plAnim = GetComponentInChildren<Animator>();

        collectedArtifacts = 1;

        playerHealth = 50;
        deathRate = 0.1f;

        doPPEffects();
    }

    // Update is called once per frame
    void Update()
    {
    	if(isHoldingArtifact) {
    		for(int i=0; i<dreamObjects.Length; i++) {
    			dreamObjects[i].SetActive(true);
    		}

    		var sinWave = (Mathf.Sin(Time.realtimeSinceStartup*2f));
    		var cosWave = (Mathf.Cos(Time.realtimeSinceStartup*2f));

    		vignettePP.intensity.value = Mathf.Lerp((0.1f*collectedArtifacts), (0.1f*collectedArtifacts), sinWave);
    		chromaticAberrationPP.intensity.value = Mathf.Lerp((0.05f*collectedArtifacts), (0.1f*collectedArtifacts), sinWave);
    		colorGradingPP.hueShift.value = Mathf.Lerp(-(10*collectedArtifacts), (10*collectedArtifacts), sinWave);
            colorGradingPP.saturation.value = -(50/collectedArtifacts);
    		lensDistPP.intensity.value = Mathf.Lerp(-(6*collectedArtifacts), (6*collectedArtifacts), sinWave);
    		lensDistPP.centerX.value = Mathf.Lerp(-(0.1f*collectedArtifacts), (0.1f*collectedArtifacts), (Mathf.Sin(Time.realtimeSinceStartup*2.2f)));
    		lensDistPP.centerY.value = Mathf.Lerp(-(0.1f*collectedArtifacts), (0.1f*collectedArtifacts), (Mathf.Cos(Time.realtimeSinceStartup*3f)));
    	}
    	else if(dreamObjects[0].activeSelf) {
    		for(int i=0; i<dreamObjects.Length; i++) {
    			dreamObjects[i].SetActive(false);
    		}
    	}

        if(nearPodium) {
            if(chromaticAberrationPP.intensity.value < 0.08*collectedArtifacts) {
                chromaticAberrationPP.intensity.value +=0.05f*collectedArtifacts;
            }
            if(colorGradingPP.saturation.value < 55/collectedArtifacts) {
                colorGradingPP.saturation.value += 1;
            }
            var sinWave = (Mathf.Sin(Time.realtimeSinceStartup));
            colorGradingPP.hueShift.value = Mathf.Lerp(-(10*collectedArtifacts), (10*collectedArtifacts), sinWave);
        }
        else {
            if(chromaticAberrationPP.intensity.value > 0) {
                chromaticAberrationPP.intensity.value -= 0.05f;
            }
            if(colorGradingPP.saturation.value > -60) {
                colorGradingPP.saturation.value -= 1;
            }
            colorGradingPP.hueShift.value = 0;
        }

        if(nearShadow) {
            if (playerHealth >= 0) {
                playerHealth -= deathRate;
                if(deathRate < 0.6f) {
                    deathRate += 0.01f;
                } 
            }
        }
        else if(playerHealth <= 50) {
            playerHealth += 0.1f; 
            if(deathRate > 0.1f) {
                deathRate -= 0.03f;
            }      
        }

        if(playerHealth <= 0f) {
        	Debug.Log("Oh No");
        }

        vignettePP.intensity.value = map(playerHealth, 0f, 50f, 0.5f, 0f);
        grainPP.intensity.value = map(playerHealth, 0f, 50f, 1f, 0.18f);
        grainPP.size.value = map(playerHealth, 0f, 50f, 3f, 1f);
        colorGradingPP.contrast.value = map(playerHealth, 0f, 50f, -80f, 0f);

        //Debug.Log(nearPodium);
    }

    public void changeIsHolding() {
    	isHoldingArtifact = !isHoldingArtifact;
    	
    	if(isHoldingArtifact) {
            plAnim.SetBool("Carry", true);
        }
    	else {
            plAnim.SetBool("Carry", false);
        }
    }

    //enable the post processing volume
    void doPPEffects() {
        //General Effects
        colorGradingPP = ScriptableObject.CreateInstance<ColorGrading>();
        colorGradingPP.enabled.Override(true);
        colorGradingPP.saturation.Override(-65f);
        colorGradingPP.contrast.Override(0f);
        motionBlurPP = ScriptableObject.CreateInstance<MotionBlur>();
        motionBlurPP.enabled.Override(true);
        motionBlurPP.shutterAngle.Override(270);
        grainPP = ScriptableObject.CreateInstance<Grain>();
        grainPP.enabled.Override(true);
        grainPP.intensity.Override(0.18f);

        //Artifact Effects
    	vignettePP = ScriptableObject.CreateInstance<Vignette>();
    	vignettePP.enabled.Override(true);
        vignettePP.intensity.Override(0);
		chromaticAberrationPP = ScriptableObject.CreateInstance<ChromaticAberration>();
		chromaticAberrationPP.enabled.Override(true);
        chromaticAberrationPP.intensity.Override(0);
        colorGradingPP.hueShift.Override(0);
		lensDistPP = ScriptableObject.CreateInstance<LensDistortion>();
		lensDistPP.enabled.Override(true);
        lensDistPP.intensity.Override(0);
        lensDistPP.centerX.Override(0f);
        lensDistPP.centerY.Override(0f);

		postProcessingVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignettePP, chromaticAberrationPP, colorGradingPP, lensDistPP, motionBlurPP, grainPP);
    }

    public void addArtifact() {
        collectedArtifacts++;
    }

    float map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }
}
