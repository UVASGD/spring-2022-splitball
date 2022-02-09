using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LogicActivator[] activators = new LogicActivator[1]; //Attach buttons/switches here
    public bool[] inverted; //Should be the same sized list as activators.  This tells unity whether a button should be on or not
    Vector2 s;
    public float doorSpeed = 15;
    public bool onlyOneNeeded = false;
    
    //destroy on close (for pathfinding reasons) set to false by default
    public bool destroyOnClose = false;
    float timeHolder = 0.0f;

    public AudioClip[] clips = new AudioClip[2]; //0: door open, 1: door close
    private bool lastSoundPlayed; //F: last played was close, T: last played was open

    private void Start()
    {
        s = transform.localScale;
        lastSoundPlayed = false;
        //inverted = new bool[activators.Length];
    }

    private void FixedUpdate()
    {
        //If you kinda forgot to put in the activator, i gotchu covered bro
        if (activators.Length == 0 || !activators[0])
        {
            Debug.LogWarning(gameObject + " does not have any logic activators plz fix");
            return;
        }

        //If you only need one button to activate, then use your big boi voice and tell unity
        bool okayAreWeGucci = true;
        if (onlyOneNeeded)
            okayAreWeGucci = false;

        //Okay gamers time to go through every single one of these buttons to see if we are big brain enough to open a door
        for (int i = 0; i < activators.Length; i++)
        {
            if (!onlyOneNeeded)
            {
                //If the button is on and inverted or if the button is off and not inverted
                if ((activators[i].on && inverted[i]) || (!activators[i].on && !inverted[i]))
                {
                    okayAreWeGucci = false;
                    break;
                }
            }
            else
            {
                //If the button is on and not inverted or if the button is off and inverted
                if ((activators[i].on && !inverted[i]) || (!activators[i].on && inverted[i]))
                {
                    okayAreWeGucci = true;
                    break;
                }
            }
        }


        //
        //
        //Below here is what you would want to modify if you wanted to add something other than doors
        //
        //


        if (okayAreWeGucci)
        {
            if(!lastSoundPlayed){ //if the last sound played was the "closed" sound
                AudioSource.PlayClipAtPoint(clips[0], transform.position); //go open
                lastSoundPlayed = true; //the last sound played was the "open" sound
            }
            //We're in
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(s.x, 0), Time.fixedDeltaTime * doorSpeed);
            timeHolder += Time.deltaTime;
            
            //destroy on close (for pathfinding)
            if(destroyOnClose)
            {
            Destroy(gameObject);
            AstarPath.active.Scan(); 
            }
        }
        else
        {
            if(lastSoundPlayed){ //if the last sound played was the "open" sound
                AudioSource.PlayClipAtPoint(clips[1], transform.position); //go close
                lastSoundPlayed = false; //the last sound played was the "close" sound
            }

            //We're in't
            transform.localScale = Vector2.Lerp(transform.localScale, s, Time.fixedDeltaTime * doorSpeed);
        }
    }
}
