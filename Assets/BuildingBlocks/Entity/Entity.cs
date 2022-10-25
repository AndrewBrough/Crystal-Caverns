using UnityEngine;

public enum EntityAnimation {
  idle = 0,
  walking = 1,
}

/// <summary>
/// Should manage state and delegate actions to other controllers.
/// eg. AttackController
/// </summary>
public class Entity : MonoBehaviour {

  private Rigidbody _rigidbody;
  private Animator _animator;

  public int health = 3;
  public float speed = 100.0f;

  protected AttackController _attackController;

  protected Vector3 _lookTarget = Vector3.zero;

  void Awake() {
    _rigidbody = GetComponent<Rigidbody>();
    _animator = GetComponent<Animator>();
    _attackController = GetComponent<AttackController>();
  }

  virtual public void Update() {
    DoLook();
  }

  //LOOK
  void Look(Vector3 lookTarget) {
    _lookTarget = lookTarget;
  }

  void DoLook() {
    if(_lookTarget != Vector3.zero) {
      transform.LookAt(new Vector3(_lookTarget.x, transform.position.y, _lookTarget.z));
    } else {
      if(_rigidbody.velocity.magnitude > 0.01f) {
        Vector3 moveDirection = transform.position + _rigidbody.velocity.normalized;
        transform.LookAt(moveDirection);
      }
    }
  }

  //MOVEMENT
  public void Move(Vector2 direction) {
    Vector3 force = new Vector3(direction.x, 0, direction.y);
    Move(force);
  }

  public void Move(Vector3 direction) {
    Vector3 force = direction * speed * Time.deltaTime;
    _rigidbody.AddForce(force);
    if(_animator) _animator.SetBool(EntityAnimation.walking.ToString(), true);
  }

  public void EndMove() {
    if(_animator) _animator.SetBool(EntityAnimation.walking.ToString(), false);
  }

  public bool IsMoving() {
    return _rigidbody.velocity.magnitude > 0;
  }

  // COMBAT
  virtual public void Attack(Entity entity = null) {
    _attackController.Attack();
    if(entity) Look(entity.transform.position);
  }

  virtual public void EndAttack() {
    _attackController.EndAttack();
    _lookTarget = Vector3.zero;
  }

  virtual public void Hurt(int amount) {
    ChangeHealth(-amount);
  }

  virtual public void Knockback(Vector3 force) {
    _rigidbody.AddForce(force);
  }

  // HEALTH
  public void ChangeHealth(int amount) {
    health += amount;
    if (health <= 0) Die();
  }

  virtual public void Die() {
    Destroy(gameObject);
  }
}
