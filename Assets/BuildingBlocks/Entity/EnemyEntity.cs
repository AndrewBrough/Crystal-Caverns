using UnityEngine;

public class EnemyEntity : Entity {

  public int touchDamage = 1;

  public void OnCollisionEnter(Collision collision) {
    if(collision.gameObject.CompareTag("Player")) {
      Entity hitEntity = collision.gameObject.GetComponent<Entity>();
      hitEntity.Hurt(touchDamage);
      Vector3 kbDir = collision.transform.position - transform.position;
      hitEntity.Knockback(kbDir.normalized * Globals.FORCE_MULTIPLIER);
    }
    
  }
}
