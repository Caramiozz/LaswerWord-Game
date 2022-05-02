using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserControl : MonoBehaviour
{

    //REMINDER FOR TOMORROW-----------------------------
    //--ADJUST REFLECTION TO SAME TYPE OF MIRRORS--
    //--ADJUST AND PERFECT LASER LOOPS WITH THE LASER INCREMENTER--
    //--GHEV A FUN--

    //static int that will count the number of the fired laser to check which letter was hit first
    public static int laserOrderCounter = 0;

    public LineRenderer mirrorLaser;


    private RaycastHit2D[] hitObjArray;


    //Lasers hit in the proper order
    public static List<GameObject> hitLettersList = new List<GameObject>();
    void Start()
    {
      
   
    }

    // Update is called once per frame
    void Update()
    {
       
        
       
    }

    //Draw the laser if it is hitting a mirror
    public void fireLaserMirror(Vector2 firingPoint , Vector3 firingDirection)
    {

        laserOrderCounter += 1;

        //Check the raycast array to not get stuck in own collider
        hitObjArray =  Physics2D.RaycastAll(firingPoint, firingDirection);

        


        //If the raycast only returns the object itself or nothing, we will send a laser to a long distance away as if it didn't hit anything
        if (hitObjArray.Length == 0 || (hitObjArray.Length == 1 && hitObjArray[0].collider.gameObject.name == this.gameObject.name))
        {
          

            if(mirrorLaser.gameObject.activeInHierarchy == false)
            {
                mirrorLaser.gameObject.SetActive(true);
            }

            mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
            mirrorLaser.SetPosition(1, new Vector2(firingPoint.x + (firingDirection.x * 50f), firingPoint.y + (firingDirection.y * 50f)));
        }
        
        //If we have a different collider, we will apply a specific laser direction for each given object depending on their name and recursively call this function
        else 
        {



            foreach (RaycastHit2D hitObj in hitObjArray)
            {


                //if the gameobject is not itself
                if (hitObj.collider.name != this.gameObject.name)
                {


                   

                    //Activate and place the mirror object
                    if (mirrorLaser.gameObject.activeInHierarchy == false)
                    {
                        mirrorLaser.gameObject.SetActive(true);
                    }

                    //mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                    //mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));



                    //From a Right Mirror to a Left Mirror
                    if (hitObj.collider.tag == "laserMirror")
                    {


                       

                        //Assign the next mirror object to use multiple times
                        GameObject nextMirrorObj = hitObj.collider.gameObject;

                        //Activate the laser from the next mirror depending on what type of mirror it is
                        //If the laser is coming from a right mirror and going to a left mirror

                        //---------------------------------
                        //FOR BOTTOM RIGHT MIRROR REFLECTION
                        //---------------------------------
                        if (this.gameObject.name.Contains("Bottom Right Mirror"))
                        {
                            //TO ITSELF
                            if (nextMirrorObj.name.Contains("Bottom Right Mirror"))
                            {


                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            //Right to Left
                            if (nextMirrorObj.name.Contains("Bottom Left Mirror"))
                            {

                                nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.up);

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            //Right to Top Left
                            if (nextMirrorObj.name.Contains("Top Left Mirror"))
                            {

                                if (firingDirection == Vector3.up)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.left);
                                }

                                if (firingDirection == Vector3.right)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.down);
                                }

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            //Right to Top Right, all we do is draw the laser
                            if (nextMirrorObj.name.Contains("Top Right Mirror"))
                            {

                                if (firingDirection == Vector3.up)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.right);
                                }

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;

                            }
                        }
                        //---------------------------------
                        //FOR BOTTOM LEFT MIRROR REFLECTION checked
                        //---------------------------------
                        if (this.gameObject.name.Contains("Bottom Left Mirror"))
                        {

                            if (nextMirrorObj.name.Contains("Top Left Mirror"))
                            {

                                if (firingDirection == Vector3.up)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.left);
                                }

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            if (nextMirrorObj.name.Contains("Bottom Right Mirror"))
                            {

                                if (firingDirection == Vector3.left)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.up);
                                }

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            
                            if (nextMirrorObj.name.Contains("Top Right Mirror"))
                            {

                                if(firingDirection == Vector3.left) 
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.down);
                                }

                                if (firingDirection == Vector3.up)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.right);
                                }
                                

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;

                            }

                            if (nextMirrorObj.name.Contains("Bottom Left Mirror"))
                            {

                               

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;

                            }


                        }

                        //---------------------------------
                        //FOR TOP LEFT MIRROR REFLECTION checked
                        //---------------------------------
                        if (this.gameObject.name.Contains("Top Left Mirror"))
                        {

                            if (nextMirrorObj.name.Contains("Bottom Right Mirror"))
                            {

                                if (firingDirection == Vector3.down)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.right);
                                }
                                if (firingDirection == Vector3.left)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.up);
                                }

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            if (nextMirrorObj.name.Contains("Bottom Left Mirror"))
                            {

                                //Only fire if the direction is vector3.down, else just draw the laser
                                if (firingDirection == Vector3.down)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.left);
                                }

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            if (nextMirrorObj.name.Contains("Top Right Mirror"))
                            {

                                nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.down);

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;

                            }

                            if (nextMirrorObj.name.Contains("Top Left Mirror"))
                            {


                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;

                            }


                        }

                        //---------------------------------
                        //FOR TOP RIGHT MIRROR REFLECTION checked
                        //---------------------------------
                        if (this.gameObject.name.Contains("Top Right Mirror"))
                        {

                            if (nextMirrorObj.name.Contains("Bottom Right Mirror"))
                            {

                                if (firingDirection == Vector3.down)
                                {
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.right);
                                }
                               

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }
                            if (nextMirrorObj.name.Contains("Top Left Mirror"))
                            {

                                nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.down);

                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }


                            //Only set the laser since we cant reflect
                            if (nextMirrorObj.name.Contains("Bottom Left Mirror"))
                            {

                                nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.up);


                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }

                            if (nextMirrorObj.name.Contains("Top Right Mirror"))
                            {


                                mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Exit method if we fired a laser
                                return;
                            }




                        }








                    }

                    //If the laser is hitting a text block, we will call a fire laser method to continue the laser
                    if(hitObj.collider.tag == "letterBlock")
                    {
                        //Assign the letterBlockObj to the hit colliders gameobject
                        GameObject letterBlockObj = hitObj.collider.gameObject;

                        mirrorLaser.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                        mirrorLaser.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                        //Hit letters are being added each time to the hitLettersList
                        //Hit letters are being added each time to the hitLettersList if it doesn't already exist
                        if (laserControl.hitLettersList.Contains(letterBlockObj) == false)
                        {
                            laserControl.hitLettersList.Add(letterBlockObj);
                        }



                        //Fire laser with specific direction from the letter block depending on the path the laser is coming from
                        if (this.name.Contains("Top Left Mirror"))
                        {

                            if (firingDirection == Vector3.left)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointLeft.position, Vector2.left);

                                //Exit method if we fired a laser
                                return;
                            }
                            if(firingDirection == Vector3.down)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointBottom.position, Vector2.down);

                                //Exit method if we fired a laser
                                return;
                            }
                           
                    
                        }
                        if (this.name.Contains("Bottom Left Mirror"))
                        {

                            if (firingDirection == Vector3.left)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointLeft.position, Vector2.left);

                                //Exit method if we fired a laser
                                return;
                            }
                            if (firingDirection == Vector3.up)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointTop.position, Vector2.up);

                                //Exit method if we fired a laser
                                return;
                            }



                        }
                        if (this.name.Contains("Bottom Right Mirror"))
                        {

                            if (firingDirection == Vector3.right)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointRight.position, Vector2.right);

                                //Exit method if we fired a laser
                                return;
                            }
                            if (firingDirection == Vector3.up)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointTop.position, Vector2.up);

                                //Exit method if we fired a laser
                                return;
                            }



                        }
                        if (this.name.Contains("Top Right Mirror"))
                        {

                            if (firingDirection == Vector3.right)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointRight.position, Vector2.right);

                                //Exit method if we fired a laser
                                return;
                            }
                            if (firingDirection == Vector3.down)
                            {
                                letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointBottom.position, Vector2.down);

                                //Exit method if we fired a laser
                                return;
                            }



                        }



                        //end the loop
                        return;

                    }






                }



            }





        }








}


    


}
