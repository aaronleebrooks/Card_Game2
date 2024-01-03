using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_Effect : ScriptableObject
{
    public List<TargetType> possibleTargets; // Enum of possible targets
    public abstract void DoEffect(Card card);
}
