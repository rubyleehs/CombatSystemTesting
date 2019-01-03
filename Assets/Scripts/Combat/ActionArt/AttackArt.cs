using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackArt : ScriptableObject {
    public HitboxInfo[] hitboxInfo;
    public PhysicalDamageProperties[] damageProperties;

    public int numberOfHits = 1;

    public float windUpDuration;
    public float chargeDuration;
    public float actionDuration;
    public float windDownDuration;

    //stun time if damaged during windup/charge, 0 means no stun
    public float counterStunDuration = 0;
    public float maxChargeDamageModifier;

    public bool canInterupt;
}
