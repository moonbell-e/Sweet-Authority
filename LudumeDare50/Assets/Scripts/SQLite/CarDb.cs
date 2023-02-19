using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CarDb : MyDataBase
{
    private const String TABLE_NAME = "Characteristics";
    private const String KEY_ID = "ID";
    private const String KEY_ARREST_DELAY = "Arrest delay";
    private const String KEY_CAPACITY = "Capacity";
    private const String KEY_SPEED = "Speed";
    private const String KEY_HP = "HP";
    private const String KEY_ACTUAL_HP = "Actual HP";
    private const String KEY_PEOPLE_IN_CAR = "PeopleIn";

    public CarDb() : base()
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
            KEY_ID + " INTEGER PRIMARY KEY, " +
            KEY_ARREST_DELAY + " INTEGER, " +
            KEY_CAPACITY + " INTEGER, " +
            KEY_SPEED + " INTEGER, " +
            KEY_HP + " INTEGER, " +
            KEY_ACTUAL_HP + " INTEGER, " +
            KEY_PEOPLE_IN_CAR + " INTEGER )";
        dbcmd.ExecuteNonQuery();
    }

    public void AddData(CarEntity car)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "INSERT INTO " + TABLE_NAME
                        + " ( "
                        + KEY_ARREST_DELAY + ", "
                        + KEY_CAPACITY + ", "
                        + KEY_SPEED + ", "
                        + KEY_HP + ", "
                        + KEY_ACTUAL_HP + ", "
                        + KEY_PEOPLE_IN_CAR + " ) "

                        + "VALUES ( '"
                        + car._arrestdelay + "', '"
                        + car._capacity + "', '"
                        + car._speed + "', '"
                        + car._hp + "', '"
                        + car._actualHP + "', '"
                        + car._peopleInCar + "' );";
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
