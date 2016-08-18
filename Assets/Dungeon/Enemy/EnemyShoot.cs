using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyAI))]
public class EnemyShoot : PlayerShoot {

    public override bool shooting {
        get {
            return ai.canShoot;
        }
    }

    EnemyAI ai;

    void Awake() {
        ai = GetComponent<EnemyAI>();
    }

    public override void Shoot() {
        if (ai.target) {
            if (GetComponent<Animator>())
                GetComponent<Animator>().SetTrigger("charge");
        }
    }

    public void ShootProjectile() {
        if (GetComponent<Health>().alive) {
            GameObject go = (GameObject)Instantiate(bulletObject, playerMesh.transform.position + (playerMesh.transform.forward * 1.5f), playerMesh.transform.rotation);
            go.tag = gameObject.tag;
        }
    }

}
