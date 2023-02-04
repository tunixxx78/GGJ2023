using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] int timeForGrowth;
    [SerializeField] float currentGrowth;
    [SerializeField] float timeForLevel;
    [SerializeField] GameObject gameoverCanvas;
    bool isTimerOn;

    //[SerializeField] GrowingRoots growingRoots;

    private void Awake()
    {
        Time.timeScale = 1;
        currentGrowth = 0;
        //growingRoots.SetMinGrowth(currentGrowth);
        //growingRoots.SetMaxGrowth(timeForGrowth);
        timeForLevel = timeForGrowth;
        isTimerOn = true;
    }

    private void Update()
    {
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

        
    }
}
