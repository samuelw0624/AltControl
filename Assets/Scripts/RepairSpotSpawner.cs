using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSpotSpawner : MonoBehaviour
{
    public GameObject repairSpots;
    float randX;
    float randY;
    Vector2 whereToSpawn;
    public float spawnRate = 10f;
    float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-1f, 2f);
            randY = Random.Range(2f, 4.5f);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(repairSpots, whereToSpawn, Quaternion.identity);
        }
    }
}
