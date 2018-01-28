using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    public bool isActive;
    Player player;
    GameObject[] playerList;
    new GameObject camera;
    private void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GetComponent<Player>();

    }
    private void Update()
    {
        if (isActive)
        {
            player.SetActive(isActive);
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
            if (Input.GetButtonUp("Fire3"))
            {
                player.OnAbilityInputDown();
            }
        }
    }
    void OnMouseDown()
    {
        foreach (GameObject players in playerList)
        {
            players.GetComponent<PlayerInput>().isActive = false;
            players.GetComponent<Player>().directionalInput = Vector2.zero;
            players.GetComponent<Player>().SetActive(false);
        }
        camera.GetComponent<Tracker>().active = gameObject;
        isActive = true;
    }
}