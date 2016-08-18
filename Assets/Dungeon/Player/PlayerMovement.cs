using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5f;

    private Animator anim;



    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Update() {

        if (GetComponent<Health>().alive) {
            Vector3 dxy = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0.0f, Input.GetAxisRaw("Vertical") * speed);

            transform.Translate(Vector3.ClampMagnitude(dxy, speed) * Time.deltaTime);

            anim.SetFloat("speed", dxy.magnitude);
        }

    }
}
