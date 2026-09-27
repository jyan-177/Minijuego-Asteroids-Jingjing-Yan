using UnityEngine;
using TMPro; 

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreText.text = "SCORE: " + PlayerMovement.SCORE;
    }
}