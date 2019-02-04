using UnityEngine;
using UnityEngine.UI;

public class BallCollision : MonoBehaviour
{
    public static bool firstclick;
    public static bool powerfulPaddleCollision;
    public static int collisionCounter;

    private AudioSource audio;

    private Rigidbody rb;

    [SerializeField]
    private Text scoreText;

    private static double score;

    [SerializeField]
    [Range(0, 1000)]
    private float added_force;

    private void Awake()
    {
        powerfulPaddleCollision = false;
        score = 0;
        audio = GetComponent<AudioSource>();
        collisionCounter = 0;
        firstclick = true;
        rb = GetComponent<Rigidbody>();
    }

    

    private void Update() {
        if (firstclick && Input.GetButtonDown("Fire1"))
        {
            //TODO Add randomness of force from paddle
            int randomDegrees = Random.RandomRange(-45, 45);
            Mathf.Sin()
            transform.parent = null;
            firstclick = false;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(added_force, added_force / 2, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (TriggerUpgrade.forceUpgrade == false)
        {
            if (collision.transform.tag == "racket")
            {
                collisionCounter++;
            }
            if (collision.transform.tag == "block")
            {
                audio.Play();
                AddScore();
            }
        } else
        {
            if (collision.transform.tag == "racket")
            {
                if (powerfulPaddleCollision == false)
                {
                    collisionCounter++;
                    powerfulPaddleCollision = true;
                    rb.AddForce(new Vector3(0, added_force / 3, 0));
                } else
                {
                    TriggerUpgrade.forceUpgrade = false;
                    powerfulPaddleCollision = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "block")
        {
            AddScore();
            audio.Play();
            Pooling.Instance.DisableFromPool(other.gameObject);
        }
    }

    public void AddScore()
    {
        score += 100;
        if (score == 100)
        {
            scoreText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 205);
        }
        if (score == 1000)
        {
            scoreText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 220);
        }
        if (score == 10000)
        {
            scoreText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 235);
        }
        scoreText.text = "Score: " + score;
    }
}
