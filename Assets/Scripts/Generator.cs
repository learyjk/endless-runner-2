using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] availableObjects;
    public List<GameObject> objects;
    private GameObject player;
    private float screenWidthInPoints;

    private float objectsMinDistance = 30.0f;
    private float objectsMaxDistance = 50.0f;

    public float objectsMinY = -1.0f;
    public float objectsMaxY = 1.0f;

    public float objectsMinRotation = -45.0f;
    public float objectsMaxRotation = 45.0f;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;

        StartCoroutine(GeneratorCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateObjectsIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }


    void AddObject(float lastObjectX)
    {
        int randomIndex = Random.Range(0, availableObjects.Length);

        GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);
        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        
        SetMinYMaxY(obj.tag);
        
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);

        //float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
        //obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

        objects.Add(obj);            
    }

    void GenerateObjectsIfRequired()
    {

        float playerX = player.transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;

        List<GameObject> objectsToRemove = new List<GameObject>();
        foreach (var obj in objects)
        {

            float objX = obj.transform.position.x;

            farthestObjectX = Mathf.Max(farthestObjectX, objX);

            if (objX < removeObjectsX) 
            {           
                objectsToRemove.Add(obj);
            }
        }

        foreach (var obj in objectsToRemove)
        {
            objects.Remove(obj);
            Destroy(obj);
        }

        if (farthestObjectX < addObjectX)
        {
            AddObject(farthestObjectX);
        }
    }

    void SetMinYMaxY(string tag)
    {
        if (tag == "CoinV")
        {
            objectsMinY = -2.4f;
            objectsMaxY = 2.4f;
        }
        if (tag == "Lightning")
        {
            objectsMinY = -2.0f;
            objectsMaxY = 2.0f;
        }
        if (tag == "Plane")
        {
            objectsMinY = -3.5f;
            objectsMaxY = 3.5f;
        }
    }
}
