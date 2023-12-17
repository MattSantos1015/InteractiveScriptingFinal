// Mathew Santos Interactive Scripting 
// this will spawn cube randomly throughout the map 

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    // creating an array of names
    public string[] Names = { "harry", "Hemione", "ron" };

    [SerializeField]
    Color[] colors;

    [SerializeField]
   public bool debug = false;

    [SerializeField]
    GameObject prefabCube;

    [SerializeField]
    float X1;
    [SerializeField]
    float X2;
    [SerializeField]
    float Y;
    [SerializeField]
    float Z1;
    [SerializeField]
    float Z2;
    [SerializeField]
    float cubesSpawned;
    [SerializeField]
    float timerWait;
    

   public bool canStartSpawnLoop = true;
    public bool isSpawning = false;
    public Score score;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Press Shift+0 to enter debug mode.");
        if (debug) Debug.Log("Press G to spawn cubes");
        if (debug) Debug.Log("The first name in the array is " + Names[0]);
      
    }


    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                debug = !debug; // toggles the boolean
                Debug.Log("Debug Mode is now " + debug);
            }
        }
     
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(CollectCubes());
        }
    }
    // spawn a cube 
    GameObject SpawnCube()
    {
        if (debug) Debug.Log("Starting SpawnCube() Function");

        if (debug) Debug.Log("Creating cube from prefab");
        GameObject cube = Instantiate(prefabCube);
        // move cube to x poition from -40 to 40, z position to -40 to 40, y position of 2

        cube.name = Names[Random.Range(0, Names.Length)];

        Vector3 newPos = new Vector3(Random.Range(X1, X2), Y, Random.Range(Z1, Z2));

        if (debug) Debug.Log("setting cube position to" + newPos);
        cube.transform.position = newPos;

        //if (debug) Debug.LogError("Pausing here to look at the position of the cube.");

        if (debug) Debug.Log("adding Rigidbody component");

       // cube.AddComponent(typeof(Rigidbody));

        Color newColor = Random.ColorHSV();
        if (debug) Debug.Log("setting color to " + newColor);
        //cube.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
        return cube;
    }
    IEnumerator CollectCubes()
    {
        // find all cubes in scene // add them to an array
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");

        int i = 0;
        while (i < cubes.Length)
        {
            cubes[i].transform.position = new Vector3(0, 2, 0);
            i += 1;
            yield return new WaitForEndOfFrame();
        }

    }

    // loop 
    IEnumerator SpawnLoop()
    {
        canStartSpawnLoop = false;
        int counter = 0;
        while (counter < cubesSpawned) // once counter is greater than 25 the while loop will shut itself down
        {
            counter += 1;
            SpawnCube();
            yield return new WaitForSeconds(timerWait);


        }
      canStartSpawnLoop = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color for the gizmos

        Vector3 spawnAreaCenter = new Vector3((X1 + X2) / 2, Y, (Z1 + Z2) / 2);
    Vector3 spawnAreaSize = new Vector3(X2 - X1, 0.1f, Z2 - Z1);

    // Calculate the center of the spawner using X1 and Z1
    Vector3 spawnerCenter = new Vector3(X1, Y, Z1);

    // Calculate the distance from the spawner center to the corner of the spawn area
    float radius = Vector3.Distance(spawnerCenter, spawnAreaCenter);

    // Draw a wireframe cube at the spawn area's center
    Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);

        // Draw a wireframe circle around the spawner
      //  Gizmos.color = Color.green;
       // Gizmos.DrawWireSphere(spawnerCenter, radius);
    }
    public void StopSpawning()
    {
        isSpawning = false;
        if (debug) Debug.Log("Spawning Stopped");
    }
 private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("target"))
        {
              StartCoroutine(SpawnLoop());
        }
    }

}
