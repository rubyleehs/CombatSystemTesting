using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombateeEntity : LiveEntity
{
    public MeleeWeapon weapon;
    public LayerMask canHitLayer;

    [Header("Debug")]
    public Transform hitbox;

    protected int attackChain = 0;
    protected AttackArt currentArt;
    protected IEnumerator currentActionCoroutine;

    protected bool isInteruptable = false;

    protected List<StatusEffect> statusEffects;

    protected virtual void Attack()
    {
        currentArt = weapon.attackArtArsenal.attackArts[weapon.attackArtChain[attackChain]];
        currentActionCoroutine = InitiateArt(currentArt);
        StartCoroutine(currentActionCoroutine);
        attackChain = (attackChain + 1) % weapon.attackArtChain.Length;
    }

    protected virtual void TakeDamage(float amount, bool canInterupt)
    {
        stats.currentHealth -= amount;
        if (canInterupt && isInteruptable) Interupt();
    }

    protected virtual void DealDamage(AttackArt art)
    {
        List<CombateeEntity> entitiesToDamage = new List<CombateeEntity>();
        GetCombateesWithinHitbox(art.hitboxInfo, canHitLayer, ref entitiesToDamage);

        for (int i = 0; i < entitiesToDamage.Count; i++)
        {
            float trueDamageDealt = 0;
            for (int a = 0; a < art.damageProperties.Length; a++)
            {
                trueDamageDealt += weapon.baseDamage * art.damageProperties[a].damageModifier; //+ defense of enemy!!
            }
            entitiesToDamage[i].TakeDamage(trueDamageDealt, art.canInterupt);
            entitiesToDamage[i].Push(art.momentumCausedAngle + Vector2.SignedAngle(Vector2.right,entitiesToDamage[i].transform.position - transform.position), art.momentumCausedMagnitude * weapon.momentumCausedMultiplier);
        }

    }

    protected override void Move(Vector2 direction)
    {
        if(currentActionCoroutine == null) base.Move(direction);
    }

    protected IEnumerator InitiateArt(AttackArt art)
    {
        currentArt = art;
        isInteruptable = true;
        //Debug.Log("Art Initiated");
        yield return new WaitForSeconds(art.windUpDuration);
        //Debug.Log("Art Wind Up End");
        float _durationBetweenHits = art.actionDuration / art.numberOfHits;

        for (int i = 0; i < art.numberOfHits; i++)
        {
            Debug.Log("lookAngle: " + lookAngle);
            this.Push(lookAngle + art.momentumInccuredAngle, art.momentumInccuredMagnitude * weapon.momentumIncurredMultiplier);
            DealDamage(art);
            yield return new WaitForSeconds(_durationBetweenHits);
        }

        isInteruptable = false;
        //Debug.Log("Art Winding Down");
        yield return new WaitForSeconds(art.windDownDuration);
        //Debug.Log("Art End");
        currentArt = null;
        currentActionCoroutine = null;
    }

    protected virtual void Interupt()
    {
        isInteruptable = false;
        StopCoroutine(currentActionCoroutine);
        currentActionCoroutine = Stun(currentArt.counterStunDuration);
        currentArt = null;
        StartCoroutine(currentActionCoroutine);


    }

    protected IEnumerator Stun(float duration)//TEMPERORY. PLS MAKE STATUS EFFECTS A SCRIPTABLE OBJECT
    {
        yield return new WaitForSeconds(duration);
    }

    private void GetCombateesWithinHitbox(HitboxInfo[] hitboxes, int checkMask, ref List<CombateeEntity> outList)
    {
        for (int i = 0; i < hitboxes.Length; i++)
        {
            GetCombateesWithinHitbox(hitboxes[i], checkMask, ref outList);
        }
    }

    private void GetCombateesWithinHitbox(HitboxInfo hitbox, int checkMask, ref List<CombateeEntity> outList)
    {
        Vector2 hitboxPos = (Vector2)transform.position + new Vector2(hitbox.position.x * transform.lossyScale.x, hitbox.position.y * transform.lossyScale.y);
        Vector2 hitboxSize = new Vector2(hitbox.size.x * transform.lossyScale.x, hitbox.size.y * transform.lossyScale.y);
        Collider2D[] collidersWithinHitbox = Physics2D.OverlapBoxAll(hitboxPos, hitboxSize, hitbox.rotation, checkMask);
        CombateeEntity _combatEntityCheck = null;

        DrawDebugHitbox(hitbox.position, hitbox.size, hitbox.rotation);

        for (int i = 0; i < collidersWithinHitbox.Length; i++)
        {
            _combatEntityCheck = collidersWithinHitbox[i].GetComponent<CombateeEntity>();

            if (_combatEntityCheck != null && !outList.Contains(_combatEntityCheck))
            {
                outList.Add(_combatEntityCheck);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (currentArt != null)
        {
            Gizmos.color = Color.red;
            HitboxInfo hitbox;
            for (int i = 0; i < currentArt.hitboxInfo.Length; i++)
            {
                hitbox = currentArt.hitboxInfo[i];
                Vector2 hitboxPos = (Vector2)transform.position + new Vector2(hitbox.position.x * transform.lossyScale.x, hitbox.position.y * transform.lossyScale.y);
                Vector2 hitboxSize = new Vector2(hitbox.size.x * transform.lossyScale.x, hitbox.size.y * transform.lossyScale.y);
                Gizmos.DrawWireCube(hitboxPos, hitboxSize);
                
            }
        }
    }

    private void DrawDebugHitbox(Vector3 relativePos, Vector2 size, float rotation)
    {
        if (hitbox == null) return;
        hitbox.transform.localScale = size;
        hitbox.transform.localRotation = Quaternion.Euler(Vector3.right * rotation);
        hitbox.transform.localPosition = relativePos;
    }
}
