using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunTimeInstantiation : MonoBehaviour
{

    [SerializeField] GameObject zombieObject;
    [SerializeField] GameObject playerObject;
    [SerializeField] TextMeshProUGUI waveTextMesh;

    private int zombiesPerWave = 8;
    private int zombieWaveHealth = 100;
    private int zombieWaveDamage = 20;
    private int currentWave = 1;

    [Serializable]
    public struct SpawnCoordinates
    {
        public float x;
        public float y;
        public float z;
    }
    [SerializeField]
    private SpawnCoordinates[] spawnLocation;

    void Start()
    {
        waveTextMesh.text = "Wave " + currentWave;
        InvokeRepeating("InstantiateWave", 0, 35f);
    }

    private void InstantiateWave()
    {
        StartCoroutine("InstantiateZombie");
    }
    IEnumerator InstantiateZombie()
    {
        for (int i = 0; i < zombiesPerWave; i++)
        {
            int randomLocation = UnityEngine.Random.Range(0, spawnLocation.Length);
            GameObject zombieInstance = Instantiate(zombieObject, new Vector3(spawnLocation[randomLocation].x, spawnLocation[randomLocation].y, spawnLocation[randomLocation].z), Quaternion.identity);
            zombieInstance.GetComponent<EnemyAI>().target = playerObject.transform;
            zombieInstance.GetComponent<EnemyHealth>().hitPoints = zombieWaveHealth;
            zombieInstance.GetComponent<EnemyAttack>().damage = zombieWaveDamage;
 
            yield return new WaitForSeconds(20 / zombiesPerWave);        
        }

        currentWave++;
        waveTextMesh.text = "Wave " + currentWave;
        if (currentWave <= 6)
        {
            zombiesPerWave+=2;
            zombieWaveHealth += 15;

            if(currentWave == 4)
            {
                zombieWaveDamage += 10;
            }
        }
        else
        {
            zombieWaveHealth += 20;

            if (currentWave == 8)
            {
                zombieWaveDamage += 10;
            }
        }
    }
}
