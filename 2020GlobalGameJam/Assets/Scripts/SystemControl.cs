using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



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
    public float totaltapelength;
    public Text resdialog;
    public string goodtext;
    public string badtext;
    public string neutraltext;
    public Button endgame;
    public Button restart;
    Dictionary<string, string> responsesearch;
    List<string> EndResponseList;
    Boolean gameend = false;
    Boolean displaystarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //transferring array stuff to dictionary for easy lookup
        responsesearch = new Dictionary<string, string>();
        EndResponseList = new List<string>();
        for (int x = 0; x < responselist.Length; x++)
        {
            responsesearch.Add(responselist[x].objname, responselist[x].response);
        }
        //restart button click
        restart.onClick.AddListener(restartClick);
        //endgame button
        endgame.onClick.AddListener(endGameclick);
        //enable and disable buttons
        restart.gameObject.SetActive(false);
        endgame.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //normal game loop
        if (!gameend)
        {
            displaystarted = false;
            restart.gameObject.SetActive(false);
            SticktoObj("Certificate");
            SticktoObj("Hole");
            changescore(1);
            if (totaltapelength <= 0)
            {
                gameend = true;
            }
        }
        else if(!displaystarted)
        {
            StartCoroutine(displayresponse());
        }
    }

    void restartClick()
	{
        gameend = false;
        EndResponseList.Clear();
        score = 0;
	}

    void endGameclick()
	{
        gameend = true;
	}

    IEnumerator displayresponse()
	{
        displaystarted = true;
        WaitForSeconds wait = new WaitForSeconds(3);
        foreach (string res in EndResponseList)
		{
            resdialog.text = res;
            yield return wait;
        }
        if (score > 0)
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

    //removes tape length
    public void CutTape(float length)
    {
        totaltapelength -= length;
    }

    public void changescore(int change)
    {
        score += change;
    }

    //input objectname to add it to the list of stickied objects
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
