using UnityEngine;

public class MousePosHandler : MonoBehaviour {

    [SerializeField] private Camera mainCamera;

    private bool canClick;

    void Update() {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }
}
