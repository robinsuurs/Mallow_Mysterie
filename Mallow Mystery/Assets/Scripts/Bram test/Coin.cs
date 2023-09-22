using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Make sure collision occurs with Player
        if (!other.gameObject.CompareTag("Player")) return;
        
        EventManager.OnCoinCollected(transform.position);
    }
}