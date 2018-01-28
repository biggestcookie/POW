using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject car;
    public GameObject robot;
    PlayerInput carInput, robotInput;

    // Use this for initialization
    void Start()
    {
        carInput = car.GetComponent<PlayerInput>();
        robotInput = robot.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            carInput.isActive = true;
            robotInput.isActive = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            carInput.isActive = false;
            robotInput.isActive = true;
        }
    }
}
