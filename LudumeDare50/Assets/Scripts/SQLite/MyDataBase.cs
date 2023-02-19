using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;

public class MyDataBase : MonoBehaviour
{
    private const string database_name = "my_db";

    public string db_connection_string;
    public IDbConnection db_connection;

    public MyDataBase()
    {
        db_connection_string = "URI=file:my_db.db";
        Debug.Log("db_connection_string: " + db_connection_string);
        db_connection = new SqliteConnection(db_connection_string);
        db_connection.Open();
    }

    ~MyDataBase()
    {
        db_connection.Close();
    }

    public virtual IDataReader GetDataById(int id)
    {
        Debug.Log("The data by id is not implemented");
        throw null;
    }

    public virtual IDataReader GetDataByString(string str)
    {
        Debug.Log("The data by string is not implemnted");
        throw null;
    }

    public virtual void DeleteDataById(int id)
    {
        Debug.Log("Deleting data by id is not implemented");
        throw null;
    }

    public virtual void DeleteDataByString(string id)
    {
        Debug.Log("Deleting data by string is not implemented");
        throw null;
    }

    public virtual IDataReader GetAllData(string table_name)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText = "SELECT * FROM " + table_name;
        IDataReader reader = dbcmd.ExecuteReader();
        return reader;
    }

    public virtual IDataReader GetAllData()
    {
        Debug.Log("Getting all data is not implemented");
        throw null; 
    }

    public virtual void DeleteAllData()
    {
        Debug.Log("Deleting all data is not implemnted");
        throw null;
    }

    public virtual IDataReader GetNumOfRows()
    {
        Debug.Log("Getting num of rows is not implemnted");
        throw null;
    }

    //database functions
    public IDbCommand GetDbCommand()
    {
        return db_connection.CreateCommand();
    }

    public void DeleteAllData(string table_name)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
        dbcmd.ExecuteNonQuery();
    }

    public IDataReader GetNumOfRows(string table_name)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText =
            "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
        IDataReader reader = dbcmd.ExecuteReader();
        return reader;
    }

    public void Close()
    {
        db_connection.Close();
    }
}

