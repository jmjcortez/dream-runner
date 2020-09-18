using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGenerator : MonoBehaviour
{

    public Transform[] tabSpawnPoints;
    public GameObject tab;

    public int chanceToSpawn;

    void Start()
    {
        
    }

    public void SpawnTab() {

        float spawnSelect = Random.Range(0, 25);

        Transform selectedSpawnPoint = tabSpawnPoints[Random.Range(0,1)];

        Vector3 spawnPoint = new Vector3(selectedSpawnPoint.transform.position.x, selectedSpawnPoint.transform.position.y + Random.Range(0,3), selectedSpawnPoint.transform.position.z);

        if (spawnSelect <= chanceToSpawn) {
            Instantiate(tab, spawnPoint, selectedSpawnPoint.transform.rotation);
        }
        
    }

}
