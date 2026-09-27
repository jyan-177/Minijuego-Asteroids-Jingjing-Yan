using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        // Mostramos el récord en la pantalla
        highScoreText.text = "MAX: " + highScore.ToString();
    }

    void Update()
    {
        // 1. Actualizamos el marcador normal en todo momento
        scoreText.text = "SCORE: " + PlayerMovement.SCORE.ToString();

        // 2. Comprobamos si la puntuación actual ha superado al récord histórico
        if (PlayerMovement.SCORE > highScore)
        {
            highScore = PlayerMovement.SCORE;
            
            // Guardamos el nuevo récord en el ordenador de forma permanente
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            // Actualizamos el texto
            highScoreText.text = "MAX: " + highScore.ToString();
        }
    }
}