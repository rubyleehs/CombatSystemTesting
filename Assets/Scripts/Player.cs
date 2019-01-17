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

    protected override void Update()
    {
        base.Update(); 
        HandlePlayerInput();

    }

    private void HandlePlayerInput()
    {
        inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        mouseDelta = MainCamera.mousePos - (Vector2)transform.position;

        if (currentActionCoroutine != null) return;

        Move(inputAxis);
        if (inputAxis.sqrMagnitude > 0) Face(inputAxis, true);
        

        if (Input.GetButton("Fire1"))
        {
            if (mouseDelta.sqrMagnitude >= meleeClickRadius * meleeClickRadius) isAiming = true;

            Face(mouseDelta,!isAiming);
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

    }
}
