using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCompany.Assault.Prototype
{
	public class CameraManager : MonoBehaviour
	{
		[SerializeField] private Transform cam;
		[SerializeField] private Transform target;
		[SerializeField] private float verticalOffset;
		[SerializeField] private float followOffset;
		[SerializeField] private float followSpeed = 2.0f;
		[SerializeField] private bool shouldLookAtTarget;

		private Vector3 targetPosition;
		private Vector3 targetDirection;

		private void FixedUpdate()
		{
			Move();
			Look();
		}

		private void Move()
		{
			targetPosition = new Vector3(target.transform.position.x, target.transform.position.y + verticalOffset, target.transform.position.z - followOffset);
			targetDirection = targetPosition - cam.position;
			cam.position += targetDirection * Time.deltaTime * followSpeed;
		}

		private void Look()
		{
			if (shouldLookAtTarget)
			{
				cam.LookAt(target);
			}
		}
	}
}