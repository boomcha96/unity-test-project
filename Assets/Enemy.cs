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

    // Start is called before the first frame update
    void Start()
    {
        enemyDataLoader = GameObject.Find("EnemyDataControl");
        enemyData = enemyDataLoader.GetComponent<EnemyData>();
        enemyData.EnemyDataLoad(ref speed, ref radius, ref waitTime, ref luck);
        groundLayer = LayerMask.GetMask("Ground");

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
            ray = new Ray(transform.position, new Vector3(offset.x, transform.position.y * (-1f), offset.z));

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
}
