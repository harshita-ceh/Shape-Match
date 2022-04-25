using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] shapeReference; //Array of shapes.

    private ObjectPool[] objectPools;
    private GameObject spawnedShape;
    private ObjectPool objectPoolCreator;


    private int randomIndex; //To determine the shape to be spawned.
    private float randomX;
    private float randomZ;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shapeReference.Length; i++)
        { // Initialize shape pools.
            Debug.Log("iteration#: " + i.ToString());
            if (shapeReference[i].name == "ob4" || shapeReference[i].name == "ob5")
            { //Either bomb or bonus, reduced spawn rate for game balance.
                objectPoolCreator = new ObjectPool(shapeReference[i], 7);
                Debug.Log("Shape ref (Special): " + shapeReference[i].name);
                objectPools[i] = objectPoolCreator;
            }
            else
            {
                objectPoolCreator = new ObjectPool(shapeReference[i], 50);
                Debug.Log("Shape ref (Regular): " + shapeReference[i].name);
                objectPools[i] = objectPoolCreator;
            }

        }

        StartCoroutine(SpawnShapes());
    }

    IEnumerator SpawnShapes()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 8));

            randomIndex = Random.Range(0, shapeReference.Length);

            spawnedShape = objectPools[randomIndex].GetPooledObject();

            if (spawnedShape != null)
            {
                randomX = Random.Range(-2.85f, 2.85f); //Set spawn perimeter depending on arena specs.
                randomZ = Random.Range(-1.8f, 1.8f);

                spawnedShape.transform.position = new Vector3(randomX, 10f, randomZ);
                spawnedShape.SetActive(true);
            }
        }
    }
}
