using UnityEngine;
using System.Collections;

public class ScoreYardController : MonoBehaviour {

    public GameObject player;

    void OnTriggerEnter(Collider other) {
        player.SendMessage ("IncreaseScore");
        other.SendMessage ("Reset");
    }
}
