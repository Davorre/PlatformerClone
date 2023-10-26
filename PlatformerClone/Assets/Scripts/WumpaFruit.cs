using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Torres, David
// 10/26/2023
// wumpa fruit
public class WumpaFruit : MonoBehaviour
{





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotate at 5 degrees
        transform.Rotate(Vector3.right * 5 * Time.deltaTime);
    }
}
