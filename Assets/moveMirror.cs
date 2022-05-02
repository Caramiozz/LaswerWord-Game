using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveMirror : MonoBehaviour
{
    //Timer to move object
    private float waitTimer = 0.5f;
    private float waitCounter;

    public GameObject mainMachine;


    //Game object to move
    private GameObject selectedObject;

    //string to decide where to move
    private string dirDecider;


    //List of the type "gridEntryData"
    public List<gridEntryData> gridDataList = new List<gridEntryData>();

    //List of each moveable object to add to gridDataList at the start
    public List<GameObject> moveableObjList;
    public List<int> startingTileList;


    //Object to store gridEntryData
    private gridEntryData addedEntryData;


    //Bool to check if a move is viable
    private bool viableMove;

    //gridEntryData type object that will store the current gridObj
    private gridEntryData currentObject;

    //integer that will determine what size the grid is... 5x5, 6x6 etc...
    public int gridSize;


    


    //Class to store grid entry data
    public class gridEntryData
    {

        public int tileNum = 0;
        public GameObject movbeableObj = null;

    }



    void Start()
    {


        //Add gridEntryDatas to the list to determine proper movement restraints later
        for (int i = 0; i < startingTileList.Count; i++)
        {

 
            //Adding each individual entry to the list to store in an organized manner
            addedEntryData = new gridEntryData();

            addedEntryData.tileNum = startingTileList[i];
            addedEntryData.movbeableObj = moveableObjList[i];


            gridDataList.Add(addedEntryData);


        }

    }

    // Update is called once per frame
    void Update()
    {

       

        //Click the object that is to be moved, if another object is not already selected
        if (selectedObject == null)
        {
            clickObject();
        }
        //If we right click, we will unselect the current moveable object
        if(selectedObject != null && startedMoveMirrorRoutine == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //Change the color of the selected object back to normal
                selectedObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

                selectedObject = null;
            }
        }


        //If there is an object selected, move it
        if (selectedObject != null && startedMoveMirrorRoutine == false)
        {
            //Move Up
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {               
                dirDecider = "Up";
                viableMove = checkGrid();

                
                //If we have a viable move after checking the grid, we can move
                if (viableMove == true)
                {

                    moveSelectedObject();
                }
            }
            //Move Down
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                dirDecider = "Down";
                viableMove = checkGrid();

                
                //If we have a viable move after checking the grid, we can move
                if (viableMove == true)
                {

                    moveSelectedObject();
                }
            }
            //Move Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dirDecider = "Left";
                viableMove = checkGrid();

                
                //If we have a viable move after checking the grid, we can move
                if (viableMove == true)
                {

                    moveSelectedObject();
                }
            }
            //Move Right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                dirDecider = "Right";
                viableMove = checkGrid();

                //If we have a viable move after checking the grid, we can move
                if (viableMove == true)
                {

                    moveSelectedObject();
                }
            }
        }





    }

    //Method that will check if the location to move to is a viable location on the grid
    //This method will return an integer denoting where the current
    private bool checkGrid()
    {

        


        int currentTileNum = 0;

        foreach(gridEntryData moveableObjData in gridDataList)
        {
            
            //Find the corresponding gameobject in the list to be able to see its tile number
            if(moveableObjData.movbeableObj.gameObject == selectedObject)
            {
                //Set currentObject to the corresponding griddata
                currentObject = moveableObjData;

                currentTileNum = moveableObjData.tileNum;


                goto DirectionAvailabilityChecker;

                
            }
         

        }
        

        DirectionAvailabilityChecker:
        //If the dirDecider is "Up", check accordingly
        if (dirDecider == "Up")
        {
            foreach (gridEntryData moveableObjData in gridDataList)
            {
                //Check if there is an object blocking the path or if we reached the upper bounds of the arena
                if (currentTileNum - 1 == moveableObjData.tileNum || (currentTileNum-1)% gridSize == 0 || currentTileNum -1 == 1)
                {               

                   

                    return false;

                }
                
            }
        }

        //If the dirDecider is "Down", check accordingly
        if (dirDecider == "Down")
        {
            foreach (gridEntryData moveableObjData in gridDataList)
            {
                //Check if there is an object blocking the path or if we reached the upper bounds of the arena
                if (currentTileNum + 1 == moveableObjData.tileNum || (currentTileNum) % gridSize == 0)
                {

                   

                    return false;

                }

            }
        }

        //If the dirDecider is "Left", check accordingly
        if (dirDecider == "Left")
        {
            foreach (gridEntryData moveableObjData in gridDataList)
            {
                //Check if there is an object blocking the path or if we reached the upper bounds of the arena
                if (currentTileNum - gridSize == moveableObjData.tileNum || currentTileNum - gridSize <= 0 || currentTileNum - gridSize == 1)
                {

                    

                    return false;

                }

            }
        }

        //If the dirDecider is "Left", check accordingly
        if (dirDecider == "Right")
        {
            foreach (gridEntryData moveableObjData in gridDataList)
            {
                //Check if there is an object blocking the path or if we reached the upper bounds of the arena
                if (currentTileNum + gridSize == moveableObjData.tileNum || currentTileNum + gridSize > gridSize * gridSize)
                {

                    

                    return false;

                }

            }
        }



        // If the object is not null, move a square up and update the tileNum
        if (currentObject != null)
        {
            if (dirDecider == "Up")
            {
                currentObject.tileNum -= 1;
            }

            if (dirDecider == "Down")
            {
                currentObject.tileNum += 1;
            }

            if (dirDecider == "Left")
            {
                currentObject.tileNum -= gridSize;
            }

            if (dirDecider == "Right")
            {
                currentObject.tileNum += gridSize;
            }

        }



        return true;

        // if we passed the checks, return true
        //return true;




    }

    //Start the coroutine that will move the objects transform...
    private void moveSelectedObject()
    {

        // Move the object slowly
        if(startedMoveMirrorRoutine == false)
        {
            StartCoroutine(moveBlock());
        }



        

    }


    //Raycast from mouse position to check for moveable objects
    private void clickObject()
    {

        //Select the mirror object to move
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {




            Vector3 directionVector = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).z + 15f);


            RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), directionVector);


            //If we click a laser mirror
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag == "laserMirror")
                {
                    selectedObject = rayHit.collider.gameObject;


                    //Change the color of the selected object to indicate its selected
                    selectedObject.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.8f, 0.2f);
                }
            }

            

        }
    }



    // Coroutine that will move the selected moveable object to a certain direction
    private bool startedMoveMirrorRoutine;
    private IEnumerator moveBlock()
    {

        startedMoveMirrorRoutine = true;

        Vector2 selectedObjectPos = selectedObject.transform.position;

       

        while (waitCounter < waitTimer)
        {
            waitCounter += Time.deltaTime;

            if (dirDecider == "Up")
            {
                selectedObject.transform.position = Vector3.Lerp(selectedObjectPos, new Vector2(selectedObjectPos.x, selectedObjectPos.y + 5f), waitCounter / waitTimer);
            }
            if (dirDecider == "Down")
            {
                selectedObject.transform.position = Vector3.Lerp(selectedObjectPos, new Vector2(selectedObjectPos.x, selectedObjectPos.y - 5f), waitCounter / waitTimer);
            }
            if (dirDecider == "Left")
            {
                selectedObject.transform.position = Vector3.Lerp(selectedObjectPos, new Vector2(selectedObjectPos.x -5f, selectedObjectPos.y) , waitCounter / waitTimer);
            }
            if (dirDecider == "Right")
            {
                selectedObject.transform.position = Vector3.Lerp(selectedObjectPos, new Vector2(selectedObjectPos.x +5f, selectedObjectPos.y), waitCounter / waitTimer);
            }



            yield return null;
        }


        //Clear the list
        laserControl.hitLettersList.Clear();
        //clear laser index number


        mainMachine.GetComponent<machineLaserControl>().fireLine();
        

        //Nullify the selected object once the movement is done
        //selectedObject = null;

        waitCounter = 0f;

        startedMoveMirrorRoutine = false;

       
    }


    


}
