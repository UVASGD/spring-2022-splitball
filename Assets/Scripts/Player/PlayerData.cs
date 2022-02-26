using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Destructible
{
    //score holder across scene
    public static int player1score =0;
    public static int player2score =0;

void Start(){
    maxHealth = 50;
    hitPoints = 50;
    }
}