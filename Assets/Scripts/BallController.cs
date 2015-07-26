using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public GameObject gc;
    public float speed, defaultSpeed;

    private AudioSource audio;
    public AudioClip beep;
    public AudioClip boop;

    private Vector3 direction;
    private GameObject gameController;
    private bool isGameRunning;
    private Renderer myRenderer;
    // Use this for initialization
    void Start () {
        myRenderer = gameObject.GetComponent<Renderer> ();
        myRenderer.material.color = Color.red;
        myRenderer.enabled = false;

        direction = new Vector3 (1, 0.25f, 0);
        isGameRunning = false;

        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" || other.tag == "AI") {
            Debug.Log ("Hit paddle");
            // TODO: (for future) use 'real' pong physics
            // float relativeIntersectY = other.collider.bounds.center.y - this.transform.position.y;
            // float normalizedRelativeIntersectionY = relativeIntersectY / (other.collider.bounds.size.y / 2);
            // float bounceAngle = normalizedRelativeIntersectionY * (Mathf.Deg2Rad * 75);
            // direction.x = speed * Mathf.Cos(bounceAngle);
            // direction.y = speed * -Mathf.Cos(bounceAngle);
            direction.x *= -1;
            speed++;
            gc.SendMessage("Faster");
            audio.PlayOneShot(beep);
        }
        else if (other.tag == "World") {
            Debug.Log ("Hit world");
            direction.y *= -1;
            audio.PlayOneShot(boop);
        }
    }

    void Reset () {
        speed = defaultSpeed;
        transform.position = Vector3.zero;
    }

    void EnableBall () {
        isGameRunning = true;
        myRenderer.enabled = true;
    }
    void DisableBall () {
        isGameRunning = false;
        myRenderer.enabled = false;
    }

    void DisableBallPause () {
        isGameRunning = false;
    }

    void FixedUpdate () {
        if (isGameRunning) {
            //transform.Translate (direction * Time.deltaTime);
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
