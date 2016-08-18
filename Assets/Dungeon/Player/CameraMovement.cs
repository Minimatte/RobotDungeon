using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public GameObject player; //The offset of the camera to centrate the player in the X axis 
    public float offsetX = 0; //The offset of the camera to centrate the player in the Z axis 
    public float offsetY = 0;
    public float offsetZ = 0;

    //The maximum distance permited to the camera to be far from the player, its used to make a smooth movement 
    public float maximumDistance = 2; //The velocity of your player, used to determine que speed of the camera 
    public float playerVelocity = 10;

    private float movementX;
    private float movementY;
    private float movementZ;

    public float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    internal float shakeAmount = 0.1f;
    internal float decreaseFactor = 1.0f;

    // Update is called once per frame 
    void Update() {
        if (player) {
            movementX = ((player.transform.position.x + offsetX - transform.position.x)) / maximumDistance;
            movementY = ((player.transform.position.y + offsetY - transform.position.y)) / maximumDistance;
            movementZ = ((player.transform.position.z + offsetZ - transform.position.z)) / maximumDistance;
            offsetZ = -offsetY / 3;
            transform.position += new Vector3((movementX * playerVelocity * Time.deltaTime), (movementY * playerVelocity * Time.deltaTime), (movementZ * playerVelocity * Time.deltaTime));

            CheckForObject();
        }

        if (shake > 0) {
            transform.localPosition = transform.localPosition + new Vector3(Random.insideUnitSphere.x * shakeAmount, 0, Random.insideUnitSphere.z * shakeAmount);
            shake -= Time.deltaTime * decreaseFactor;

        } else {
            shake = 0;
        }

    }

    public void ShakeCamera(float time, float force) {
        shake = time;
        shakeAmount = force;
    }


    public void CheckForObject() {
        Ray ray = new Ray(transform.position + transform.up, transform.forward);
        RaycastHit[] hits;
        float posDistance = Vector3.Distance(player.transform.position, transform.position) * 0.8f;
        hits = Physics.RaycastAll(ray, posDistance);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.GetComponent<Renderer>()) {

                Transparency t = hit.collider.gameObject.GetComponent<Transparency>();
                if (t == null)
                    t = hit.collider.gameObject.AddComponent<Transparency>();
                t.BeTransparent();
            }
        }
    }
}
