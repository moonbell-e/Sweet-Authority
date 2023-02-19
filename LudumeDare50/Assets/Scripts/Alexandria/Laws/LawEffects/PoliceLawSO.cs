using UnityEngine;

namespace Laws.Effects
{
    [CreateAssetMenu (fileName = "NewLawEffect", menuName = "Scriptable Objects/Laws/Effects/Police")]
    public class PoliceLawSO : LawEffectSO
    {
        [SerializeField]
        private PoliceLawType _type;
        [SerializeField]
        private AffectedAvtozaks _affects;
        [SerializeField]
        private bool _inPercent;
        [SerializeField]
        private float _value;

        public PoliceLawType Type => _type;
        public AffectedAvtozaks Affects => _affects;
        public bool InPercent => _inPercent;
        public float Value => _value;
    }

    public enum AffectedAvtozaks
    {
        All,
        Random
    }

    public enum PoliceLawType
    {
        Health,
        Speed,
        Capacity,
        ArrestDelay,
        Spawn
    }
}