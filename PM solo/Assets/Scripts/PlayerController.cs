using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    
    Rigidbody rb;

    float inputX;
    float inputY;
    public float speed = 5f;
    public float Xsensitivity = 0.1f;
    public float Ysensitivity = 0.1f;
    public float cameraYMaxMin = 179;

    Vector3 camerOffset = new Vector3 (0, 3.5f, -6);
    Camera playerCam;
    InputAction lookAxis;
    Vector2 cameraRotation = new Vector2 (-10, 0);
    


    void Start()
    {
        rb = GetComponent <Rigidbody>();

        playerCam = Camera.main;
        lookAxis = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    void Update()
    {

        //Camera
        /*playerCam.transform.position = transform.position + (camerOffset);
        cameraRotation.x += lookAxis.ReadValue<Vector2>().x * Xsensitivity;
        cameraRotation.y += lookAxis.ReadValue<Vector2>().y * Ysensitivity;
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -cameraYMaxMin, cameraYMaxMin);
        playerCam.transform.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);*/
        Quaternion playerRotation = Quaternion.identity;
        playerRotation.y = playerCam.transform.rotation.y;
        playerRotation.w = playerCam.transform.rotation.w;
        transform.rotation = playerRotation;

        //Movement
        Vector3 tempMove = rb.linearVelocity;
        tempMove.x = inputY * speed;
        tempMove.z = inputX * speed;
        rb.linearVelocity = (tempMove.x * transform.forward) + (tempMove.y * transform.up) + (tempMove.z * transform.right);

    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();
        inputX = InputAxis.x;
        inputY = InputAxis.y;

    }
}
