using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageType : ScriptableObject
{
}

[CreateAssetMenu(menuName = "DamageType/Magical")]
public class MagicalDamageType: DamageType
{

}

[CreateAssetMenu(menuName = "DamageType/Physical")]
public class PhysicalDamageType : DamageType
{

}