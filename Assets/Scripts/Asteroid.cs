using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;               // Velocidad propia de este asteroide
    public float angularVelocityRange = 40f;

    public int scoreValue = 10;
    public GameObject[] subAsteroids;      // Prefabs hijos
    public int numberOfAsteroids = 2;    // Siempre se parte en 2, o se puede modificar en el inspector

    private Rigidbody2D rb;
    private bool isDestroying = false;

    void Start()
    {
        ConfigurarRigidbodyYMovimiento();
    }

    void ConfigurarRigidbodyYMovimiento()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
        }

        rb.linearDamping = 0f;
        rb.angularDamping = 0f;

        float velocidadFinal = (speed > 0f) ? speed : 3f;
        rb.linearVelocity = Random.insideUnitCircle.normalized * velocidadFinal;
        rb.angularVelocity = Random.Range(-angularVelocityRange, angularVelocityRange);
    }

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroying) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isDestroying = true;
            PlayerMovement.SCORE += scoreValue;

            Vector2 hitPosition = transform.position;

            if (subAsteroids != null && subAsteroids.Length > 0 && numberOfAsteroids == 2)
            {
                Vector2 dirBase = Random.insideUnitCircle.normalized;

                for (int i = 0; i < 2; i++)
                {
                    GameObject prefabHijo = subAsteroids[Random.Range(0, subAsteroids.Length)];
                    
                    if (prefabHijo != null)
                    {
                        GameObject nuevoHijo = Instantiate(prefabHijo, hitPosition, Quaternion.identity);

                        Rigidbody2D rbHijo = nuevoHijo.GetComponent<Rigidbody2D>();
                        if (rbHijo == null)
                        {
                            rbHijo = nuevoHijo.AddComponent<Rigidbody2D>();
                            rbHijo.gravityScale = 0f;
                        }

                        Vector2 direccionFinal = (i == 0) ? dirBase : -dirBase;
                        direccionFinal = Quaternion.Euler(0, 0, Random.Range(-25f, 25f)) * direccionFinal;

                        Asteroid scriptHijo = nuevoHijo.GetComponent<Asteroid>();
                        float velocidadHijo = (scriptHijo != null && scriptHijo.speed > 0f) ? scriptHijo.speed : 4f;

                        rbHijo.linearDamping = 0f;
                        rbHijo.angularDamping = 0f;
                        rbHijo.linearVelocity = direccionFinal.normalized * velocidadHijo;
                    }
                }
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        // 2. Si choca con la nave
        else if (collision.gameObject.CompareTag("Nave"))
        {
            PlayerMovement.SCORE = 0; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}