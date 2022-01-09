using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameEnd : MonoBehaviour
{
    public GlobalVars global;
    public MapData mapData;

    private void Update()
    {
        if(!isEmpty(mapData.path))
        {
            if (!mapData.firstStart && !GlobalData.GetVar("minigameFailed", GlobalData.hubStats) && mapData.path[mapData.path.Count - 1].Equals("Pend"))
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
