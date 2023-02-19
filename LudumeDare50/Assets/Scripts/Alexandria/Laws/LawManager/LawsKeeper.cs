using UnityEngine;
using System;

namespace Laws.Managers
{
    public class LawsKeeper : MonoBehaviour
    {
        [SerializeField]
        private LawTier[] _lawTiers;

        public LawTier[] LawTiers => _lawTiers;
    }

    [Serializable]
    public class LawTier
    {
        [SerializeField]
        private int _minimalRevolutionLevel;
        [SerializeField] [Range (1, 100)]
        private int _weight;
        [SerializeField]
        private LawContentSO[] _lawsContent;
        
        public int MinimalLevel => _minimalRevolutionLevel;
        public int Weight => _weight;
        public LawContentSO[] LawsContent => _lawsContent;
    }
}