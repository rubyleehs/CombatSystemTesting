using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CombateeEntity
{
    
    public float meleeClickRadius;

    private Vector2 inputAxis;
    private Vector2 mouseDelta;


    [HideInInspector]
    public bool isAiming;
    protected override void Awake()
    {
        base.Awake();
        currentArt = weapon.attackArtArsenal.attackArts[0];
    }

    private void Update()
    {
        HandlePlayerInput();

    }

    protected override void Move(Vector2 direction)
    {
        base.Move(direction);
    }

    private void HandlePlayerInput()
    {
        inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        mouseDelta = MainCamera.mousePos - (Vector2)transform.position;

        if (currentActionCoroutine != null) return;

        Move(inputAxis);

        if (Input.GetButton("Fire1"))
        {
            Look(mouseDelta);

            if (mouseDelta.sqrMagnitude >= meleeClickRadius * meleeClickRadius) isAiming = true;
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            if (isAiming)
            {
                Debug.Log("Ranged");
                isAiming = false;
            }
            else
            {
                Debug.Log("Melee");
                Attack();
            }
        }

        else
        {
            if (inputAxis.sqrMagnitude > 0) Look(inputAxis);//

        }
    }
}
