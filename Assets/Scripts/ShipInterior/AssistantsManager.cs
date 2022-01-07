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
        if (GlobalDataHandler.GetPref(GlobalDataHandler.HUB_TUTORIAL1))
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
            GlobalDataHandler.SavePref(GlobalDataHandler.HUB_TUTORIAL1, true);
        }
    }
}
