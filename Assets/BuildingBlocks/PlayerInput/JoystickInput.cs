using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This should get player touch location then mesaure the distance they move their finger from that position
/// Then pass that direction with magnitude to the touchMoved action.
/// TODO return a vector relative to the % of screen covered by the touch move instead of pixel values. Currently returns a normal vector without magnitude.
/// </summary>
public class JoystickInput : MonoBehaviour {

  public Transform joystickUI; // requires screen space overlay Canvas

  public UnityEvent touchBegan;
  public UnityEvent touchEnded;
  public InputPositionEvent touchMoved;

  protected Vector2 _touchStartPosition = Vector2.zero;

  void Awake() {
    HideJoystick();
  }

  public void Update() {
    if(Input.touchCount > 0) {
      Touch touch = Input.GetTouch(0);
      if (touch.phase == TouchPhase.Began) {
        Begin(touch.position);
      }
      if (touch.phase == TouchPhase.Ended) {
        End();
      }
      if (touch.phase == TouchPhase.Moved) {
        Move(touch.position);
      }
      if (touch.phase == TouchPhase.Stationary) {
        Move(touch.position);
      }
    } else { // click support as backup
      if(Input.GetMouseButtonDown(0)) {
        Begin(Input.mousePosition);
      }
      if (Input.GetMouseButtonUp(0)) {
        End();
      }
      if(Input.GetMouseButton(0)) {
        Move(Input.mousePosition);
      }
    }
  }
  
  void Begin(Vector2 position) {
    ShowJoystick(position);
    _touchStartPosition = position;
    touchBegan.Invoke();
  }

  void End() {
    HideJoystick();
    _touchStartPosition = Vector2.zero;
    touchEnded.Invoke();
  }

  void Move(Vector2 position) {
    Vector2 direction = position - _touchStartPosition;
    touchMoved.Invoke(direction.normalized);
  }

  // Joystick UI
  void ShowJoystick(Vector2 position) {
    if(joystickUI) {
      joystickUI.gameObject.SetActive(true);
      joystickUI.position = position;
    }
  }

  void HideJoystick() {
    if(joystickUI) joystickUI.gameObject.SetActive(false);
  }
}