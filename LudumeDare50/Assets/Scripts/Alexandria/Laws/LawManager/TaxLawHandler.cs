using UnityEngine;
using System.Collections;
using Laws.Effects;
using GameDataKeepers;

namespace Laws.Managers
{
    public class TaxLawHandler : MonoBehaviour
    {
        private float _bufferValue;

        public void ActivateEffect(StoragesKeeper storagesKeeper, TaxLawSO effect, float duration, float delay)
        {
            StartCoroutine(HandleEffect(storagesKeeper, effect, duration, delay));
        }

        private IEnumerator HandleEffect(StoragesKeeper storagesKeeper, TaxLawSO effect, float duration, float delay)
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

        private void ApplyEffect(StoragesKeeper storagesKeeper, TaxLawSO effect, float duration, bool reverse, float rememberValue)
        {
            var moneySystem = storagesKeeper.MoneySystem;
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
                case TaxLawType.Income:
                    if(effect.InPercent) 
                        if(reverse)
                        {
                            moneySystem.ChangeIncome(moneySystem.Income + (int)rememberValue);
                        }
                        else 
                        {
                            var newIncome = (int)(value * moneySystem.Income);
                            if(effect.Duration > 0) _bufferValue = moneySystem.Income - newIncome;
                            moneySystem.ChangeIncome(newIncome);
                        }
                    else
                        moneySystem.ChangeIncome((int)value + moneySystem.Income);
                break;

                case TaxLawType.Budget:
                    if(effect.InPercent) 
                        if(reverse)
                        {
                            moneySystem.ChangeMoneyCount((int)rememberValue);
                        }
                        else 
                        {    
                            var newMoney = (int)(value * moneySystem.MoneyAmount);
                            if(effect.Duration > 0) _bufferValue = moneySystem.MoneyAmount - newMoney;
                            moneySystem.MoneyAmount = newMoney;
                        }
                    else
                        moneySystem.ChangeMoneyCount((int)value);
                break;
            }
        }
    }
}