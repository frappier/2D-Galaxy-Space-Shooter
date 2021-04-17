using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _laserSpeed = 8f;


    // Update is called once per frame
    void Update()
    {
        //Move laser up
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        //If laser position >=8 destroy laser object
        if(transform.position.y >= 8f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

}
