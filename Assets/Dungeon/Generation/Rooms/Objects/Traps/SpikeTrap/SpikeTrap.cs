using UnityEngine;
using System.Collections;
using System;

public class SpikeTrap : Trap {

    Animator anim;
    public float damage = 5;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    public override void TriggerTrap() {
        anim.SetTrigger("TriggerTrap");
    }

    public void DealDamage() {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, transform.localScale / 1.2f, Vector3.up);

        AudioSource.PlayClipAtPoint(triggerSound, transform.position);

        foreach (RaycastHit hit in hits) {
            if (((1 << hit.collider.gameObject.layer) & triggeredBy) != 0) {
                hit.collider.SendMessageUpwards("ApplyDamage", damage);
               // Camera.main.GetComponent<CameraMovement>().ShakeCamera(0.1f, 0.1f);
            }
        }
    }
}
