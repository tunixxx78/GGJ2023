using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //[SerializeField] int timeForGrowth;
    //[SerializeField] float currentGrowth;
    //[SerializeField] float timeForLevel;
    //bool isTimerOn;

    [SerializeField] GameObject gameoverCanvas;

    public int plrLives, maxHealth;

    [SerializeField] Sprite[] rootStages;
    [SerializeField] Image currentImare;

    //for possible progression/time left bar.

    //[SerializeField] GrowingRoots growingRoots;

    private void Awake()
    {
        Time.timeScale = 1;
        plrLives = maxHealth;

        //for possible progression/time bar.

        //currentGrowth = 0;
        //growingRoots.SetMinGrowth(currentGrowth);
        //growingRoots.SetMaxGrowth(timeForGrowth);
        //timeForLevel = timeForGrowth;
        // isTimerOn = true;
    }

    private void Update()
    {

        //for possible progression/time bar.
        /*
        if (isTimerOn)
        {
            timeForLevel -= Time.deltaTime;
            currentGrowth += Time.deltaTime;
            //growingRoots.SetGrowth(currentGrowth);

            if(timeForLevel <= 0)
            {
                isTimerOn = false;
                Time.timeScale = 0;
                gameoverCanvas.SetActive(true);
            }
        }
        */

        if(plrLives <= 0)
        {
            Time.timeScale = 0;
            gameoverCanvas.SetActive(true);
            currentImare.enabled = false;
        }

        
    }

    public void ChangeRootImage()
    {
        currentImare.sprite = rootStages[plrLives - 1];
    }
}
