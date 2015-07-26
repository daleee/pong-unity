using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public int scoreToWin;
    public int fasterDefaultFontSize;

    private Text fasterText;
    private Text startPromptText;
    private Text winnerText;
    private Text pauseText;
    private bool isGameRunning;
    private bool isPaused;

    GameObject paddle1;
    GameObject paddle2;
    GameObject ball;

    // Use this for initialization
    void Start () {
        GameObject top = GameObject.Find ("Top");
        GameObject bottom = GameObject.Find ("Bottom");
        top.GetComponent<Renderer> ().material.color = Color.black;
        bottom.GetComponent<Renderer> ().material.color = Color.black;

        fasterText = GameObject.Find("Faster! Text").GetComponent<Text>();
        fasterText.color = Color.clear;
        fasterText.fontSize = fasterDefaultFontSize;

        startPromptText = GameObject.Find("Start Prompt").GetComponent<Text>();

        winnerText = GameObject.Find("Winner").GetComponent<Text>();
        winnerText.enabled = false;

        pauseText = GameObject.Find("Pause").GetComponent<Text>();
        pauseText.enabled = false;

        isGameRunning = false;
        isPaused = false;

        paddle1 = GameObject.Find("Player 1");
        paddle2 = GameObject.Find("Player 2");

        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update () {
        if (isGameRunning == false && Input.GetKey(KeyCode.Space)) {
            Debug.Log("Game ON!");
            StartGame();
        }
        if (isGameRunning == true && Input.GetKeyDown(KeyCode.P)) {
            if (isPaused) {
                UnpauseGame();
                isPaused = false;
            }
            else {
                PauseGame();
                isPaused = true;
            }
        }
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void StartGame () {
        // make sure winner text is gone from last game
        if (winnerText.enabled == true) {
            winnerText.enabled = false;
        }
        // enable paddle inputs
        paddle1.SendMessage("EnablePlayer");
        paddle2.SendMessage("EnablePlayer");
        // spawn ball
        ball.SendMessage("EnableBall");
        // disable start prmpt
        startPromptText.enabled = false;
        // TODO: count down form 3?
        isGameRunning = true;
    }

    void PauseGame () {
        Debug.Log("Game paused.");
        pauseText.enabled = true;
        paddle1.SendMessage("DisablePlayer");
        paddle2.SendMessage("DisablePlayer");
        ball.SendMessage("DisableBallPause");
    }

    void UnpauseGame () {
        Debug.Log("Game unpaused.");
        pauseText.enabled = false;
        paddle1.SendMessage("EnablePlayerPause");
        paddle2.SendMessage("EnablePlayerPause");
        ball.SendMessage("EnableBall");
    }

    void EndGame (PlayerInfo info) {
        // disable game
        isGameRunning = false;
        // disable paddle inputs
        paddle1.SendMessage("DisablePlayer");
        paddle2.SendMessage("DisablePlayer");
        // stop ball from moving
        ball.SendMessage("DisableBall");
        // display winner name
        winnerText.enabled = true;
        winnerText.text = info.name + " WINS!";

        startPromptText.enabled = true;
    }

    void NotifyOnScore (PlayerInfo info) {
        fasterText.fontSize = fasterDefaultFontSize;
        if (info.score >= scoreToWin) {
            // end game
            EndGame(info);
        }
    }

    void Faster () {
        fasterText.fontSize += 6;
        StartCoroutine(FadeFasterText());
    }

    IEnumerator FadeFasterText () {
        float duration = 1.0f;
        float currentTime = 0f;

        while(currentTime < duration) {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            fasterText.color = new Color(1f, 0f, 0f, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
