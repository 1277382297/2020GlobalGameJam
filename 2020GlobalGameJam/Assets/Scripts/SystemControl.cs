using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



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
    }

    // Update is called once per frame
    void Update()
    {
        //normal game loop
        if(!gameend)
        {
            if (totaltapelength <= 0)
            {
                gameend = true;
            }
        }
        if (Input.GetButtonDown("F"))
        {
            gameend = false;
        }
    }

    public void CutTape(float length)
    {
        totaltapelength -= length;
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
