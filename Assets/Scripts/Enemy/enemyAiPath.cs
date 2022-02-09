using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class enemyAiPath : Destructible
{
    //what to chase after
    Transform target;
    //speed
    public float speed = 200f;
    //how many nodes to look ahead
    public float nextWaypointDistance = .75f;
    Path path;
    int currentWaypoint = 0;
    public GameManager gm;
    public Slider healthbar;

    public float timeToDie;

    bool reachedEndofPath = false;
    Seeker seeker;
    public Rigidbody2D rb;
    //distance enemy will stop tracking player when they get this close (and melee range)
    public float stopChase = 2.0f;

    public bool alwaysTrack = false;
    public Animator animator;


    // public EnemyHealthBar healthbar;


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
        target = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //loop to find past
        InvokeRepeating("UpdatePath", 0f, .5f);
        // healthbar.SetHealth(hitPoints, maxHealth);

    }

    void UpdatePath()
    {

        //only call StartPath if the end of the path ahs allready been reached
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
        if (gm.isActive == true)
        {

            //increments the swing timer
            lastSwing += Time.deltaTime;

            //check distance from enemy to player to stop crowding
            float toTarget = Vector2.Distance(rb.position, target.position);


            if (path == null)
            {
                return;
            }

            if (!alwaysTrack)
            {
                GraphNode node1 = AstarPath.active.GetNearest(rb.position, NNConstraint.Default).node;
                GraphNode node2 = AstarPath.active.GetNearest(target.position, NNConstraint.Default).node;
                if (!PathUtilities.IsPathPossible(node1, node2))
                {
                    path = null;
                    return;
                }
            }


            //resets path if no path or reached end of path and is far enough away from player
            if ((reachedEndofPath == true) & stopChase <= toTarget)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
                reachedEndofPath = false;
                return;
            }
            //check if reached end of path or to closer to player


            if (currentWaypoint >= path.vectorPath.Count || stopChase > toTarget)
            {
                //deal melee damage (add indicator)
                //Attack(target.GetComponent<Destructible>());

                reachedEndofPath = true;
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

            //if(//distance to greate from current way put add strong force pushing back toward it (normalize vector towards it like with crosshair)
            //      rb2d.AddForce(aim.normalized * crosshairDistance * dashPower, ForceMode2D.Impulse); 
            //)

            //check if move far enough to next node on path and update if so
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
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

    public override void Die()
    {
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