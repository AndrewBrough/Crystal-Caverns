using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIAction {
  Idle = 0,
  Move = 1,
  Attack = 2,
}

// Do thing
// When done thing, do next thing (defined at end of thing 1)
public class AI : MonoBehaviour {
  protected Entity _entity;
  public Transform targetPosition;
  public float followTime = 1.0f;
  public float timeBetweenAttacks = 1.0f;

  List<AIAction> actionQueue;

  void Start() {
    _entity = GetComponent<Entity>();
    actionQueue = new List<AIAction>();
    StartCoroutine(Follow());
  }

  void Update() {
    if(!targetPosition && actionQueue.Count > 0) return; // remove if want to do something while not moving

    AIAction currentAction = actionQueue[0];
    if(currentAction == AIAction.Move) {
      Vector3 direction = targetPosition.position - transform.position;
      _entity.Move(direction.normalized);
    }
  }

  // ACTIONS
  // An action must add an action to queue, wait, then remove itself and start another action (or not)
  virtual public IEnumerator Follow() {
    actionQueue.Add(AIAction.Move);
    yield return new WaitForSeconds(followTime);
    actionQueue.RemoveAt(0);
    StartCoroutine(Idle());
  }

  virtual public IEnumerator Idle() {
    actionQueue.Add(AIAction.Idle);
    yield return new WaitForSeconds(timeBetweenAttacks);
    actionQueue.RemoveAt(0);
    StartCoroutine(Follow());
  }
}