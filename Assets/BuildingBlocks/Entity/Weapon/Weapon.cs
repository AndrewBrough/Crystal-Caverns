using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  public int damage = 1;
  protected Collider _collider;

  public Animator animator;

  void OnValidate() {
    BoxCollider boxCollider = GetComponent<BoxCollider>();
    if(!boxCollider) boxCollider = gameObject.AddComponent<BoxCollider>();
    boxCollider.isTrigger = true;
    animator = GetComponentInChildren<Animator>();
  }

  void Awake() {
    _collider = GetComponent<Collider>();
    _collider.isTrigger = true;
    _collider.enabled = false;
  }

  /// <summary>
  /// Play animation which will control hitbox.
  /// </summary>
  virtual public void Attack() {
    animator.SetTrigger("Attack");
  }

  void OnTriggerEnter(Collider collider) {
    Entity entity = collider.GetComponent<Entity>();
    if(entity) {
      entity.Hurt(damage);
      Vector3 kbDir = entity.transform.position - transform.position;
      entity.Knockback(kbDir.normalized * Globals.FORCE_MULTIPLIER);
    }
  }

  public void EnableTrigger() {
    _collider.enabled = true;
  }

  public void DisableTrigger() {
    _collider.enabled = false;
  }

}