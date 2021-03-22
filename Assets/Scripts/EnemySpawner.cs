using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    private IEnumerator Start()
    {
        if (looping == true)
        {
            while (looping)
            {
                int random = Random.Range(0, waveConfigs.Count);
                yield return StartCoroutine(SpawnAllWaves(random));
            }
        }
        else if(looping == false)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
    }

    private IEnumerator SpawnAllWaves(int i)
    {
        for( ; i < waveConfigs.Count; i++)
        {
            WaveConfig currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            WaveConfig currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, waveConfig.GetEnemyPrefab().transform.rotation);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
