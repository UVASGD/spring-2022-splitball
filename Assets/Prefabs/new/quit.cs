using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class quit : MonoBehaviour
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
        { }
    }

    void reset()
    {
        Application.Quit();
    }
}
