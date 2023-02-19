using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Police;
using City;

namespace GameDataKeepers
{
    public class PoliceDataStorage : MonoBehaviour
    {
        [SerializeField] private Animator _truckDestroyedText;
        [SerializeField] private TrucksInCharge _trucksInCharge;
        private AvtozakUpgradeSystem _avtozakUpgrade;
        private AvtozakShop _avtozakShop;
        private List<AvtozakBehavior> _avtozaks;
        private List<PoliceStation> _policeStations;


        public AvtozakShop AvtozakShop => _avtozakShop;
        public AvtozakUpgradeSystem AvtozakUpgrade => _avtozakUpgrade;
        public List<AvtozakBehavior> Avtozaks => _avtozaks;
        public List<PoliceStation> PoliceStations => _policeStations;

        private void Awake()
        {
            _avtozakShop = FindObjectOfType<AvtozakShop>();
            _avtozakUpgrade = FindObjectOfType<AvtozakUpgradeSystem>();
            _avtozaks = FindObjectsOfType<AvtozakBehavior>().ToList();
            _policeStations = FindObjectsOfType<PoliceStation>().ToList();
            foreach (var avtozak in _avtozaks)
            {
                _trucksInCharge.AddAvtozakButton(avtozak.gameObject);
                avtozak.Destructed += RemoveAvtozak;
            }
            foreach(var station in _policeStations)
                station.AvtozakSpawned += AddAvtozak;
        }

        private void AddAvtozak(AvtozakBehavior avtozak)
        {
            _trucksInCharge.AddAvtozakButton(avtozak.gameObject);
            _avtozaks.Add(avtozak);
            avtozak.Destructed += RemoveAvtozak;
        }

        private void RemoveAvtozak(AvtozakBehavior avtozak)
        {
            _trucksInCharge.RemoveAvtozakButton(avtozak.gameObject);
            _avtozaks.Remove(avtozak);
            _truckDestroyedText.Play("TruckDestroyed_Anim", -1, 0f);
        }
    }
}