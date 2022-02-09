using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawnStraight : MonoBehaviour
{
    //For animating purposes
    public Animator animator;
    //what is fired
    public GameObject bullet;
    //where bullet is spawned from
    public Transform spawnPoint;


    //attack-y stuff (taken from spencers work on enemyAI)
    public float shootTimer; //ie how long between shots
    public float lastFire = 0; //the actual timer maybe I should use different names lol
                               // This is the minimum Time (seconds) required for animation!!!
    float setupTime = 1.25f;

    public bool up = false;


    void Start()
    {
        //get components from objects
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //check to see if shoot
        //incrememnt shot timer
        lastFire += Time.deltaTime;

        animator.SetFloat("TimeLeft", shootTimer - lastFire);
        //fire if timer higher than shot clock
        if (lastFire >= shootTimer)
        {
            lastFire = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        float angle;
        if (!up)
            angle = 180;
        else
            angle = 0;

        //rotate by angle
        spawnPoint.rotation = Quaternion.Euler(Vector3.forward * (angle));

        Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    }
}
