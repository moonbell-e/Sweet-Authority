using UnityEngine;
using Police;
using FMODUnity;

namespace City
{
    public class PoliceStation : Square
    {
        [SerializeField] private GameObject _avtozakPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _arrivingPoint;
        [SerializeField] private Transform _pointerPoint;
        public event SendAvtozak AvtozakSpawned;
        private bool _isTriggeredEvent;
        private GameObject _arrivingSquare;

        public Transform PointerPoint => _pointerPoint;
        public Transform ArrivingPoint => _arrivingPoint;

        [SerializeField]
        private int id;

        private void Start()
        {
            _arrivingSquare = this.gameObject;    
        }

        public void SetArrivingPoint(Vector3 position, GameObject arrivingSquare)
        {
            _arrivingPoint.position = position;
            _arrivingSquare = arrivingSquare;
        }

        public void SpawnAvtozak(bool shopping)
        {
            if (MoneySystem.Instance.MoneyAmount >= _avtozakPrefab.GetComponent<AvtozakBehavior>().AvtozakPrice)
            {
                GameObject spawnedAvtozak = Instantiate(_avtozakPrefab, _spawnPoint.position, Quaternion.identity);
                AvtozakBehavior avtozakBehaviour = GetAvtozakBehavior(spawnedAvtozak);
                avtozakBehaviour.Initialize(this, id);
                MyDataBaseValues.Instance.UpdateCarData(avtozakBehaviour.ID, avtozakBehaviour.ArrestDelay, avtozakBehaviour.Capacity, avtozakBehaviour.Speed, avtozakBehaviour.Health, avtozakBehaviour.Health, avtozakBehaviour.PeopleIn);
                avtozakBehaviour.MoveCommand(_arrivingPoint.position, _arrivingSquare);
                if(shopping) DecreaseMoney(avtozakBehaviour);
                if (_isTriggeredEvent) return;
                _isTriggeredEvent = true;
                AvtozakSpawned?.Invoke(avtozakBehaviour);
                id++;
            }
        }

        private AvtozakBehavior GetAvtozakBehavior(GameObject spawnedAvtozak)
        {
            return spawnedAvtozak.GetComponent<AvtozakBehavior>();
        }

        private void DecreaseMoney(AvtozakBehavior avtozakBehavior)
        {
            MoneySystem.Instance.DecreaseMoneyAmount(avtozakBehavior.AvtozakPrice);
            RuntimeManager.PlayOneShot(FMODSingleton.Instance.moneySound);
        }
    }
}