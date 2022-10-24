using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {
  public Transform targetToFollow;
  Rigidbody _rigidbody;

  public float speed = 1.0f;
  float _initialHeight = 0.0f;

  void Start() {
    _rigidbody = GetComponent<Rigidbody>();
    _initialHeight = transform.position.y;
  }

  void Update() {

    Vector3 dir = targetToFollow.position - transform.position;
    Vector3 targetPos = new Vector3(dir.x, dir.y + _initialHeight, dir.z);
    _rigidbody.AddForce(targetPos * speed);
  }
}