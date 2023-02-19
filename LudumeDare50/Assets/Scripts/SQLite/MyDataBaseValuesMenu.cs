using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class MyDataBaseValuesMenu : MonoBehaviour
{
    public static MyDataBaseValuesMenu Instance { get; private set; }

    [Header("Settings")]
    public int complexity;
    public int resolution;
    public int quality;
    public string fullscreen;
    public float music;
    public float SFX;
    private SettingsDb settings;

    private void Awake()
    {
        if (Instance != null)
            Debug.Log("Two Singletons!!1");
        else
            Instance = this;
    }

    private void Start()
    {
        AddNewSettings();
        settings = new SettingsDb();
    }

    public void AddNewSettings()
    {
        settings.AddData(new SettingEntity(1, 1, 2, 0, 1.0f, 0.5f));
    }

    public void UpdateComplexity(int id)
    {
        IDbCommand dbcmd = settings.GetDbCommand();
        dbcmd.CommandText = $"UPDATE Settings  SET Complexity = {id} WHERE ID = 1;";
        dbcmd.ExecuteNonQuery();
        IDataReader reader = settings.GetAllData();
        Debug.Log("Complexity: " + reader["Complexity"]);
    }

    public void UpdateMusicVolume(float value)
    {
        value = Mathf.Round(value * 10.0f) * 0.1f;
        IDbCommand dbcmd = settings.GetDbCommand();
        dbcmd.CommandText = $"UPDATE Settings  SET Music = {value} WHERE ID = 1;";
        dbcmd.ExecuteNonQuery();
        IDataReader reader = settings.GetAllData();
        Debug.Log("Music: " + reader["Music"]);
    }

    public void UpdateFullscreenToggle(bool isOn)
    {
        Debug.Log(isOn);
        IDbCommand dbcmd = settings.GetDbCommand();
        //dbcmd.ExecuteNonQuery();
        if (isOn)
        {
            dbcmd.CommandText = $"UPDATE Settings  SET Fullscreen = {1} WHERE ID = 1;";
        }
        else
            dbcmd.CommandText = $"UPDATE Settings  SET Fullscreen = {0} WHERE ID = 1;";
        IDataReader reader = settings.GetAllData();
        Debug.Log("Fullscreen: " + reader["Fullscreen"]);
    }
}
