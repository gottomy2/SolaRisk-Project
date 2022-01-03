using System.Collections;
using UnityEngine;

public class GameButton : MonoBehaviour {

    [SerializeField]
    public AudioClip clickSound;

    [SerializeField]
    public AudioClip switchOn;

    [SerializeField]
    public int index;

    [SerializeField]
    public SimonGameBoard simonGameBoard;

    private Color32 activeColor = new Color32(124, 252, 0, 255);
    private Color32 inactiveColor = new Color32(41, 41, 41, 255);
    private Color32 errorColor = new Color32(255, 0, 0, 255);

    private void Awake(){
        ExtractIndexFromName();

        simonGameBoard = GetComponentInParent<SimonGameBoard>();
    }

    public IEnumerator PlayBlinkRoutine(float active, float cooldown){
        SetActive();
        SoundManager.PlaySound(switchOn);
        yield return new WaitForSeconds(active);
        SetInactive();
        SoundManager.PlaySound(switchOn);
        yield return new WaitForSeconds(cooldown);
    }

    public IEnumerator PlayErrorRoutine(){
        SetWronglyActive();
        yield return new WaitForSeconds(0.3f);
        SetInactive();
        yield return new WaitForSeconds(0.3f);
    }

    public void PlayerClick(){
        StartCoroutine(PlayBlinkRoutine(0.1f, 0.1f));
        SoundManager.PlaySound(clickSound);
        simonGameBoard.HandleClick(index);
    }

    public int GetIndex(){
        return index;
    }

    private void ExtractIndexFromName(){
        index = int.Parse(gameObject.name.Split('_')[1]);
    }

    private void SetActive(){
        gameObject.GetComponent<SpriteRenderer>().color = activeColor;
    }

    private void SetWronglyActive(){
        gameObject.GetComponent<SpriteRenderer>().color = errorColor;
    }

    private void SetInactive(){
        gameObject.GetComponent<SpriteRenderer>().color = inactiveColor;
    }

}
