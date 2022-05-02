using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterBlockLaser : MonoBehaviour
{

    //Transforms and gameobjects
    public Transform pointTop, pointBottom, pointLeft, pointRight;

    //public LineRenderer listOfLetterLasers[laserIndexNumber];

    public GameObject innerBox;

    //List of letter lasers, these will be used if there is a loop and an extra laser needs to be used
    public List<LineRenderer> listOfLetterLasers;

    // keep track of which laser index we should use from the listOfLetterLasers, REMEMBER THIS PART, IMPORTANT FOR CROSSING LASERS !
    [HideInInspector]
    public int laserIndexNumber;

    //array of raycasthits
    private RaycastHit2D[] hitObjArray;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


 


    public void fireLaserLetterBlock(Vector2 firingPoint, Vector3 firingDirection)
    {

       

        if (listOfLetterLasers[laserIndexNumber].gameObject.activeInHierarchy == false)
        {
            listOfLetterLasers[laserIndexNumber].gameObject.SetActive(true);

            innerBox.GetComponent<SpriteRenderer>().color = Color.yellow;
        }


        //laserOrderCounter += 1;

        //Check the raycast array to not get stuck in own collider
        hitObjArray = Physics2D.RaycastAll(firingPoint, firingDirection);


       

        //If the raycast only returns the object itself or nothing, we will send a laser to a long distance away as if it didn't hit anything
        if (hitObjArray.Length == 0 || (hitObjArray.Length == 1 && hitObjArray[0].collider.gameObject.name == this.gameObject.name))
        {


            

            if (listOfLetterLasers[laserIndexNumber].gameObject.activeInHierarchy == false)
            {
                listOfLetterLasers[laserIndexNumber].gameObject.SetActive(true);
            }


            //This might matter, CHECK LATER IF BUG HAPPENS**********************************************************
            //******************************************
            listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
            listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(firingPoint.x + (firingDirection.x * 50f), firingPoint.y + (firingDirection.y * 50f)));
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
                    //if (laserLine.gameObject.activeInHierarchy == false)
                    //{
                    //    laserLine.gameObject.SetActive(true);
                    //}

                    //laserLine.SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                    //laserLine.SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                  



                    //If the laser is hitting a text block and the laser is coming from a mirror, we will call a fire laser method to continue the laser
                    if (hitObj.collider.tag == "letterBlock")
                    {

                        

                        //Assign the letterBlockObj to the hit colliders gameobject
                        GameObject letterBlockObj = hitObj.collider.gameObject;

                        //Hit letters are being added each time to the hitLettersList if it doesn't already exist
                        if (laserControl.hitLettersList.Contains(letterBlockObj) == false)
                        {
                            laserControl.hitLettersList.Add(letterBlockObj);
                        }

                        Debug.Log("Added " + letterBlockObj + " From : " + this.gameObject.name);

                        //Directions depending on which side the current letter block fired from that will be passed to the next letter block
                        if (firingPoint.Equals(pointLeft.position))
                        {

                            listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                            listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                            //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                            laserIndexNumber += 1;

                            letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointLeft.position, Vector2.left);


                           
                            //Exit out once we fire a laser
                            return;
                            

                        }
                        if (firingPoint.Equals(pointRight.position))
                        {

                            listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                            listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                            //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                            laserIndexNumber += 1;

                            letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointRight.position, Vector2.right);

                            //Exit out once we fire a laser
                            return;

                        }
                        if (firingPoint.Equals(pointTop.position))
                        {

                            Debug.Log("Fired from " + this.gameObject.name + " to " + letterBlockObj);

                            listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                            listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                            //Increment the laser index number afterwards
                            laserIndexNumber += 1;

                            letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointTop.position, Vector2.up);

                            //Exit out once we fire a laser
                            return;

                        }
                        if (firingPoint.Equals(pointBottom.position))
                        {

                            listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                            listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                            //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                            laserIndexNumber += 1;

                            letterBlockObj.GetComponent<letterBlockLaser>().fireLaserLetterBlock(letterBlockObj.GetComponent<letterBlockLaser>().pointBottom.position, Vector2.down);

                            //Exit out once we fire a laser
                            return;

                        }
                        //----------------------
                        //---Directions over----
                        //----------------------
                        //exit the function once we fire next laser
                        return;

                    }

                    //------------------
                    //If we hit a mirror
                    //------------------
                    else if(hitObj.collider.tag == "laserMirror")
                    {

                        

                        //Activate laser if it isnt active, increment laser number by one
                        if (listOfLetterLasers[laserIndexNumber].gameObject.activeInHierarchy == false)
                        {
                            listOfLetterLasers[laserIndexNumber].gameObject.SetActive(true);
                         
                        }
                         
                        GameObject nextMirrorObj = hitObj.collider.gameObject;

                        //If we fire the laser from the top position of the square
                        if (firingPoint.Equals(pointTop.position))
                        {
                            //----------------
                            //----------------
                            if (nextMirrorObj.gameObject.name.Contains("Top Left Mirror"))
                            {

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number afterwards
                                laserIndexNumber += 1;

                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.up))
                                {
                                    
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.left);

                                    //Exit out once we fire a laser
                                    return;
                                }
                              
                            }
                            //----------------
                            //----------------
                            if (nextMirrorObj.gameObject.name.Contains("Top Right Mirror"))
                            {

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number afterwards
                                laserIndexNumber += 1;

                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.up))
                                {

                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.right);

                                    //Exit out once we fire a laser
                                    return;
                                }

                            }

                            //----------------
                            //----------------
                            if (nextMirrorObj.gameObject.name.Contains("Bottom Right Mirror"))
                            {

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number afterwards
                                laserIndexNumber += 1;

                                return;

                            }
                            //----------------
                            //----------------
                            if (nextMirrorObj.gameObject.name.Contains("Bottom Left Mirror"))
                            {

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number afterwards
                                laserIndexNumber += 1;

                                return;

                            }
                        }

                        //----------------------------------------------------------
                        //If we fire the laser from the right position of the square 
                        //----------------------------------------------------------
                        if (firingPoint.Equals(pointRight.position))
                        {
                            if (nextMirrorObj.gameObject.name.Contains("Top Left Mirror"))
                            {


                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;



                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.right))
                                {
                                    
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.down);

                                    //Exit out once we fire a laser
                                    return;


                                }


                            }
                            //----------
                            if (nextMirrorObj.gameObject.name.Contains("Top Right Mirror"))
                            {

                              

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;

                                return;

                            }
                            //----------
                            if (nextMirrorObj.gameObject.name.Contains("Bottom Left Mirror"))
                            {

                               

                                
                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;

                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.right))
                                {

                                    Debug.Log("lasered to the top from " + this.gameObject.name);
                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.up);

                                    //Exit out once we fire a laser
                                    return;


                                }


                            }
                            //----------
                            if (nextMirrorObj.gameObject.name.Contains("Bottom Right Mirror"))
                            {

                               

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;

                                return;



                            }
                            //----------
                        }

                        //If we fire the laser from the bottom position of the square 
                        if (firingPoint.Equals(pointBottom.position))
                        {
                            if (nextMirrorObj.gameObject.name.Contains("Bottom Right Mirror"))
                            {

                               

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;



                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.down))
                                {

                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.right);

                                    //Exit out once we fire a laser
                                    return;


                                }

                            }
                            //--------------------------
                            if (nextMirrorObj.gameObject.name.Contains("Bottom Left Mirror"))
                            {


                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;



                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.down))
                                {

                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.left);

                                    //Exit out once we fire a laser
                                    return;


                                }

                            }
                            //--------------------------
                            if (nextMirrorObj.gameObject.name.Contains("Top Right Mirror"))
                            {

                               

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;



                                return;

                            }
                            //--------------------------
                            if (nextMirrorObj.gameObject.name.Contains("Top Left Mirror"))
                            {

                               

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;


                                return;
                                

                            }
                            //--------------------------







                        }
                        //If we fire the laser from the left position of the square 
                        if (firingPoint.Equals(pointLeft.position))
                        {
                            if (nextMirrorObj.gameObject.name.Contains("Top Right Mirror"))
                            {

                              

                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;



                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.left))
                                {

                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.down);

                                    //Exit out once we fire a laser
                                    return;


                                }


                            }
                            //-------------------------------
                            if (nextMirrorObj.gameObject.name.Contains("Top Left Mirror"))
                            {

                            


                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;

                                return;


                            }
                            //-------------------------------

                            if (nextMirrorObj.gameObject.name.Contains("Bottom Right Mirror"))
                            {


                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;



                                //If the laser came from the bottom or from the right
                                if (firingDirection.Equals(Vector2.left))
                                {

                                    nextMirrorObj.GetComponent<laserControl>().fireLaserMirror(hitObj.point, Vector3.up);

                                    //Exit out once we fire a laser
                                    return;


                                }


                            }
                            //-------------------------------

                            if (nextMirrorObj.gameObject.name.Contains("Bottom Left Mirror"))
                            {


                                listOfLetterLasers[laserIndexNumber].SetPosition(0, new Vector3(firingPoint.x, firingPoint.y));
                                listOfLetterLasers[laserIndexNumber].SetPosition(1, new Vector2(hitObj.point.x, hitObj.point.y));

                                //Increment the laser index number after setting the first laser! HAS TO BE AFTER THE .SetPositions...
                                laserIndexNumber += 1;

                                return;

                            }
                            //-------------------------------





                        }










                    }



                    



                }

                

            }

            



        }



    }





}
