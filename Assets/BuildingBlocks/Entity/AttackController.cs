using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

  [Tooltip("Which Weapon component to perform the attack.")]
  public Weapon weapon;
  [Tooltip("Time in seconds to repeat attack. Set to 0 for no repeating.")]
  public float timeBetweenAttacks = 1.0f;

  protected Entity _target; // unused

  void Awake() {
    weapon = GetComponentInChildren<Weapon>();
  }

  virtual public void Attack(Entity target = null) {
    _target = target;
    StartCoroutine(AttackRoutine());
  }

  virtual public void EndAttack() {
    StopCoroutine(AttackRoutine());
  }

  IEnumerator AttackRoutine() {
    while(timeBetweenAttacks > 0.0f) {
      weapon.Attack();
      yield return new WaitForSeconds(timeBetweenAttacks);
    }
  }

}