using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyAI : MonoBehaviour {

    internal Transform target;

    public Transform upperBody;

    public float movementSpeed = 1;
    public float rotationSpeed = 1;

    public float distanceAway = 5;
    public float shootRange = 2;
    public float loseAggroRange = 10;

    internal bool canShoot {
        get {
            if (target && target.root.gameObject.GetComponent<Health>().alive)
                return Vector3.Distance(transform.position, target.transform.position) <= distanceAway + shootRange;
            else
                return false;
        }
    }
    internal bool inDistance {
        get {
            if (target)
                return Vector3.Distance(transform.position, target.transform.position) <= distanceAway;
            else
                return false;
        }
    }


    private Quaternion lookRotation;
    private Vector3 direction;
    private EnemyHealth health;
    private Animator anim;

    void Awake() {
        health = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (target) {
            upperBody.LookAt(target);
            if (inDistance) {
                anim.SetFloat("speed", 0);
            } else {
                transform.position = Vector3.Lerp(transform.position, target.position, movementSpeed * Time.deltaTime);
                anim.SetFloat("speed", 1);
                direction = (target.position - transform.position).normalized;
                lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }


            // CheckForDeaggro();
        } else {
            anim.SetFloat("speed", 0);
        }
    }

    private void CheckForDeaggro() {
        if (Vector3.Distance(transform.position, target.position) > loseAggroRange)
            target = null;
    }

    void OnTriggerEnter(Collider other) {
        if (target == null && health.alive) {
            if (other.gameObject.tag == "Player" && other.gameObject.layer == LayerMask.NameToLayer("Player")) {
                target = other.transform;
            }
        }
    }
}
