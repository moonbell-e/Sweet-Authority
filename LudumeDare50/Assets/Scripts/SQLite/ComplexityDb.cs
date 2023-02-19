using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ComplexityDb : MyDataBase
{
    private const String TABLE_NAME = "Complexity";
    private const String KEY_MODE = "Mode";
    private const String KEY_MEETING_DELAY = "MeetingDelay";
    private const String KEY_GOLD = "GoldPerSecond";
    private const String KEY_CARD = "CardChanceModifier";

    public ComplexityDb() : base()
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
            KEY_MODE + " INTEGER PRIMARY KEY, " +
            KEY_MEETING_DELAY + " INTEGER, " +
            KEY_GOLD + " INTEGER, " +
            KEY_CARD + " REAL )";
        dbcmd.ExecuteNonQuery();
    }

    public void AddData(ComplexityEntity complexity)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "INSERT INTO " + TABLE_NAME
                        + " ( "
                        + KEY_MEETING_DELAY + ", "
                        + KEY_GOLD + ", "
                        + KEY_CARD + " ) "

                        + "VALUES ( '"
                        + complexity._meetingsDelay + "', '"
                        + complexity._goldPerSecond + "', '"
                        + complexity._cardChanceModifier + "' );";
        dbcmd.ExecuteNonQuery();
    }

    public override IDataReader GetDataById(int id)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_MODE + " = '" + id + "'";
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
            "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_MODE + " = '" + id + "'";
        dbcmd.ExecuteNonQuery();
    }

    public override void DeleteAllData()
    {
        Debug.Log("Deleting Table");

        DeleteAllData(TABLE_NAME);
    }

}
