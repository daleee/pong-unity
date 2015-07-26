using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIController : MonoBehaviour {

    public int score;
    public float speed;

    public PlayerInfo info;

    public Boundary playBoundary;

    public GameObject gameController;
    public GameObject ball;

    public GameObject scoreTextObject;
    private Text myScoreText;

    private Rigidbody myBody;

    private bool isGameRunning;

    public float timeUntilDirectionCheck;
    private float currentDirectionCheckTime;
    private float direction;
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

        currentDirectionCheckTime = 0f;
        direction = 0f;
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

    void Update () {
        currentDirectionCheckTime += Time.deltaTime;
        if (currentDirectionCheckTime >= timeUntilDirectionCheck) {
            currentDirectionCheckTime = 0f;
            if (ball.transform.position.y > transform.position.y) {
                direction = 1;
            }
            else {
                direction = -1;
            }
        }
    }

    void FixedUpdate () {
        if (isGameRunning) {

            Vector3 newPos = new Vector3 (
                transform.position.x,
                Mathf.Clamp(myBody.position.y + direction * Time.deltaTime * speed, playBoundary.yMin, playBoundary.yMax),
                0f
                );
            myBody.MovePosition(newPos);
        }
    }
}
