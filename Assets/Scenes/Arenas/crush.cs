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
    float toWait = 2.5f;
    float count = 0;
    int whichOne;

    bool goLeft;
    bool goRight;
    bool goUp;
    bool goDown;

    public GameManager gm;
    float distance = 0;
    bool returner = false;

    // Update is called once per frame
    void Update()
    {
        if (gm.isActive)
        {
            count += Time.deltaTime;
            if (count >= toWait & okToGo == true)
            {
                okToGo = false;
                whichOne = Random.Range(0, 3);
                if (whichOne == 0)
                    goLeft = true;
                if (whichOne == 1)
                    goDown = true;
                if (whichOne == 2)
                    goUp = true;
                if (whichOne == 3)
                    goRight = true;
            }



            if (goDown)
            {
                distance += Time.deltaTime;
                bottom.transform.position = (new Vector3(bottom.transform.position.x, bottom.transform.position.y + (1 * Time.deltaTime), 0));

                if (distance > 1.0f)
                    returner = true;

            }
            if (returner & goDown)
            {
                goDown = false;
                returner = false;
                distance = 0;
                okToGo = true;
                count = 0;
            }




            if (goUp)
            {
                distance += Time.deltaTime;
                top.transform.position = (new Vector3(top.transform.position.x, top.transform.position.y - (1 * Time.deltaTime), 0));

                if (distance > 1.0f)
                    returner = true;
            }
            if (returner & goUp)
            {
                goUp = false;
                returner = false;
                distance = 0;
                okToGo = true;
                count = 0;
            }




            if (goLeft)
            {
                distance += Time.deltaTime;
                left.transform.position = (new Vector3(left.transform.position.x + (1 * Time.deltaTime), left.transform.position.y, 0));

                if (distance > 1.0f)
                    returner = true;
            }
            if (returner & goLeft)
            {
                goLeft = false;
                returner = false;
                distance = 0;
                okToGo = true;
                count = 0;
            }



            if (goRight)
            {
                distance += Time.deltaTime;
                right.transform.position = right.transform.position = (new Vector3(right.transform.position.x - (1 * Time.deltaTime), right.transform.position.y, 0));

                if (distance > 1.0f)
                    returner = true;
            }
            if (returner & goRight)
            {
                goRight = false;
                returner = false;
                distance = 0;
                okToGo = true;
                count = 0;
            }

        }

    }
}
