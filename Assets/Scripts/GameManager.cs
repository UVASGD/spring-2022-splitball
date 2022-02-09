using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //gameObjects
    public GameObject ball;
    public GameObject player;
    public GameObject player2;

    public bool isActive = false;

    // UI Elements
    public Button startButton;
    public TextMeshProUGUI levelCoverText;
    public Button exitButton;
    public Button controlButton;
    public Button aboutButton;
    public TextMeshProUGUI controlText;
    public TextMeshProUGUI aboutText;
    public Button backButton;

    public Slider healthBar;
    public Slider dashBar;

    // Start is called before the first frame update
    void Start()
    {
        try {
            startButton.gameObject.GetComponent<Button>();
            startButton.onClick.AddListener(StartGame);

            exitButton.gameObject.GetComponent<Button>();
            exitButton.onClick.AddListener(ExitGame);

            controlButton.gameObject.GetComponent<Button>();
            controlButton.onClick.AddListener(ShowControl);

            aboutButton.gameObject.GetComponent<Button>();
            aboutButton.onClick.AddListener(ShowAbout);

            backButton.gameObject.GetComponent<Button>();
            backButton.onClick.AddListener(back);

            healthBar.gameObject.GetComponent<Slider>();
            dashBar.gameObject.GetComponent<Slider>();
        }
        catch {

        }
    }

    // Control
    void ShowControl() {
        levelCoverText.gameObject.SetActive(false);
        controlText.gameObject.SetActive(true);
        controlButton.gameObject.SetActive(false);
        aboutButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
    }

    // About
    void ShowAbout() {
        levelCoverText.gameObject.SetActive(false);
        aboutText.gameObject.SetActive(true);
        controlButton.gameObject.SetActive(false);
        aboutButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
    }

    // BAck
    void back() {
        levelCoverText.gameObject.SetActive(true);
        controlText.gameObject.SetActive(false);
        aboutText.gameObject.SetActive(false);
        controlButton.gameObject.SetActive(true);
        aboutButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //should be constant
        if(Input.GetKey("escape")){
            Application.Quit();
        }
        if(Input.GetKeyUp("space"))
        {
            //make multipler a variable that can be powered up or strengthens over time
            Vector2 dir = player.GetComponent<Rigidbody2D>().velocity;
            player.GetComponent<Rigidbody2D>().velocity = player2.GetComponent<Rigidbody2D>().velocity *2;
            player2.GetComponent<Rigidbody2D>().velocity = dir *2;
        }
    }

    // Start the game
    public void StartGame() {
        isActive = true;
        try {
            healthBar.gameObject.SetActive(true);
            dashBar.gameObject.SetActive(true);
        }
        catch {}
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartMenu") {
		    SceneManager.LoadScene("Level0");
        }
        if (scene.name == "Victory" || scene.name == "Defeat") {
		    SceneManager.LoadScene("StartMenu");
        }
        try {
            startButton.gameObject.SetActive(false);
            levelCoverText.gameObject.SetActive(false);
        }
        catch {}
    }

    public void ResetGame(){
        isActive = false;
        Scene scene = SceneManager.GetActiveScene();
        
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void UpdateHealth(float health) {
        if (health <= 0){
            healthBar.value = 0f;
            return;
        }
        healthBar.value = health;
    }

    public void UpdateStamina(float stamina) {
        if (stamina <= 0) {
            dashBar.value = 0f;
            return;
        }
        dashBar.value = stamina;
    }

}
