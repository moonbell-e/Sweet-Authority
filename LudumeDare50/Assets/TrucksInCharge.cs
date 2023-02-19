using InputSystem;
using Police;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TrucksInCharge : MonoBehaviour
{
    [SerializeField] private GameObject _avtozakButton;
    [SerializeField] private Transform _grid;
    [SerializeField] private List<Button> _avtozakButtons;
    [SerializeField] private InputHandler _inputHandler;
    
    

    public void AddAvtozakButton(GameObject avtozakTruck)
    {
        Button avtozak = Instantiate(_avtozakButton, _grid).GetComponent<Button>();
        avtozak.GetComponent<AvtozakButton>().SetAvtozak(avtozakTruck);
        _avtozakButtons.Add(avtozak);
        avtozak.onClick.AddListener(() => _inputHandler.SelectAvtozak(avtozakTruck));
    }

    public void RemoveAvtozakButton(GameObject avtozakTruck)
    {
        foreach (var avtozakButton in _avtozakButtons)
        {
            AvtozakButton avtozakButtonScript = avtozakButton.GetComponent<AvtozakButton>();
            if (avtozakButtonScript.avtozak == avtozakTruck)
            {
                Destroy(avtozakButton.gameObject);
            }
        }
    }
}
