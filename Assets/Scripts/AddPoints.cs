using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPoints : MonoBehaviour
{
    bool isntGrabbed = true;
    public LayerMask playerMask;
    public float grabRadius = 5f;
    public static int score = 0;
    public Transform visualModel;

    public GameObject effect;
    float t;// Start is called before the first frame update
    void Start()
    {
        t=Random.Range(0f, 360f);
        visualModel.Rotate(t, 0f, 0f);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t+=Time.deltaTime;
        t%=360;
        visualModel.localPosition = new Vector3(0f, Mathf.Sin(t*4)*0.25f+1.25f, 0f);
        visualModel.Rotate(180*Time.deltaTime, 0f, 0f);
        if(Physics.CheckSphere(transform.position,grabRadius,playerMask) && isntGrabbed){
            isntGrabbed=false;
            gameObject.SetActive(false);
            score+=1;
            Instantiate(effect, visualModel.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
