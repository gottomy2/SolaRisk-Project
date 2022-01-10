using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantsManager : MonoBehaviour
{
    public GlobalVars global;
    public GameObject assistant1;


    // Start is called before the first frame update
    private void Awake()
    {
        if (GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath))
        {
            assistant1.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (assistant1 == null)
        {
            GlobalData.SetVar("hubTutorial1", true, GlobalData.dialoguePath);
        }
    }
}
