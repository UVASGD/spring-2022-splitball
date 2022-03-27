using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFirePowerup : MonoBehaviour
{
    public float bulletForce = 100.0f;
    public Rigidbody2D rb;

    public int Friendly = 0;

    void Start()
    {
        //fires bullet with it's force
        //rb.velocity = transform.right*bulletForce;
        rb.velocity = transform.up * bulletForce;
    }

    public void setFri(int friend)
    {
        Friendly = friend;
    }


    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.gameObject.tag == "levelTile")
        {
             Destroy(gameObject);
        }
        /*
        if (target.name.Equals("Player"))
            if (Friendly == 1)
            {

                Debug.Log("Here");
                
            }
        //else

        //for somereason really hates camerabounds of unmoving camera
        if (target.name != "CameraBounds")
        {

            //checks to see if hit player

        }
        

        Debug.Log(target.name);
      //  Time.timeScale = 0;
        /*
        
        if (target.tag == "enemy")
            return;

        //for somereason really hates camerabounds of unmoving camera
        if (target.name != "CameraBounds")
        {

            //checks to see if hit player
            //if (target.name.Equals("Player"))
                //removes object
              //  Time.timeScale = 0;
           // Destroy(gameObject);
        }
        */

    }
}
