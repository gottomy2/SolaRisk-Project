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
        if (global.getVar("hubTutorial1", global.dialoguePath))
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
            global.setVar("hubTutorial1", true, global.dialoguePath);
        }
    }
}
