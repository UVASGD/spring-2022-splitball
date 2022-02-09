using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour
{

    //For animating purposes
    public Animator animator;
 //what is fired
 public GameObject bullet;
//where bullet is spawned from
 public Transform spawnPoint;
//what to fire at
Transform target;
public GameManager gm;

 //attack-y stuff (taken from spencers work on enemyAI)
    public float shootTimer; //ie how long between shots
    public float lastFire = 0; //the actual timer maybe I should use different names lol
    //offsetMin of bullet fire (0 by default)
    public float offsetMin =0.0f;
    //offsetMax of bullet fire (0 by default)
    public float offsetMax =0.0f;

    // This is the minimum Time (seconds) required for animation!!!
    float setupTime = 1.25f;


   void Start()
    {
        //get components from objects
        target = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

 void Update ()
 {
  if (gm.isActive) {
    //check to see if shoot
    //incrememnt shot timer
    lastFire += Time.deltaTime;

    animator.SetFloat("TimeLeft", shootTimer - lastFire);
    //fire if timer higher than shot clock
    if (lastFire >= shootTimer){
      lastFire = 0;
      Shoot();
    }
  }
}

    void Shoot()
    {
     //rotate spawnPoint to face player
    Vector2 direction = target.position - spawnPoint.position;
    direction.Normalize();
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //randomly add or subtract a number between max or min to fire angle
    bool addOrSub  = (Random.value > 0.5f);
    if(addOrSub)
    angle += Random.Range(offsetMin,offsetMax);
    else
    angle -= Random.Range(offsetMin,offsetMax);
    //rotate by angle
    spawnPoint.rotation = Quaternion.Euler(Vector3.forward * (angle-90));

        //fire bullet from spawnPoint
    Instantiate(bullet,spawnPoint.position, spawnPoint.rotation);
    }
}
