using UnityEngine;

namespace Laws.Effects
{
    public abstract class LawEffectSO : ScriptableObject
    {
        [SerializeField]
        protected LawScope _scope;
        [SerializeField] [Range (0, 10000)]
        protected float _duration;
        [SerializeField] [Range (0, 10000)]
        protected float _delay;
        
        public LawScope Scope => _scope;
        public float Duration => _duration;
        public float Delay => _delay;
    }

    public enum LawScope
    {
        Tax,
        PublicOpinion,
        Mitings,
        Police
    }
}