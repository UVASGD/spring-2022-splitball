using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicActivator : MonoBehaviour
{
    //All button/switch logic stored here
    //Click and drag the button gameobject out of the prefab folder to make more of these bad bois
    public Animator animator;

    public bool on = false;
    public int whoInteracts; //0 = ball only, 1 = player only, 2 = both/any
    public int type = 0; //0 = red button (remains active even when stepped off), 1 = blue button (deactivates if stepped off), 2 = switch (step on to toggle on/off), 3 = sticky
    Color c;

    public AudioClip clip; //the clack of a press

    private void Start()
    {

        //c = GetComponent<SpriteRenderer>().color;
        animator = GetComponent<Animator>();
        animator.SetBool("On", on);

        c = GetComponent<SpriteRenderer>().color;
        //setting colors to only one type based on convienece 
        //(why?) red is just sticky but hard, so not its player only and sticky 
        //switch and red can be both cause why not
        if(type == 0 || type == 2){
            whoInteracts = 2;
        }
        else if(type == 1){
            whoInteracts = 1;
        }
        else if(type == 3){
            whoInteracts = 0;
        }

    }

    //Called whenever something enters the button/switch
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the ball has collided with the button
        if (collision.gameObject.GetComponent<LogicInteractable>() && (whoInteracts == 2 || collision.gameObject.GetComponent<LogicInteractable>().interactableType == whoInteracts))
        {
            AudioSource.PlayClipAtPoint(clip, transform.position); //go clack
            //If this is a switch
            if (type == 2)
            {
                //Toggle activate state
                on = !on;
                animator.SetBool("On", on);

                if (on)
                {
                    /*Color col = c;
                    col.r += 30;
                    col.b += 30;
                    col.g += 30;
                    GetComponent<SpriteRenderer>().color = col;*/
                }
                else
                {
                    //GetComponent<SpriteRenderer>().color = c;
                }
            }
            else
            {
                on = true;
                animator.SetBool("On", on);
                /*Color col = c;
                col.r += 10;
                col.b += 10;
                col.g += 10;
                GetComponent<SpriteRenderer>().color = col;*/
            }
        }
    }
    //Called whenever something leaves the button/switch
    private void OnTriggerExit2D(Collider2D collision)
    {
        //If not a red button
        if (type != 0 && collision.gameObject.GetComponent<LogicInteractable>())
        {
            //If blue button or sticky && correct type of object
            if ((type == 1 || type == 3) && collision.gameObject.GetComponent<LogicInteractable>().interactableType == whoInteracts)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position); // go clack
                on = false;
                animator.SetBool("On", on);
                //GetComponent<SpriteRenderer>().color = c;
            }
        }
    }
}
