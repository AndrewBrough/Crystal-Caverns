using System.Collections;
using UnityEngine;

/// <summary>
/// Spawn object with AI and assign player as targetPosition
/// </summary>
public class EnemySpawner : Spawner {

  protected Transform playerTransform;

  override public void Start() {
    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    base.Start();
  }

  override public void SpawnThing() {
    GameObject.Instantiate(thingToSpawn, transform.position, transform.rotation);
    AI ai = thingToSpawn.GetComponent<AI>();
    if(ai) ai.targetPosition = playerTransform;
  }
}