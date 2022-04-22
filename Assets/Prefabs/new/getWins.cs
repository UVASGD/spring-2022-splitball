using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class getWins : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI holder;

    // Update is called once per frame
    void Start()
    {
        holder.text = (PlayerData.player1wins + " : " + PlayerData.player2wins);
        //        TMPro.TextMeshProUGUI hold = gameObject.GetComponent<TextMeshProUGUI>();
    }
}
