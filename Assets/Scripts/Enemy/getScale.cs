using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getScale : MonoBehaviour
{
    //enemy to use range of
    public Transform enemyBase;

    void Start()
    {

        float hold = 0;
        //gets value from enemy it is assigned too and places in vector
         if(enemyBase.GetComponent<enemyAiPath>() != null)
         {
            hold = enemyBase.GetComponent<enemyAiPath>().giveScale();
         }
        else if (enemyBase.GetComponent<enemyAiStill>() != null)
        {
            hold = enemyBase.GetComponent<enemyAiStill>().giveScale();
        }
        else
            hold = enemyBase.GetComponent<enemyAiPatrol>().giveScale();
        

        Vector3 scaling = new Vector3(hold, hold, hold);
        //gives vector to scale object
        gameObject.transform.localScale = scaling;
    }
}
