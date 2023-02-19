using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using FMODUnity;

public class PauseSystem : MonoBehaviour
{
    public static event Action OnPauseClicked;
    [SerializeField] private GameObject _pausePanel;
    private bool _isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
                PauseClicked();
            else
                ResumeClicked();
        }
    }
    public void PauseClicked()
    {
        OnPauseClicked?.Invoke();
        _isPaused = true;
        _pausePanel.SetActive(_isPaused);
        Time.timeScale = 0f;
    }

    public void ResumeClicked()
    {
        _isPaused = false;
        _pausePanel.SetActive(_isPaused);
        FMODSingleton.Instance.PlayButtonSound();
        Time.timeScale = 1f;
    }


    public void GoToMainMenu()
    {
        FMODSingleton.Instance.PlayButtonSound();
        SceneManager.LoadScene("Menu");
    }
}
