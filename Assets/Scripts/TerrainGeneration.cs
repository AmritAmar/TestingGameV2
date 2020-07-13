using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainGeneration : MonoBehaviour
{
    public static TerrainGeneration instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("More than one TerrainGeneration Script!");
            return;
        }
        instance = this;
    }
    
    [Serializable]
    public class LandType{
        public string name;
        public int ID;
        public bool isResource;
        public int maxAmount;
        public float probGen;
        public float yOffset = 0f;
        public Material landMaterial;
        public GameObject landObj;
    }
    public LandType[] landTypes;
    
    GameObject world;

    public int x;
    public int y;
    
    public int seed;

    public GameObject[,] landMap;

    // Start is called before the first frame update
    void Start()
    {
        if (seed > 0) {
            UnityEngine.Random.InitState(seed);
        }
        world = new GameObject("World");
        landMap = new GameObject[x,y];
        generateTerrain();
    }

    private Mesh CombineMeshes(List<Mesh> meshes)
    {
        var combine = new CombineInstance[meshes.Count];
        for (int i = 0; i < meshes.Count; i++)
        {
            combine[i].mesh = meshes[i];
            combine[i].transform = transform.localToWorldMatrix;
        }

        var mesh = new Mesh();
        mesh.CombineMeshes(combine, false, true, false);
        return mesh;
    }

    public void generateTerrain() {
        List<Mesh> meshList = new List<Mesh>();

        for(int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                //Land Type Generated
                float prob = UnityEngine.Random.value;
                foreach (var landT in landTypes) {
                    if (prob < landT.probGen) {
                        GameObject cube = Instantiate(landT.landObj);
                        cube.transform.SetParent(world.transform);
                        cube.name = "Coordinate_" + i.ToString() + "_" + j.ToString();
                        
                        meshList.Add(cube.GetComponent<MeshFilter>().mesh);

                        Renderer cubeRenderer = cube.GetComponent<Renderer>();
                        cubeRenderer.material = landT.landMaterial;
                        cube.transform.position = new Vector3(i, landT.yOffset, j);

                        //Set properties
                        cube.GetComponent<LandAttributes>().setAttributes(landT.name, landT.ID, landT.isResource, landT.maxAmount, landT.landMaterial);
                        
                        landMap[i,j] = cube;
                        break; 
                    }
                }
            }
        }
        MeshFilter sc = world.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer xc = world.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        //var mesh = CombineMeshes(meshList);
        //sc.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
