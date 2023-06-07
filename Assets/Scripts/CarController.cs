using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody ourRigidbody;

    public AudioSource audioS;
    private float moveInput;
    private float turnInput;
    public float forwardSpeed=10f;
    public float reverseSpeed=3f;
    public float turnSpeed = 10f;
    public float maxSpeed = 50f;
    public float dragCutoff = 0.1f;
    public float driftCutoff = 0.6f;

    

    void Start()
    {
        
    }
    void Update()
    {
        audioS.pitch = Vector3.Project( ourRigidbody.velocity, ourRigidbody.transform.forward).magnitude/maxSpeed*0.1f+0.9f;
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
        
        ourRigidbody.MoveRotation(Quaternion.Euler( 0,turnInput*Time.deltaTime*turnSpeed*ourRigidbody.velocity.magnitude,0)*ourRigidbody.rotation);
        if (moveInput>0){
            moveInput *= forwardSpeed;
        }else{
            moveInput *= reverseSpeed;
        }
        
        float moveAdd = (1-(Mathf.Abs(ourRigidbody.velocity.magnitude)/maxSpeed))*moveInput*Time.deltaTime;
        //float moveAdd = moveInput;
        if(moveInput==0){
            ourRigidbody.velocity-=ourRigidbody.velocity*dragCutoff*Time.deltaTime;
        }
        ourRigidbody.velocity-=Vector3.Project( ourRigidbody.velocity, ourRigidbody.transform.right)*driftCutoff*Time.deltaTime;
        ourRigidbody.velocity+=moveAdd*transform.forward;

    }

    void FixedUpdate()
    {
        
    }
}
