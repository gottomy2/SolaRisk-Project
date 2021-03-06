using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextToScreen : MonoBehaviour
{
    public string text =
       "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam aliquam, ante ac consectetur imperdiet, " +
       "nisl ligula pulvinar enim, quis aliquet diam dolor ut diam. Donec tincidunt congue magna quis ornare. " +
       "Nullam commodo diam in maximus accumsan. Vivamus sit amet ornare tellus. Mauris eu blandit nisi. " +
       "Nam a lacus enim. Aenean et ipsum vel ipsum faucibus aliquet at ut ex.";
    public GameObject captainPortait;
    public bool endingCredits = false;
    public TextMeshProUGUI[] textBoxes;

    private GameObject nameForm;
    private List<string> textList;
    private int counter=0;
    
    private bool nextSequence = true;
    private bool outro = false;

    void Start()
    {
        textList = text.Split('.').ToList();
        textList.RemoveAt(textList.Count - 1);
        captainPortait.SetActive(false);
        nameForm = GameObject.Find("NameForm");
        
    }

    void Update()
    {
        if (nextSequence && counter < textList.Count)
        {
            StartCoroutine(ChangeText());
            nextSequence = false;
        }
        if (outro)
        {
            outro = false;
            StartCoroutine(OpenOutro());
        }
    }

    public IEnumerator ChangeText()
    {
        int visible = 0;
        for(int i = 0; i < textBoxes.Length; i++)
        {
            StartCoroutine(textBoxes[i].GetComponent<FadeText>().FadeTextToZeroAlpha(1f, textBoxes[i].GetComponent<TextMeshProUGUI>()));
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < textBoxes.Length; i++)
        {
            if (counter == textList.Count) break;
            textBoxes[i].GetComponent<TextMeshProUGUI>().text = textList[counter];
            counter++;
            visible++;
        }
        for (int i = 0; i < visible; i++)
        {
            StartCoroutine(textBoxes[i].GetComponent<FadeText>().FadeTextToFullAlpha(1f, textBoxes[i].GetComponent<TextMeshProUGUI>()));
            yield return new WaitForSeconds(3f);
        }
        yield return new WaitForSeconds(2f);
        if (counter == textList.Count) outro = true;
        nextSequence = true;
    }
    public IEnumerator OpenOutro()
    {
        for (int i = 0; i < textBoxes.Length; i++)
        {
            if (!textBoxes[i].GetComponent<FadeText>().faded)
            {
                StartCoroutine(textBoxes[i].GetComponent<FadeText>().FadeTextToZeroAlpha(1f, textBoxes[i].GetComponent<TextMeshProUGUI>()));
            }
        }
        yield return new WaitForSeconds(2f);
        if (endingCredits)
        {
            SceneManager.LoadScene("Assets/Scenes/Epilogue/EpilogueScene.unity");
        }
        else
        {
            captainPortait.SetActive(true);
            textBoxes[1].GetComponent<TextMeshProUGUI>().text = "A TWOJE IMI? TO?";
            StartCoroutine(captainPortait.GetComponent<FadeText>().FadeImageToFullAlpha(1f));
            StartCoroutine(textBoxes[1].GetComponent<FadeText>().FadeTextToFullAlpha(1f, textBoxes[1].GetComponent<TextMeshProUGUI>()));
            Cursor.lockState = CursorLockMode.Confined;
            nameForm.SetActive(true);
            nameForm.GetComponent<NameFormReworked>().FadeIn();
        }
    }
}