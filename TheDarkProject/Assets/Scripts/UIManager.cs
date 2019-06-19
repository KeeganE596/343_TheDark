
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    public void DisBoolAnimator(Animator anim) {
        anim.SetBool("Displayed", false);
    }

    public void EnBoolAnimator(Animator anim) {
        anim.SetBool("Displayed", true);
    }

    public void OpenScene(int scene)
    {
        Application.LoadLevel(1);
    }
}
