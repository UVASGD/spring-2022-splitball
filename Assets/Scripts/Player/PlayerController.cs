using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : Destructible
{

    //TODOS FROM PlayerMovement.cs
    //max speed (both for dash and normal, consider a deacceleration of maxSpeed after a dash?)
    //delete PlayerMovement once done

    //inputs
    public Vector2 movement;
    public Vector2 aim;
    public bool fire;

    public GameManager gm;

    public Animator animator;

    //sounds
    public AudioClip[] clips = new AudioClip[6]; //0: dash, 1: hit wall/ball, 2: take damage, 3: dying, 4: power up (I actually also put this in an audio source lol), 5: heal ()

    //im sorta copying this from last year's project im 90% sure some of it is not necessary
    Rigidbody2D rb2d;
    PlayerData stats;
    public float currentMaxSpeed;
    public float maxMoveSpeed = 10f;
    public float maxDashSpeed = 20f;
    public float movePower = 5f;
    public float dashCD = 1f;
    public float lastDash = 0f;
    public float dashPower = 10f;
    public Sprite neutral;
    public Sprite dash;
    public Transform pos;

    //iframes
    public float iFrameCounter = 0f; //the one that counts up
    public float iFrameDuration; //how long they are invicinble after being attacked

    //power ups/commnads
    public bool RecallActive = false;

    // Bool portal
    public bool ballEntered1 = false;
    public bool ballEntered2 = false;

    //DEATH
    public float timeToDie;

    // Heal
    public float healPower = 20f;

    //Status
    public bool balloon = false;
    float balloonTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerData>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        RecallActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isActive) {
                        
            movement = new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"));
            fire = Input.GetKey("q");
            aim = rb2d.velocity.normalized;
            //   InvokeRepeating("RegenerateStamina", 0f, .5f);

        }
        iFrameCounter += Time.deltaTime;
        lastDash += Time.deltaTime;

    }

    void FixedUpdate() {
        
        //NOTE FOR FUTURE DEVELOPING:
        //to make a "dash" use rb2d.AddForce(new Vector2(speed,speed), ForceMode2D.Impulse)

        if(fire && lastDash >= dashCD && stats.dashes != 0){
            StartCoroutine(Deccelerate());
            AudioSource.PlayClipAtPoint(clips[0], transform.position);
            rb2d.velocity = new Vector2(0,0);
            rb2d.AddForce(aim.normalized * dashPower, ForceMode2D.Impulse);
            lastDash = 0f;
            stats.dashes -= 1;
        }


        rb2d.AddForce(movement * movePower);
      
        if(rb2d.velocity.magnitude > currentMaxSpeed){
            //note: using velocity makes it easily push physics objects away instead of bouncing off of them (as intended)2
                rb2d.velocity = rb2d.velocity.normalized * currentMaxSpeed;
        }

        Vector2 dir = rb2d.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        animator.SetFloat("Angle", angle);
        animator.SetFloat("VelMag", rb2d.velocity.magnitude);
        
        if (balloon == true)
            ballooning();
    }

    private void ballooning()
    {
        if (balloonTimer <= 2.22f)
        {
            if (rb2d.transform.localScale.x < 1.76f)
            {
                rb2d.transform.localScale += new Vector3(0.02f, 0.02f, 0.0f);
            }
            else
                balloonTimer += Time.deltaTime;
        }
        else
        {
            rb2d.transform.localScale -= new Vector3(0.02f, 0.02f, 0.0f);
        }

        if (rb2d.transform.localScale.x == 1)
        {
            balloonTimer = 0.0f;
            balloon = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        //checks if powerup
        if(collision.gameObject.tag == "Boost")
        {
            checkPowerUp(collision);
        }

        if (collision.gameObject.tag == "Recall" ) {
            RecallActive = true;
            collision.gameObject.SetActive(false);
        }
    }


    //New powerups implimented here (and other controler)
    //TODO fix/add audio, may be better to add onto the power up itself
    //where to add power up code
    private void checkPowerUp(Collider2D col)
    {
        //adds to dahses 
        if (col.gameObject.name == "Boost(Clone)")
        {
            stats.dashes += 1;
            //GetComponent<AudioSource>().Play();

            powerUpSpawn.spawnedPowerUps -= 1;
            col.gameObject.SetActive(false);
            Debug.Log("Speedi Boi");
        }

        if (col.gameObject.name.Equals("BoostPad(Clone)"))
        {
            //GetComponent<AudioSource>().Play();
            Instantiate(col.GetComponent<boostPadHolder>().boostPad, GameObject.Find("Player2").GetComponent<Transform>().position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));

            col.gameObject.SetActive(false);
            powerUpSpawn.spawnedPowerUps -= 1;
        }

        if (col.gameObject.name.Equals("Balloon(Clone)"))
        {
            //GetComponent<AudioSource>().Play();
            GameObject.Find("Player2").GetComponent<Player2Controler>().balloon = true;

            col.gameObject.SetActive(false);
            powerUpSpawn.spawnedPowerUps -= 1;
        }


        if (col.gameObject.name == "Heal")
        {
            Debug.Log("Heal");
            Heal(healPower);
            AudioSource.PlayClipAtPoint(clips[5], transform.position);
            col.gameObject.SetActive(false);
        }
    }


    public override void Heal(float amount) {
        float sum = this.hitPoints += amount;
        if (sum >= maxHealth) {
            this.hitPoints = maxHealth;
        }
        else {
            this.hitPoints = sum;
        }
        gm.UpdateHealth(hitPoints);
    }

    private void OnCollisionEnter2D(Collision2D col){
        //if we hit anything we go "ping"
        AudioSource.PlayClipAtPoint(clips[1], transform.position);
    }

    IEnumerator PowerUp(float duration) {
        movePower += 5f;
        maxMoveSpeed += 5f;
        maxDashSpeed += 5f;
        currentMaxSpeed += 5f;
        yield return new WaitForSeconds(duration);
        maxMoveSpeed -= 5f;
        maxDashSpeed -= 5f;
        currentMaxSpeed -= 5f;
        movePower -= 5f;
    }

    IEnumerator Deccelerate() {
        currentMaxSpeed = maxDashSpeed;
        while (currentMaxSpeed > maxMoveSpeed){
            yield return new WaitForSeconds(0.05f);
            currentMaxSpeed -= 1f;
        }
        currentMaxSpeed = maxMoveSpeed;
        
    }

    public override void Die() {
        animator.SetBool("Dying", true);
        AudioSource.PlayClipAtPoint(clips[3], transform.position);
		StartCoroutine(StartDying());
    }

    IEnumerator StartDying(){
        yield return new WaitForSeconds(timeToDie);
        gm.isActive = false;
        SceneManager.LoadScene("Defeat");
    }

    IEnumerator Flash(float x) {
    while(iFrameCounter < iFrameDuration) {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(x);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(x);
     }
 }


    public override void TakeDamage(float amount)
    {
        if(iFrameCounter > iFrameDuration){
            iFrameCounter = 0f;
            StartCoroutine(Flash(.1f));
            this.hitPoints -= amount;
            gm.UpdateHealth(hitPoints);
            //so kind of you to bring this line over commented out <3
            AudioSource.PlayClipAtPoint(clips[2], transform.position);
            if (hitPoints <= 0)
            {
                Die();
            }
        }
    }

    public void RegenerateStamina() {
        gm.UpdateStamina(lastDash);
    }
}