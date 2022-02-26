using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawn : MonoBehaviour
{
    //array of all spawn points
    //genrealyl give it blank objects to pull x y from to spawn powerups

    public GameObject[] activators = new GameObject[0]; //Attach buttons/switches here

    //list of all powerups that can spawn. putting naything else in here will spawn it and idk what that will do
    public GameObject[] spawners = new GameObject[0];

    //time that has passed since last spawn
    float spawnTimer = 0f;
    //time to check if should spawn power up
    public float spawnChecker = 5f;
    //chance of power up spawning each time it is checked
    public int spawnChance = 50;
    int spawnChanceHolder = 50;
    //number of powerups out
    public static int spawnedPowerUps = 0;
    //chance to increase powerup spawn each time after it doesn't
    public int increaseChance = 15;

    // Start is called before the first frame update
    void Start()
    {
        spawnedPowerUps = 0;
        spawnChanceHolder = spawnChance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnedPowerUps == 0)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnChecker)
            {

                spawnTimer = 0f;
                int randomnum = Random.Range(1, 100);

                if (spawnChance > randomnum)
                {

                    spawnedPowerUps += 1;
                    spawnChance = spawnChanceHolder;

                    //get random place and random powerup and spawn it
                    int randomplace = Random.Range(0, activators.Length);
                    int randomthing = Random.Range(0, spawners.Length);
                    Instantiate(spawners[randomthing], activators[randomplace].GetComponent<Transform>().position, activators[randomplace].GetComponent<Transform>().rotation);


                }
                else
                    spawnChance += increaseChance;
            }
        }
    }
}
