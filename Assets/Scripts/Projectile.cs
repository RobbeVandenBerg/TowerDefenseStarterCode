using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        //Rotate the projectile towards the target
    }

    // Update is called once per frame
    void Update()
    {
        // When target is null, it no longer exists and this  
        // object has to be removed 

        // next, move the projectile towards the target 

        // finally, check if the distance between this object and 
        // the target is smaller than 0.2. If so, destroy this object. 
    }
}
