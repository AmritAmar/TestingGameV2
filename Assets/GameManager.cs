using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("More than one GameManager Script!");
            return;
        }
        instance = this;
    }

    public float tickRate = 1f; //Number of seconds per TICK

    public List<GameObject> flans;
    public List<GameObject> buildings;

    private float curTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        flans = new List<GameObject>();
        buildings = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > tickRate) {
            curTime = 0;
            tickAll();
        }
    }

    private void tickAll() {
        foreach (var flan in flans) {
            ITickable[] tickables = flan.GetComponents<ITickable>();
            foreach (ITickable tickable in tickables) {
                tickable.tick();
            }
            //flan.GetComponent<ITickable>().tick();
        }
        foreach (var building in buildings) {
            ITickable[] tickables = building.GetComponents<ITickable>();
            foreach (ITickable tickable in tickables) {
                tickable.tick();
            }
        }
    }
}
