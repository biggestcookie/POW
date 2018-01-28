using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Controller))]
public class InteractObject : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        GiveControl();
    }

    void GiveControl()
    {
        foreach (ControllingObject ControlObj in Resources.FindObjectsOfTypeAll<ControllingObject>())
        {
            Destroy(ControlObj);
        }
        ControllingObject ControlledObject = gameObject.AddComponent(typeof(ControllingObject)) as ControllingObject;
    }


}