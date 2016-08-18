using UnityEngine;
using System.Collections;

public abstract class Trap : MonoBehaviour {

    public float cooldown = 2;
    private float currentCooldown = 0;
    private bool canTrigger { get { return currentCooldown <= 0; } }

    public LayerMask triggeredBy;

    public AudioClip triggerSound;

    void Update() {
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;
        else
            currentCooldown = 0;
    }

    void OnTriggerEnter(Collider coll) {
        if (((1 << coll.gameObject.layer) & triggeredBy) != 0) {
            if (canTrigger) {
                TriggerTrap();
                currentCooldown = cooldown;
            }
        }
    }

    abstract public void TriggerTrap();
}
