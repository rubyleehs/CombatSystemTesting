using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackArt : ScriptableObject {

    public PhysicalDamageProperties[] damageProperties;

    public int windUpDuration;
    public int chargeDuration;
    public int actionDuration;
    public int windDownDuration;

    //stun time if damaged during windup/charge, 0 means no stun
    public int counterStunDuration = 0;
    public float maxChargeDamageModifier;

    public abstract void Initiate();  
}
