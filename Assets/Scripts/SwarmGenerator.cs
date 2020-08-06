using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmGenerator : MonoBehaviour
{
    public GameObject[] swarms;
    public bool summon = true;
    public float summonTime = 1.0f;

    private IEnumerator coroutine;
    private Transform[] spawnPoints;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        coroutine = SummonSwarm(summonTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator SummonSwarm(float waitTime)
    {
        while (summon)
        {
            yield return new WaitForSeconds(waitTime);
            int randomPoint = Random.Range(1, spawnPoints.Length);
            Vector2 spawnPoint = spawnPoints[randomPoint].position;
            InstantiateRandomSwarm(spawnPoint);
        }
    }

    private void InstantiateRandomSwarm(Vector2 spawnPoint)
    {
        GameObject swarmToSummon = swarms[Random.Range(0, swarms.Length)];
        Instantiate(swarmToSummon, spawnPoint, Quaternion.identity);
    }
}
