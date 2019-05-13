using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class playerHealthScript : MonoBehaviour
{
    double playerHealth;
    bool dying;

    PostProcessVolume ppVolume;
    Vignette vignette;
    float vIntensity;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 50;
		dying = false;
		vIntensity = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if(dying && playerHealth >= 0) {
        	playerHealth -= 0.1;
        	vIntensity += 0.05f;
        	//if(ppVolume = null) {
        		//doEffects();
        	//}
        }
        else if(!dying && playerHealth <= 50) {
        	playerHealth += 1;
        	vIntensity += 0.1f;
        }
        if(playerHealth < 50) {
        	vignette.intensity.value = vIntensity;
        }
        

        if(playerHealth >= 50 && ppVolume != null) {
        	RuntimeUtilities.DestroyVolume(ppVolume, true, true);
        }
        //Debug.Log(playerHealth);
    }

    public void changeInDyingRange() {
       	//dying = !dying;
       	if(dying) {
       		dying = false;
       	}
       	else
       	dying = true;
       	Debug.Log("here");
    }

    void doEffects() {
    	vignette = ScriptableObject.CreateInstance<Vignette>();
    	vignette.enabled.Override(true);
        vignette.intensity.Override(0.1f);

    	ppVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
    }

    public double getHealth() {
    	//Start();
    	return playerHealth;
    }

    public bool getIfDying() {
    	return dying;
    }
}
