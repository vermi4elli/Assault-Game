using UnityEngine;

public class CameraController : MonoBehaviour
{
    //objects
    public GameObject mainCamera;
    public Transform player;

    //needed constants
    public float verticalOffset = 20f;
    public float followOffset = 10f;
    private Vector3 followOffsetVector;
    public float followSpeed = 5f;

    //camera movement variables
    private float verticalMovement = 0;
    private Vector3 direction = Vector3.zero;

    private void Start()
    {
        followOffsetVector = new Vector3(followOffset * -1, 0f, followOffset * -1);
    }

    private void Update()
    {
        verticalMovement = player.position.y + verticalOffset;

        direction = player.position + followOffsetVector;
        direction.y = verticalMovement;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = direction - mainCamera.transform.position;
        mainCamera.transform.Translate(moveDirection * followSpeed * Time.deltaTime, Space.World);
    }
}
