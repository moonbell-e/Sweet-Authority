using UnityEngine;
using System.Collections.Generic;
using FMODUnity;
using GameDataKeepers;
using System.Collections;

namespace Laws.Managers
{
    public class LawChooser : MonoBehaviour
    {
        [SerializeField]
        private GameObject _law;
        [SerializeField]
        private Transform _lawsPoint;
        [SerializeField] [EventRef]
        private string _lawChoosed;
        [SerializeField] [Range (0, 100)]
        private int _emptyChance;
        [SerializeField]
        private int _lawLimit;
        private StoragesKeeper _storagesKeeper;
        private List<Law> _laws;

        private void Start()
        {
            StartCoroutine(ReadCardChance());
            _laws = new List<Law>(_lawLimit);
            _storagesKeeper = FindObjectOfType<StoragesKeeper>();
            var squares = _storagesKeeper.MitingsStorage.MitingSquares;
            foreach(var square in squares)
                square.MitingEnded += SpawnLaw;
        }

        private LawContentSO ChooseRandomLawContent()
        {
            LawTier lawTier = ChooseRandomLawTier();
            if(lawTier == null) return null;
            int randomLawIndex = Random.Range(0, lawTier.LawsContent.Length);
            return lawTier.LawsContent[randomLawIndex];
        }

        private LawTier ChooseRandomLawTier()
        {
            var lawTiers = _storagesKeeper.LawsKeeper.LawTiers;
            float level = _storagesKeeper.RevolutionBar.Level;
            List<LawTier> availableTiers = new List<LawTier>(); 
            int totalWeight = 0;
            for(int i = 0; i < lawTiers.Length; i++)
            {
                if(lawTiers[i].MinimalLevel > level) 
                {
                    break;
                }
                else 
                {
                    availableTiers.Add(lawTiers[i]);
                    totalWeight += lawTiers[i].Weight;
                }
            }
            if(availableTiers.Count == 0) return null;
            int randomResult = Random.Range(0, totalWeight);
            for(int i = 0; i < availableTiers.Count; i++)
            {
                if(randomResult <= availableTiers[i].Weight) return availableTiers[i]; 
                else randomResult -= availableTiers[i].Weight;
            }
            return availableTiers[availableTiers.Count - 1];
        }

        private void RemoveLaw(Law law)
        {
            _laws.Remove(law);
        }

        private void SpawnLaw()
        {
            if(_laws.Count >= _lawLimit) return;
            if(Random.Range(1, 101) <= _emptyChance) return;
            LawContentSO lawContent = ChooseRandomLawContent();
            if(lawContent == null) return;
            var law = Instantiate(_law, _lawsPoint.position, Quaternion.identity, _lawsPoint).GetComponent<Law>();
            law.Initialize(lawContent);
            _laws.Add(law);
            law.LawApplyed += RemoveLaw;
            RuntimeManager.PlayOneShot(_lawChoosed);
        }

        private IEnumerator ReadCardChance()
        {
            yield return new WaitForSeconds(0.5f);
            _emptyChance = MyDataBaseValues.Instance.ParseToInt(MyDataBaseValues.Instance.cardChanceModifier);
        }
    }
}