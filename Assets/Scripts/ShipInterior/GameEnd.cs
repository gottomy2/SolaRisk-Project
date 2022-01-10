using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameEnd : MonoBehaviour
{
    private void Update()
    {
        if(!isEmpty(GlobalData.path))
        {
            if (!GlobalData.firstStart && !GlobalData.GetVar("minigameFailed", GlobalData.hubStats) && GlobalData.path[GlobalData.path.Count - 1].Equals("Pend"))
            {
                Cursor.lockState = CursorLockMode.Confined;
                SceneManager.LoadScene("Assets/Scenes/Fillers/FillerZF.unity");
            }
        }
    }

    private static bool isEmpty<T>(List<T> list)
    {
        if (list == null)
        {
            return true;
        }
        return !list.Any();
    }
}
