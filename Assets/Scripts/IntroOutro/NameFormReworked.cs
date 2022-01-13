using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameFormReworked : MonoBehaviour
{
    private GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        button = GameObject.Find("Button");
        button.GetComponent<Button>().onClick.AddListener(SetName);
        gameObject.SetActive(false);
    }

    void SetName()
    {
        string name = gameObject.GetComponent<TMP_InputField>().text;
        if (name.Length > 0) GlobalData.playerName=name;
        else GlobalData.playerName = "Jacek";
        Debug.Log("PlayerName: "+GlobalData.playerName);
        GlobalData.DIALOGUE_DICTIONARY.Add(
            0, new[]
            {
                "Witaj na statku kapitanie " + GlobalData.playerName + "!",
                "Od czasu twojego ostatniego lotu trochê siê pozmienia³o.",
                "Pozwól ¿e Ciê oprowadzê, zanim wyruszymy w podró¿.",
                "To nic innego jak symulacja...",
                "Zreszt¹ to pewnie nie Twoja pierwsza!",
                "Zacznijmy od g³ównego panelu, to jest od Mapy!"
            }
        );


        SceneManager.LoadScene("Assets/Scenes/ShipInterior/InteriorScene.unity");
    }
    public void FadeIn()
    {
        StartCoroutine(FadeImageToFullAlpha(1f, gameObject.GetComponent<Image>()));
        StartCoroutine(FadeImageToFullAlpha(1f, button.GetComponent<Image>()));
        StartCoroutine(FadeTextToFullAlpha(1f, button.GetComponentInChildren<TextMeshProUGUI>()));
    }
    public IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}