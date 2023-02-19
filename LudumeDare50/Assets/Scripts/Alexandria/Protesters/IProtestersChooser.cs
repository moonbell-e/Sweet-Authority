using UnityEngine;
using City;

namespace Protesters
{
    public delegate void ProtestersChoosed(int people, int power, MitingSquare square);

    public interface IProtestersChooser
    {
        public event ProtestersChoosed ProtestersChoosed;
    }
}