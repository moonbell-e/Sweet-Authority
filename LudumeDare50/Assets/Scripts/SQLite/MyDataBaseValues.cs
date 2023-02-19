using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MyDataBaseValues : MonoBehaviour
{
    public static MyDataBaseValues Instance { get; private set; }

    [Header("Complexity")]
    public string meetingDelay;
    public string goldPerSecond;
    public string cardChanceModifier;
    public string _mode;

    [Header("Cars")]
    public Car cars = new Car();

    [Header("Databases")]
    private ComplexityDb _complexity;
    private SettingsDb _settings;
    private CarDb _car;

    private void Awake()
    {
        if (Instance != null)
            Debug.Log("Two Singletons!!1");
        else
            Instance = this;
    }
    private void Start()
    {
        _settings = new SettingsDb();
        _complexity = new ComplexityDb();
        _car = new CarDb();
        ReadComplexityData();
        ReadCarData(1);
        ReadCarData(2);
    }

    public void ReadComplexityData()
    {
        IDataReader readerMode = _settings.GetAllData();
        while (readerMode.Read())
        {
            Debug.Log("Complexity: " + readerMode[1]);
            _mode = readerMode[1].ToString();

            if (_mode == "1")
            {
                IDataReader reader = _complexity.GetDataById(1);
                while (reader.Read())
                {
                    Debug.Log("Mode: " + reader[0] + " MeetingDelay: " + reader[1] + " GoldPerSecond: " + reader[2] + "CardChanceMofidier: " + reader[3]);
                    meetingDelay = reader[1].ToString();
                    goldPerSecond = reader[2].ToString();
                    cardChanceModifier = reader[3].ToString();

                }
            }
            else if (_mode == "2")
            {
                IDataReader reader = _complexity.GetDataById(2);
                while (reader.Read())
                {
                    Debug.Log("Mode: " + reader[0] + " MeetingDelay: " + reader[1] + " GoldPerSecond: " + reader[2] + "CardChanceMofidier: " + reader[3]);
                    meetingDelay = reader[1].ToString();
                    goldPerSecond = reader[2].ToString();
                    cardChanceModifier = reader[3].ToString();

                }
            }
            else if (_mode == "3")
            {
                IDataReader reader = _complexity.GetDataById(3);
                while (reader.Read())
                {
                    Debug.Log("Mode: " + reader[0] + " MeetingDelay: " + reader[1] + " GoldPerSecond: " + reader[2] + "CardChanceMofidier: " + reader[3]);
                    meetingDelay = reader[1].ToString();
                    goldPerSecond = reader[2].ToString();
                    cardChanceModifier = reader[3].ToString();

                }
            }
        }

    }

    public void ReadCarData(int id)
    {
        IDataReader reader = _car.GetDataById(id);
        while(reader.Read())
        {
            Debug.Log("Test Value Arrest" + reader[1]);
            cars.carValues[id - 1].arrestDelay = ParseToInt(reader[1].ToString());
            cars.carValues[id - 1].capacity = ParseToInt(reader[2].ToString());
            cars.carValues[id - 1].speed = ParseToInt(reader[3].ToString());
            cars.carValues[id - 1].hp = ParseToInt(reader[4].ToString());
            cars.carValues[id - 1].actualHP = ParseToInt(reader[5].ToString());
            cars.carValues[id - 1].peopleIn = ParseToInt(reader[6].ToString());
        }
    }

    
    public void UpdateCarData(int id, int arrestDelay, int capacity, int speed, int HP, int actualHP, int peopleIn)
    {
        _car.AddData(new CarEntity(id, arrestDelay, capacity, speed, HP, actualHP, peopleIn));
        IDbCommand dbcmd = _car.GetDbCommand();
        dbcmd.CommandText = "INSERT INTO Characteristics"
                        + " ( "
                        + "Arrest delay" + ", "
                        + "Capacity" + ", "
                        + "Speed" + ", "
                        + "HP" + ", "
                        + "Actual HP" + ", "
                        + "People In" + " ) "

                        + "VALUES ( '"
                        + arrestDelay + "', '"
                        + capacity + "', '"
                        + speed + "', '"
                        + HP + "', '"
                        + actualHP + "', '"
                        + peopleIn + "' );";
        Debug.Log("New");

    }

    public void LoadCarData(int id, float arrestDelay, int capacity, float speed, int hp, int actualHP, int peopleIn)
    {
        arrestDelay = cars.carValues[id].arrestDelay;
        capacity = cars.carValues[id].capacity;
        speed = cars.carValues[id].speed;
        hp = cars.carValues[id].hp;
        actualHP = cars.carValues[id].actualHP;
        peopleIn = cars.carValues[id].peopleIn;
    }

    public int ParseToInt(string value)
    {
        int newValue = 0;
        if (int.TryParse(value, out newValue))
            return newValue;
        return 0;
    }
}
