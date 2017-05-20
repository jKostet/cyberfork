using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraHandler : NetworkBehaviour
{

    public float howFarAway = 5f;           // How high the camera is
    public float howFarStart = 8f;          // How high the camera is at spawn
    public float zoomOutFactor = 0.15f;     // How much camera zooms out in movement
    public float zoomOutSpeed = 7f;         // How quickly zooming happens
    public float smoothness = 7.0f;         // How smooth the camera moves - lower value, smoother camera

    private Transform playerTransform;
    private Transform cameraTransform;
    private Camera mainCamera;
    private float z;
    void Start()
    {
        SearchPlayer();
        cameraTransform = Camera.main.transform;
        mainCamera = cameraTransform.GetComponent<Camera>();
        mainCamera.orthographicSize = howFarAway;
        z = transform.position.z;

        if(playerTransform != null)
        {
            Vector3 playerPos = playerTransform.position;

            this.transform.position = new Vector3(playerPos.x, playerPos.y, z);
        }
        mainCamera.orthographicSize = howFarStart;
    }

    void FixedUpdate()
    {
        if(playerTransform == null)
        {
            SearchPlayer();
            return;
        }
        // Smooth movement:
        Vector3 newPos = playerTransform.position;
        newPos.z = z;

        transform.position = Vector3.Lerp(transform.position, newPos, smoothness * Time.deltaTime);

        // Camera zooming out due to speed:
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, howFarAway * (1 + (newPos - transform.position).magnitude * zoomOutFactor), zoomOutSpeed * Time.deltaTime);
    }

    void SearchPlayer()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (var player in players)
        {
            if (player.isLocalPlayer)
            {
                playerTransform = player.transform;
                break;
            }
        }
    }
}
