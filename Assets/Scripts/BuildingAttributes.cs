using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttributes : MonoBehaviour
{
    [Header("Building Properties")]     
    public string buildingName;
    public int bID;

    public string direction;
    public bool rotateCW = false;

    public Vector3 offset;

    void Update() {
        float heading = Mathf.Atan2(transform.right.z, transform.right.x) * Mathf.Rad2Deg;

        if (heading < 0) {
            heading = heading + 2*180;
        }

        heading = (int)heading;

        if (heading == 0) {
            direction = "N";
        } else if (heading == 90) {
            direction = "W";
        } else if (heading == 180) {
            direction = "S";
        } else if (heading == 270) {
            direction = "E";
        }

        if (rotateCW) {
            rotateCW = false;
            rotateBuildingCW();
        }
    }

    void rotateBuildingCW() {
        if (direction == "N") {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        } else if (direction == "E") {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        } else if (direction == "S") {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
        } else if (direction == "W") {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
    }
}
