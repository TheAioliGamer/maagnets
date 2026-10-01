using JetBrains.Annotations;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject camPivot;
    private GameObject cam;
    public Renderer lastSeen;
    public float moveSpeed;
    public float sprintMod;
    public float jumpHeight;
    public float sensitivity;
    public float magnet;
    private float pitch;
    private float yaw;
    public Vector3 wallJump;
    public bool grounded;
    public bool walled;
    public bool ceilinged;
    public bool sprinting;
    public bool crouching;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //Debug.Log(rb);
        if (grounded == false)
        {
            crouching = false;
        }
        else
        {
            grounded = true;
            walled = false;
            ceilinged = false;
            rb.linearVelocity = Vector3.zero;
        }
        if (walled == true || ceilinged == true)
        {
            rb.linearVelocity = Vector3.zero;
        }
        #region camera movement
        // This code is all essential for the mouse to control the camera
        pitch = Mathf.Clamp(pitch, -90, 90);
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch += sensitivity * Input.GetAxis("Mouse Y");
        camPivot.transform.eulerAngles = new Vector3(-pitch, yaw, 0.0f);
        player.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        #endregion
        #region jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            grounded = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && ceilinged == true)
        {
            rb.AddForce(Vector3.down * jumpHeight, ForceMode.Impulse);
            ceilinged = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && walled == true && grounded == false && ceilinged == false)
        {
            rb.AddForce(wallJump * jumpHeight, ForceMode.Impulse);
            walled = false;
        }
        #endregion
        #region magnetism
        RaycastHit highlight;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out highlight))
        {
            if(highlight.collider.CompareTag("Ground") || highlight.collider.CompareTag("Wall") || highlight.collider.CompareTag("Ceiling"))
            {
                //Debug.Log("Grond heheheh");
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    rb.AddForce(highlight.transform.up * magnet, ForceMode.Acceleration);
                }
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    rb.AddForce(-highlight.transform.up * magnet, ForceMode.Acceleration);
                }
            }
            // highlights what surface is being magnetised
            Renderer redner = highlight.collider.gameObject.GetComponent<Renderer>();
            if (redner != null)
            {
                if (redner.CompareTag("Ground") || redner.CompareTag("Wall") || redner.CompareTag("Ceiling"))
                if (lastSeen != redner)
                {
                    if (lastSeen != null)
                    {
                        Color c = lastSeen.material.color;
                        c.r = 1;
                        c.g = 1;
                        lastSeen.material.color = c;
                    }
                }
                Color heehee = redner.material.color;
                heehee.r = 0.6f;
                heehee.g = 0.6f;
                redner.material.color = heehee;
                lastSeen = redner;
            }
        }
        #endregion
    }

    // is called once every set amount of time
    void FixedUpdate()
    {
        #region player movement
        #region sprinting
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            sprinting = true;
            crouching = false;
            sprintMod = 1.5f;
        }
        else
        {
            sprinting = false;
            sprintMod = 1;
        }
        #endregion
        #region crouching
        /*
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.C))
        {
            if (sprinting == false && grounded == true)
            {
                crouching = true;
            }
            else
            {
                crouching = false;
            }
        }
        else
        {
            crouching = false;
        }
        if (crouching == true)
        {
            moveSpeed = 5;
            player.transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            moveSpeed = 10;
            player.transform.localScale = new Vector3(1, 1, 1);
        }
        */
        #endregion
        #region walking
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 moveFore = new Vector3(0, 0, 1);
            transform.position += (transform.right * moveFore.x + transform.forward * moveFore.z) * moveSpeed * sprintMod * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 moveBack = new Vector3(0, 0, -1);
            transform.position += (transform.right * moveBack.x + transform.forward * moveBack.z) * moveSpeed * sprintMod * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 moveLeft = new Vector3(-1, 0, 0);
            transform.position += (transform.right * moveLeft.x + transform.forward * moveLeft.z) * moveSpeed * sprintMod * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 moveRight = new Vector3(1, 0, 0);
            transform.position += (transform.right * moveRight.x + transform.forward * moveRight.z) * moveSpeed * sprintMod * Time.deltaTime;
        }

        #endregion
        #endregion
    }
}
