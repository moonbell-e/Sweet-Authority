using UnityEngine;
using FMODUnity;
using GameDataKeepers;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverWindow;
    [SerializeField] [EventRef]
    private string _loseSound;

    private void Awake()
    {
        FindObjectOfType<StoragesKeeper>().RevolutionBar.RevolutionLevelMaximum += GameOver;
        _gameOverWindow.SetActive(false);
        Time.timeScale = 1;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        RuntimeManager.PlayOneShot(_loseSound);
        _gameOverWindow.SetActive(true);
    }
}
