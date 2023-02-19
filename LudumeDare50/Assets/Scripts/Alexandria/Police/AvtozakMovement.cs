using UnityEngine;
using UnityEngine.AI;
using FMODUnity;
using City;
using Protesters;

namespace Police
{
    public class AvtozakMovement : MonoBehaviour
    {
        public event EventHappend ArrivedOnMiting;
        public event EventHappend LeavedMiting;
        public event EventHappend ArrivedOnPoliceStation;
        public event EventHappend LeavedPoliceStation;
        [SerializeField]
        private NavMeshAgent _agent;
        [Header ("Sound paths")]
        [SerializeField] [EventRef]
        private string _movingOn;
        [SerializeField] [EventRef]
        private string _stop;
        private AvtozakBehavior _behavior;
        private Square _onSquare;
        private Square _targetSquare;
        private bool _inMoving;

        public Square OnSquare => _onSquare;

        public void Initialize(float speed, AvtozakBehavior behavior, Square square)
        {
            _behavior = behavior;
            _agent.speed = speed;
            _onSquare = square;
            if(square == null) return;
            square.EnteredSquare += ArrivedOnSquare;
            ArrivedOnSquare(gameObject.GetComponent<Collider>(), square);
        }

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
            if(_inMoving && Random.Range(0, 7) < 6) return;
            RuntimeManager.PlayOneShot(_movingOn);
            _inMoving = true;
        }
        
        public void MoveToSquare(Vector3 point, Square square)
        {
            MoveToPoint(point);
            if(square == _onSquare || square == _targetSquare) return;
            if(_targetSquare != null) _targetSquare.EnteredSquare -= ArrivedOnSquare;
            square.EnteredSquare += ArrivedOnSquare;
            _targetSquare = square;
        }
        
        private void PlayStopSound()
        {
            if(_inMoving == false) return;
            RuntimeManager.PlayOneShot(_stop);
            _inMoving = false;
        }
        
        private void ArrivedOnSquare(Collider collider, Square square)
        {
            PlayStopSound();
            var avtozak = collider.GetComponent<AvtozakMovement>();
            if(avtozak != this) return;
            _onSquare = square;
            _onSquare.LeavedSquare += LeaveSquare;
            _targetSquare = null;
            square.EnteredSquare -= ArrivedOnSquare;
            var miting = square.GetComponent<MitingSquare>();
            if(miting != null)
            {
                miting.MitingStarted += _behavior.StartArrests;
                miting.MitingEnded += _behavior.EndArrests;
                if(miting.Miting == null) return;
                ArrivedOnMiting?.Invoke();
            }
            else
            {
                var policeStation = square.GetComponent<PoliceStation>();
                if(policeStation == null) return;
                ArrivedOnPoliceStation?.Invoke();
            }
        }

        private void LeaveSquare(Collider collider, Square square)
        {
            var avtozak = collider.GetComponent<AvtozakMovement>();
            if(avtozak != this) return;
            _onSquare = null;
            square.LeavedSquare -= LeaveSquare;
            var miting = square.GetComponent<MitingSquare>();
            if(miting != null)
            {
                miting.MitingStarted -= _behavior.StartArrests;
                miting.MitingEnded -= _behavior.EndArrests;
                if(miting.Miting == null) return;
                LeavedMiting?.Invoke();
            }
            else
            {
                var policeStation = square.GetComponent<PoliceStation>();
                if(policeStation == null) return;
                LeavedPoliceStation?.Invoke();
            }
        }
    }
}