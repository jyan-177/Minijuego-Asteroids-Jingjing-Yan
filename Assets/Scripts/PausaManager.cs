using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        // También puedes pulsar la tecla ESC para pausar/reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Oculta el menú de pausa
        Time.timeScale = 1f;          // Reanuda el tiempo del juego
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);  // Muestra el menú de pausa
        Time.timeScale = 0f;          // Congela completamente el tiempo del juego
        isPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;          // Restaura el tiempo antes de recargar
        PlayerMovement.SCORE = 0;     // Resetea los puntos si quieres que se borren al reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
