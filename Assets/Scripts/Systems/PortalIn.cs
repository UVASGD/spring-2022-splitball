using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIn : MonoBehaviour
{
    Animator anim;
    private SpriteRenderer sprite;
    private CapsuleCollider2D capsule;
    public GameObject player;
    public PlayerController playerControl;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Ball" && this.name == "PortalIn1") {
            Vector2 tp = GameObject.Find("PortalOut1").transform.position;
            ball.transform.position = tp;
            playerControl.ballEntered1 =  true;
        }
        if (collision.gameObject.name == "Ball" && this.name == "PortalIn2") {
            Vector2 tp = GameObject.Find("PortalOut2").transform.position;
            ball.transform.position = tp;
            playerControl.ballEntered2 =  true;
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalIn1") {
            Vector2 tp = GameObject.Find("PortalOut1").transform.position;
            player.transform.position = tp;
            anim.SetBool("out", true);
            StartCoroutine(disablePortal(1.5f));
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalIn2") {
            Vector2 tp = GameObject.Find("PortalOut2").transform.position;
            player.transform.position = tp;
            anim.SetBool("out", true);
            StartCoroutine(disablePortal(1.5f));
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalOut1") {
            if (playerControl.ballEntered1 == true) {
                anim.SetBool("out", true);
                StartCoroutine(disablePortal(1.5f));
            }
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalOut2") {
            if (playerControl.ballEntered2 == true) {
                anim.SetBool("out", true);
                StartCoroutine(disablePortal(1.5f));
            }
        }
///copyed to double 

        if (collision.gameObject.name == "Ball" && this.name == "PortalIn3") {
            Vector2 tp = GameObject.Find("PortalOut3").transform.position;
            ball.transform.position = tp;
            playerControl.ballEntered1 =  true;
        }
        if (collision.gameObject.name == "Ball" && this.name == "PortalIn4") {
            Vector2 tp = GameObject.Find("PortalOut4").transform.position;
            ball.transform.position = tp;
            playerControl.ballEntered2 =  true;
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalIn3") {
            Vector2 tp = GameObject.Find("PortalOut3").transform.position;
            player.transform.position = tp;
            anim.SetBool("out", true);
            StartCoroutine(disablePortal(1.5f));
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalIn4") {
            Vector2 tp = GameObject.Find("PortalOut4").transform.position;
            player.transform.position = tp;
            anim.SetBool("out", true);
            StartCoroutine(disablePortal(1.5f));
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalOut3") {
            if (playerControl.ballEntered1 == true) {
                anim.SetBool("out", true);
                StartCoroutine(disablePortal(1.5f));
            }
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalOut4") {
            if (playerControl.ballEntered2 == true) {
                anim.SetBool("out", true);
                StartCoroutine(disablePortal(1.5f));
            }
        }

        if (collision.gameObject.name == "Ball" && this.name == "PortalIn5") {
            Vector2 tp = GameObject.Find("PortalOut5").transform.position;
            ball.transform.position = tp;
            playerControl.ballEntered1 =  true;
        }
        if (collision.gameObject.name == "Ball" && this.name == "PortalIn6") {
            Vector2 tp = GameObject.Find("PortalOut6").transform.position;
            ball.transform.position = tp;
            playerControl.ballEntered2 =  true;
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalIn5") {
            Vector2 tp = GameObject.Find("PortalOut5").transform.position;
            player.transform.position = tp;
            anim.SetBool("out", true);
            StartCoroutine(disablePortal(1.5f));
        }
        if (collision.gameObject.name == "Player" && this.name == "PortalIn6") {
            Vector2 tp = GameObject.Find("PortalOut6").transform.position;
            player.transform.position = tp;
            anim.SetBool("out", true);
            StartCoroutine(disablePortal(1.5f));
        }
    }

    

    IEnumerator disablePortal(float duration) {
        yield return new WaitForSeconds(duration);
        sprite.enabled = false;
        capsule.enabled = false;
    }

}
