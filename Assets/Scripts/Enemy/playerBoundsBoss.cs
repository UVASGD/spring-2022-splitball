using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBoundsBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().velocity = col.gameObject.GetComponent<Rigidbody2D>().velocity * -1;
        }
    }
}
