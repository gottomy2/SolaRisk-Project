using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantsManager : MonoBehaviour
{
    public GameObject assistant1;
    public GameObject assistant2;


    // Start is called before the first frame update
    private void Awake()
    {
        //assistant2 = GameObject.Find("assistantImage2");
        if (GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath))
        {
            assistant1.gameObject.SetActive(false);
        }
        if (GlobalData.GetVar("repairsTutorialActive", GlobalData.dialoguePath))
        {
            GlobalData.DIALOGUE_DICTIONARY.Add(4, new[] {
                        "W razie gdyby nie uda³o nam siê bezpiecznie dolecieæ do planety...",
                        "Bêdziemy zmuszeni do podjêcia napraw na statku przed nastêpn¹ podró¿¹.",
                        "Zasymulowa³em wszystkie mo¿liwe zniszczenia, cobyœ wprawi³ siê nieco",
                        "Punkty napraw znajduj¹ siê za œluz¹, to jak - spróbujemy coœ naprawiæ?"
                    }
            );
            if(assistant2 != null)
            {
                assistant2.SetActive(true);
            }
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            if (assistant2 != null)
            {
                assistant2.SetActive(false);
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (assistant2 == null && GlobalData.GetVar("repairsTutorialActive", GlobalData.dialoguePath))
        {
            Cursor.lockState = CursorLockMode.Locked;
            GlobalData.SetVar("repairsTutorialActive", false, GlobalData.dialoguePath);
        }

        if (GlobalData.GetVar(GlobalData.HUB_TUTORIAL1, GlobalData.dialoguePath))
            return;

        if (assistant1 == null)
        {
            GlobalData.SetVar(GlobalData.HUB_TUTORIAL1, true, GlobalData.dialoguePath);
        }
    }
}
