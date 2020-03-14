using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPC : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    
    //needed for movement of the character
    private Vector2 movement = Vector2.zero;
    public float moveSpeedKeys = 10f;

    //needed for the rotation of the character
    private Vector2 mousePos = Vector2.zero;
    private Vector2 mousePressPos = Vector2.zero;
    private bool mouseLeftKeyPressed = false;
    public float moveSpeedMouse = 2f;

    //test value
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    void Update()
    {
        //GetKeysInput();
        GetMousePos();
        GetMousePosAnyway();
    }

    private void GetMousePosAnyway()
    {
        mousePos = Input.mousePosition;
    }

    private void GetKeysInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    private void GetMousePos()
    {
        //controlls the fact if the LKM is pressed or not
        if (Input.GetMouseButtonDown(0))
        {
            mousePressPos = Input.mousePosition;
            mouseLeftKeyPressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseLeftKeyPressed = false;
        }

        //controlls the mouse position when the key is holded down
        if (mouseLeftKeyPressed)
        {
            mousePos = Input.mousePosition;
        }
    }

    private void FixedUpdate()
    {
        //MovePlayerViaKeys();
        //RotatePlayerAnyway();

        
        RotatePlayer();
        MovePlayerViaMouse();
    }

    private void MovePlayerViaMouse()
    {
        if (mouseLeftKeyPressed)
        {
            //Vector2 direction = mousePos - mousePressPos;
            //direction.Normalize();
            //Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);
            //Debug.Log(String.Format("x: {0}, y: {1}, z: {2}", moveDirection.x, moveDirection.y, moveDirection.z));
            //player.transform.Translate(moveDirection * moveSpeedMouse * Time.deltaTime, Space.Self);

            Debug.Log(String.Format("x: {0}, y: {1}, z: {2}", player.transform.forward.x, 
                                                              player.transform.forward.y, 
                                                              player.transform.forward.z));
            player.transform.Translate(player.transform.forward * moveSpeedMouse * Time.deltaTime, Space.World);
        }
    }

    private void RotatePlayer()
    {
        if (mouseLeftKeyPressed)
        {
            Vector2 direction = mousePos - mousePressPos;
            direction.Normalize();
            Vector3 lookDirection = new Vector3(direction.x, 0f, direction.y);
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            player.transform.rotation = rotation;
        }
    }

    private void MovePlayerViaKeys()
    {
        player.transform.Translate(new Vector3(movement.x, 0f, movement.y) * moveSpeedKeys * Time.deltaTime);
    }

    private void RotatePlayerAnyway()
    {
        Vector3 lookDirection = player.GetComponent<Rigidbody>().velocity;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        player.transform.rotation = rotation;
    }
}
