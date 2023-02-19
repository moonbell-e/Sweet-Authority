using UnityEngine;
using Police;
using City;
using GameDataKeepers;

namespace InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform _pointer;
        private StoragesKeeper _storagesKeeper;
        private GameObject _selectedObject;

        private void Start()
        {
            _storagesKeeper = FindObjectOfType<StoragesKeeper>();
            _pointer.gameObject.SetActive(false);
            DeselectObject();
        }

        public void SelectUnit(RaycastHit hit)
        {
            var policeStation = hit.collider.gameObject.GetComponent<PoliceStation>();
            if(_selectedObject != null)
            {
                var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
                if(avtozak != null && policeStation != null)
                {
                    if(avtozak.OnPoliceStation == policeStation)
                    {
                        _storagesKeeper.PoliceStorage.AvtozakUpgrade.StartUpgrade(avtozak);
                        avtozak.LeavedPoliceStation += _storagesKeeper.PoliceStorage.AvtozakUpgrade.EndUpgrade;
                        return;
                    }
                }
                DeselectObject();
            }

            _selectedObject = hit.collider.gameObject;
            if(policeStation != null)
                _storagesKeeper.PoliceStorage.AvtozakShop.OpenAvtozakShop(policeStation);
            OutlineObject();
        }

        public void SelectAvtozak(GameObject avtozak)
        {
            if (_selectedObject != null)
                DeselectObject();
            _selectedObject = avtozak;
            OutlineObject();
        }

        private void OutlineObject()
        {
            var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
            if(avtozak != null) 
            {
                avtozak.ChangeOutlineState(true);
                return;
            }

            var policeStation = _selectedObject.GetComponent<PoliceStation>();
            if(policeStation != null)
            {
                _pointer.position = policeStation.PointerPoint.position;
                _pointer.gameObject.SetActive(true);
            }
        }

        public void DeselectObject()
        {
            _storagesKeeper.PoliceStorage.AvtozakUpgrade.EndUpgrade();
            _storagesKeeper.PoliceStorage.AvtozakShop.CloseAvtozakShop();
            if(_selectedObject == null) return;
            var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
            if(avtozak != null) avtozak.ChangeOutlineState(false);
            else _pointer.gameObject.SetActive(false);
            _selectedObject = null;
        }

        public void HandleInput(RaycastHit hit)
        {
            if(_selectedObject == null) return;
            var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
            if(avtozak != null)
            {
                avtozak.MoveCommand(hit.point, hit.collider.gameObject);
                return;
            }
            var policeStation = _selectedObject.GetComponent<PoliceStation>();
            if(policeStation != null)
            {
                policeStation.SetArrivingPoint(hit.point, hit.collider.gameObject);
            }
        }
    }
}