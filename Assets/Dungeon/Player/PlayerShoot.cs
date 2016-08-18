using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public int shootTriggerButton = 0;

    public virtual bool shooting { get { return Input.GetMouseButton(shootTriggerButton); } }

    bool canShoot { get { return cooldown <= 0 && GetComponent<Health>().alive; } }

    float cooldown = 0;
    public float maxcooldown = 0.05f;

    public GameObject bulletObject;

    public GameObject playerMesh;

    public GameObject[] weapons;
    void Update() {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if (cooldown < 0)
            cooldown = 0;

    }

    void FixedUpdate() {
        if (shooting && canShoot) {
            cooldown = maxcooldown;
            Shoot();
        }
    }

    public virtual void Shoot() {
        foreach (GameObject weapon in weapons) {
            GameObject go = (GameObject)Instantiate(bulletObject, weapon.transform.position + (weapon.transform.forward), weapon.transform.rotation);
            go.tag = gameObject.tag;

            if (weapon.GetComponent<Animator>()) {
                weapon.GetComponent<Animator>().SetTrigger("Shoot");
            }

        }
    }
}
