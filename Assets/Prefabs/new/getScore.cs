using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class getScore : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI holder;

    // Update is called once per frame
    void Start()
    {
        holder.text = (PlayerData.player1score + " : " + PlayerData.player2score);
//        TMPro.TextMeshProUGUI hold = gameObject.GetComponent<TextMeshProUGUI>();
    }
}
