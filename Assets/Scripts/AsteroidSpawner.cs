using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidGrandePrefab;
    public float tiempoEntreSpawns = 3f;
    public float tiempoEsperaInicial = 1f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(tiempoEsperaInicial);

        // Bucle infinito que genera asteroides continuamente
        while (true)
        {
            SpawnAsteroid();

            // Espera el tiempo configurado antes de generar el siguiente
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidGrandePrefab == null) return;

        // Elige una posición aleatoria horizontal en la pantalla (de 0 a 1)
        float randomX = Random.Range(0f, 1f);

        Vector3 viewportPos = new Vector3(randomX, 1.1f, 0f);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportPos);
        worldPos.z = 0f;

        Instantiate(asteroidGrandePrefab, worldPos, Quaternion.identity);
    }
}