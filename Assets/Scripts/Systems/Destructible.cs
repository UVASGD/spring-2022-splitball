using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    public float maxHealth;
    public float hitPoints;

    //TakeDamage() with no parameters does fatal damage
    public virtual void TakeDamage()
    {
        Die();
    }

    public virtual void TakeDamage(float amount)
    {
        this.hitPoints -= amount;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    public virtual void Die() {
		Destroy(gameObject);
    }

    //Heal() with no parameters heals to full
    public virtual void Heal()
    {
        hitPoints = this.maxHealth;
    }

    public virtual void Heal(float amount)
    {
        hitPoints += amount;

        //limit overheal
        if (hitPoints >= maxHealth)
        {
            hitPoints = maxHealth;
        }
    }

    public virtual void UpdateHealth() {

    }

}