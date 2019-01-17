using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStatusEffect : MonoBehaviour {

    public bool isBeneficial;
    public bool isOngoing;

    public float durationToExpiry;

    //Initial Effect
    public float initialEffectValue;//(one)maybe dont this in this class?? since its not going to be used anyway.

    //Persistant Effect
    public float persistantEffectValue;//(one)

    //Tick Effect
    public float intervalBetweenTickEffect;
    public float durationToNextTickEffect;
    public float valuePerTick;

    //End Effect
    public float endEffectValue;//(one)

    protected abstract void OnInitialEffect(LiveEntityStats stats);
    protected abstract void SetPersistantEffect(LiveEntityStats stats);
    protected abstract void EndPersistantEffect(LiveEntityStats stats);
    protected abstract void OnTickEffect(LiveEntityStats stats);
    protected abstract void OnEndEffect(LiveEntityStats stats);


    public void EffectUpdate(LiveEntityStats stats)
    {
        if (!isOngoing && durationToExpiry >= 0)
        {
            OnInitialEffect(stats);
            SetPersistantEffect(stats);
        }

        if (isOngoing && durationToNextTickEffect <= 0)
        {
            durationToNextTickEffect = intervalBetweenTickEffect + durationToNextTickEffect;
            OnTickEffect(stats);
        }

        durationToExpiry -= GameManager.deltaTime;
        durationToNextTickEffect -= GameManager.deltaTime;
        isOngoing = durationToExpiry > 0;

        if (!isOngoing)
        {
            EndPersistantEffect(stats);
            OnEndEffect(stats);
        }
    }
}
