using UnityEngine;

public class SettingEntity : MonoBehaviour
{
    public int _id;
    public int _complexity;
    public int _resolution;
    public int _quality;
    public int _fullscreen;
    public float _music;
    public float _SFX;

    public SettingEntity(int id, int res, int quality, int fullscreen, float music, float SFX)
    {
        _id = id;
        _resolution = res;
        _quality = quality;
        _fullscreen = fullscreen;
        _music = music; 
        _SFX = SFX;
    }
}
