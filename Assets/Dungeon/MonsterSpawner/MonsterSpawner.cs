using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour {

    public List<EnemyAI> PossibleEnemies = new List<EnemyAI>();

    public float spawnCooldown = 4;

    public LayerMask targetThese;

    void Start() {
        gameObject.tag = "Destructable";
        StartCoroutine(SpawnMonster());
    }

    public IEnumerator SpawnMonster() {
        while (true) {
            if (transform.root.GetComponent<GameDungeonRoom>().active) {

                if (!GetComponent<Health>().alive)
                    break;
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0.1f, targetThese);
                foreach (RaycastHit hit in hits) {
                    if (hit.collider.gameObject.tag == "Player") {
                        if (hit.collider.gameObject.GetComponent<Health>().alive) {

                            EnemyAI go = (EnemyAI)Instantiate(PossibleEnemies[Random.Range(0, PossibleEnemies.Count)], new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
                            go.target = hit.collider.gameObject.transform;
                            break;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(spawnCooldown);

        }
        yield return null;
    }

}
