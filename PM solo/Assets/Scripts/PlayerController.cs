using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    Ray jumpRay;

    float inputX;
    float inputY;
    public float speed = 5f;
    public float JumpHeight = 10f;
    public float JumpDis = 1.1f;
    public int MaxHealth = 10;
    public int Health = 10;
    public int HealAmount = 5;

    Vector3 camerOffset = new Vector3(0, 3.5f, -6);
    Camera playerCam;
    InputAction lookAxis;
    Vector2 cameraRotation = new Vector2(-10, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerCam = Camera.main;
        lookAxis = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        jumpRay = new Ray(transform.position, -transform.up);

    }

    void Update()
    {

        //Camera
        Quaternion playerRotation = Quaternion.identity;
        playerRotation.y = playerCam.transform.rotation.y;
        playerRotation.w = playerCam.transform.rotation.w;
        transform.rotation = playerRotation;

        //Movement
        Vector3 tempMove = rb.linearVelocity;
        tempMove.x = inputY * speed;
        tempMove.z = inputX * speed;
        rb.linearVelocity = (tempMove.x * transform.forward) + (tempMove.y * transform.up) + (tempMove.z * transform.right);

        //Jump Ray
        jumpRay.origin = transform.position;
        jumpRay.direction = -transform.up;

        //Max Health & Respawn
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();
        inputX = InputAxis.x;
        inputY = InputAxis.y;

    }

    public void Jump()
    {

        if (Physics.Raycast(jumpRay, JumpDis))
            rb.AddForce(transform.up * JumpHeight, ForceMode.Impulse);

    }

    //Tag Collision
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "KillBox")
        {
            Health = 0;
        }
        
        if((other.tag == "Heal") && (Health < MaxHealth))
        {
            Health += HealAmount;
            Destroy(other.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hazrds")
        {
            Health -= 2;
        }
    }

}
