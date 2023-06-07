using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAThing : MonoBehaviour
{
    public Transform[] shootPoints;
    public GameObject projectile;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire(){
        foreach (var shootPoint in shootPoints)
        {
            Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        }
    }
}
