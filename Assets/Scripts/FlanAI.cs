using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlanAI : MonoBehaviour, ITickable
{

    FlanAttributes flanAttributes;
    TerrainGeneration terrainGeneration;
    GameManager gameManager;
    public float speed = 5f;
    private Vector3 wayPoint = new Vector3(0,1,0);

    private int tickDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        flanAttributes = GetComponent<FlanAttributes>();
        terrainGeneration = TerrainGeneration.instance;
        gameManager = GameManager.instance;
        wayPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, wayPoint) > 0.2f) {
            //Do Something for animation
        } else {
            //Stop animation
        }

    }

    public void tick() {
        //Tick Delay based on where we are, will be modified with another method called get tickDelay
        if (tickDelay > 0) {
            tickDelay--;
            return;
        }

        //try drop off something

        //try pick up something

        move();
    }

    private void move() {
        //First get orientation and tiles around me
        string direction = flanAttributes.direction;
        
        GameObject northTile = terrainGeneration.landMap[flanAttributes.x, flanAttributes.y+1];
        GameObject westTile = terrainGeneration.landMap[flanAttributes.x-1, flanAttributes.y];
        GameObject southTile = terrainGeneration.landMap[flanAttributes.x, flanAttributes.y-1];
        GameObject eastTile = terrainGeneration.landMap[flanAttributes.x+1, flanAttributes.y];

        LandNode northLN = northTile.GetComponent<LandNode>();
        LandNode westLN = westTile.GetComponent<LandNode>();
        LandNode southLN = southTile.GetComponent<LandNode>();
        LandNode eastLN = eastTile.GetComponent<LandNode>();

        GameObject curTile = terrainGeneration.landMap[flanAttributes.x, flanAttributes.y];
        curTile.GetComponent<LandNode>().hasFlan = false;
        curTile.GetComponent<LandNode>().flanOn = null;

        //Facing North
        if (direction == "N") {
            // Go Forward
            if (northLN.getBuildingID() == 1) {
                if (northLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (northLN.flanOn.GetComponent<FlanAI>().willMove(1)) { 
                        northLN.hasFlan = true;
                        northLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y + 1);
                        return;
                    }
                } else {
                    northLN.hasFlan = true;
                    northLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y + 1);
                    return;
                }
            }

            // Go Right
            if (eastLN.getBuildingID() == 1) {
                if (eastLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (eastLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        eastLN.hasFlan = true;
                        eastLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x + 1, 1, flanAttributes.y);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    eastLN.hasFlan = true;
                    eastLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x + 1, 1, flanAttributes.y);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                    return;
                }
            }

            // Go Left
            if (westLN.getBuildingID() == 1) {
                if (westLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (westLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        westLN.hasFlan = true;
                        westLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x - 1, 1, flanAttributes.y);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    westLN.hasFlan = true;
                    westLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x - 1, 1, flanAttributes.y);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
                    return;
                }
            }
        }

        //Facing East -90
        if (direction == "E") {
            // Go Forward
            if (eastLN.getBuildingID() == 1) {
                if (eastLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (eastLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        eastLN.hasFlan = true;
                        eastLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x + 1, 1, flanAttributes.y);
                        return;
                    }
                } else {
                    eastLN.hasFlan = true;
                    eastLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x + 1, 1, flanAttributes.y);
                    return;
                }
            }

            // Go Right
            if (southLN.getBuildingID() == 1) {
                if (southLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (southLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        southLN.hasFlan = true;
                        southLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y - 1);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    southLN.hasFlan = true;
                    southLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y - 1);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                    return;
                }
            }

            // Go Left
            if (northLN.getBuildingID() == 1) {
                if (northLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (northLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        northLN.hasFlan = true;
                        northLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x, 1,flanAttributes.y + 1);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    northLN.hasFlan = true;
                    northLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x, 1,flanAttributes.y + 1);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                    return;
                }
            }

        }

        //Facing South
        if (direction == "S") {
            // Go Forward
            if (southLN.getBuildingID() == 1) {
                if (southLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (southLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        southLN.hasFlan = true;
                        southLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y - 1);
                        return;
                    }
                } else {
                    southLN.hasFlan = true;
                    southLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y - 1);
                    return;
                }
            }

            // Go Right
            if (westLN.getBuildingID() == 1) {
                if (westLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (westLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        westLN.hasFlan = true;
                        westLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x - 1, 1, flanAttributes.y);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    westLN.hasFlan = true;
                    westLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x - 1, 1, flanAttributes.y);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
                    return;
                }
            }

            // Go Left
            if (eastLN.getBuildingID() == 1) {
                if (eastLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (eastLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        eastLN.hasFlan = true;
                        eastLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x + 1, 1, flanAttributes.y);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    eastLN.hasFlan = true;
                    eastLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x + 1, 1, flanAttributes.y);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                    return;
                }
            }

        }

        //Facing West
        if (direction == "W") {
            // Go Forward
            if (westLN.getBuildingID() == 1) {
                if (westLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (westLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        westLN.hasFlan = true;
                        westLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x - 1 , 1, flanAttributes.y);
                        return;
                    }
                } else {
                    westLN.hasFlan = true;
                    westLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x - 1 , 1, flanAttributes.y);
                    return;
                }
            }

            // Go Right
            if (northLN.getBuildingID() == 1) {
                if (northLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (northLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        northLN.hasFlan = true;
                        northLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y + 1);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    northLN.hasFlan = true;
                    northLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y + 1);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                    return;
                }
            }

            // Go Left
            if (southLN.getBuildingID() == 1) {
                if (southLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (southLN.flanOn.GetComponent<FlanAI>().willMove(1)) {
                        southLN.hasFlan = true;
                        southLN.flanOn = this.gameObject;
                        wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y - 1);
                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                        return;
                    }
                } else {
                    southLN.hasFlan = true;
                    southLN.flanOn = this.gameObject;
                    wayPoint = new Vector3(flanAttributes.x, 1, flanAttributes.y - 1);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                    return;
                }
            }

        }

        //Can't move? Reset this
        curTile.GetComponent<LandNode>().hasFlan = true;
        curTile.GetComponent<LandNode>().flanOn = this.gameObject;
    }

    public bool willMove(int depth) {
        if (depth > 10) {
            return false;
        }
        //First get orientation and tiles around me
        string direction = flanAttributes.direction;
        
        GameObject northTile = terrainGeneration.landMap[flanAttributes.x, flanAttributes.y+1];
        GameObject westTile = terrainGeneration.landMap[flanAttributes.x-1, flanAttributes.y];
        GameObject southTile = terrainGeneration.landMap[flanAttributes.x, flanAttributes.y-1];
        GameObject eastTile = terrainGeneration.landMap[flanAttributes.x+1, flanAttributes.y];

        LandNode northLN = northTile.GetComponent<LandNode>();
        LandNode westLN = westTile.GetComponent<LandNode>();
        LandNode southLN = southTile.GetComponent<LandNode>();
        LandNode eastLN = eastTile.GetComponent<LandNode>();
        
        //Facing North
        if (direction == "N") {
            // Go Forward
            if (northLN.getBuildingID() == 1) {
                if (northLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (northLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Right
            if (eastLN.getBuildingID() == 1) {
                if (eastLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (eastLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Left
            if (westLN.getBuildingID() == 1) {
                if (westLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (westLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }
        }

        //Facing East -90
        if (direction == "E") {
            // Go Forward
            if (eastLN.getBuildingID() == 1) {
                if (eastLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (eastLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Right
            if (southLN.getBuildingID() == 1) {
                if (southLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (southLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Left
            if (northLN.getBuildingID() == 1) {
                if (northLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (northLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

        }

        //Facing South
        if (direction == "S") {
            // Go Forward
            if (southLN.getBuildingID() == 1) {
                if (southLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (southLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Right
            if (westLN.getBuildingID() == 1) {
                if (westLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (westLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Left
            if (eastLN.getBuildingID() == 1) {
                if (eastLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (eastLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

        }

        //Facing West
        if (direction == "W") {
            // Go Forward
            if (westLN.getBuildingID() == 1) {
                if (westLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (westLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Right
            if (northLN.getBuildingID() == 1) {
                if (northLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (northLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            // Go Left
            if (southLN.getBuildingID() == 1) {
                if (southLN.hasFlan) {
                    //Get Flan on Tile and check if it will move next turn
                    if (southLN.flanOn.GetComponent<FlanAI>().willMove(depth + 1)) {
                        return true;
                    }
                } else {
                    return true;
                }
            }
        }

        //Cannot move
        return false;
    }
}
