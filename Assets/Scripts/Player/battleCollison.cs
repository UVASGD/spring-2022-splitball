﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleCollison : MonoBehaviour
{
    //collider used only for battle mode to see if hit side walls

    public void OnTriggerEnter2D(Collider2D collision)
    {

        

        if (collision.gameObject.tag == "levelTile")
        {

            if (gameObject.name == "Player")
            {
                PlayerData.player2score += 1;
                gameObject.GetComponent<PlayerController>().Die();
                GameManager.loserFound = true;
            }

            if (gameObject.name == "Player2")
            {
                PlayerData.player1score += 1;
                gameObject.GetComponent<Player2Controler>().Die();
                GameManager.loserFound = true;
            }

            //            if game object name = player one
            //              increase player one score. 
            //            die. 
        }
    }
}
