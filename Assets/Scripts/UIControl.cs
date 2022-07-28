using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _gameOverText;

    public void RestartButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void GameOver(string gameOverText)
    {
        _gameOverPanel.SetActive(true);
        _gameOverText.text = gameOverText;
    }

    private void Awake()
    {
        _gameOverPanel.SetActive(false);
    }
}
