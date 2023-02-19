using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GameDataKeepers;

namespace InputSystem
{
    public class InputDetector : MonoBehaviour
    {   
        [SerializeField]
        private InputHandler _inputHandler;
        [SerializeField]
        private LayerMask _ignoreLayers;
        [SerializeField]
        private LayerMask[] _selectableLayers;
        private EventSystem _eventSystem;
        private bool _inputDeactivated;

        private void Awake()
        {
            _eventSystem = FindObjectOfType<EventSystem>();
            FindObjectOfType<StoragesKeeper>().RevolutionBar.RevolutionLevelMaximum += DeactivateInput;
            _inputDeactivated = false;
        }

        private void Update()
        {
            if(_inputDeactivated) return;
            if(Input.GetMouseButtonDown(0)) LeftMouseButton();
            else if(Input.GetMouseButtonDown(1)) RightMouseButton();
        }

        public void DeactivateInput()
        {
            _inputDeactivated = true;
        }

        public void ActivateInput()
        {
            _inputDeactivated = false;
        }

        private void LeftMouseButton()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            for (int i = 0; i < _selectableLayers.Length; i++)
            {
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, _selectableLayers[i]))
                {
                    _inputHandler.SelectUnit(hit);
                    return;
                } 
            }

            PointerEventData pointerEventData = new PointerEventData(_eventSystem);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            _eventSystem.RaycastAll(pointerEventData, results);
 
            if(results.Count > 0) return;
            _inputHandler.DeselectObject();
        }

        private void RightMouseButton()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, Mathf.Infinity, _ignoreLayers)) 
            {
                _inputHandler.DeselectObject();
                return;
            }
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) _inputHandler.HandleInput(hit);
        }
    }
}