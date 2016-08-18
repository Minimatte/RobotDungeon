using UnityEngine;
using System.Collections;

public class RespawnButton : MonoBehaviour {

	public void RespawnPlayer() {
        GameEvents.RespawnPlayer(0, 0);
        GetComponent<Animator>().enabled = false;
        gameObject.SetActive(false);
    }
}
