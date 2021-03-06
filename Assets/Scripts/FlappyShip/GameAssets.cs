using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;

    public static GameAssets GetInstance() {
        return instance;
    }

    private void Awake() {
        instance = this;
    }

    public Sprite pipeHeadSprite;
    public Transform pfPipeHead;
    public Transform pfPipeBody;

    [SerializeField]
    public AudioClip jumpSound;
    public AudioClip scoreSound;
    public AudioClip deathSound;
    public AudioClip takeOffSound;

}