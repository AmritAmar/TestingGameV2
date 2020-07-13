using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAI : MonoBehaviour, ITickable
{
    BuildingAttributes buildingAttributes;
    TerrainGeneration terrainGeneration;
    GameManager gameManager;
    public GameObject flan;
    public int x; 
    public int y;
    public bool spawnedFlan = false;
    
    // Start is called before the first frame update
    void Start()
    {
        buildingAttributes = GetComponent<BuildingAttributes>();
        terrainGeneration = TerrainGeneration.instance;
        gameManager = GameManager.instance;
        x = (int)(transform.position.x+0.5);
        y = (int)(transform.position.z+0.5);
    }

    public void tick() {
        //Try to spawn if not spawned yet
        if (!spawnedFlan) {
            spawnFlan();
        }
    }

    private void spawnFlan() {
        //Facing North
        if (buildingAttributes.direction == "N") {
            GameObject tile = terrainGeneration.landMap[x, y+1];
            //Check if road in front
            if (tile.GetComponent<LandNode>().getBuildingID() == 1) {
                //Spawn a Flan in!
                spawnedFlan = true;
                GameObject ourFlan = (GameObject)Instantiate(flan, new Vector3(transform.position.x, 1, transform.position.z + 1), flan.transform.rotation);
                ourFlan.transform.eulerAngles = new Vector3(ourFlan.transform.eulerAngles.x, 0,  ourFlan.transform.eulerAngles.z);
                flan = ourFlan;
                flan.GetComponent<FlanAttributes>().house = this.gameObject;
                gameManager.flans.Add(flan);
                return;
            }
        }

        if (buildingAttributes.direction == "E") {
            GameObject tile = terrainGeneration.landMap[x+1, y];
            //Check if road in front
            if (tile.GetComponent<LandNode>().getBuildingID() == 1) {
                //Spawn a Flan in!
                spawnedFlan = true;
                GameObject ourFlan = (GameObject)Instantiate(flan, new Vector3(transform.position.x+1, 1, transform.position.z), flan.transform.rotation);
                ourFlan.transform.eulerAngles = new Vector3(ourFlan.transform.eulerAngles.x, 90,  ourFlan.transform.eulerAngles.z);
                flan = ourFlan;
                flan.GetComponent<FlanAttributes>().house = this.gameObject;
                gameManager.flans.Add(flan);
                return;
            }
        }

        if (buildingAttributes.direction == "S") {
            GameObject tile = terrainGeneration.landMap[x, y-1];
            //Check if road in front
            if (tile.GetComponent<LandNode>().getBuildingID() == 1) {
                //Spawn a Flan in!
                spawnedFlan = true;
                GameObject ourFlan = (GameObject)Instantiate(flan, new Vector3(transform.position.x, 1, transform.position.z - 1), flan.transform.rotation);
                ourFlan.transform.eulerAngles = new Vector3(ourFlan.transform.eulerAngles.x, 180,  ourFlan.transform.eulerAngles.z);
                flan = ourFlan;
                flan.GetComponent<FlanAttributes>().house = this.gameObject;
                gameManager.flans.Add(flan);
                return;
            }
        }

        if (buildingAttributes.direction == "W") {
            GameObject tile = terrainGeneration.landMap[x-1, y];
            //Check if road in front
            if (tile.GetComponent<LandNode>().getBuildingID() == 1) {
                //Spawn a Flan in!
                spawnedFlan = true;
                GameObject ourFlan = (GameObject)Instantiate(flan, new Vector3(transform.position.x-1, 1, transform.position.z), flan.transform.rotation);
                ourFlan.transform.eulerAngles = new Vector3(ourFlan.transform.eulerAngles.x, -90,  ourFlan.transform.eulerAngles.z);
                flan = ourFlan;
                flan.GetComponent<FlanAttributes>().house = this.gameObject;
                gameManager.flans.Add(flan);
                return;
            }
        }

        Debug.Log("Tried to Spawn Flan but couldn't"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
