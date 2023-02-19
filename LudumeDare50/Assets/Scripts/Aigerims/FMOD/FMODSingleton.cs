using UnityEngine;
using FMODUnity;

public class FMODSingleton : MonoBehaviour
{
    [EventRef] public string moneySound;
    [EventRef] public string buttonPushSound;
    [EventRef] public string engineOnSound;
    [EventRef] public string engineOffSound;
    [EventRef] public string meetingStartSound;
    [EventRef] public string meetingEndSound;
    #region Singleton Init
    private static FMODSingleton _instance;
    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
    }
    public static FMODSingleton Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }
    static void Init() // Init script
    {
        _instance = FindObjectOfType<FMODSingleton>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion
    private void Initialize()
    {
        enabled = true;
    }

    public void PlayButtonSound()
    {
        RuntimeManager.PlayOneShot(buttonPushSound);
    }
}
