using UnityEngine;
using System.Collections;
using System;

public class ScorePickup : Pickup {

    public int scoreToAdd = 1;
    public override void TriggerPickup() {
        GameEvents.AddScore(scoreToAdd);
    }
}
