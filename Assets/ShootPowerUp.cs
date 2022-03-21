using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPowerUp : MonoBehaviour
{

    public GameObject bullet;

    public void Fire(int toAim)
    {
        if (toAim == 2)
        {
            Transform target = GameObject.Find("Player2").GetComponent<Transform>();
            Transform spawnPoint = GameObject.Find("Player").GetComponent<Transform>();

            Vector2 direction = target.position - spawnPoint.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //randomly add or subtract a number between max or min to fire angle
                angle += Random.Range(-3f, 3f);
            //rotate by angle

            //fire bullet from spawnPoint

            GameObject shooter = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
             //   Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            

            shooter.GetComponent<bulletFirePowerup>().Friendly=1;

            shooter.GetComponent<Transform>().rotation = Quaternion.Euler(Vector3.forward * (angle - 90));


        }

        else if (toAim == 1)
        {

            Transform target = GameObject.Find("Player").GetComponent<Transform>();
            Transform spawnPoint = GameObject.Find("Player2").GetComponent<Transform>();

            Vector2 direction = target.position - spawnPoint.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //randomly add or subtract a number between max or min to fire angle
            angle += Random.Range(-3f, 3f);
            //rotate by angle
 
            //fire bullet from spawnPoint
            GameObject shooter = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);


            shooter.GetComponent<bulletFirePowerup>().Friendly = 2;

            shooter.GetComponent<Transform>().rotation = Quaternion.Euler(Vector3.forward * (angle - 90));
        }
    }
}
