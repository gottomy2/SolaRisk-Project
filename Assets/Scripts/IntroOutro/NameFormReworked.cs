using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameFormReworked : MonoBehaviour
{
    private GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Button");
        button.GetComponent<Button>().onClick.AddListener(SetName);
        gameObject.SetActive(false);
    }

    void SetName()
    {
        string name = gameObject.GetComponent<TMP_InputField>().text;
        if (name.Length > 0) GlobalData.playerName=name;
        else GlobalData.playerName = "Kapitan Jacek";
        Debug.Log("PlayerName: "+GlobalData.playerName);
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