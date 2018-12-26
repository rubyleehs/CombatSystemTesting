using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionArt/Attack/Melee")]
public class MeleeArt : AttackArt
{
    public HitboxInfo[] hitboxInfo;

    public Vector3 meleeStartPos;
    public Vector3 meleeEndPos;
 
    public override void Initiate()
    {
    }

}
