using UnityEngine;

public class CarEntity : MonoBehaviour
{
    public int _ID;
    public int _arrestdelay;
    public int _capacity;
    public int _speed;
    public int _hp;
    public int _actualHP;
    public int _peopleInCar;

    public CarEntity(int id, int arrestDelay, int capacity, int speed, int hp, int actualHP, int peopleInCar)
    {
        _ID = id;
        _arrestdelay = arrestDelay;
        _capacity = capacity;
        _speed = speed;
        _hp = hp;
        _actualHP = actualHP;
        _peopleInCar = peopleInCar;
    }

}
