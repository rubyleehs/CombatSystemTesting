using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float deltaTime;
    public static Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;    
    }

    private void Update()
    {
        deltaTime = Time.deltaTime;
        Tick(deltaTime);
    }

    public void Tick(float dt)
    {
        //Do shit
    }
}
