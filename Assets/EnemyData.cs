using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyData : MonoBehaviour
{
    public EnemyStats enemyStats;

    private void Awake()
    {
        string json = File.ReadAllText(Application.dataPath + "/EnemyState.json");
        enemyStats = EnemyStats.CreateFromJSON(json);
    }

    public void EnemyDataLoad(ref float speed, ref float radius, ref float waitTime, ref float luck)
    {
        speed = Random.Range(enemyStats.speed_min, enemyStats.speed_max);
        radius = Random.Range(enemyStats.radius_min, enemyStats.radius_max);
        waitTime = Random.Range(enemyStats.waitTime_min, enemyStats.waitTime_max);
        luck = Random.Range(enemyStats.luck_min, enemyStats.luck_max);
    }

    [System.Serializable]
    public class EnemyStats
    {
        public float speed_min;
        public float speed_max;
        public float radius_min;
        public float radius_max;
        public float waitTime_min;
        public float waitTime_max;
        public float luck_min;
        public float luck_max;

        public static EnemyStats CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<EnemyStats>(jsonString);
        }

    }

}
