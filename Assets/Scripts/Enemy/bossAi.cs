using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossAi : MonoBehaviour
{
    //0 = nothing, 1 = starting animation, 2 = shoot squares, 3 = shoot squares and move, 4 respawning squares
    //shot squares can be destroyed adn if they are leaves slot open 
    int phase = 0;
    int attackSquare = 0;
    public GameManager gm;
    public int hp = 3;
    //holds all squares used for boss room
    public List<GameObject> bossList = new List<GameObject>();
    bool firstGo = false;
    float holder;
    public List<int> boxNumbers = new List<int>();
    public List<int> numbersHolder = new List<int>();

    public List<int> toRemove = new List<int>();

    public AudioClip[] clips = new AudioClip[3]; //0: attack start, 1: take damage, 2: death, (3: laugh when injures player: this is "audio" in boxBullet)

    double waitTime = 2;
    void Start()
    {
        if (!gm)
            gm = GameObject.FindObjectOfType<GameManager>();
        phase = 1;

        //finds all squares in game space and adds them to list
        //Hey this is spencer Imma just gonna do this in the prefab so I can do some collision ignore in a different place
        //foreach (GameObject bossSqaure in GameObject.FindGameObjectsWithTag("bossSquare"))
        //{
            //bossList.Add(bossSqaure);
        //}
        for (int i = 0; i < bossList.Count; i++)
            boxNumbers.Add(i);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gm.isActive){
            if (holder > 2 && firstGo == false)
            {
                Phase1();
                firstGo = true;
            }
            else
                holder += Time.deltaTime;


            if (phase == 1 && bossList[attackSquare].GetComponent<boxBullet>().goNextShot())
            {
                Phase1();
            }
            else if (phase == 2 && firstGo == true)
            {
                if (checkShots())
                    if (numbersHolder.Count == 0)
                        Phase2();
            }
        }
    }

    public void goPhase2()
    {
        phase = 2;
    }

    bool checkShots()
    {
        foreach (int count in toRemove)
        {
            numbersHolder.Remove(count);
        }
        toRemove = new List<int>();
        foreach (int count in numbersHolder)
        {
            if (bossList[count].GetComponent<boxBullet>().goNextShot())
            {
                boxNumbers.Add(count);
                toRemove.Add(count);
            }
            else
                return false;
        }
        return true;
    }

    public int getHP()
    {
        return hp;
    }

    void Phase2()
    {
        AudioSource.PlayClipAtPoint(clips[0], transform.position);
        int holder;
        int shooter;
        for (int i = 0; i < 4 - hp + 1; i++)
        {
            holder = (Random.Range(0, boxNumbers.Count - 1));
            shooter = boxNumbers[holder];

            numbersHolder.Add(shooter);
            boxNumbers.Remove(shooter);

            bossList[shooter].GetComponent<boxBullet>().Shoot();
        }
    }

    void Phase1()
    {
        AudioSource.PlayClipAtPoint(clips[0], transform.position);
        //get random sqaure
        attackSquare = (Random.Range(0, bossList.Count - 1));

        //   bossList[attackSquare].GetComponent<SpriteRenderer>().color = Color.blue;

        bossList[attackSquare].GetComponent<boxBullet>().Shoot();
    }

    public void timeDown()
    {
        AudioSource.PlayClipAtPoint(clips[1], transform.position);
        waitTime -= .2;
    }

    public void Die(){
        AudioSource.PlayClipAtPoint(clips[2], transform.position);
        StartCoroutine(FadinOut());
    }

     IEnumerator FadinOut() {
        phase = -1;

        foreach (GameObject bossSqaure in bossList)
        {
            //if (bossSqaure.GetComponent<boxBullet>().destroyable == true)
            //{
            bossSqaure.GetComponent<boxBullet>().Respawn();
            bossSqaure.GetComponent<boxBullet>().damage = 0;
            //}
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Victory");

    }

}