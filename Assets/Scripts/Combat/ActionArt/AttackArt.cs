using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArt : ScriptableObject {
    public HitboxInfo[] hitboxInfo;
    public PhysicalDamageProperties[] damageProperties;

    public int numberOfHits = 1;
    public float momentumInccuredAngle;
    public float momentumInccuredMagnitude;

    public float momentumCausedAngle;
    public float momentumCausedMagnitude;

    public float windUpDuration;
    public float chargeDuration;
    public float actionDuration;
    public float windDownDuration;

    //stun time if damaged during windup/charge, 0 means no stun
    public float counterStunDuration = 0;
    public float maxChargeDamageModifier;

    public bool canInterupt;
}
