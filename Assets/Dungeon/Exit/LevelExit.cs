using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class LevelExit : MonoBehaviour {
    public LayerMask playerLayer;

    void Start() {
        if (!GetComponent<Collider>().isTrigger)
            GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider coll) {

        if (((1 << coll.gameObject.layer) & playerLayer) != 0) {
            GameEvents.NextLevel();
        } else {
            Destroy(coll.gameObject);
        }
    }

}
