using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;

public class MeetingsDb : MyDataBase
{
    private const String TABLE_NAME = "MeetingCharacteristics";
    private const String KEY_ID = "ID";
    private const String KEY_PEOPLE = "People";
    private const String KEY_FORCE = "Force";

    public MeetingsDb(): base()
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
            KEY_ID + " INTEGER PRIMARY KEY, " +
            KEY_PEOPLE + " INTEGER, " +
            KEY_FORCE + " INTEGER )";
        dbcmd.ExecuteNonQuery();
    }

    public void AddData(MeetingsEntity meetingsEntity)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_PEOPLE + ", "
                + KEY_FORCE + " ) "

                + "VALUES ( '"
                + meetingsEntity._people + "', '"
                + meetingsEntity._force + "' );";
        dbcmd.ExecuteNonQuery();
    }
    public override IDataReader GetDataById(int id)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
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
            "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
        dbcmd.ExecuteNonQuery();
    }

    public override void DeleteAllData()
    {
        Debug.Log("Deleting Table");

        DeleteAllData(TABLE_NAME);
    }
}
