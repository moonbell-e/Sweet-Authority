using UnityEngine;
using TMPro;
using Laws.Managers;

namespace Laws
{
    public delegate void SendLaw(Law law);

    public class Law : MonoBehaviour
    {
        public event SendLaw LawApplyed;
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _description;
        private LawContentSO _content;
        private Lawmaker _lawmaker;
        
        public void ActivateLaw()
        {
            _lawmaker.AdoptLaw(_content);
            LawApplyed?.Invoke(this);
            Destroy(gameObject);
        }

        public void Initialize(LawContentSO influence)
        {
            _lawmaker = FindObjectOfType<Lawmaker>();
            _content = influence;
            _name.text = _content.Name;
            _description.text = _content.Description;
        }
    }
}