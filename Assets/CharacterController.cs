using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float maxSpeed = 3.0f;
    float rotation = 0.0f;
    float camRotation = 0.0f;
    float rotationSpeed = 2.0f;
    float camRotationSpeed = 0.5f;
    GameObject cam;
    Rigidbody myRigidbody;
    

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 1.0f;

    public GameObject startGameWaypoint;
    public GameObject gameEndWaypoint;
    public Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
       
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            transform.position = gameEndWaypoint.transform.position;
        }

        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);
        myAnim.SetBool("isOnGround", isOnGround);
        
       //transform.position = transform.position + (transform.forward * Input.GetAxis("Vertical") * maxSpeed);
       if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
       {
           myAnim.SetTrigger("jumped");
           myRigidbody.AddForce(transform.up * jumpForce);
       }

       Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;
       myAnim.SetFloat("speed",newVelocity.magnitude);
       myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

       rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
       transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

       camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;
       cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.1f, 0.01f));

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "GameEnd")
        {
            transform.position = gameEndWaypoint.transform.position;
        }

        if (other.tag == "GameStartButton")
        {
            transform.position = startGameWaypoint.transform.position;
        }

        if (other.tag == "QuitGameButton")
        {
            Application.Quit();
            Debug.Log("E");
        }
        
        

    }

   



}