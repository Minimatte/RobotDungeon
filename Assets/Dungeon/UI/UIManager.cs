using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text scoreTextObject;
    private static Text scoreText;
    public Image healthForeground;
    public Button respawnButtonObject;
    private static Button respawnButton;
    void Awake() {
        scoreText = scoreTextObject;
        scoreText.text = "Score: " + GameEvents.score;
        respawnButton = respawnButtonObject;
    }

    void Update() {
        PlayerHealth hp = GameEvents.playerInstance.GetComponent<PlayerHealth>();
        RectTransform scale = healthForeground.rectTransform;
        scale.localScale = new Vector3((hp.currentHealth / hp.maxHealth), 1f, 1f);
    }

    public static void SetScore() {
        scoreText.text = "Score: " + GameEvents.score;
        scoreText.gameObject.GetComponent<Animator>().SetTrigger("TriggerAnimation");
        //Play cool animations as well!
    }

    public static void ShowRespawnButton() {
        respawnButton.gameObject.SetActive(true);
        respawnButton.GetComponent<Animator>().enabled = true;
    }

}
