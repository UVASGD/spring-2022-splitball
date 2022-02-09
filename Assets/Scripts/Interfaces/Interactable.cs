using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //TODO: "layer" this so all things that can interact with stuff (balls, players, obstacles?) can interact with them
    //this stuff being boosters, bouncy walls (like the things in pinball), slow zones, etc 

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void BoostIt(float angle, float force){
        //TRIGGGGGONOMETRYYYYYYYYYYYYYYYYY!!!!!!!!!
        //note the angle is its offset from 90 (cuz idk how to rotate sprites) so were flipping cos and sin, then negating sin I think
        Vector2 forceVector = new Vector2(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        //Vector2 forceVector;
        //forceVector.x = direction 
        rb2d.AddForce(forceVector * force, ForceMode2D.Impulse);
    }

}
