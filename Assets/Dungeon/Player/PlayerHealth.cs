using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : Health {

    public override void ApplyDamage(float damage) {
        Camera.main.GetComponent<CameraMovement>().ShakeCamera(0.1f, 0.3f);
        base.ApplyDamage(damage);
    }

    public Transform[] bodyParts;

    public override void Kill() {
        //GameEvents.RespawnPlayer(0, 0);
        gameObject.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("Default");
        if (GetComponent<Animator>())
            GetComponent<Animator>().SetFloat("speed", 0); // reset player walking
        if (GetComponent<CapsuleCollider>())
            GetComponent<CapsuleCollider>().radius = 0.1f;
        foreach (Transform child in bodyParts) {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
            child.gameObject.AddComponent<BoxCollider>();
            child.gameObject.AddComponent<Rigidbody>().mass = 1;
            child.parent = null;
        }

        UIManager.ShowRespawnButton();
        // Destroy(gameObject);
    }
}
