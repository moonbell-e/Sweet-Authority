using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VcaController : MonoBehaviour
{
    private FMOD.Studio.VCA _vcaController;
    private Slider _slider;
    public string VcaName;

    private void Awake()
    {
        _vcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        _slider = GetComponent<Slider>();
    }

    public void SetVolume(float volume)
    {
        Debug.Log("SetVolume");
        MyDataBaseValuesMenu.Instance.UpdateMusicVolume(1f);
        _vcaController.setVolume(volume);
    }

    public void ResetVolume()
    {
        _vcaController.setVolume(1f);
        MyDataBaseValuesMenu.Instance.UpdateMusicVolume(1f);
        _slider.value = 1f;
    }
}
