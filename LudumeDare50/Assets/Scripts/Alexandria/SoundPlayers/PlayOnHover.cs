using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

namespace SoundPlayers
{
    public class PlayOnHover : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] [EventRef]
        private string _soundPath;

        public void OnPointerEnter(PointerEventData eventData)
        {
            RuntimeManager.PlayOneShot(_soundPath);    
        } 
    }
}