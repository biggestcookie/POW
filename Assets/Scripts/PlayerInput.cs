using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    public bool isActive;
    Player player;
    GameObject[] players;
    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if (isActive)
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);
            if (Input.GetButtonDown("Fire1"))
            {
                player.OnJumpInputDown();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                player.OnJumpInputUp();
            }
            if (Input.GetButtonUp("Fire2"))
            {
                isActive = true;
            }
        }
    }
    void OnMouseDown()
    {
        foreach (GameObject gameObject in players)
        {
            gameObject.GetComponent<PlayerInput>().isActive = false;
            gameObject.GetComponent<Player>().directionalInput = Vector2.zero;
        }
        isActive = true;
    }
}