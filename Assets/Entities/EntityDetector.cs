using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class EntityDetectedEvent : UnityEvent<Entity> {}

public class EntityDetector : MonoBehaviour {
  public float detectionRadius = 5.0f;
  public string tagToDetect = null;

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
    }
  }

  void OnTriggerExit(Collider collider) {
    Entity entity = collider.GetComponent<Entity>();
    if(entity && ValidateTag(entity)) {
      detectedEntities.Remove(entity);
      undetectAction.Invoke(entity);
    }
  }
}