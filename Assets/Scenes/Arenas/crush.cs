using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crush : MonoBehaviour
{
    public GameObject bottom;
    public GameObject top;
    public GameObject left;
    public GameObject right;

    bool okToGo = true;
    float toWait = 4;
    float count = 0;
    int whichOne;

    bool goLeft;
    bool goRight;
    bool goUp;
    bool goDown;

    bool returner;

    float distance = 0;
    Vector3 loc;
    float xcord;
    float ycord;

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count >= toWait & okToGo == true)
        {
            okToGo = false;
            whichOne = Random.Range(0, 3);
            if (whichOne == 0)
            { 
                goLeft = true;
                xcord = left.GetComponent<Rigidbody2D>().position.x;
                ycord = left.GetComponent<Rigidbody2D>().position.y;
            }
        if (whichOne == 1)
            {
                goDown = true;
                xcord = bottom.GetComponent<Rigidbody2D>().position.x;
                ycord = bottom.GetComponent<Rigidbody2D>().position.y;
            }
        if (whichOne == 2)
            { goUp = true;
                xcord = top.GetComponent<Rigidbody2D>().position.x;
                                ycord = top.GetComponent<Rigidbody2D>().position.y;
            }
        if (whichOne == 3)
            {
                goRight = true;
                xcord = right.GetComponent<Rigidbody2D>().position.x;
                ycord = right.GetComponent<Rigidbody2D>().position.y;
            }
        }

        if (distance > 2.2f)
            returner = true;








        if (goDown)
            if (!returner)
            {
                distance += Vector3.Distance(loc, bottom.GetComponent<Rigidbody2D>().position);
                loc = bottom.GetComponent<Rigidbody2D>().position;

                bottom.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -10) * .1f);
            }
            else
            {
                {
                    distance += Vector3.Distance(loc, bottom.GetComponent<Rigidbody2D>().position);
                    loc = bottom.GetComponent<Rigidbody2D>().position;

                    bottom.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10) * .1f);
                }
            }

        if(returner & bottom.GetComponent<Rigidbody2D>().transform.position.y >= ycord -.2f  & goDown)
        {
            goDown = false;
            bottom.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            bottom.GetComponent<Rigidbody2D>().transform.position = (new Vector2(xcord, ycord -.2f));
            returner = false;
            distance = 0;
            okToGo = true;
            count = 0;
        }




        if (goUp)
            if (!returner)
            {
                distance += Vector3.Distance(loc, top.GetComponent<Rigidbody2D>().position);
                loc = top.GetComponent<Rigidbody2D>().position;

                top.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10) * .1f);
            }
            else
            {
                {
                    distance += Vector3.Distance(loc, top.GetComponent<Rigidbody2D>().position);
                    loc = top.GetComponent<Rigidbody2D>().position;

                    top.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -10) * .1f);
                }
            }

        if (returner & top.GetComponent<Rigidbody2D>().transform.position.y >= ycord + .2f & goUp)
        {
            goUp = false;
            top.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            top.GetComponent<Rigidbody2D>().transform.position = (new Vector2(xcord, ycord + .2f));
            returner = false;
            distance = 0;
            okToGo = true;
            count = 0;
        }




        if (goLeft)
            if (!returner)
            {
                distance += Vector3.Distance(loc, left.GetComponent<Rigidbody2D>().position);
                loc = left.GetComponent<Rigidbody2D>().position;

                left.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0) * .1f);
            }
            else
            {
                {
                    distance += Vector3.Distance(loc, left.GetComponent<Rigidbody2D>().position);
                    loc = left.GetComponent<Rigidbody2D>().position;

                    left.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10,0 ) * .1f);
                }
            }

        if (returner & left.GetComponent<Rigidbody2D>().transform.position.x >= xcord + .4f & goLeft)
        {
            goLeft = false;
            left.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            left.GetComponent<Rigidbody2D>().transform.position = (new Vector2(xcord+.4f, ycord));
            returner = false;
            distance = 0;
            okToGo = true;
            count = 0;
        }



        if (goRight)
            if (!returner)
            {
                distance += Vector3.Distance(loc, right.GetComponent<Rigidbody2D>().position);
                loc = right.GetComponent<Rigidbody2D>().position;

                right.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0) * .1f);
            }
            else
            {
                {
                    distance += Vector3.Distance(loc, right.GetComponent<Rigidbody2D>().position);
                    loc = right.GetComponent<Rigidbody2D>().position;

                    right.GetComponent<Rigidbody2D>().AddForce(new Vector2(10,0 ) * .1f);
                }
            }

        if (returner & right.GetComponent<Rigidbody2D>().transform.position.x >= xcord - .4f & goRight)
        {
            goRight = false;
            right.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            right.GetComponent<Rigidbody2D>().transform.position = (new Vector2(xcord - .4f, ycord));
            returner = false;
            distance = 0;
            okToGo = true;
            count = 0;
        }



    }
}
