using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Variables

    [SerializeField] private PlayerHandler player;
    [SerializeField] private TMP_Text numberOfCoinsText;

    #endregion

    private void OnEnable()
    {
        EventManager.CoinCollected += EventManagerOnCoinCollected;
    }

    private void OnDisable()
    {
        EventManager.CoinCollected -= EventManagerOnCoinCollected;
    }

    private void EventManagerOnCoinCollected(Vector3 position)
    {
        numberOfCoinsText.text = $"Coins: {player.GetNumberOfCoins()}";
    }
}