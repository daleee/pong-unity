using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, yMin, yMax;
}

public class PlayerInfo {
    public int score;
    public string name;
}

public class PlayerController : MonoBehaviour {

    public int score;
    public float speed;

    public PlayerInfo info;

    public Boundary playBoundary;

    public GameObject gameController;

    public GameObject scoreTextObject;
    private Text myScoreText;

    private Rigidbody myBody;

    private bool isGameRunning;
    // Use this for initialization
    void Start () {
        // set initial score
        score = 0;
        // set colour of paddle
        gameObject.GetComponent<Renderer> ().material.color = Color.white;
        // set default speed
        speed = 10f;
        // get reference to rigidbody
        myBody = GetComponent<Rigidbody> ();
        // get reference to score text
        myScoreText = scoreTextObject.GetComponent<Text>();

        DisablePlayer();

        info = new PlayerInfo();
        info.score = 0;
        info.name = this.name;
    }

    void EnablePlayer () {
        // reset score
        info.score = score = 0;
        myScoreText.text = score.ToString();
        isGameRunning = true;
    }

    void EnablePlayerPause () {
        isGameRunning = true;
    }

    void DisablePlayer () {
        isGameRunning = false;
    }

    void IncreaseScore () {
        info.score = ++score;
        myScoreText.text = score.ToString();
        gameController.SendMessage("NotifyOnScore", info);
    }

    void FixedUpdate () {
        if (isGameRunning) {
            float input_y = Input.GetAxisRaw ("Vertical");
            Vector3 newPos = new Vector3 (
                transform.position.x,
                Mathf.Clamp(myBody.position.y + input_y * Time.deltaTime * speed, playBoundary.yMin, playBoundary.yMax),
                0f
                );
            myBody.MovePosition(newPos);
        }
    }
}
