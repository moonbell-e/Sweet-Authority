using UnityEngine;

namespace Laws.Effects
{
    [CreateAssetMenu (fileName = "NewLawEffect", menuName = "Scriptable Objects/Laws/Effects/Public Opinion")]
    public class PublicOpinionLawSO : LawEffectSO
    {
        [SerializeField]
        private PublicOpinionLawType _type;
        [SerializeField]
        private bool _inPercent;
        [SerializeField]
        private float _value;

        public PublicOpinionLawType Type => _type;
        public bool InPercent => _inPercent;
        public float Value => _value;
    }

    public enum PublicOpinionLawType
    {
        Income,
        Budget
    }
}