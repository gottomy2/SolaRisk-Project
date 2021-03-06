using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;
    private bool condition;

    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        condition = (SceneManager.GetActiveScene() == SceneManager.GetSceneByPath("Assets/Scenes/ShipInterior/InteriorScene.unity"));

        if (condition)
        {
            if (!GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath) || GlobalData.GetVar("repairsTutorialActive",GlobalData.dialoguePath))
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            // Lock the mouse cursor to the game in planetary View.
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (condition)
        {
            if (GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath) && !GlobalData.GetVar("repairsTutorialActive", GlobalData.dialoguePath))
            {
                Cursor.lockState = CursorLockMode.Locked;
                // Get smooth velocity.
                Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
                Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
                frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
                velocity += frameVelocity;
                velocity.y = Mathf.Clamp(velocity.y, -90, 90);

                // Rotate camera up-down and controller left-right from velocity.
                transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
                character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }
    }
}
