using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //gameObjects
   // public GameObject ball;
    public GameObject player;
    public GameObject player2;
    public static bool loserFound;

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

    public Slider switchBarLeft;
    public Slider switchBarRight;
    public Slider dashBar1;
    public Slider dashBar2;

    //Timer for switch
    public float lastSwitch = 0f;
    public float switchCD = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
        loserFound = false;
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
            
            switchBarLeft.gameObject.GetComponent<Slider>();
            switchBarRight.gameObject.GetComponent<Slider>();
            dashBar1.gameObject.GetComponent<Slider>();
            dashBar2.gameObject.GetComponent<Slider>();

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
       if(!isActive){
           if(Input.GetKey("space")){
               StartGame();
           }
       }
        //checks condition to win, should be different for each kind of game
        //TODO allow player to fizzle out and die before switching to next scene, play animation adding to score
        if (isActive)
        {
            lastSwitch += Time.deltaTime;

            checkWin();
            //should be constant
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
            if (Input.GetKeyUp("space") && lastSwitch >= switchCD)
            {
                //make multipler a variable that can be powered up or strengthens over time
                Vector2 dir = player.GetComponent<Rigidbody2D>().velocity;
                player.GetComponent<Rigidbody2D>().velocity = player2.GetComponent<Rigidbody2D>().velocity * 2;
                player2.GetComponent<Rigidbody2D>().velocity = dir * 2;
                lastSwitch = 0f;
            }

            InvokeRepeating("UpdateSwitch", 0f, .5f);
        }
    }
    //update this with rules of game
    public void checkWin()
    {
        player.GetComponent<battleCollison>().OnTriggerEnter2D(player.GetComponent<Collider2D>());
        player2.GetComponent<battleCollison>().OnTriggerEnter2D(player.GetComponent<Collider2D>());
        if(loserFound == true)
        {
            isActive = false;
            resetForScene();
            //TODO make this random
            //ported to player controllers so to let animation play out
           // SceneManager.LoadScene("Bunker");
        }
    }

    //resets static values before switch to next secne, also checks for winner
    public void resetForScene()
    {
     //   Time.timeScale = 0;
        
        loserFound = false;
        
    if (PlayerData.player1score >= 5 && PlayerData.player1score != PlayerData.player2score)
        {
            PlayerData.player2score = 0;
            PlayerData.player1score = 0;
            PlayerData.player1wins += 1;
            SceneManager.LoadScene("Victory");
        }

    if (PlayerData.player2score >= 5 && PlayerData.player1score != PlayerData.player2score)
    {
            PlayerData.player1score = 0;
            PlayerData.player2score = 0;
            PlayerData.player2wins += 1;
            SceneManager.LoadScene("Defeat");
        }
    }


    // Start the game
    public void StartGame() {
        isActive = true;
        try {
            switchBarLeft.gameObject.SetActive(true);
            switchBarRight.gameObject.SetActive(true);
            dashBar1.gameObject.SetActive(true);
            dashBar2.gameObject.SetActive(true);
        }
        catch {}
        Scene scene = SceneManager.GetActiveScene();



        if (scene.name == "StartMenu") {
            //TODO this needs to load random game
            int levelGen = UnityEngine.Random.Range(3, 6);
            SceneManager.LoadScene(levelGen);
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
    /*
    public void UpdateHealth(float health) {
        if (health <= 0){
            healthBar.value = 0f;
            return;
        }
        healthBar.value = health;
    }
    */
    public void UpdateStamina1(float stamina) {
        if (stamina <= 0) {
            dashBar1.value = 0f;
            return;
        }
        dashBar1.value = stamina;
    }

    public void UpdateStamina2(float stamina)
    {
        if (stamina <= 0)
        {
            dashBar2.value = 0f;
            return;
        }
        dashBar2.value = stamina;
    }


    public void UpdateSwitch()
    {
        float stamina = lastSwitch / switchCD;

        if (stamina <= 0)
        {
            switchBarRight.value = 0f;
            switchBarLeft.value = 0f;
            return;
        }
    
        else
        {
            switchBarRight.value = stamina;
            switchBarLeft.value = stamina;
        }
    }
}
