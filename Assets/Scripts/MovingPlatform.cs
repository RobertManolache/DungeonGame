using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] public int PlatformSpeed=4;
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
  
    }
    void Update()
    {
        myRigidbody.velocity = new Vector2(PlatformSpeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            PlatformSpeed = -PlatformSpeed;
        }
    }
}
