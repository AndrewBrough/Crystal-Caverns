using UnityEngine;

public class MovementArrow : MonoBehaviour {
  
  public void Show() {
    gameObject.SetActive(true);
  }

  public void Hide() {
    gameObject.SetActive(false);
  }

  public void Point(Vector2 direction) {
    Vector3 dirIn3d = new Vector3(direction.x, 0, direction.y);
    transform.LookAt(transform.position + dirIn3d);
  }

  void Awake() {
    Hide();
  }
}