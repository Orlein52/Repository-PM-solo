using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    Ray jumpRay;
    Ray interactRay;
    RaycastHit interactHit;
    GameObject pickupObj;

    public PlayerInput input;
    public Transform weaponSlot;
    public Weapon currentWeapon;

    float inputX;
    float inputY;
    public float speed = 5f;
    public float JumpHeight = 10f;
    public float JumpDis = 1.1f;
    public int MaxHealth = 10;
    public int Health = 10;
    public int HealAmount = 5;
    public float interactDis = 1f;

    Vector3 camerOffset = new Vector3(0, 3.5f, -6);
    Camera playerCam;
    InputAction lookAxis;
    Vector2 cameraRotation = new Vector2(-10, 0);

    public bool attacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerCam = Camera.main;
        lookAxis = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        jumpRay = new Ray(transform.position, -transform.up);

        input = GetComponent<PlayerInput>();
        interactRay = new Ray(transform.position, transform.forward);
        weaponSlot = transform.GetChild(0);

        currentWeapon = null;

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

        //Attack & Weapons

        interactRay.origin = transform.position;
        interactRay.direction = transform.forward;

        if (Physics.Raycast(interactRay, out interactHit, interactDis))
        {
            if (interactHit.collider.tag == "weapon")
            {
                pickupObj = interactHit.collider.gameObject;
            }
        }
        else
        {
            pickupObj = null;
        }

        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack && attacking)
                currentWeapon.fire();
        }


    }

    public void firemodeSwitch()
    {
        

        if (currentWeapon != null)
        {
            if (currentWeapon.weaponID == 1)
            {
                currentWeapon.GetComponent<Grenade>().changeFiremode();
            }
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

    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                {
                    attacking = true;
                }
                else
                {
                    attacking = false;
                }
            }
            else if (context.ReadValueAsButton())
            {
                currentWeapon.fire();
            }
        }
    }

    public void DropWeapon()
    {
        if(currentWeapon)
        {
            currentWeapon.GetComponent<Weapon>().unequip();
            currentWeapon = null;
        }
    }

    public void Reload()
    {
        if (currentWeapon)
            if (!currentWeapon.reloading)
            {
                currentWeapon.reload();
            }
    }

    public void Interact()
    {
        if (pickupObj)
        {
            if (pickupObj.tag == "weapon")
            {
                if (currentWeapon)
                    DropWeapon();
                pickupObj.GetComponent<Weapon>().equip(this);
                
            }
        }
        else
        {
            Reload();
            

        }
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

        if (other.tag == "Enemy")
        {
            Health -= 3;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hazrds")
        {
            Health -= 2;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Health -= 3;
        }

    }


}
