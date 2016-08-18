using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour {

    public LayerMask canPickup;

    void Awake() {
        if (!GetComponent<Collider>().isTrigger)
            GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other) {
        if (((1 << other.gameObject.layer) & canPickup) != 0) {
            TriggerPickup();
            Destroy(gameObject);
        }
    }

    abstract public void TriggerPickup();

}
