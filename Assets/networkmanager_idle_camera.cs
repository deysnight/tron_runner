using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class networkmanager_idle_camera : NetworkManager {

    [SerializeField] Transform sceneCamera;
    [SerializeField] float camRotationRadius = 24.0f;
    [SerializeField] float camRotationSpeed = 3.0f;
    [SerializeField] bool canRotate = true;

    float rotation;

    public override void OnStartClient(NetworkClient client)
    {
        canRotate = false;
    }

    public override void OnStartHost()
    {
        canRotate = false;
    }

    public override void OnStopClient()
    {
        canRotate = true;
    }

    public override void OnStopHost()
    {
        canRotate = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (!canRotate) return;
        rotation += camRotationSpeed * Time.deltaTime;
        if (rotation >= 360.0f) rotation -= 360.0f;

        sceneCamera.position = Vector3.zero;
        sceneCamera.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        sceneCamera.Translate(0.0f, camRotationRadius, -camRotationRadius);
        sceneCamera.LookAt(Vector3.zero);
	}
}
