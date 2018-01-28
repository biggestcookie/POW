using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    static Vector2 FirstPosition;
    public bool isActive;
    Player player;
    GameObject[] playerList;
    GameObject lightning;
    new GameObject camera;
    private void Start()
    {
        lightning = GameObject.FindGameObjectWithTag("Lightning");
        playerList = GameObject.FindGameObjectsWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        player = GetComponent<Player>();
        if (isActive)
        {
            FirstPosition = transform.position;
        }
    }
    private void Update()
    {
        if (isActive)
        {
            player.SetActive(isActive);
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);
            FirstPosition = transform.position;
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

            players.GetComponent<PlayerInput>().isActive = false;
            players.GetComponent<Player>().directionalInput = Vector2.zero;
            players.GetComponent<Player>().SetActive(false);
        }
        camera.GetComponent<Tracker>().active = gameObject;
        isActive = true;

        // Set Scale
        float SizeScale = 5.0f;
        Vector2 NewSize = new Vector2();
        SpriteRenderer lightningRenderer = lightning.GetComponent<SpriteRenderer>();
        print(Vector3.Distance(FirstPosition, transform.position) / SizeScale);
        NewSize.Set(Vector3.Distance(transform.position, FirstPosition) / SizeScale, lightningRenderer.size.y);
        lightningRenderer.size = NewSize;

        // Set Rotation
        lightning.transform.rotation = Quaternion.Euler(0, 0, 0);
        lightning.transform.Rotate(0, 0, AngleBetweenVector2(FirstPosition, transform.position));

        // Set Position
        float posOffset = 0.0f;
        if (transform.position.x >= FirstPosition.x)
        {
            posOffset = lightning.GetComponent<Renderer>().bounds.size.x / 2;
        }
        else if (transform.position.x < FirstPosition.x)
        {
            posOffset = -(lightning.GetComponent<Renderer>().bounds.size.x / 2);
        }
        Vector2 VectorOffset = new Vector2();
        VectorOffset.Set(posOffset, 0);
        lightning.transform.position = FirstPosition + VectorOffset;

        FirstPosition = transform.position;
    }
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}