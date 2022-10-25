using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour {

  [Tooltip("Which Weapon component to perform the attack.")]
  public Weapon weapon;
  [Tooltip("Time in seconds to repeat attack. Set to 0 for no repeating. Take care not to make this faster than your attack animation or your animation will be cut short.")]
  public float timeBetweenAttacks = 1.0f;

  protected Entity _target; // unused
  protected IEnumerator attackCoroutine;

  void Awake() {
    weapon = GetComponentInChildren<Weapon>();
  }

  virtual public void Attack(Entity target = null) {
    _target = target;
    attackCoroutine = AttackRoutine();
    StartCoroutine(attackCoroutine);
  }

  virtual public void EndAttack() {
    StopCoroutine(attackCoroutine);
  }

  IEnumerator AttackRoutine() {
    while(timeBetweenAttacks > 0.0f) {
      weapon.Attack();
      yield return new WaitForSeconds(timeBetweenAttacks);
    }
  }

}