using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwan_Bugs : MonoBehaviour
{
    public Transform[] spwanLocation;
    public GameObject[] whatToSpawnPrefab;
    public GameObject LBugPrefab;

    public int poolSize;
    private Queue<GameObject> bug = new Queue<GameObject>();
    private Queue<GameObject> bug2 = new Queue<GameObject>();
    private Queue<GameObject> bug3 = new Queue<GameObject>();
    private Queue<GameObject> vBug = new Queue<GameObject>();
    private Queue<GameObject> vBug2 = new Queue<GameObject>();
    private Queue<GameObject> vBug3 = new Queue<GameObject>();

    private float currentTime;
    private float lastBugSpawnTime;
    private float lastVBugSpawnTime;

    private int prevPosInd;
    public bool lifeSpawned = false;

    void Start()
    {
        GameObject temp;
        //Creating Pool
        for (int i = 0; i < poolSize; i++)
        {
            temp = Instantiate(whatToSpawnPrefab[0], new Vector3(250f, 250f, 250f), Quaternion.identity) as GameObject;
            temp.SetActive(false);
            bug.Enqueue(temp);
            temp = Instantiate(whatToSpawnPrefab[1], new Vector3(250f, 250f, 250f), Quaternion.identity) as GameObject;
            temp.SetActive(false);
            bug2.Enqueue(temp);
            temp = Instantiate(whatToSpawnPrefab[2], new Vector3(250f, 250f, 250f), Quaternion.identity) as GameObject;
            temp.SetActive(false);
            bug3.Enqueue(temp);
            temp = Instantiate(whatToSpawnPrefab[3], new Vector3(250f, 250f, 250f), Quaternion.identity) as GameObject;
            temp.SetActive(false);
            vBug.Enqueue(temp);
            temp = Instantiate(whatToSpawnPrefab[4], new Vector3(250f, 250f, 250f), Quaternion.identity) as GameObject;
            temp.SetActive(false);
            vBug2.Enqueue(temp);
            temp = Instantiate(whatToSpawnPrefab[5], new Vector3(250f, 250f, 250f), Quaternion.identity) as GameObject;
            temp.SetActive(false);
            vBug3.Enqueue(temp);
        }
    }

    void Update()
    {
        currentTime = Time.time;

        if (currentTime - lastBugSpawnTime > GameData.bugSpawnDelay && bug.Count != 0)
        {
            SpawnBug();
            lastBugSpawnTime = currentTime;
        }

        if (currentTime - lastVBugSpawnTime > GameData.vBugSpawnDelay && vBug.Count != 0)
        {
            SpawnVBug();
            lastVBugSpawnTime = currentTime;
        }

        if (!lifeSpawned && GameData.score % 50 == 0 && GameData.score != 0)
        {
            lifeSpawned = true;
            SpawnLBug();
        }

        else if (lifeSpawned && GameData.score % 50 != 0) lifeSpawned = false;
    }

    void SpawnBug()
    {
        int posInd = Random.Range(0, spwanLocation.Length);
        while (posInd == prevPosInd)
        {
            posInd = Random.Range(0, spwanLocation.Length);
        }
        prevPosInd = posInd;
        int type = Random.Range(0,3);
        GameObject temp = null;
        if(type == 0) temp = bug.Dequeue();
        else if(type == 1) temp = bug2.Dequeue();
        else temp = bug3.Dequeue();
        temp.SetActive(true);
        temp.transform.position = spwanLocation[posInd].position;
        temp.GetComponent<BugController>().Initialize();
    }

    void SpawnVBug()
    {
        int posInd = Random.Range(0, spwanLocation.Length);
        while (posInd == prevPosInd)
        {
            posInd = Random.Range(0, spwanLocation.Length);
        }
        prevPosInd = posInd;

        int type = Random.Range(3,6);
        GameObject temp = null;
        if(type == 3) temp = vBug.Dequeue();
        else if(type == 4) temp = vBug2.Dequeue();
        else temp = vBug3.Dequeue();

        temp.SetActive(true);
        temp.transform.position = spwanLocation[posInd].position;
        temp.GetComponent<VBugController>().Initialize();
    }

    void SpawnLBug()
    {
        int posInd = Random.Range(0, spwanLocation.Length);
        while (posInd == prevPosInd)
        {
            posInd = Random.Range(0, spwanLocation.Length);
        }
        prevPosInd = posInd;
        GameObject temp = Instantiate(LBugPrefab, spwanLocation[posInd].position, Quaternion.identity);
        temp.GetComponent<VBugController>().Load();
    }

    public void InsertIntoBugPool(ref GameObject item)
    {
        item.SetActive(false);
        if(item.tag == "bug") bug.Enqueue(item);
        else if(item.tag == "bug2") bug2.Enqueue(item);
        else bug3.Enqueue(item);
    }

    public void InsertIntoVBugPool(ref GameObject item)
    {
        item.SetActive(false);
        if(item.tag == "vBug") vBug.Enqueue(item);
        else if(item.tag == "vBug2") vBug2.Enqueue(item);
        else vBug3.Enqueue(item);
    }
}
