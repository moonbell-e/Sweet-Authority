using UnityEngine;
using System.Linq;
using City;

namespace Protesters
{
    public class ProtestersSpawner : MonoBehaviour
    { 
        [SerializeField]
        private GameObject _protestorsWarning;
        private RevolutionBar _revolutionBar;

        private void Awake()
        {
            _revolutionBar = FindObjectOfType<RevolutionBar>();
            var protestersChoosers = FindObjectsOfType<MonoBehaviour>().OfType<IProtestersChooser>();
            foreach(var pc in protestersChoosers)
                pc.ProtestersChoosed += SpawnProtestors;
        }

        public void SpawnProtestors(int maxPeople, int maxPower, MitingSquare square)
        {
            var HUD = square.HUD;
            var protest = Instantiate(_protestorsWarning, HUD.position, HUD.rotation, square.HUD).GetComponent<ProtestWarning>();
            protest.Initialize(maxPeople, maxPower, _revolutionBar, square);
            square.StartMiting(protest);
        }
    }
}