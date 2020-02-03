using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject exPrefab;
    List<GameObject> tapeextentions;
    int tapenum = 0;
    LivesManager managerforlives;
    public float spacingAmount = 120;
    // Start is called before the first frame update
    void Start()
    {
        managerforlives = FindObjectOfType<LivesManager>();
        tapeextentions = new List<GameObject>();
        while(tapenum < managerforlives.currentLives)
        {
            GameObject inst = Instantiate(exPrefab, FindObjectOfType<Canvas>().transform);
            //inst.transform.SetParent(parent);
            inst.transform.position = new Vector3(inst.transform.position.x + spacingAmount*tapenum, inst.transform.position.y, inst.transform.position.z);
            tapeextentions.Add(inst);
            tapenum++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        while(managerforlives.currentLives < tapenum && tapenum > 0)
        {
            tapeextentions[tapenum-1].SetActive(false);
            tapenum--;
        }
    }
}
