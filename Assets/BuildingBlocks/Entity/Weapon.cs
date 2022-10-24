using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

  public int damage = 1;

  public void Attack() {
    // do thing
    // turn on trigger vol
    // play animation

  }

  void OnTriggerEnter(Collider collider) {
    Entity entity = collider.GetComponent<Entity>();
    if(entity) {
      entity.Hurt(damage);
      Vector3 kbDir = entity.transform.position - transform.position;
      entity.Knockback(kbDir.normalized * Globals.FORCE_MULTIPLIER);
    }
  }

  void OnValidate() {
    BoxCollider boxCollider = GetComponent<BoxCollider>();
    if(!boxCollider) boxCollider = gameObject.AddComponent<BoxCollider>();
    boxCollider.isTrigger = true;
  }
}