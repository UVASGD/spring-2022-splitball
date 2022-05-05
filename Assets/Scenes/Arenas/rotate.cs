using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public GameObject rotateAround;
    public GameObject[] respawns;
    public GameManager gm;

    // Update is called once per frame
    void Update()
    {
        if (gm.isActive)
        {
            transform.RotateAround(rotateAround.transform.position, Vector3.forward, 20 * Time.deltaTime);

            respawns = GameObject.FindGameObjectsWithTag("Boost");
            if (respawns != null)
                if (respawns.Length > 0)
                {
                    respawns[0].transform.parent = transform;
                }
            respawns = null;
        }
    }
}
