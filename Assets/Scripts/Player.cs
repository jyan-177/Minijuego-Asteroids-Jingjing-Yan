using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float drag;
    public float angularSpeed;

    [Header("Disparo")]
    public float shootRate = 0.15f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public static int SCORE=0;


    private Rigidbody2D rb;
    private float vertical;
    private float horizontal;
    private bool shooting;
    private bool canShoot = true;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = drag;
    }

    void Update (){
        vertical= InputManager.Vertical;
        horizontal= InputManager.Horizontal;
        shooting = InputManager.Fire;

        Rotate ();
        Shoot ();
    }

    void FixedUpdate(){
        rb.AddForce (transform.right * acceleration * vertical);
        if(rb.linearVelocity.magnitude > maxSpeed){
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

    }

    private void Rotate (){
        if(horizontal == 0){
            return;
        }
        transform.Rotate(0,0,-angularSpeed*horizontal*Time.deltaTime);
    }

    private void Shoot(){
        if(shooting && canShoot){
            StartCoroutine (FireRate ());
            
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroide"))
        {
            Lose();
    }
    }

    public void Lose(){
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        SCORE = 0;
    }

    private IEnumerator FireRate (){

        canShoot = false;
        if (firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }

        yield return new WaitForSeconds(shootRate);
        canShoot = true;
            

    }
}