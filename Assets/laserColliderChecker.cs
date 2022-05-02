using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserColliderChecker : MonoBehaviour
{

    



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        //this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(this.transform.position.y, this.transform.position.y - 1f, Time.deltaTime), this.transform.position.z);

        //this.transform.localScale =  new Vector3(this.transform.localScale.x, Mathf.Lerp(this.transform.localScale.y, this.transform.localScale.y + 2f, Time.deltaTime), this.transform.localScale.z);

        



    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "laserMirror")
        {

           


        }




    }
}
