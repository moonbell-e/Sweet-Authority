using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexityEntity : MonoBehaviour
{
    public int _mode;
    public int _meetingsDelay;
    public int _goldPerSecond;
    public float _cardChanceModifier;

    public ComplexityEntity(int mode, int meetingsDelay, int goldPerSecond, float cardChanceModifier)
    {
        _mode = mode;
        _meetingsDelay = meetingsDelay;
        _goldPerSecond = goldPerSecond;
        _cardChanceModifier = cardChanceModifier;
    }
}
