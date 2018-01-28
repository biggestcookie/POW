using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    private Vector2 velocity;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        //if (player != null)
        {
            float posX = player.transform.position.x / 2 + 10;
            this.transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }
    }

}