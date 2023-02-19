using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Linq;
using System.Text;

public class SettingsDb : MyDataBase
{
    private const String TABLE_NAME = "Settings";
    private const String KEY_ID = "ID";
    private const String KEY_COMPLEXITY = "Complexity";
    private const String KEY_RESOLUTION = "Resolution";
    private const String KEY_QUALITY = "Quality";
    private const String KEY_FULLSCREEN = "Fullscreen";
    private const String KEY_MUSIC = "Music";
    private const String KEY_SFX = "SFX";
    
    public SettingsDb() : base()
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
            KEY_ID + " INTEGER PRIMARY KEY, " +
            KEY_COMPLEXITY + " INTEGER, " +
            KEY_RESOLUTION + " INTEGER, " +
            KEY_QUALITY + " INTEGER, " +
            KEY_FULLSCREEN + " INTEGER, " +
            KEY_MUSIC + " REAL, " +
            KEY_SFX + " REAL )";
        dbcmd.ExecuteNonQuery();
    }

    public void AddData(SettingEntity settings)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "INSERT INTO " + TABLE_NAME
                        + " ( "
                        + KEY_COMPLEXITY + ", "
                        + KEY_RESOLUTION + ", "
                        + KEY_QUALITY + ", "
                        + KEY_FULLSCREEN + ", "
                        + KEY_MUSIC + ", "
                        + KEY_SFX + " ) "

                        + "VALUES ( '"
                        + settings._complexity + "', '"
                        + settings._resolution + "', '"
                        + settings._quality + "', '"
                        + settings._fullscreen + "', '"
                        + settings._music + "', '"
                        + settings._SFX + "' );";
        dbcmd.ExecuteNonQuery();
    }

    public override IDataReader GetDataById(int id)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_COMPLEXITY + " = '" + id + "'";
        return dbcmd.ExecuteReader();
    }

    public override IDataReader GetDataByString(string str)
    {
        return base.GetDataByString(str);
    }

    public override IDataReader GetAllData()
    {
        return base.GetAllData(TABLE_NAME);
    }

    public override void DeleteDataByString(string id)
    {
        base.DeleteDataByString(id);
    }

    public override void DeleteDataById(int id)
    {
        Debug.Log("Deleting Settings: " + id);

        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText =
            "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_COMPLEXITY + " = '" + id + "'";
        dbcmd.ExecuteNonQuery();
    }

    public override void DeleteAllData()
    {
        Debug.Log("Deleting Table");

        DeleteAllData(TABLE_NAME);
    }
}
