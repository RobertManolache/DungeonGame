using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
    [SerializeField] int pointsForEnemy = 200;

    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        //initializare componente
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * arrowSpeed;
    }

    void Update()
    {
        //directia sagettii
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        

    }
    //distrugerea obiectului la coliziune
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Enemy")
        {
            FindObjectOfType<GameSesion>().AddScore(pointsForEnemy);
            Destroy(collision.gameObject);
           
        }
         Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
