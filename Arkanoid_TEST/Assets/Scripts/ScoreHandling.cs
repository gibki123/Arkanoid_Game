using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandling : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        AllignScoreText();
        text.text = "Score: " + BallCollision.score;
    }

    public void AllignScoreText()
    {
        
        if (BallCollision.score < 100)
        {
            text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 375);
        }
        else if (BallCollision.score < 1000)
        {
            text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 490);
        }
        else if (BallCollision.score < 10000)
        {
            text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 545);
        }
        else if (BallCollision.score < 100000)
        {
            text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
        }
    }
}
