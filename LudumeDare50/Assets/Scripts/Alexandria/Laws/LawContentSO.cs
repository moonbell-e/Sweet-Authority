using UnityEngine;
using Laws.Effects;

namespace Laws
{
    [CreateAssetMenu(fileName = "NewLaw", menuName = "Scriptable Objects/Laws/Content")]
    public class LawContentSO : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField] [TextArea (5, 10)]
        private string _description;
        [Header ("Law effects")]
        [SerializeField]
        private LawEffectSO[] _effects;

        public string Name => _name;
        public string Description => _description;
        public LawEffectSO[] Effects => _effects;
    }
}