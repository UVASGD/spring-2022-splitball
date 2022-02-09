using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyAiStill : Destructible
{
   //what to chase after
    Transform target;
    //speed
    Rigidbody2D rb;
    //distance enemy will stop tracking player when they get this close (and melee range)
    public float stopChase = 2.0f;

    public float timeToDie;

    // Animator for death animation purposes
    public Animator animator;

    // Healthbar
    public Slider healthbar;

    //melee enemy variables //from specners work in enemyAi.cs
      //attack-y stuff
    public float damage = 5.0f; //damage per attack
    public float swingTimer =1.0f; //ie how long between swings
    public float lastSwing = 0; //the actual timer maybe I should use different names lol

    void Start()
    {
        //get components from objects
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {   
        //increments the swing timer
        lastSwing += Time.deltaTime;

        //check distance from enemy to player to stop crowding
        //float toTarget = Vector2.Distance(rb.position, target.position); 
       
        /*if(stopChase > toTarget)
        {
            //deal melee damage (add indicator)
            Attack(target.GetComponent<Destructible>());
        }
        */
    }

    void OnCollisionEnter2D(Collision2D col){
       
        if (col.gameObject.name == "Player"){
            Attack(col.gameObject.GetComponent<Destructible>());
        }
    }

    //taken from spencers work in enemyAi.cs
    private void Attack(Destructible target){
        if (lastSwing >= swingTimer){
            target.TakeDamage(damage);
            lastSwing = 0;
        }
    }


      public float giveScale()
    {
        //returns the distance  
        return stopChase;
    }

    public override void Die() {
        animator.SetBool("Dying", true);
		StartCoroutine(StartDying());
    }

    IEnumerator StartDying(){
        swingTimer = timeToDie * 4;
        damage = 0f;
        GetComponent<bulletSpawn>().enabled = false;
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }

    public override void UpdateHealth()
    {
        healthbar.value = hitPoints;
    }

}