using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    static Vector2 FirstPosition;
    public bool isActive;
    Player player;
    PlayerInput input;
    GameObject[] playerList;
    //public GameObject lightning;
    new GameObject camera;
    Light lighter;
    private void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GetComponent<Player>();
        lighter = gameObject.GetComponentInChildren(typeof(Light)) as Light;
        //lightning = GameObject.FindGameObjectWithTag("Lightning");
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
        /*float SizeScale = 1.0f;
        Vector2 NewSize = new Vector2();
        // SpriteRenderer lightningRenderer = lightning.GetComponent<SpriteRenderer>();
        print(Vector3.Distance(FirstPosition, transform.position) / SizeScale);
        NewSize.Set(Vector3.Distance(transform.position, FirstPosition) / SizeScale, lightningRenderer.size.y);
        //lightningRenderer.size = NewSize;

        // Set Rotation
        //lightning.transform.rotation = Quaternion.Euler(0, 0, 0);
        //lightning.transform.Rotate(0, 0, AngleBetweenVector2(FirstPosition, transform.position));

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

        FirstPosition = transform.position;*/
    }
    /*void AddLight()
    {
        if (lighter != null)
        {
            lighter.intensity = 3;
        }
    }
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }*/
}