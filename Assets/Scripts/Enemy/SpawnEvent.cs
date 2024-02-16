using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvent : MonoBehaviour
{
    public GameObject kite;
    public GameObject player1;
    float randX, randY;
    Vector2 SpawnLocation;
    [SerializeField]
    private float spawnRate;
    [SerializeField]
    float nextSpawn = 0.0f;
    [SerializeField]
    bool isSpawned = false;
    [SerializeField]
    private float startSpawn;



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.instance.gameStart)
        {
            if (!isSpawned)
            {

                StartCoroutine(RandomValue());
                StartCoroutine(Spawn());
            }
        }



        //spawn randomly within the game range
        /*if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            //Vector2 playerDire = player1.transform.position;
            randX = Random.Range(-40,44);
            randY = Random.Range(8, 14);
            SpawnLocation = new Vector2(randX,randY);
            Instantiate(kite, SpawnLocation, Quaternion.identity);

        }*/
    }


    IEnumerator Spawn()
    {
        if(startSpawn > nextSpawn)
        {
            //Kite spawns around player
            nextSpawn = Time.time + spawnRate;
            randY = Random.Range(8, 15);
            Vector2 playerDire = player1.transform.position;
            SpawnLocation = new Vector2(randX + playerDire.x, randY);
            Instantiate(kite, SpawnLocation, Quaternion.identity);
            isSpawned = true;
            yield return new WaitForSeconds(10);
            if (GameObject.FindWithTag("Kite") != null)
            {
                
                Destroy(GameObject.FindWithTag("Kite"));

            }
            isSpawned = false;
        }
        else
        {
            startSpawn += Time.deltaTime;
        }

    }

    IEnumerator RandomValue()
    {

        // generate random value axis-x
        float value = Random.Range(1, 10);
        if(value >= 5)
        {
            randX = Random.Range(-2, -7);
        }
        else
        {
            randX = Random.Range(2, 7);
        }

        yield return randX;
    }
}
