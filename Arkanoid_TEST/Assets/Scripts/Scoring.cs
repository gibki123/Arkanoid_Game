using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private static double score;

    void Awake()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }
     void Start()
    {
        score = 0;
    }


    private void OnCollisionEnter(Collision collision)
    {
        AddScore();
    }
    private void OnTriggerEnter(Collider other)
    {
        AddScore();
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
