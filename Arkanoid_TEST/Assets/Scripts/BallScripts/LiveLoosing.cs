using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LiveLoosing : MonoBehaviour
{

    private int numberOfLives;

    [SerializeField]
    private Text livesText;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        numberOfLives = 3;
    }

     private void Update()
    {
        if(transform.position.y < -10)
        {
            numberOfLives--;
            livesText.text = "Lives: " + numberOfLives.ToString();
            rb.isKinematic = true;
            rb.velocity = new Vector3(0, 0, 0);
            RestartPosition();
            BallCollision.firstclick = true;
            UpgradesHandling.Instance.AllUpgradesEnd();
        }
        if(numberOfLives == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RestartPosition()
    {
        transform.SetParent(GameObject.Find("Paddle").transform);
        transform.parent.position = new Vector3(0, -6.5f, 0);
        transform.position = new Vector3(0,-6f,0);
    }
}
