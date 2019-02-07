using UnityEngine;
using UnityEngine.UI;

public class BallCollision : MonoBehaviour
{
    public static bool firstBallShot;
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

    private Vector3 paddleDimensions;
    private Vector3 paddlePosition;

    private void Awake()
    {
        UpgradesHandling.forceSum = addedForceMax + addedForceMin;
        score = 0;
        audio = GetComponent<AudioSource>();
        collisionCounter = 0;
        firstBallShot = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (firstBallShot && Input.GetButtonDown("Fire1"))
        {
            force = Random.Range(addedForceMin,addedForceMax);
            transform.parent = null;
            firstBallShot = false;
            rb.isKinematic = false;
            rb.AddForce(new Vector3((addedForceMax + addedForceMin)-force,force, 0));        
        }
        Vector3 velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {     
        if (collision.transform.tag == "racket")
        {
            collisionCounter++;            
            Vector3 collisionPosition = collision.contacts[0].point;
            paddleDimensions = GameObject.FindGameObjectWithTag("racket").transform.localScale;
            paddlePosition = GameObject.FindGameObjectWithTag("racket").transform.position;
            Vector3 centerDistance = paddlePosition - collisionPosition;
            float centerDistancePercentage = centerDistance.x / (paddleDimensions.x/2);
            if(collisionPosition.y - paddlePosition.y > 0)
            {
                rb.velocity = new Vector3(0, 0, 0);
                if (centerDistancePercentage < -0.50f)
                {
                    rb.AddForce(addedForceMax - addedForceMin, addedForceMax, 0);
                } else if (centerDistancePercentage > 0.50f)
                {
                    rb.AddForce(-(addedForceMax - addedForceMin), addedForceMax, 0);
                } else
                {
                    rb.AddForce(0, Mathf.Sqrt(addedForceMax * addedForceMax + addedForceMin * addedForceMin), 0);
                }
            }                        
        }
        if (collision.transform.tag == "block")
        {
            audio.Play();
            AddScore();
            Pooling.Instance.DisableFromPool(collision.gameObject);
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
