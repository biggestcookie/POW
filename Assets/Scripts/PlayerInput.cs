using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    public bool isActive;
    Player player;
    PlayerInput input;
    GameObject[] playerList;
    new GameObject camera;
    Light lighter;
    private void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GetComponent<Player>();
        lighter = gameObject.GetComponentInChildren(typeof(Light)) as Light;
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
            if (Input.GetButtonDown("Fire3"))
            {
                player.OnAbilityInputDown();
            }
        }
    }
    void OnMouseDown()
    {
        foreach (GameObject players in playerList)
        {
            input = players.GetComponent<PlayerInput>();
            input.isActive = false;
            players.GetComponent<Player>().directionalInput = Vector2.zero;
            if (input.lighter != null)
            {
                input.lighter.intensity = 0;
            }

        }
        camera.GetComponent<Tracker>().active = gameObject;
        AddLight();
        isActive = true;
    }
    void AddLight()
    {
        if (lighter != null)
        {
            lighter.intensity = 3;
        }
    }
}