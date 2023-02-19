using UnityEngine;

namespace Laws.Effects
{
    [CreateAssetMenu (fileName = "NewLawEffect", menuName = "Scriptable Objects/Laws/Effects/Mitings")]
    public class MitingsLawSO : LawEffectSO
    {
        [SerializeField]
        private MitingsLawType _type;
        [SerializeField]
        private bool _inPercent;
        [SerializeField]
        private float _value;

        public MitingsLawType Type => _type;
        public bool InPercent => _inPercent;
        public float Value => _value;
    }

    public enum MitingsLawType
    {
        People,
        Power
    }
}