using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  public int damage = 1;
  public float attackDuration_seconds = 0.2f;
  protected Collider _collider;

  void OnValidate() {
    BoxCollider boxCollider = GetComponent<BoxCollider>();
    if(!boxCollider) boxCollider = gameObject.AddComponent<BoxCollider>();
    boxCollider.isTrigger = true;
  }

  void Awake() {
    _collider = GetComponent<Collider>();
    _collider.isTrigger = true;
    _collider.enabled = false;
  }

  virtual public void Attack() {
    Debug.Log("Attack!");
    // play animation or set trigger enabled
    _collider.enabled = true;
    StartCoroutine(EndAttack());
  }

  IEnumerator EndAttack() {
    yield return new WaitForSeconds(attackDuration_seconds);
    _collider.enabled = false;
  }

  void OnTriggerEnter(Collider collider) {
    Entity entity = collider.GetComponent<Entity>();
    if(entity) {
      entity.Hurt(damage);
      Vector3 kbDir = entity.transform.position - transform.position;
      entity.Knockback(kbDir.normalized * Globals.FORCE_MULTIPLIER);
    }
  }

}