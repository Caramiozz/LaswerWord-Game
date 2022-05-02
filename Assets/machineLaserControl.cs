using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class machineLaserControl : MonoBehaviour
{
    public Transform startPoint;

    private Vector3 laserStartPosition;



    public LineRenderer mainLaser;

    private Vector2 firingPoint;

    //RaycastHit
    RaycastHit2D laserHit;


    //List of line renderers
    public List<GameObject> lasersList;

    //List of letters innerbox to reset the colors if needed
    public List<GameObject> lettersInnerBoxList;



    //Bool to check if the word is fully complete
    private int correctNumberChecker = 0;

    //Correct combination word
    public List<GameObject> correctLetterOrderList;

    //Victory UI that will appear
    public GameObject victoryUIObject;





    void Start()
    {
        

        if (startPoint != null)
        {
            laserStartPosition = startPoint.position;
        }

        fireLine();

    }

    // Update is called once per frame
    void Update()
    {
        //Fire down from the startingmachine...
        //fireLaser(laserStartPosition, Vector3.down);
        /*
        for(int i = 0; i < laserControl.hitLettersList.Count; i++)
        {
            Debug.Log(i + " + " + laserControl.hitLettersList[i]);
        }
        */
      
    }

    public void fireLine()
    {

        



        //Disable each laser first
        foreach (GameObject laserObj in lasersList)
        {
            laserObj.SetActive(false);
        }

        //Recolor each letter to grey and 
        foreach (GameObject letterInnerBox in lettersInnerBoxList)
        {
            
            letterInnerBox.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
        }
        


        laserHit =  Physics2D.Raycast(laserStartPosition, Vector3.down);

        

        if (laserHit.collider != null)
        {
            //activate the laser if its not active
            if(mainLaser.gameObject.activeInHierarchy == false)
            {
                mainLaser.gameObject.SetActive(true);
            }

            GameObject laserHitObj = laserHit.collider.gameObject;

            if (laserHitObj.tag == "laserMirror")
            {

    
                //Draw the line from the start point to the end
                

                if (laserHitObj.name.Contains("Bottom Right Mirror"))
                {

                    mainLaser.SetPosition(0, new Vector3(laserStartPosition.x, laserStartPosition.y, laserStartPosition.z));
                    mainLaser.SetPosition(1, new Vector2(laserHit.point.x, laserHit.point.y));

                    laserHitObj.GetComponent<laserControl>().fireLaserMirror(laserHit.point, Vector3.right);

                }

                if (laserHitObj.name.Contains("Bottom Left Mirror"))
                {

                    mainLaser.SetPosition(0, new Vector3(laserStartPosition.x, laserStartPosition.y, laserStartPosition.z));
                    mainLaser.SetPosition(1, new Vector2(laserHit.point.x, laserHit.point.y));

                    laserHitObj.GetComponent<laserControl>().fireLaserMirror(laserHit.point, Vector3.left);

                }

                //If its a top mirror we should stop the laser
                if(laserHitObj.name.Contains("Top Right Mirror") || laserHitObj.name.Contains("Top Left Mirror"))
                {

                    mainLaser.SetPosition(0, new Vector3(laserStartPosition.x, laserStartPosition.y, laserStartPosition.z));
                    mainLaser.SetPosition(1, new Vector2(laserHit.point.x, laserHit.point.y));

                }



            }


        }
        //If there are no mirrors or anything, go out of the screen
        else
        {
            //activate the laser if its not active
            if (mainLaser.gameObject.activeInHierarchy == false)
            {
                mainLaser.gameObject.SetActive(true);
            }


            mainLaser.SetPosition(0, new Vector3(laserStartPosition.x, laserStartPosition.y, laserStartPosition.z));
            mainLaser.SetPosition(1, new Vector2(laserStartPosition.x, laserStartPosition.y - 50f));


        }

        
        //Reset laserIndexNumber using the correctLetterOrderList to access each individual block
        foreach (GameObject letterBlockObj in correctLetterOrderList)
        {
            letterBlockObj.GetComponent<letterBlockLaser>().laserIndexNumber = 0;
        }
        



        //Once we move , reset the letters list to form it again
        //----------Check letters-----------

        //Debug.Log(correctLetterOrderList.Count + " " + laserControl.hitLettersList.Count);
        for (int i = 0; i < laserControl.hitLettersList.Count; i++)
        {
            Debug.Log("Element " +  i  + "  Object Name " + laserControl.hitLettersList[i].gameObject.name + "Count was " + laserControl.hitLettersList.Count);
           
        }

        //Check completion, only if we have the same amount of letters
        if (correctLetterOrderList.Count == laserControl.hitLettersList.Count)
        {

            


            for (int i = 0; i < laserControl.hitLettersList.Count; i++)
            {
                if (laserControl.hitLettersList[i].gameObject == correctLetterOrderList[i].gameObject)
                {

                    correctNumberChecker += 1;
                }
                else
                {

                    //Clear the list and reset correct number checker
                    correctNumberChecker = 0;
                    laserControl.hitLettersList.Clear();
                }

            }

            //If correct numbers are equal to the full word
            if (correctNumberChecker == laserControl.hitLettersList.Count)
            {
                Debug.Log("we won zulul");

                victoryUIObject.SetActive(true);

            }


        }





    }

    
}
