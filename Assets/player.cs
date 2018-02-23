using UnityEngine;
using UnityEngine.Networking;

public class player : NetworkBehaviour {

    public float movementSpeed = 10;
    public float turningSpeed = 60;
    public GameObject camera;
    public float rotateSpeedX = 5;
    public float rotateSpeedY = 2;
    Vector3 offset;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            Destroy(camera);
            return;
        }
        offset = transform.position - camera.transform.position;
    }

    void Update()
    {
        //player
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(horizontal, 0, 0);

        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        //playere camera
        float m_horizontal = Input.GetAxis("Mouse X") * rotateSpeedX;
        transform.Rotate(0, m_horizontal, 0);

        float desiredAngle = transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        camera.transform.position = transform.position - (rotation * offset);

        Vector3 dir = transform.position - camera.transform.position;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg + 270;

        Vector3 rot = camera.transform.eulerAngles;
        if (rot.x > 180.0f) { rot.x -= 360.0f; }
        float mouseY = Input.GetAxis("Mouse Y") * -rotateSpeedY;

        rot.x = Mathf.Clamp(rot.x + mouseY, -60.0f, 40.0f);
        rot.y = -angle;
        camera.transform.eulerAngles = rot;
    }
}
