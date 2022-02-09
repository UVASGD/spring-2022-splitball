using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    public float force;
    public float lastBoost;
    public float boostTimer;
    public AudioClip clip;
   
    // Start is called before the first frame update
    void Start()
    {
        lastBoost = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lastBoost+=Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if a an interactable object hits it
        if(col.gameObject.GetComponent<Interactable>()){
            AudioSource.PlayClipAtPoint(clip, transform.position); //go wooooosh
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //if a an interactable object hits it
        if(col.gameObject.GetComponent<Interactable>()){
            BoostIt(col.gameObject.GetComponent<Interactable>());
        }

    }

    private void BoostIt(Interactable target){
        if(lastBoost>=boostTimer){
            target.BoostIt(transform.eulerAngles.z, force);
            lastBoost = 0f;
        }
    }

}
