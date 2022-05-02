using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class uiButtons : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void continueButton()
    {
        //Check current scenes build index to move to the next level
        int currentSceneNum = SceneManager.GetActiveScene().buildIndex;

        //Reset all static variables
        laserControl.hitLettersList.Clear();


        Debug.Log("Going to level " + currentSceneNum);
        SceneManager.LoadScene(currentSceneNum + 1);

    }
}
