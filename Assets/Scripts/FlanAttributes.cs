using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlanAttributes : MonoBehaviour
{
    public GameObject house;
    public int x;
    public int y;
    public float speed = 5f;
    public string direction;
    public bool isCarrying = false;
    public int itemID = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
        x = (int)(transform.position.x + 0.5f);
        y = (int)(transform.position.z + 0.5f);

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
    }

    public void dropItem() {
        itemID = 0;
        isCarrying = false;
    }
}
