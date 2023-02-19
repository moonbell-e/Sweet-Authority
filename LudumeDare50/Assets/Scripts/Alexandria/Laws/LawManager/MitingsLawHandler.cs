using UnityEngine;
using System.Collections;
using Laws.Effects;
using GameDataKeepers;

namespace Laws.Managers
{
    public class MitingsLawHandler : MonoBehaviour
    {
        private StoragesKeeper _storagesKeeper;
        private float[] _bufferValues;

        public void ActivateEffect(StoragesKeeper storagesKeeper, MitingsLawSO effect, float duration, float delay)
        {
            StartCoroutine(HandleEffect(storagesKeeper, effect, duration, delay));
        }

        private IEnumerator HandleEffect(StoragesKeeper storagesKeeper, MitingsLawSO effect, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            _bufferValues = new float[0];
            ApplyEffect(storagesKeeper, effect, duration, false, _bufferValues);
            float[] rememberValues = new float[_bufferValues.Length];
            for (int i = 0; i < _bufferValues.Length; i++)
                rememberValues[i] = _bufferValues[i];
            if(duration > 0)
            {
                yield return new WaitForSeconds(duration);
                ApplyEffect(storagesKeeper, effect, duration, true, rememberValues);
            }
        }

        private void ApplyEffect(StoragesKeeper storagesKeeper, MitingsLawSO effect, float duration, bool reverse, float[] rememberValue)
        {
            var mitingStorage = storagesKeeper.MitingsStorage;
            var value = effect.Value;   
            if(effect.InPercent) value *= 0.01f;
            if(reverse)
            {
                if(effect.InPercent) value = 1 / value;
                else value = -value;
            }
            else
            {
                if(effect.Duration > 0) _bufferValues = new float[mitingStorage.Mitings.Count];
            }
            
            switch(effect.Type)
            {
                case MitingsLawType.People:
                    if(effect.InPercent) 
                    {
                        for(int i = 0; i < mitingStorage.Mitings.Count; i++)
                        {
                            var miting = mitingStorage.Mitings[i];
                            if(reverse)
                            {
                                miting.ChangeProtesters((int)rememberValue[i], 0);
                            }
                            else
                            {
                                var people = miting.Protest.PeopleBar.value;
                                int changes = (int)((value * people) - people);
                                if(effect.Duration > 0) _bufferValues[i] = -changes;
                                miting.ChangeProtesters(changes, 0);
                            }
                        }
                    }
                    else
                    {
                        foreach(var miting in mitingStorage.Mitings)
                            miting.ChangeProtesters((int)value, 0);
                    }
                break;

                case MitingsLawType.Power:
                    if(effect.InPercent) 
                    {
                        for(int i = 0; i < mitingStorage.Mitings.Count; i++)
                        {
                            var miting = mitingStorage.Mitings[i];
                            if(reverse)
                            {
                                miting.ChangeProtesters(0, (int)rememberValue[i]);
                            }
                            else
                            {
                                var power = miting.Protest.PowerBar.value;
                                var changes = (value * power) - power; 
                                if(effect.Duration > 0) _bufferValues[i] = -changes;
                                miting.ChangeProtesters(0, changes);
                            }
                        }
                    } 
                    else
                    {
                        foreach(var miting in mitingStorage.Mitings)
                            miting.ChangeProtesters(0, value);
                    }  
                break;
            }
        }
    }
}