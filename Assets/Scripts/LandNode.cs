using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandNode : MonoBehaviour
{
    private LandAttributes landA;
    private Renderer rend;
    private Color normalColor;
    public bool hasFlan = false;
    public GameObject flanOn = null;

    private GameObject building;
    private int buildingID = 0;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        landA = GetComponent<LandAttributes>();
        normalColor = rend.material.color;
    }

    void OnMouseDown() {
        if (landA.isResource) {
            Debug.Log("This is a resource!");
            return;
        }

        if (hasFlan) {
            Debug.Log("This tile has a flan on it");
            return;
        }

        if (building != null) {
            Debug.Log("This already has a structure!");
            return;
        } 

        GameObject b = BuildManager.instance.getBuildingToBuild();

        if (b.GetComponent<BuildingAttributes>().buildingName == "null") { //Just a way to not select anything
            Debug.Log("Nothing selected!");
        } else {
            building = (GameObject)Instantiate(b, transform.position + b.GetComponent<BuildingAttributes>().offset, b.transform.rotation);
            building.transform.SetParent(this.transform);
            buildingID = b.GetComponent<BuildingAttributes>().bID; //Set current building ID on this tile
            
            //Add reference to GameManager
            GameManager.instance.buildings.Add(building);
        }
        
    }

    public int getBuildingID() {
        return buildingID;
    }

    public GameObject getBuilding() {
        return building;
    }

    void OnMouseEnter() {
        rend.material.color = Color.yellow;

        // Shadow version
        GameObject b = BuildManager.instance.getBuildingToBuild();

        if (b.GetComponent<BuildingAttributes>().buildingName == "null") { //Just a way to not select anything
            //Don't render anthing
        } else {
            
        }
    }

    void OnMouseExit() {
        rend.material.color = normalColor;
    }
}
