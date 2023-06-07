using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goTowardsPlayer : MonoBehaviour
{
    public Transform player;
    public Rigidbody ourRigidbody;

    Vector2 our2Dpos;
    Vector2 player2Dpos;
    Vector2 forward2D;

    private float moveInput;
    private float turnInput;
    public float forwardSpeed=5f;
    public float reverseSpeed=3f;
    public float turnSpeed = 10f;
    public float maxSpeed = 50f;
    public float dragCutoff = 0.1f;
    public float driftCutoff = 0.6f;
    public float chaseKoeff = 1f;
    public Transform leftChecker;
    public Transform rightChecker;


    public float maxCheckerDist=1f;
    public LayerMask checkerMask;

    float rightPotential=1f;
    float leftPotential=1f;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        moveInput = 1f;
        RaycastHit leftHit;
        RaycastHit rightHit;
        our2Dpos = new Vector2(ourRigidbody.position.x, ourRigidbody.position.z);
        player2Dpos = new Vector2(player.position.x, player.position.z);
        forward2D = new Vector2(transform.forward.x, transform.forward.z);
        turnInput = 0f;
        rightPotential=1f;
        leftPotential=1f;
        if(Physics.Raycast(leftChecker.position, leftChecker.forward, out leftHit, maxCheckerDist, checkerMask)){
            turnInput+=1-(leftHit.distance)/maxCheckerDist;
            leftPotential = (leftHit.distance)/maxCheckerDist;
        }
        if(Physics.Raycast(rightChecker.position, rightChecker.forward, out rightHit, maxCheckerDist, checkerMask)){
            turnInput-=1-(rightHit.distance)/maxCheckerDist;
            rightPotential = (rightHit.distance)/maxCheckerDist;
        }
        
        turnInput = Mathf.Clamp(turnInput, -1f,1f);
        //Quaternion newRotation = Quaternion.Euler( 0,turnInput*Time.deltaTime*turnSpeed*ourRigidbody.velocity.magnitude + ourRigidbody.rotation.eulerAngles.y,0);
        Quaternion newRotation = Quaternion.Euler( 0, ourRigidbody.rotation.eulerAngles.y,0);

        newRotation = Quaternion.Euler( 0,turnInput*Time.fixedDeltaTime*turnSpeed*ourRigidbody.velocity.magnitude + ourRigidbody.rotation.eulerAngles.y,0);
        float angle = Vector3.SignedAngle(transform.forward,player.position-transform.position,Vector3.up);
        if((angle > 15 && rightPotential>0.25f) || (angle < 15 && leftPotential>0.25f)){
            newRotation = Quaternion.RotateTowards(newRotation, Quaternion.LookRotation(player.position-transform.position, Vector3.up) , Time.fixedDeltaTime*turnSpeed*ourRigidbody.velocity.magnitude);
        }

        if (ourRigidbody.velocity.sqrMagnitude < 0.5f){
            newRotation = Quaternion.RotateTowards(newRotation, Quaternion.LookRotation(-transform.forward, Vector3.up) , Time.fixedDeltaTime*turnSpeed);
        }        

        ourRigidbody.MoveRotation(newRotation);
        //ourRigidbody.MoveRotation(Quaternion.RotateTowards(ourRigidbody.rotation, Quaternion.LookRotation(player.position-ourRigidbody.position, Vector3.up), turnSpeed*Time.fixedDeltaTime*5));
        if (moveInput>0){
            moveInput *= forwardSpeed;
        }else{
            moveInput *= reverseSpeed;
        }
        
        float moveAdd = (1-(Mathf.Abs(ourRigidbody.velocity.magnitude)/maxSpeed))*moveInput*Time.fixedDeltaTime;
        //float moveAdd = moveInput;
        if(moveInput==0){
            ourRigidbody.velocity-=ourRigidbody.velocity*dragCutoff*Time.fixedDeltaTime;
        }
        ourRigidbody.velocity-=Vector3.Project( ourRigidbody.velocity, ourRigidbody.transform.right)*driftCutoff*Time.fixedDeltaTime;
        ourRigidbody.velocity+=moveAdd*transform.forward;

    }
    void OnCollisionStay(Collision other)
    {
        Quaternion newRotation = Quaternion.Euler( 0, ourRigidbody.rotation.eulerAngles.y,0);
        newRotation = Quaternion.RotateTowards(newRotation, Quaternion.LookRotation(-transform.forward, Vector3.up) , Time.fixedDeltaTime*turnSpeed*10);
        ourRigidbody.rotation = newRotation;
    }
}
