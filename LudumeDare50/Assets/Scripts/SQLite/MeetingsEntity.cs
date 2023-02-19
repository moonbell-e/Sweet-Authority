using UnityEngine;

public class MeetingsEntity : MonoBehaviour
{
    public int _ID;
    public int _people;
    public int _force;

    public MeetingsEntity(int id, int people, int force)
    {
        _ID = id;
        _people = people;
        _force = force;
    }
}
