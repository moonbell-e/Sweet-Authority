using UnityEngine;
using TMPro;
using City;
using Police;

public class AvtozakShop : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopWindow;
    [SerializeField]
    private TextMeshProUGUI _priceText;
    [SerializeField]
    private GameObject _avtozak;
    private PoliceStation _policeStation;

    private void Awake()
    {
        var price = _avtozak.GetComponent<AvtozakBehavior>().AvtozakPrice;
        _priceText.text = "1 police truck - " + price.ToString();
    }

    public void OpenAvtozakShop(PoliceStation policeStation)
    {
        _policeStation = policeStation;
        
        _shopWindow.SetActive(true);
    }
    
    public void BuyAvtozak()
    {
        if(_policeStation == null)
        {
            Debug.LogError("No police station");
            return;
        }
        _policeStation.SpawnAvtozak(true);
        _shopWindow.SetActive(false);
    }

    public void CloseAvtozakShop()
    {
        _policeStation = null;
        _shopWindow.SetActive(false);
    }
}