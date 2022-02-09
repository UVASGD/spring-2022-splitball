using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameManager gm;
    public PlayerController pc;

    //magnet
    public Rigidbody2D ballrb;
    public float magnetSpeed;

    public float maxSpeed;

    //sounds
    public AudioClip[] clips = new AudioClip[3]; //0: discharge, 1: recharge, 2: bonk
    private bool hasRecharged;

    //ball walls
    public Collider2D[] ballWalls;

    public Animator animator;

    //attack properties
    public float damage = 10;
    public float swingTimer = 1; //ie how long between "attacks" (just so it doesnt do a lot of little attacks, may have to modify this system)
    public float lastSwing = 0; //the actual timer maybe I should use different names lol

    //This is for sticky buttons to hold ball in place until player hit
    public GameObject tiedStickyButton = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ballrb = GetComponent<Rigidbody2D>();
        pc = gm.player.GetComponent<PlayerController>();
        //If game manager not specified, find it
        if (!gm)
            gm = GameObject.FindObjectOfType<GameManager>();
        //Changed this so you dont need to find name of game manager anymore, just the component
        
        foreach(Collider2D i in ballWalls){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), i);
        }

        hasRecharged = false;
    }

    // Update is called once per frame
    void Update()
    {
        lastSwing += Time.deltaTime;
        if(ballrb.velocity.magnitude > maxSpeed){
            //note: using velocity makes it easily push physics objects away instead of bouncing off of them (as intended)2
                ballrb.velocity = ballrb.velocity.normalized * maxSpeed;
        }

        if(!hasRecharged && lastSwing > swingTimer)
        {
            animator.SetBool("Charged", true);
            hasRecharged = true;
            AudioSource.PlayClipAtPoint(clips[1], transform.position);
        }
    
    }
    private void FixedUpdate()
    {
        Vector2 dir = (this.transform.position - gm.player.transform.position).normalized;

        if(Input.GetKey("space") && gm.isActive){
            ballrb.AddForce(-dir*magnetSpeed);
        }

        if(pc.RecallActive==true){
            ballrb.velocity = new Vector2(0, 0);
            tiedStickyButton = null;
            transform.position =  new Vector2(gm.player.transform.position.x +1f, gm.player.transform.position.y-1f);
            pc.RecallActive = false;
        }

        //If its on a sticky button
        if (tiedStickyButton)
        {
            //Then slow down the ball and stick it to center
            transform.position = Vector3.Lerp(transform.position, tiedStickyButton.transform.position, Time.fixedDeltaTime * 5);
            GetComponent<Rigidbody2D>().velocity *= 0.8f;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.name == "0 to 1") {
			SceneManager.LoadScene("Level1");
		}
        if (col.gameObject.name == "Goal") {
			this.transform.position = new Vector3(-10, 0, -1);
			SceneManager.LoadScene("Level2");
		}
		if (col.gameObject.name == "2 to 3") {
			SceneManager.LoadScene("Level3");
		}
        if (col.gameObject.name == "3 to 4") {
			SceneManager.LoadScene("Level4");
		}
        if (col.gameObject.name == "4 to win") {
			SceneManager.LoadScene("Victory");
		}
		// if (collision.CompareTag("3 to win")) {
		// 	gm.isActive = false;
		// 	SceneManager.LoadScene("Victory");
		// 	Destroy(gameObject);			
		// }

        //First tests to see if collided with button, THEN tests to see if its a sticky button
        else if (col.gameObject.GetComponent<LogicActivator>() && col.gameObject.GetComponent<LogicActivator>().type == 3)
        {
            tiedStickyButton = col.gameObject;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        AudioSource.PlayClipAtPoint(clips[2], transform.position);
        //If player hit
        if (col.gameObject.GetComponent<PlayerController>())
        {
            tiedStickyButton = null;
        }

        //if a non-player destructible is hit
        else if(col.gameObject.GetComponent<Destructible>()){
            Attack(col.gameObject.GetComponent<Destructible>());
        }

    }

    private void Attack(Destructible target){
        if(target.GetComponent<boxBullet>() && !target.GetComponent<boxBullet>().destroyable){
            return;
        }
        if (lastSwing >= swingTimer){
            AudioSource.PlayClipAtPoint(clips[0], transform.position);
            hasRecharged = false;
            target.TakeDamage(damage);
            target.UpdateHealth();
            animator.SetBool("Charged", false);
            lastSwing = 0;
        }
    }

}
