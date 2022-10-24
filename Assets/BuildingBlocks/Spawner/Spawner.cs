using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {
  public GameObject thingToSpawn;
  public int numThingsToSpawn = 5;
  public float timeBetweenSpawns = 1.0f;
  public int numIntervals = 1;
  public float timeBetweenIntervals = 0.0f;

  virtual public void Start() {
    StartCoroutine("IntervalRoutine");
  }

  IEnumerator IntervalRoutine() {
    int intervalCount = 0;
    while (intervalCount < numIntervals) {
      yield return new WaitForSeconds(timeBetweenIntervals);
      int spawnCount = 0;
      while (spawnCount < numThingsToSpawn) {
        yield return new WaitForSeconds(timeBetweenSpawns);
        SpawnThing();
        spawnCount++;
      }
      intervalCount++;
    }
    Destroy(gameObject);
  }

  virtual public void SpawnThing() {
    GameObject.Instantiate(thingToSpawn, transform.position, transform.rotation);
  }
}