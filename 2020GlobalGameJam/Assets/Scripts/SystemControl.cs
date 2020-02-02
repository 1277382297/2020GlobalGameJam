using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystemControl : MonoBehaviour
{
    //making serializable object to let it be seen in editor
    [Serializable]
    public struct nametoobj
    {
        public string objname;
        public string response;
    }

    [SerializeField]
    private nametoobj[] responselist;
    //values
    int score = 0;
    public Text resdialog;
    public string goodtext;
    public string badtext;
    public string neutraltext;
    public string scenename;

    //manual button to end the game
    public Button restart;
    Dictionary<string, string> responsesearch;
    List<string> EndResponseList;
    Boolean gameend = false;


    // Start is called before the first frame update
    void Start()
    {
        //transferring array stuff to dictionary for easy lookup
        for (int x = 0; x < responselist.Length; x++)
        {
            responsesearch.Add(responselist[x].objname, responselist[x].response);
        }
        //restart button click
        restart.onClick.AddListener(restartClick);
        //enable and disable buttons
        restart.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //normal game loop
        if(!gameend)
        {
            
        }
        else
        {
            StartCoroutine(displayresponse());
        }
    }

    void restartClick()
	{
        SceneManager.LoadScene(scenename);
	}
    
    IEnumerator displayresponse()
	{
        foreach(string res in EndResponseList)
		{
            resdialog.text = res;
            yield return new WaitForSeconds(5);
		}
        if(score > 0)
		{
            resdialog.text = goodtext;
		}
        else if(score < 0)
		{
            resdialog.text = badtext;
		}
		else
		{
            resdialog.text = neutraltext;
		}
        restart.gameObject.SetActive(true);
	}
    
    public void finishgame()
    {
        gameend = true;
    }

    public void changescore(int change)
    {
        score += change;
    }

    public void SticktoObj(string Objname)
    {
        //add to response at the end when sticking
        if (responsesearch.ContainsKey(Objname))
        {
            EndResponseList.Add(responsesearch[Objname]);
        }
		else
		{
            Debug.Log("Sticked object not in list!");
		}
    }

    public List<string> GetResponses()
	{
        return EndResponseList;
	}
}