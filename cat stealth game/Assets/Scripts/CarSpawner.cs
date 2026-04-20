using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public GameObject car;

    public List<GameObject> carList;

    private Vector3 spawnPoint;

    public Vector3 spawnRotation;
    public Vector3 moveDirection;
    public float carSpeed;

    public float carSpawnTimer;
    public float carDespawnTimer;

    void Start()
    {
        spawnPoint = transform.position;
        StartCoroutine(CarSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject carInstance in carList)
        {
            carInstance.transform.Translate(moveDirection * carSpeed * Time.deltaTime);
        }
    }

    void SpawnCar()
    {
        var carInstance = Instantiate(car, spawnPoint, Quaternion.Euler(spawnRotation));
        carList.Add(carInstance);
        StartCoroutine(CarDespawner(carInstance));
    }

    public IEnumerator CarSpawnRoutine()
    {
        yield return new WaitForSeconds(carSpawnTimer);
        SpawnCar();
        StartCoroutine(CarSpawnRoutine());
    }

    public IEnumerator CarDespawner(GameObject car)
    {
        yield return new WaitForSeconds(carDespawnTimer);
        Destroy(car);
        carList.Remove(car);
    }
}
