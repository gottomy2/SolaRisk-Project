using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground && when the dialogue with first assistant is completed
        if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded) && GlobalData.GetVar("hubTutorial1", GlobalData.dialoguePath))
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
