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
        private float moveSpeed = 10f;
        private Vector2 joystickCenter;

        [SerializeField]
        private WeaponManager.WeaponManagerData weaponData;
        private WeaponManager weaponManager;

        private void Awake()
        {
            weaponManager = new WeaponManager(weaponData);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            HandlePlayerMovement();
        }

        private void HandlePlayerMovement()
        {
            Move();
            Look();
            Shoot();
        }

        private void Shoot()
        {
            if (Input.GetMouseButtonDown(0))
            {
                weaponManager.Use();
            }
        }

        private void Move()
        {
            Vector3 movePosition = Vector3.zero;

            movePosition.x = Input.GetAxis("Horizontal");
            movePosition.z = Input.GetAxis("Vertical");

            actor.Translate(movePosition * moveSpeed * Time.deltaTime, Space.World);
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
            float angle = Mathf.Atan2(relMousePos.x, relMousePos.y) * Mathf.Rad2Deg + 30;
            Debug.Log(angle);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            return rotation;
        }
    }
}