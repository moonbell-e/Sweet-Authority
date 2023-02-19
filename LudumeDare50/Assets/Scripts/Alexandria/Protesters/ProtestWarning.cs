using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;
using City;

namespace Protesters
{
    public delegate void EventHappend();

    public class ProtestWarning : MonoBehaviour
    {
        public event EventHappend ProtestEnded;
        [SerializeField]
        private RectTransform _transform;
        [SerializeField]
        private Miting _miting;

        [Header ("Sound paths")]
        [SerializeField] [EventRef]
        private string _spawnMiting;
        [SerializeField] [EventRef]
        private string _endMiting;

        [Header ("People")]
        [SerializeField]
        private Slider _peopleBar;
        [SerializeField]
        private TextMeshProUGUI _peopleCount;
        [Header ("Power")]

        [SerializeField]
        private Slider _powerBar;
        [SerializeField]
        private TextMeshProUGUI _powerCount;

        public Miting Miting => _miting;
        public Slider PeopleBar => _peopleBar;
        public Slider PowerBar => _powerBar;

        public void Initialize(int maxPeople, float maxPower, RevolutionBar revolutionBar, MitingSquare square)
        {
            _miting.Initialize(revolutionBar, square);
            _peopleBar.maxValue = maxPeople;
            _peopleBar.value = maxPeople;
            _powerBar.maxValue = maxPower;
            _powerBar.value = maxPower;
            ShowCount();
            RuntimeManager.PlayOneShot(_spawnMiting);
        }
        
        public void EndProtest()
        {
            ProtestEnded?.Invoke();
            RuntimeManager.PlayOneShot(_endMiting);
            Destroy(gameObject);
            return;
        }

        public void ShowCount()
        {
            _peopleCount.text = _peopleBar.value.ToString();
            _powerCount.text = _powerBar.value.ToString();
        }
    }
}