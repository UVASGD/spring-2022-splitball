using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    public Button restartButton;

    void Start()
    {
        try
        {
            restartButton.gameObject.GetComponent<Button>();
            restartButton.onClick.AddListener(reset);
        }
        catch
        {        }
    }

    void reset()
    {
        PlayerData.player1score = 0;
        PlayerData.player2score = 0;

        int levelGen = UnityEngine.Random.Range(3, 6);
        SceneManager.LoadScene(levelGen);

    }
}
