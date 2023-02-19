using UnityEngine;

namespace Laws.Effects
{
    [CreateAssetMenu (fileName = "NewLawEffect", menuName = "Scriptable Objects/Laws/Effects/Tax")]
    public class TaxLawSO : LawEffectSO
    {
        [SerializeField]
        private TaxLawType _type;
        [SerializeField]
        private bool _inPercent;
        [SerializeField]
        private float _value;
        
        public TaxLawType Type => _type;
        public bool InPercent => _inPercent;
        public float Value => _value;
    }

    public enum TaxLawType
    {
        Income,
        Budget
    }
}