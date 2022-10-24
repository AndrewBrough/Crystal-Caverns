using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


[System.Serializable]
public class TouchActions {
  public InputPositionEvent touchBegan;
  public InputPositionEvent touchMoved;
  public InputPositionEvent touchEnded;
  public InputPositionEvent touchStationary;
}

[System.Serializable]
public class ClickActions {
  public InputPositionEvent mouseDown;
  public InputPositionEvent mouseUp;
  public InputPositionEvent mouseMove;
}

[System.Serializable]
public class InputPositionEvent : UnityEvent<Vector2> {}

public class PlayerInput : MonoBehaviour {

  public List<TouchActions> touchActions;
  public List<ClickActions> clickActions;

  void FixedUpdate() {
    // if touch do touch, otherwise do click
    if(Input.touchCount > 0) {
      for(int i = 0; i < Input.touchCount; i++) {
        Touch touch = Input.GetTouch(i);
        TouchActions actions = touchActions[i];
        Vector2 direction = GetDirectionNormal(touch.position);
        if (touch.phase == TouchPhase.Began) { actions.touchBegan.Invoke(direction); }
        if (touch.phase == TouchPhase.Ended) { actions.touchEnded.Invoke(direction); }
        if (touch.phase == TouchPhase.Moved) { actions.touchMoved.Invoke(direction); }
        if (touch.phase == TouchPhase.Stationary) { actions.touchStationary.Invoke(direction); }
      }
    } else {
      for(int i = 0; i < 1; i++) {
        if(clickActions.Count >= i) {
          ClickActions actions = clickActions[i];
          Vector2 direction = GetDirectionNormal(Input.mousePosition);
          if(Input.GetMouseButtonDown(i)) actions.mouseDown.Invoke(direction);
          if(Input.GetMouseButtonUp(i)) actions.mouseUp.Invoke(direction);
          if(Input.GetMouseButton(i)) actions.mouseMove.Invoke(direction);
        }
      }
    }
  }

  Vector2 GetDirectionNormal(Vector2 inputPosition) {
    float x = inputPosition.x - Screen.width / 2;
    float y = inputPosition.y - Screen.height / 2;
    Vector2 target = new Vector2(x, y);
    target.Normalize();
    return target;
  }
}

