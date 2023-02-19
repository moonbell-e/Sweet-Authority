using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using FMODUnity;

public class MenuSystem: MonoBehaviour
{
    [Header("Levels To Load")]
    [SerializeField] private string _newGameLevel;

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Toggle _fullScreenToggle;

    [Header("Resolution Dropdowns")]
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    public Resolution[] _resolutions;

    [SerializeField] private TMP_Dropdown _gameModeDropdown;

    [SerializeField] [EventRef] private string _buttonPushSound;
    [SerializeField] private MyDataBaseValuesMenu _myDataBaseValues;

    [SerializeField] private int qualityLevel;
    [SerializeField] private bool isFullScreen;

    #region Singleton Init
    private static MenuSystem _instance;
    private static bool isInitialized = false; // A bit faster singleton

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static MenuSystem Instance // Init not in order
    {
        get
        {
            if (!isInitialized)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<MenuSystem>();
        if (_instance != null)
        {
            _instance.Initialize();
            isInitialized = true;
        }
    }
    #endregion

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("AddedResolutions"))
        {
            _resolutions = Screen.resolutions;

            List<string> _options = new List<string>();

            int _currentResolutionIndex = 0;

            for (int i = 0; i < _resolutions.Length; i++)
            {
                string _option = _resolutions[i].width + " x " + _resolutions[i].height + " " + _resolutions[i].refreshRate + " Hz";
                _options.Add(_option);
                if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                    _currentResolutionIndex = i;
            }

            _resolutionDropdown.AddOptions(_options);
            _resolutionDropdown.value = _currentResolutionIndex;
            _resolutionDropdown.RefreshShownValue();
        }
    }

    public void StartingNewGame()
    {
        PlayerPrefs.SetInt("AddedResolutions", 1);
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetQuality(int qualityIndex)
    {
        qualityLevel = qualityIndex;
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void SetGameMode(int gameModeIndex)
    {
        _myDataBaseValues.UpdateComplexity(gameModeIndex + 1);
    }



    public void SetResolution(int resolutionIndex)
    {
        Resolution _resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen()
    {
        Debug.Log(_fullScreenToggle.isOn);
        isFullScreen = _fullScreenToggle.isOn;
        Screen.fullScreen = isFullScreen;
        _myDataBaseValues.UpdateFullscreenToggle(_fullScreenToggle.isOn);
    }

    public void ResetGameMode()
    {
        _gameModeDropdown.value = 0;
        _myDataBaseValues.UpdateComplexity(1);
    }

    public void ResetGraphics()
    {
        _qualityDropdown.value = 1;
        QualitySettings.SetQualityLevel(1);

        _fullScreenToggle.isOn = true;
        Screen.fullScreen = true;

        Resolution _currentResolution = Screen.currentResolution;
        Screen.SetResolution(_currentResolution.width, _currentResolution.height, Screen.fullScreen);
        _resolutionDropdown.value = _resolutions.Length;
    }


    public void PlayButtonPushSound()
    {
        RuntimeManager.PlayOneShot(_buttonPushSound);
    }

    private void Initialize()
    {
        enabled = true;
    }
}
