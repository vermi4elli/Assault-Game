using System;
using UnityEngine;

public class PlayerMovementMobile : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    //needed for movement of the character
    private Vector2 movement = Vector2.zero;

    //needed for the rotation of the character
    private Vector2 fingerPos = Vector2.zero;
    private Vector2 fingerPressPos = Vector2.zero;
    private bool fingerPressing = false;
    public float moveSpeed = 2f;

    void Update()
    {
        GetTouchPos();
    }

    private void GetTouchPos()
    {
        //controlls the fact if the mouse is still touching the screen or not
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            fingerPressPos = Input.GetTouch(0).position;
            fingerPressing = true;
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
        {
            fingerPressing = false;
        }

        //controlls the finger position when the finger is holded down
        if (fingerPressing)
        {
            fingerPos = Input.GetTouch(0).position;
        }
    }

    private void FixedUpdate()
    {
        RotatePlayer();
        MovePlayerViaMouse();
    }

    private void MovePlayerViaMouse()
    {
        if (fingerPressing)
        {
            Debug.Log(String.Format("x: {0}, y: {1}, z: {2}", player.transform.forward.x,
                                                              player.transform.forward.y,
                                                              player.transform.forward.z));

            player.transform.Translate(player.transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void RotatePlayer()
    {
        if (fingerPressing)
        {
            Vector2 direction = fingerPos - fingerPressPos;
            direction.Normalize();
            Vector3 lookDirection = new Vector3(direction.x, 0f, direction.y);

            //Correcting the angle accordingly to the camera rotation
            // (thinking the camera is static in it's rortation)
            lookDirection = Quaternion.Euler(0f, 44f, 0f) * lookDirection;

            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            player.transform.rotation = rotation;
        }
    }
}