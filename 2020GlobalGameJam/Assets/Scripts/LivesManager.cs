using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    int maxLives = 7;

    public int currentLives = 999;
    SystemControl theSystemController;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
        theSystemController = FindObjectOfType<SystemControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLives(int lifeChangeAmount)
    {
        currentLives += lifeChangeAmount;
        if (currentLives <= 0)
        {
            theSystemController.finishgame();
        }
    }

    public bool moreThanZeroLives()
    {
        if (currentLives > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
