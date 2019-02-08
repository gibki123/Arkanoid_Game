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
            UpgradesHandling.Instance.AllUpgradesEnd();
            BallCollision.firstBallShot = true;          
        }
        if(numberOfLives == 0 && BlockSpawning.endlessLevelling == false)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (numberOfLives == 0 && BlockSpawning.endlessLevelling == true)
        {
            SceneManager.LoadScene("YourScore");
        }
    }

    public void RestartPosition()
    {
        GameObject paddle = GameObject.Find("Paddle");
        transform.SetParent(paddle.transform);
        transform.parent.position = new Vector3(0, -6.5f, 0);
        transform.position = new Vector3(0, -6f, 0);
        paddle.GetComponent<Renderer>().material.color = Color.white;
    }
}
