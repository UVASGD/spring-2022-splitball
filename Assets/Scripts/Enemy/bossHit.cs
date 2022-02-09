using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossHit : Destructible
{
    public GameObject TheBoss;
    private Rigidbody2D ballRb;
    // Start is called before the first frame update

    void Start(){
        ballRb = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
        foreach(GameObject i in TheBoss.GetComponent<bossAi>().bossList){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), i.GetComponent<Collider2D>());
        }
    }

    void OnCollisionEnter2D(Collision2D col){
       
        if (col.gameObject.name == "Player"){
            col.gameObject.GetComponent<Destructible>().TakeDamage(TheBoss.GetComponent<bossAi>().bossList[1].GetComponent<boxBullet>().damage);
        }
    }

    public override void TakeDamage(float damage){
        TheBoss.GetComponent<bossAi>().hp -= 1;

        if (TheBoss.GetComponent<bossAi>().hp == 0){
            TheBoss.GetComponent<bossAi>().Die();
            return;
            //SceneManager.LoadScene("Victory");
        }

        //Debug.Log("Hit" + TheBoss.GetComponent<bossAi>().getHP());

        TheBoss.GetComponent<bossAi>().timeDown();

        ballRb.velocity = new Vector2(0, 0);

        StartCoroutine(ReturnBall(ballRb.transform));
        //col.gameObject.GetComponent<Rigidbody2D>().transform.position = Vector2.Lerp(transform.position, new Vector2(0,0), Time.fixedDeltaTime * 5);

        foreach (GameObject bossSqaure in TheBoss.GetComponent<bossAi>().bossList)
        {
            if (bossSqaure.GetComponent<boxBullet>().destroyable == true)
            {
                bossSqaure.GetComponent<boxBullet>().Respawn();
            }
        }

        TheBoss.gameObject.GetComponent<bossAi>().goPhase2();
    }

    IEnumerator ReturnBall(Transform ball) {
        float timeSoFar = 0f;
        float duration = 1f;
        while(timeSoFar < duration){
            ball.position = Vector2.Lerp(ball.position, new Vector2(0,0), timeSoFar/duration);
            timeSoFar += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        ball.position = new Vector2(0,0);
        yield return null;
    }

}