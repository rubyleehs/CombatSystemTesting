using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageProperties{

}
[System.Serializable]
public struct MagicalDamageProperties : IDamageProperties
{
    public float damageAmount;
    public MagicalDamageType damageType;
}

[System.Serializable]
public struct PhysicalDamageProperties : IDamageProperties
{
    public float damageModifier;
    public PhysicalDamageType damageType;
}
