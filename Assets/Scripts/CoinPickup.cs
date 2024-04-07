using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsForCoin= 100;
    [SerializeField] AudioClip coinPickupSound;
    bool wasCollected;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSesion>().AddScore(pointsForCoin); 
            AudioSource.PlayClipAtPoint(coinPickupSound, Camera.main.transform.position);
            Destroy(gameObject); 
        }
    }
}
