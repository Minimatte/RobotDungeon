using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : Health {


    private Animator anim;

    public AudioClip deathSound;
    public ParticleSystem robotBlood;

    public List<GameObject> Drops = new List<GameObject>();

    void Awake() {
        gameObject.tag = "Destructable";
        anim = GetComponent<Animator>();
    }

    public override void ApplyDamage(float damage) {
        base.ApplyDamage(damage);
        Transform t = GetComponent<EnemyAI>().upperBody;

        Instantiate(robotBlood, (new Vector3(t.position.x, t.position.y, t.position.z)), Quaternion.identity);
    }

    void DropItem() {
        Instantiate(Drops[Random.Range(0, Drops.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    public override void Kill() {

        GetComponent<EnemyAI>().target = null;

        anim.SetTrigger("kill");
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        DropItem();

        foreach (Transform t in transform) {
            t.gameObject.AddComponent<Rigidbody>();
            t.GetComponent<Collider>().enabled = true;

            t.tag = "Untagged";
            t.parent = null;


        }
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(10f, transform.position, 1f);
        gameObject.GetComponent<Rigidbody>().mass = 1f;
        //Destroy(gameObject);
    }
}
