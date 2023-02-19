using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

namespace Protesters
{
    public delegate void ValueChanged(float value);

    public class RevolutionBar : MonoBehaviour
    {
        public event EventHappend RevolutionLevelMaximum;
        public event ValueChanged RevolutionLevelChanged;
        [SerializeField]
        private Slider _bar;
        [SerializeField]
        private float _passiveMoodChanging;
        [SerializeField]
        private float _passiveMoodChangingPeriod;
        [SerializeField] [EventRef]
        private string _crowdSound;
        private FMOD.Studio.EventInstance instance;
        private float _moodСhanging;

        public float Level => _bar.value;
        public float PassiveMoodChanging => _passiveMoodChanging;
        public float MoodChanging => _moodСhanging;

        private void Awake()
        {
            instance = RuntimeManager.CreateInstance(_crowdSound);
            instance.start();
            _passiveMoodChangingPeriod *= 100;    
        }

        private void FixedUpdate() 
        {
            var passiveMoodChanging = _passiveMoodChanging * Time.fixedDeltaTime / _passiveMoodChangingPeriod;
            ChangeRevolutionLevel(_moodСhanging + passiveMoodChanging);
        }

        public void ChangeMoodChanging(float value)
        {
            _moodСhanging += value;
        }

        public void ChangePassiveMoodChanging(float value)
        {
            _passiveMoodChanging += value;
        }

        public void SetPassiveMoodChanging(float moodChanging)
        {
            _passiveMoodChanging = moodChanging;
        }

        public void ChangeRevolutionLevel(float value)
        {
            int oldValue = Mathf.FloorToInt(_bar.value);
            _bar.value += value;
            if(_bar.value >= _bar.maxValue)
            {
                instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                instance.release();
                RevolutionLevelMaximum?.Invoke();
                return;
            }
            if(_bar.value <= 0) _bar.value = 0;
            if(oldValue != Mathf.FloorToInt(_bar.value)) RevolutionLevelChanged?.Invoke(_bar.value);
            instance.setParameterByName("time", _bar.value/_bar.maxValue);
        }
    }
}