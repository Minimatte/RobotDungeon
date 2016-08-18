using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BulletScript : MonoBehaviour {

    public GameObject explosionEffect;
    public float explosionEffectLifeTime = 2;

    public float bulletSpeed = 50;
    public float lifeTime = 1;

    public string sendMessageName = "ApplyDamage";
    public float damage = 10f;

    private bool isColliding;

    public AudioClip spawnSound;

    void Start() {
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        Destroy(gameObject, lifeTime);
        AudioSource.PlayClipAtPoint(spawnSound, transform.position);
    }

    void Update() {
        isColliding = false;
    }

    void OnCollisionEnter(Collision coll) {
        if (isColliding)
            return;
        isColliding = true;

        GetComponent<Collider>().enabled = false;
        if (explosionEffect)
            Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), explosionEffectLifeTime);

        if (coll.gameObject.tag == "Destructable") {
            if (coll.gameObject.GetComponent<Health>())
                coll.gameObject.SendMessage(sendMessageName, damage);
            else if (coll.transform.parent != null)
                coll.gameObject.SendMessageUpwards(sendMessageName, damage);
            Destroy(gameObject);
        }


        if (coll.transform.root.GetComponent<Health>())
            if (coll.transform.root.gameObject.GetComponent<Health>().alive)
                if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy") {
                    if (coll.gameObject.tag != gameObject.tag) {
                        coll.gameObject.SendMessageUpwards(sendMessageName, damage);
                        Destroy(gameObject);
                    } else { // if its the same tag as the owner
                        Destroy(gameObject);
                    }
                } else { // if we hit ground or walls
                    Camera.main.GetComponent<CameraMovement>().ShakeCamera(0.05f, 0.05f * (GetComponent<Rigidbody>().velocity.magnitude / bulletSpeed));
                    GetComponent<Rigidbody>().isKinematic = true;

                }
    }

}
