using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private int _coins = 0;

    private void OnEnable() => EventManager.CoinCollected += EventManagerOnCoinCollected;
    private void OnDisable() => EventManager.CoinCollected -= EventManagerOnCoinCollected;
    private void EventManagerOnCoinCollected(Vector3 position) => _coins++;

    public int GetNumberOfCoins() => _coins;
}