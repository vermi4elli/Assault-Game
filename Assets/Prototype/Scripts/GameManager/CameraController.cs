using System.Security;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //objects
    public GameObject mainCamera;
    private Transform player;
    private Transform playerLoin;

    //needed constants
    public float verticalOffset = 20f;
    public float followOffset = 10f;
    private Vector3 followOffsetVector;
    public float followSpeed = 5f;

    // to choose which layer objects colliders to detect
    public LayerMask layerMask;

    // saves information about an obstacle in between the player and the main camera
    private bool hidingObstacle = false;
    private GameObject obstacleInAWay;
    private Shader obstacleInAWayShader;

    public Shader transparentShader;

    //camera movement variables
    private float verticalMovement = 0;
    private Vector3 direction = Vector3.zero;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        player = GetComponent<PlayerManager>().player.transform;
        playerLoin = GetComponent<PlayerManager>().playerLoin.transform;
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
        mainCamera.transform.LookAt(player.transform.position);

        RaycastHit hit;
        if (Physics.Linecast(Camera.main.transform.position, playerLoin.position, out hit, layerMask))
        {
            if (!hidingObstacle) {
                obstacleInAWay = hit.transform.gameObject;
                hidingObstacle = true;

                obstacleInAWayShader = obstacleInAWay.GetComponent<Renderer>().material.shader;
                obstacleInAWay.GetComponent<Renderer>().material.shader = transparentShader;
            }
        }
        else
        {
            if (hidingObstacle)
            {
                obstacleInAWay.GetComponent<Renderer>().material.shader = obstacleInAWayShader;
                hidingObstacle = false;
            }
        }
    }
}
