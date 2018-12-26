using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/MeleeWeapon")]
public class MeleeWeapon : ScriptableObject
{
    public float baseDamage;

    public MagicalDamageProperties[] damageProperties; //magical propeties: holy,dark,fire,lightning
    public AttackArtArsenal attackArtArsenal; //all possible attack arts

}
