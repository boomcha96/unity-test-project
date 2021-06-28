using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed;
    public float radius;
    public float waitTime;
    public float luck;
    EnemyData enemyData;
    public GameObject enemyDataLoader;

    LayerMask groundLayer;
    private Vector3 targetPoint;
    private Ray ray;

    bool toDestroy = false;
    private float sleepTime = 0;
    private bool toSleep = false;
    private float dist = 0;

    //string json;

    

    // Start is called before the first frame update
    void Start()
    {
        enemyDataLoader = GameObject.Find("EnemyDataControl");
        enemyData = enemyDataLoader.GetComponent<EnemyData>();
        //Debug.Log(enemyData.enemyStats);
        //(speed, radius, waitTime, luck) = enemyData.EnemyDataLoad();
        enemyData.EnemyDataLoad(ref speed, ref radius, ref waitTime, ref luck);
        groundLayer = LayerMask.GetMask("Ground");

        //EnemyStats enemyStats = JsonUtility.FromJson<EnemyStats>(json);
        //Debug.Log(enemyStats.speed_min);

        //speed = Random.Range(enemyStats.speed_min, enemyStats.speed_max);
        //radius = Random.Range(enemyStats.radius_min, enemyStats.radius_max);
        //waitTime = Random.Range(enemyStats.waitTime_min, enemyStats.waitTime_max);
        //luck = Random.Range(enemyStats.luck_min, enemyStats.luck_max);

        targetPoint = FindTarget();
        transform.LookAt(targetPoint);


    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, targetPoint);
        if (dist > Time.deltaTime * speed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            if (!toSleep)
            {
                if (Random.value < 0.3f) {
                    sleepTime = waitTime;
                    toSleep = true;
                }
            }
            if (sleepTime <= 0)
            {
                targetPoint = FindTarget();
                transform.LookAt(targetPoint);
                toSleep = false;
            }
            else
            {
                sleepTime -= Time.deltaTime;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }

    private Vector3 FindTarget()
    {
        Vector3 pos;
        do
        {
            Vector3 offset = Random.insideUnitSphere * radius;
            pos = transform.position + offset;
            //Debug.Log(transform.position + " " + pos);
            //ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, 0f, pos.z));
            ray = new Ray(transform.position, new Vector3(offset.x, transform.position.y * (-1f), offset.z));
            //Debug.Log(ray + " " + Physics.Raycast(ray, radius + 5f, groundLayer));

        } while (!Physics.Raycast(ray, radius + 5f, groundLayer));
        
        return new Vector3(pos.x, transform.position.y, pos.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name
        Enemy otherCube = collision.collider.gameObject.GetComponent<Enemy>();
        float sumLuck = this.luck + otherCube.luck;
        if (!(otherCube.toDestroy || this.toDestroy))
        {
            if (Random.Range(0f, sumLuck) >= this.luck)
            {
                toDestroy = true;
                Destroy(this.gameObject);
            }
            else
            {
                otherCube.toDestroy = true;
                Destroy(collision.collider.gameObject);
            }
        }
        if (otherCube.toDestroy)
        {
            targetPoint = transform.position;
        }
    }

    public void Deconstruct(out float speed, out float radius, out float waitTime, out float luck)
    {
        speed = this.speed;
        radius = this.radius;
        waitTime = this.waitTime;
        luck = this.luck;
    }

    //[System.Serializable]
    //private class EnemyStats
    //{
    //    public float speed_min;
    //    public float speed_max;
    //    public float radius_min;
    //    public float radius_max;
    //    public float waitTime_min;
    //    public float waitTime_max;
    //    public float luck_min;
    //    public float luck_max;

    //    //public static EnemyStats CreateFromJSON(string jsonString)
    //    //{
    //    //    return JsonUtility.FromJson<EnemyStats>(jsonString);
    //    //}

    //}
}
