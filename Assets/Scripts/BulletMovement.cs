using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidadBala = 8f;
    private Rigidbody2D rbBala;

    void Start()
    {
        rbBala = GetComponent<Rigidbody2D>();
        rbBala.linearVelocity = transform.right * velocidadBala;
        Destroy(gameObject, 2f); // Se destruye a los 2 segundos si no impacta
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con un asteroide, la bala se destruye
        if (collision.gameObject.CompareTag("Asteroide"))
        {
            Destroy(gameObject);
        }
    }
}