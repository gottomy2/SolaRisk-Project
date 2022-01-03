using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    private bool canClick;
    private bool canType;
    private bool isMouseDown;

    void Update() {
        checkMouse();
        checkKeyboard();
    }

    private void checkMouse(){
     if (canClick && Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if(hit.collider != null){
                hit.collider.gameObject.GetComponent<GameButton>().PlayerClick();
            }
        }
    }

    private void checkKeyboard(){
        SimonGameBoard g = SimonGameBoard.GetInstance();

        if (canType && Input.GetKeyDown(KeyCode.Space)) {
          g.StartGame();
        }
    }

    public void SetCanClick(bool can){
        this.canClick = can;
    }

    public void SetCanType(bool can){
        this.canType = can;
    }
}
