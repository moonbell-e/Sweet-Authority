using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvtozakButton : MonoBehaviour
{
    [SerializeField] private GameObject _avtozak;

    public GameObject avtozak => _avtozak;

    public void SetAvtozak(GameObject avtozak)
    {
        _avtozak = avtozak;
    }
}
