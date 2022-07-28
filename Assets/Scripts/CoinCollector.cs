using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private GameField _gameField;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] protected Text _coinsView;
    [SerializeField] private int _score;
    [SerializeField] private int _currentCoinAmountOnField;

    public void OnCoinCollected()
    {
        _score += 1;
        _currentCoinAmountOnField -= 1;
        ShowScore(_score);

        if (_currentCoinAmountOnField <= 0)
        { 
            _playerControl.SetIsGameOver(true, "You WIN");
        }
    }

    private void OnEnable()
    {
        _gameField.FieldIsReady += OnFieldReady;
    }

    private void OnDisable()
    {
        _gameField.FieldIsReady -= OnFieldReady;
    }

    private void OnFieldReady(int coins)
    {
        _currentCoinAmountOnField = coins;
    }

    private void ShowScore(int score)
    {
        _coinsView.text = $"Coins: {score}";
    }
}
