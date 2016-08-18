using UnityEngine;
using System.Collections;

public class DestructableHealth : Health {

    public Transform[] bodyParts;
    public Transform[] destroyThese;


    void Update() {
        if (GetComponent<Animator>()) {
            if (alive)
                GetComponent<Animator>().SetFloat("healthPercent", currentHealth / maxHealth);

        }
    }


    public override void Kill() {

        if (transform.root.GetComponent<GameDungeonRoom>().killThese.Contains(gameObject))
            transform.root.GetComponent<GameDungeonRoom>().killThese.Remove(gameObject);

        if (GetComponent<Animator>())
            Destroy(GetComponent<Animator>());
        gameObject.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("Default");

        foreach (Transform dest in destroyThese) {
            Destroy(dest.gameObject);
        }

        foreach (Transform child in bodyParts) {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
            child.gameObject.AddComponent<Rigidbody>().mass = 1;
            child.parent = null;
        }


    }
}
