using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

    ParticleSystem ps;

    void Awake() {
        ps = GetComponent<ParticleSystem>();    
    }


    void Update() {
        if (!ps.IsAlive())
            Destroy(gameObject);
    }
}
