using UnityEngine;
using System.Collections;

public abstract class Health : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    public bool alive { get { return currentHealth > 0; } }

    public virtual void ApplyDamage(float damage) {
        if (alive)
            currentHealth -= damage;
        if (!alive) {
            Kill();
            currentHealth = 0;
        }
    }

    public abstract void Kill();



}
