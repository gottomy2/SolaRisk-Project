using UnityEngine;

public class MouseHandler : MonoBehaviour {
    
    private bool canClick;
    private bool isMouseDown;

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if(hit.collider != null){
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color32(124, 252, 0, 255);
            }
        }
        
    }
}
