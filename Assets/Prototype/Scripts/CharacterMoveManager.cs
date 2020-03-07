using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCompany.Assault.Prototype
{
    public class CharacterMoveManager : MonoBehaviour
    {
        [SerializeField]
        private Transform actor;
        [SerializeField]
        private float moveSpeed = 4.0f;

        // Update is called once per frame
        void Update()
        {
            HandlePlayerMovement();
        }

        private void HandlePlayerMovement()
        {
            Move();
            Look();
        }

        private void Move()
        {
            Vector3 movePosition = Vector3.zero;

            movePosition.x = -Input.GetAxis("Horizontal");
            movePosition.z = -Input.GetAxis("Vertical");

            actor.Translate(movePosition * moveSpeed * Time.deltaTime);
        }

        private void Look()
        {
            Quaternion mouseOrientation = GetMouseInput();

            actor.rotation = mouseOrientation;            
        }

        private Quaternion GetMouseInput()
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 relMousePos = new Vector2(mousePosition.x - Screen.width / 2, mousePosition.y - Screen.height / 2);
            float angle = Mathf.Atan2(relMousePos.y, relMousePos.x) * Mathf.Rad2Deg * -1;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            return rotation;
        }
    }
}