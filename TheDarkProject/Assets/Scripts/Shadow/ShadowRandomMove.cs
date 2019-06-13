using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRandomMove : MonoBehaviour
{
    /*Vector3 offset0;
    Vector3 offset1;
    Vector3 offset2;
    Vector3 offset3;
    Vector3 offset4;
    Vector3[] offsets;*/

    GameObject[] planesObjs;
    Area[] areas;

    // Start is called before the first frame update
    void Start()
    {
        planesObjs = GameObject.FindGameObjectsWithTag("areaPlane");
        //Debug.Log(planesObjs.Length);
        areas = new Area[planesObjs.Length];

        for (int i = 0; i < planesObjs.Length; i++) {
            areas[i] = new Area(0, 0, planesObjs[i]);
           // Debug.Log(areas[i].checkForArtifact());
        }

        /*Vector3 offset0 = new Vector3(0, 100, 0);
        Vector3 offset1 = new Vector3((transform.localScale.x*0.75f), 100, 0);
        Vector3 offset2 = new Vector3(-(transform.localScale.x*0.75f), 100, 0);
        Vector3 offset3 = new Vector3(0, 100, (transform.localScale.z*0.75f));
        Vector3 offset4 = new Vector3(0, 100, -(transform.localScale.z*0.75f));
        offsets = new Vector3[] { offset0, offset1, offset2, offset3, offset4 };*/
    }

    // Update is called once per frame
    void Update() {
        int b = 0;
        foreach (Area a in areas) {
            Debug.Log(a.checkForArtifact() + ", " + b);
            b++;
        }
        if(b==6) {
            b = 0;
        }
    }

    public Vector3 pickArea(int i) {
        updateAreaRanges();
        Vector3 newPosition = getArea(i).transform.position + pickAreaPoint(i);
        return newPosition;
    }

    public Vector3 getAreaPoint(int i) {
        Vector3 newPosition = getArea(i).transform.position + pickAreaPoint(i);
        return newPosition;
    }

    Vector3 pickAreaPoint(int i) {
        Vector3 areaScale = getArea(i).transform.localScale;

        float xR = Random.Range(-(areaScale.x*10)/2, (areaScale.x*10)/2);
        float zR = Random.Range(-(areaScale.z*10)/2, (areaScale.z*10)/2);
        return new Vector3(xR, 100, zR);
    }

    public GameObject currentArea(float i) {
        updateAreaRanges();
        return getArea(i);
    }

    void updateAreaRanges() {
        int containsArtifact = 0;
        foreach (Area a in areas) {
            if (a.checkForArtifact()) {
                containsArtifact++;
            }
        }
        if (containsArtifact > 0) {
            int small = 6 - containsArtifact;
            int range = (100 - (small * 5)) / containsArtifact;
            int min = 0;
            int max = range;
            foreach (Area a in areas) {
                if (a.checkForArtifact()) {
                    a.setMin(min);
                    a.setMax(max);
                    min = min + range;
                    max = max + range;
                }
                else {
                    a.setMin(min);
                    a.setMax(min + 5);
                    min = min + 5;
                    max = max + 5;
                }
            }
        }
    }

    GameObject getArea(float i) {
        foreach (Area a in areas) {
            if (i > a.getMin() && i < a.getMax()) {
                return a.getAreaPlane();
            }
        }
        return areas[0].getAreaPlane();
    }

    public bool getIfHasPlayer(float i) {
        foreach (Area a in areas) {
            if (i > a.getMin() && i < a.getMax()) {
                return a.checkForPlayer();
            }
        }
        return false;
    }

    public class Area {
        float minRange;
        float maxRange;
        GameObject areaPlane;

        checkInArea artCheckScript;

        public Area(float minRange, float maxRange, GameObject areaPlane) {
            this.minRange = minRange;
            this.maxRange = maxRange;
            this.areaPlane = areaPlane;

            artCheckScript = areaPlane.GetComponent<checkInArea>();
        }

        public void setMin(float min) {
            minRange = min;
        }

        public void setMax(float max) {
            maxRange = max;
        }

        public float getMin() {
            return minRange;
        }

        public float getMax() {
            return maxRange;
        }

        public Vector3 getScale() {
            return areaPlane.transform.localScale;
        }

        public GameObject getAreaPlane() {
            return areaPlane;
        }

        public bool checkForArtifact() {
            return artCheckScript.getHasArtifact();
        }

        public bool checkForPlayer() {
            return artCheckScript.getHasPlayer();
        }
    }
}
