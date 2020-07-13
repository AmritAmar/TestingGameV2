using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager Script!");
            return;
        }
        instance = this;
    }

    public GameObject[] buildings;

    public int curBuildingSelected = 0;

    public GameObject getBuildingToBuild() {
        return buildings[curBuildingSelected];
    }

    public void setBuildingToBuild(int changeBuildingSelection) {
        // TODO, rather than directly assuming the index position of building, find it given the actual Building ID 
        curBuildingSelected = changeBuildingSelection;
    }

    void Start()
    {   
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
