using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject enemy;
    public GameObject particles;

    private float delayEmitting = 0.5f;
    private bool isEmittingParticles = false;

    private void Start()
    {
        particles.GetComponent<ParticleSystem>().Stop();
    }

    private void Update()
    {
        if (isEmittingParticles)
            particles.GetComponent<ParticleSystem>().Play();
        else
            particles.GetComponent<ParticleSystem>().Stop();

    }

    //-- Spawn de los enemigos --
    public void SpawnEnemies(Vector3 spawnPoint, int enemies)
    {
        Vector3 nextSpawn;
        StartCoroutine(EmittParticle());
        for (int i = 0; i < enemies; i++)
        {
            nextSpawn = new Vector3 (spawnPoint.x + i*2, spawnPoint.y , spawnPoint.z+i*2);
            enemy.GetComponent<EnemyController>().InstantiateEnemy(nextSpawn);
        }
    }

    //-- Emisión de partículas durante unos segundos --
    private IEnumerator EmittParticle()
    {
        isEmittingParticles = true;
        yield return new WaitForSeconds(delayEmitting);
        isEmittingParticles = false;
    }
}
