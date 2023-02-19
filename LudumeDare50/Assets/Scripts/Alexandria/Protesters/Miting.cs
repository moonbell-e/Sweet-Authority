using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using City;
using Police;

namespace Protesters
{
    public class Miting : MonoBehaviour
    {
        [SerializeField]
        private ProtestWarning _protestWarning;
        [SerializeField]
        private float _revolutionPeriod;
        [SerializeField]
        private float _resistPeriod;
        private RevolutionBar _revolutionBar;
        private MitingSquare _square;
        private bool _resisting;
        private float _moodChanging;

        public ProtestWarning Protest => _protestWarning;
        public bool Resisting => _resisting;

        public void Initialize(RevolutionBar revolutionBar, MitingSquare square)
        {
            _revolutionPeriod *= 100;
            SetNewMoodChanging();
            _revolutionBar = revolutionBar;
            _revolutionBar.ChangeMoodChanging(_moodChanging);
            _square = square;
            _resisting = false;
        }

        private void ReturnOldMoodChanging()
        {
            _revolutionBar.ChangeMoodChanging(-_moodChanging);
        }

        private void SetNewMoodChanging()
        {
            _moodChanging = _protestWarning.PowerBar.value * Time.fixedDeltaTime / _revolutionPeriod;
        }

        public void StartResist()
        {
            _resisting = true;
            ReturnOldMoodChanging();
            StartCoroutine(AttackAvtozaks());
        }

        public void EndResist()
        {
            StopAllCoroutines();
            _revolutionBar.ChangeMoodChanging(_moodChanging);
            _resisting = false;
        }

        private IEnumerator AttackAvtozaks()
        {
            yield return new WaitForSeconds(_resistPeriod);
            List<AvtozakBehavior> avtozaks = new List<AvtozakBehavior>();
            foreach(var avtozak in _square.AvtozaksOnSquare)
                avtozaks.Add(avtozak);
            var damage = _protestWarning.PowerBar.value / avtozaks.Count;
            foreach(var avtozak in avtozaks)
                avtozak.TakeDamage(damage);
            if(avtozaks.Count > 0) StartCoroutine(AttackAvtozaks());
            else EndResist();
        }

        public void ChangeProtesters(int newPeople, float newPower)
        {
            var people = _protestWarning.PeopleBar;
            var power = _protestWarning.PowerBar;
            people.maxValue += newPeople;
            people.value += newPeople;
            power.maxValue += newPower;
            power.value += newPower;
            if(_resisting == false) 
            {
                ReturnOldMoodChanging();
                SetNewMoodChanging();
                _revolutionBar.ChangeMoodChanging(_moodChanging);
            }
            else 
            {
                SetNewMoodChanging();
            }
            if(people.value <= 0 || power.value <= 0) 
                _protestWarning.EndProtest();
            _protestWarning.ShowCount();
        }

        public void ArrestPeople()
        {
            var people = _protestWarning.PeopleBar;
            var power = _protestWarning.PowerBar;
            people.value --;
            if(people.value <= 0) 
            {
                _protestWarning.EndProtest();
                return;
            }
            float percent = people.value / people.maxValue;
            power.value = power.maxValue * percent;
            if(_resisting == false) 
            {
                ReturnOldMoodChanging();
                SetNewMoodChanging();
                _revolutionBar.ChangeMoodChanging(_moodChanging);
            }
            else 
            {
                SetNewMoodChanging();
            }
            _protestWarning.ShowCount();
        }
    }
}