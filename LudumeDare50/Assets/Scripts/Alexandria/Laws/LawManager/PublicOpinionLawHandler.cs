using UnityEngine;
using System.Collections;
using Laws.Effects;
using GameDataKeepers;

namespace Laws.Managers
{
    public class PublicOpinionLawHandler : MonoBehaviour
    {
        private StoragesKeeper _storagesKeeper;
        private float _bufferValue;

        public void ActivateEffect(StoragesKeeper storagesKeeper, PublicOpinionLawSO effect, float duration, float delay)
        {
            StartCoroutine(HandleEffect(storagesKeeper, effect, duration, delay));
        }

        private IEnumerator HandleEffect(StoragesKeeper storagesKeeper, PublicOpinionLawSO effect, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            _bufferValue = 0;
            ApplyEffect(storagesKeeper, effect, duration, false, _bufferValue);
            float rememberValue = _bufferValue;
            if(duration > 0)
            {
                yield return new WaitForSeconds(duration);
                ApplyEffect(storagesKeeper, effect, duration, true, rememberValue);
            }
        }

        private void ApplyEffect(StoragesKeeper storagesKeeper, PublicOpinionLawSO effect, float duration, bool reverse, float rememberValue)
        {
            var revolution = storagesKeeper.RevolutionBar;
            var value = effect.Value;   
            if(effect.InPercent) value *= 0.01f;
            if(reverse)
            {
                if(effect.InPercent) value = 1 / value;
                else value = -value;
            }
            else
            {
                if(effect.Duration > 0) _bufferValue = 0;
            }
            
            switch(effect.Type)
            {
                case PublicOpinionLawType.Income:
                    if(effect.InPercent) 
                    {
                        if(reverse)
                        {
                            revolution.ChangePassiveMoodChanging(rememberValue);
                        }
                        else
                        {
                            if(revolution.PassiveMoodChanging < 0) value = 1 / value;
                            var newPassiveMoodChanging = value * revolution.PassiveMoodChanging;
                            if(effect.Duration > 0) _bufferValue = revolution.PassiveMoodChanging - newPassiveMoodChanging;
                            revolution.SetPassiveMoodChanging(newPassiveMoodChanging);
                        }
                    }
                    else
                    {
                        revolution.ChangePassiveMoodChanging(value);
                    }
                break;

                case PublicOpinionLawType.Budget:
                    if(effect.InPercent) 
                    {
                        if(reverse)
                        {
                            revolution.ChangeRevolutionLevel(rememberValue);
                        }
                        else
                        {
                            var changes = (value * revolution.Level) - revolution.Level;
                            if(effect.Duration > 0) _bufferValue = -changes;
                            revolution.ChangeRevolutionLevel(changes);
                        }
                    } 
                    else
                    {
                        revolution.ChangeRevolutionLevel(value);
                    }  
                break;
            }
        }
    }
}