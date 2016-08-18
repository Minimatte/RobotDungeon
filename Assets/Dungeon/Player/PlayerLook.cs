using UnityEngine;
using System.Collections;

public class PlayerLook : MonoBehaviour {
    public GameObject bottomBody;
    public LayerMask layer;

    private Quaternion lookRotation;
    private Vector3 direction;
    public float rotationSpeed = 1;

    public Health playerHealth;

    void Update() {
        if (playerHealth.alive) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, int.MaxValue, layer)) {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z), Vector3.up);

                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
                    direction = (new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;
                    lookRotation = Quaternion.LookRotation(direction);
                    bottomBody.transform.rotation = Quaternion.Slerp(bottomBody.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                }
            }
        }
    }
}
