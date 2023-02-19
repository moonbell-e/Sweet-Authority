using UnityEngine;
using System.Collections.Generic;
using Protesters;
using Police;

namespace City
{
    public delegate void OnSquare(Collider collider, Square square);

    public abstract class Square : MonoBehaviour
    {
        public event OnSquare EnteredSquare;
        public event OnSquare LeavedSquare;
        [SerializeField]
        protected Transform _center;
        protected List<AvtozakBehavior> _avtozaksOnSquare = new List<AvtozakBehavior>();

        public Vector3 Center => _center.position;
        public List<AvtozakBehavior> AvtozaksOnSquare => _avtozaksOnSquare;

        protected virtual void OnTriggerEnter(Collider other)
        {
            EnteredSquare?.Invoke(other, this);
            var avtozak = other.GetComponent<AvtozakBehavior>();
            if(avtozak == null) return;
            _avtozaksOnSquare.Add(avtozak);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            LeavedSquare?.Invoke(other, this);
            var avtozak = other.GetComponent<AvtozakBehavior>();
            if(avtozak == null) return;
            _avtozaksOnSquare.Remove(avtozak);
        }

        public virtual void LeaveSquare(AvtozakBehavior avtozak)
        {
            _avtozaksOnSquare.Remove(avtozak);
        }
    }
}