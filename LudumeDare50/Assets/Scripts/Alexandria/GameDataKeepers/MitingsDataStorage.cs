using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Protesters;
using City;

namespace GameDataKeepers
{
    public class MitingsDataStorage : MonoBehaviour
    {
        private List<MitingSquare> _mitingSquares;
        private List<Miting> _mitings;

        public List<MitingSquare> MitingSquares => _mitingSquares;
        public List<Miting> Mitings => _mitings;

        private void Awake()
        {
            _mitingSquares = FindObjectsOfType<MitingSquare>().ToList();
            _mitings = new List<Miting>();
            foreach(var square in _mitingSquares)
            {
                square.MitingSpawned += AddMiting;
                square.MitingDespawned += RemoveMiting;
            }
        }

        private void AddMiting(Miting miting)
        {
            _mitings.Add(miting);
        }

        private void RemoveMiting(Miting miting)
        {
            _mitings.Remove(miting);
        }
    }
}