using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Torres, David
// 10/26/2023
// wumpa fruit
public class WumpaFruit : MonoBehaviour
{

    public float rotateSpeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate at "X" amount of degrees
        transform.Rotate(0, rotateSpeed, 0, Space.World );


        
            
           
    }

   
    
}
