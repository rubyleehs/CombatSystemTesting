using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public float durationRemaining;
    public float durationToNextApply;
    public float applyIntervalDuration;

    public float currentValue;
    public float valuePerApply;

    public abstract void Apply(CombateeEntity combatee, ref float value);
}
