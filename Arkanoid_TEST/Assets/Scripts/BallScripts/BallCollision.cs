using UnityEngine;
using UnityEngine.UI;

public class BallCollision : MonoBehaviour
{
    public static bool firstclick;
    public static int collisionCounter;

    private AudioSource audio;

    private Rigidbody rb;

    [SerializeField]
    private Text scoreText;

    private static double score;

    [SerializeField]
    [Range(0, 1000)]
    private int addedForceMin;

    [SerializeField]
    [Range(0, 1000)]
    private int addedForceMax;

    private int force;

    private void Awake()
    {
        score = 0;
        audio = GetComponent<AudioSource>();
        collisionCounter = 0;
        firstclick = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (firstclick && Input.GetButtonDown("Fire1"))
        {
            force = Random.Range(addedForceMin,addedForceMax);
            transform.parent = null;
            firstclick = false;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(addedForceMax-force,force, 0));        
        }
        Vector3 velocity = rb.velocity;
        Debug.Log(velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (UpgradesHandling.forceUpgrade == false)
        {
            if (collision.transform.tag == "racket")
            {
                collisionCounter++;
            }
            if (collision.transform.tag == "block")
            {
                audio.Play();
                AddScore();
                Pooling.Instance.DisableFromPool(collision.gameObject);
            }
        } else
        {
            if (collision.transform.tag == "racket")
            {
                    collisionCounter++;
            }
            if (collision.transform.tag == "block")
            {
                audio.Play();
                AddScore();
                Pooling.Instance.DisableFromPool(collision.gameObject);
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
