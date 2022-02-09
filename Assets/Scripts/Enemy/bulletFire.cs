using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFire : MonoBehaviour
{
public float bulletForce = 100.0f;
public Rigidbody2D rb;

 public float damage= 5.0f; //damage per attack
void Start ()
{
        //fires bullet with it's force
        //rb.velocity = transform.right*bulletForce;
        rb.velocity = transform.up * bulletForce;
}


void OnTriggerEnter2D(Collider2D target)
{   
    //turn off friendly fire
    if (target.tag == "enemy")
    return;

    //for somereason really hates camerabounds of unmoving camera
    if(target.name != "CameraBounds" )
    {
        
        //checks to see if hit player
        if(target.name.Equals("Player"))
            Attack(target.GetComponent<Destructible>());
        //removes object
        Destroy(gameObject);
    }
}
    //deals damage based on bullet damage
    private void Attack(Destructible target){
            target.TakeDamage(damage);
       
}
}
