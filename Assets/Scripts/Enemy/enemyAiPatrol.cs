using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class enemyAiPatrol : Destructible
{
    //what to chase after
    Transform target;
    bool goToOne = true;
    Vector3 point1;
    Vector3 point2;
    public float point1x;
    public float point1y;
    public float point2x;
    public float point2y;

    public float timeToDie;

    //For animation
    public Animator animator;

    public Slider healthbar; 

    //speed
    public float speed = 200f;
    //how many nodes to look ahead
    public float nextWaypointDistance = .75f;
    Path path;
    int currentWaypoint = 0;

    bool reachedEndofPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    //distance enemy will stop tracking player when they get this close (and melee range)
    public float stopChase = 2.0f;


    //melee enemy variables //from specners work in enemyAi.cs
    //attack-y stuff
    public float damage = 5.0f; //damage per attack
    public float swingTimer = 1.5f; //ie how long between swings
    public float lastSwing = 0; //the actual timer maybe I should use different names lol

    void Start()
    {
        //get components from objects
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        point1 = new Vector3(point1x, point1y, 0);
        point2 = new Vector3(point2x, point2y, 0);
        target = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        //loop to find past
        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        //only call StartPath if the end of the path ahs allready been reached
        if (seeker.IsDone())
            if (goToOne)
                seeker.StartPath(rb.position, point1, OnPathComplete);
            else
                seeker.StartPath(rb.position, point2, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        //resets path
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        //increments the swing timer
        lastSwing += Time.deltaTime;


        //check if should attack
        float toTarget = Vector2.Distance(rb.position, target.position);
        //if (stopChase <= toTarget)
        //Attack(target.GetComponent<Destructible>());


        if (path == null)
        {
            return;
        }


        //check if reached end of path or to closer to player
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            goToOne = !goToOne;
            return;
        }

        if (reachedEndofPath == true)
        {
            if (goToOne)
                seeker.StartPath(rb.position, point1, OnPathComplete);
            else
                seeker.StartPath(rb.position, point2, OnPathComplete);
            reachedEndofPath = false;
            return;
        }


        //havent reached end of path
        else
        {
            reachedEndofPath = false;
        }
        //use path to get vector of motion
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        //apply force to object in direction
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        //check distance to next node on path
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        //check if move far enough to next node on path and update if so
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
       
        if (col.gameObject.name == "Player"){
            Attack(col.gameObject.GetComponent<Destructible>());
        }
    }

    //taken from spencers work in enemyAi.cs
    private void Attack(Destructible target)
    {
        if (lastSwing >= swingTimer)
        {
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

    IEnumerator StartDying()
    {
        swingTimer = timeToDie * 4;
        damage = 0f;
        speed = 0f;
        GetComponent<bulletSpawn>().enabled = false;
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }

    public override void UpdateHealth()
    {
        healthbar.value = hitPoints;
    }

}