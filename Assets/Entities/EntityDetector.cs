using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class EntityDetectedEvent : UnityEvent<Entity> {}

public class EntityDetector : MonoBehaviour {
  public float detectionRadius = 5.0f;
  public string tagToDetect = null;
  public float timeBetweenRepeatActions = 1.0f;

  public EntityDetectedEvent detectAction;
  public EntityDetectedEvent undetectAction;
  public List<Entity> detectedEntities = new List<Entity>(); // TODO make private when done debugging
  

  void OnValidate() {
    SphereCollider sphereCollider = GetComponent<SphereCollider>();
    if(!sphereCollider) sphereCollider = gameObject.AddComponent<SphereCollider>();
    sphereCollider.isTrigger = true;
    sphereCollider.radius = detectionRadius;
  }

  void Update() {
    detectedEntities.ForEach(entity => {
      if(!entity) detectedEntities.Remove(entity);
    });
  }

  bool ValidateTag(Entity entity) {
    return tagToDetect != null ? entity.CompareTag(tagToDetect) : true;
  }

  void OnTriggerEnter(Collider collider) {
    Entity entity = collider.GetComponent<Entity>();
    if(entity && ValidateTag(entity)) {
      detectedEntities.Add(entity);
      detectAction.Invoke(entity);
      StartCoroutine(RepeatAction());
    }
  }

  void OnTriggerExit(Collider collider) {
    Entity entity = collider.GetComponent<Entity>();
    if(entity && ValidateTag(entity)) {
      detectedEntities.Remove(entity);
      undetectAction.Invoke(entity);
    }
  }

  IEnumerator RepeatAction() {
    while(detectedEntities.Count > 0) {
      yield return new WaitForSeconds(timeBetweenRepeatActions);
      if(detectedEntities.Count > 0) { // make sure the entity hasn't left before trying to invoke on it
        Entity firstDetectedEntity = detectedEntities[0];
        detectAction.Invoke(firstDetectedEntity);
      } else {
        undetectAction.Invoke(null);
      }
    }
  }
}