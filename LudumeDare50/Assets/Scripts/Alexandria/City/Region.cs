using UnityEngine;
using System;
using System.Collections;
using Protesters;

namespace City
{
    public class Region : MonoBehaviour, IProtestersChooser
    {
        public event ProtestersChoosed ProtestersChoosed;
        [SerializeField]
        private MitingSquare[] _squares;
        [SerializeField]
        private RevolutionStage[] _stages;
        [SerializeField]
        private float _meetingsDelay;
        private int _curStageIndex;
        private bool _revolutionStarted;

        private void Awake()
        {
            FindObjectOfType<RevolutionBar>().RevolutionLevelChanged += ChangeStage;;
            StartCoroutine(ReadMeetingsDelay());
            _revolutionStarted = false;
            ChangeStage(0);
        }

        private void ChangeStage(float level)
        {
            if(_stages[0].MinRevolutionLevel > level)
            {
                _curStageIndex = -1;
                StopAllCoroutines();
                _revolutionStarted = false;
                return;
            }
            for(int i = 0; i < _stages.Length; i++)
            {
                if(_stages[i].MinRevolutionLevel > level)
                {
                    _curStageIndex = i - 1;
                    if(_revolutionStarted == false)
                    {
                        StartCoroutine(RevolutionDelay());
                        _revolutionStarted = true;
                    }
                    return;
                }
            }
            _curStageIndex = _stages.Length - 1;
            if(_revolutionStarted == false) 
            {
                StartCoroutine(RevolutionDelay());
                _revolutionStarted = true;
            }
        }

        private IEnumerator RevolutionDelay()
        {
            var stage = _stages[_curStageIndex];
             //UnityEngine.Random.Range(stage.MinDelay, stage.MaxDelay); 
            yield return new WaitForSeconds(_meetingsDelay);
            ChooseProtestors();
            StartCoroutine(RevolutionDelay());
        }

        private void ChooseProtestors()
        {
            var stage = _stages[_curStageIndex];
            int people = UnityEngine.Random.Range(stage.MinNumber, stage.MaxNumber + 1);
            int power = UnityEngine.Random.Range(stage.MinPower, stage.MaxPower + 1);
            var square = _squares[UnityEngine.Random.Range(0, _squares.Length)];
            if(square.Miting != null) square.Miting.ChangeProtesters(people, power);
            else ProtestersChoosed?.Invoke(people, power, square);
        }

        private IEnumerator ReadMeetingsDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _meetingsDelay = _meetingsDelay = MyDataBaseValues.Instance.ParseToInt(MyDataBaseValues.Instance.meetingDelay);
        }
    }



    [Serializable]
    public class RevolutionStage
    {
        [SerializeField]
        private int _minRevolutionLevel;
        [SerializeField]
        private float _minDelay;
        [SerializeField]
        private float _maxDelay;
        [SerializeField]
        private int _minNumber;
        [SerializeField]
        private int _maxNumber;
        [SerializeField]
        private int _minPower;
        [SerializeField]
        private int _maxPower;

        public int MinRevolutionLevel => _minRevolutionLevel;
        public float MinDelay => _minDelay;
        public float MaxDelay => _maxDelay;
        public int MinNumber => _minNumber;
        public int MaxNumber => _maxNumber;
        public int MinPower => _minPower;
        public int MaxPower => _maxPower;
    }
}