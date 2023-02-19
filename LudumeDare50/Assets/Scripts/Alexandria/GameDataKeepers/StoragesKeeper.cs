using UnityEngine;
using Protesters;
using Laws.Managers;

namespace GameDataKeepers
{
    public class StoragesKeeper : MonoBehaviour
    {
        [SerializeField]
        private MoneySystem _moneySystem;
        [SerializeField]
        private RevolutionBar _revolutionBar;
        [SerializeField]
        private MitingsDataStorage _mitingsStorage;
        [SerializeField]
        private PoliceDataStorage _policeStorage;
        [SerializeField]
        private LawsKeeper _lawsKeeper;
        [SerializeField]
        private PauseSystem _pauseSystem;

        public MoneySystem MoneySystem => _moneySystem;
        public RevolutionBar RevolutionBar => _revolutionBar;
        public MitingsDataStorage MitingsStorage => _mitingsStorage;
        public PoliceDataStorage PoliceStorage => _policeStorage;
        public LawsKeeper LawsKeeper => _lawsKeeper;
        public PauseSystem PauseSystem => _pauseSystem;
    }
}