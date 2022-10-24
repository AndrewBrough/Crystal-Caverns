using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity {

  override public void Die() {
    SceneManager.LoadScene("SampleScene");
  }
}
