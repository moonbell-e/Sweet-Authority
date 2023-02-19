using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Laws.Effects;
using GameDataKeepers;
using Police;
using City;

namespace Laws.Managers
{
    public class PoliceLawHandler : MonoBehaviour
    {
        private StoragesKeeper _storagesKeeper;
        private List<AvtozakBehavior> _avtozaks;

        public void ActivateEffect(StoragesKeeper storagesKeeper, PoliceLawSO effect, float duration, float delay)
        {
            if (effect.Type == PoliceLawType.Spawn)
                StartCoroutine(HandleSpawn(storagesKeeper, effect, duration, delay));
            else
                StartCoroutine(HandleEffect(storagesKeeper, effect, duration, delay));
        }

        private IEnumerator HandleEffect(StoragesKeeper storagesKeeper, PoliceLawSO effect, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            ApplyEffect(storagesKeeper, effect, duration, false);
            if (duration > 0)
            {
                yield return new WaitForSeconds(duration);
                ApplyEffect(storagesKeeper, effect, duration, true);
            }
        }

        private void ApplyEffect(StoragesKeeper storagesKeeper, PoliceLawSO effect, float duration, bool reverse)
        {
            var policeStorage = storagesKeeper.PoliceStorage;
            if (policeStorage.Avtozaks.Count == 0) return;
            var value = effect.Value;
            if (effect.InPercent) value *= 0.01f;
            if (reverse)
            {
                if (effect.InPercent) value = 1 / value;
                else value = -value;
            }

            _avtozaks = new List<AvtozakBehavior>();
            switch (effect.Affects)
            {
                case AffectedAvtozaks.All:
                    foreach (var avtozak in policeStorage.Avtozaks)
                        _avtozaks.Add(avtozak);
                    break;

                case AffectedAvtozaks.Random:
                    var randomIndex = Random.Range(0, policeStorage.Avtozaks.Count);
                    _avtozaks.Add(policeStorage.Avtozaks[randomIndex]);
                    break;
            }

            switch (effect.Type)
            {
                case PoliceLawType.Health:
                    if (effect.InPercent)
                        foreach (var avtozak in _avtozaks)
                        {
                            avtozak.Upgrade((int)(value * avtozak.Health), avtozak.Speed, avtozak.Capacity, avtozak.ArrestDelay);
                            //MyDataBaseValues.Instance.UpdateCarData(avtozak.ID, avtozak.ArrestDelay, avtozak.Capacity, avtozak.Speed, avtozak.Health, avtozak.Health, avtozak.PeopleIn);
                        }
                    else
                        foreach (var avtozak in _avtozaks)
                            avtozak.Upgrade((int)value + avtozak.Health, avtozak.Speed, avtozak.Capacity, avtozak.ArrestDelay);
                    break;

                case PoliceLawType.Speed:
                    if (effect.InPercent)
                        foreach (var avtozak in _avtozaks)
                            avtozak.Upgrade(avtozak.Health, (int)value * avtozak.Speed, avtozak.Capacity, avtozak.ArrestDelay);
                    else
                        foreach (var avtozak in _avtozaks)
                            avtozak.Upgrade(avtozak.Health, (int)value + avtozak.Speed, avtozak.Capacity, avtozak.ArrestDelay);
                    break;

                case PoliceLawType.Capacity:
                    if (effect.InPercent)
                        foreach (var avtozak in _avtozaks)
                            avtozak.Upgrade(avtozak.Health, avtozak.Speed, (int)(value * avtozak.Capacity), avtozak.ArrestDelay);
                    else
                        foreach (var avtozak in _avtozaks)
                            avtozak.Upgrade(avtozak.Health, avtozak.Speed, (int)value + avtozak.Capacity, avtozak.ArrestDelay);
                    break;

                case PoliceLawType.ArrestDelay:
                    if (effect.InPercent)
                        foreach (var avtozak in _avtozaks)
                            avtozak.Upgrade(avtozak.Health, avtozak.Speed, avtozak.Capacity, (int)value * avtozak.ArrestDelay);
                    else
                        foreach (var avtozak in _avtozaks)
                            avtozak.Upgrade(avtozak.Health, avtozak.Speed, avtozak.Capacity, (int)value + avtozak.ArrestDelay);
                    break;
            }
            _avtozaks = null;
        }

        private IEnumerator HandleSpawn(StoragesKeeper storagesKeeper, PoliceLawSO effect, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            var value = Mathf.Abs(effect.Value);
            var policeStorage = storagesKeeper.PoliceStorage;
            List<AvtozakBehavior> spawnedAvtozaks = new List<AvtozakBehavior>();
            _avtozaks = new List<AvtozakBehavior>();
            if (effect.InPercent)
            {
                value = value * 0.01f * policeStorage.Avtozaks.Count;
                for (int i = 0; i < (int)value; i++)
                {
                    var randomIndex = Random.Range(0, policeStorage.PoliceStations.Count);
                    policeStorage.PoliceStations[randomIndex].AvtozakSpawned += AddSpawnedAvtozak;
                    policeStorage.PoliceStations[randomIndex].SpawnAvtozak(false);
                    policeStorage.PoliceStations[randomIndex].AvtozakSpawned -= AddSpawnedAvtozak;
                }
            }
            else
            {
                switch (effect.Affects)
                {
                    case AffectedAvtozaks.All:
                        foreach (var policeStation in policeStorage.PoliceStations)
                        {
                            policeStation.AvtozakSpawned += AddSpawnedAvtozak;
                            for (int i = 0; i < (int)value; i++)
                                policeStation.SpawnAvtozak(false);
                            policeStation.AvtozakSpawned -= AddSpawnedAvtozak;
                        }
                        break;

                    case AffectedAvtozaks.Random:
                        var randomIndex = Random.Range(0, policeStorage.PoliceStations.Count);
                        policeStorage.PoliceStations[randomIndex].AvtozakSpawned += AddSpawnedAvtozak;
                        policeStorage.PoliceStations[randomIndex].SpawnAvtozak(false);
                        policeStorage.PoliceStations[randomIndex].AvtozakSpawned -= AddSpawnedAvtozak;
                        break;
                }
            }
            foreach (var avtozak in _avtozaks)
                spawnedAvtozaks.Add(avtozak);
            _avtozaks = null;
            if (duration > 0)
            {
                yield return new WaitForSeconds(duration);
                foreach (var avtozak in spawnedAvtozaks)
                    if (policeStorage.Avtozaks.Contains(avtozak))
                        avtozak.Destruction();
            }
        }

        private void AddSpawnedAvtozak(AvtozakBehavior avtozak)
        {
            _avtozaks.Add(avtozak);
        }
    }
}