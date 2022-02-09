using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArrow : MonoBehaviour
{
    public Transform player;
    public Transform ball;
    public Transform arrow;
    public Vector2 aim;
    public float crosshairDistance = 4f;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        ball = GameObject.Find("Ball").GetComponent<Transform>();
        arrow = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //position arrow
        aim = ball.position - player.position;
        arrow.position = (Vector2) player.position + (aim.normalized * crosshairDistance);
        //hide if ball is too close
        if(aim.magnitude <= 6f){
            //hide
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else{
            //show
            GetComponent<SpriteRenderer>().enabled = true;
        }
        //angle arrow
        angle = Vector2.Angle(new Vector2(1,0), aim);
        if(aim.y < 0){
            angle = -angle;
        }
        arrow.eulerAngles = new Vector3(arrow.eulerAngles.x, arrow.eulerAngles.y, angle);
    }
}
