using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPool : MonoBehaviour
{
    [SerializeField]
    private int maxPoolSize; //Number of copies per shape (except special ones like bomb and bonus).

    [SerializeField]
    private GameObject[] shapeReference; //Array of defined shapes.

    public List<ObjectPool> pools; //Array of object pools. Needs to be equal in length to shapeReference[].

    public GameObject spawnedShape; //Holds the shape object that is being spawned. Used as a temp variable.

    [SerializeField]
    public Material[] shapeMaterial; //For material randomization.

    [SerializeField]
    public Texture[] shapeTex; //For texture randomization.

    public Renderer shapeRenderer;
    private int randomIndex; //To determine the shape to be spawned.
    private int randomMaterial; //Renderer parameters
    private int randomTexture;
    private float randomX; //Coordinate parameters
    private float randomZ;



    private void Start()
    {
        pools = new List<ObjectPool>();

        for (int i = 0; i < shapeReference.Length; i++)
        {
            ObjectPool tmp;
            if (shapeReference[i].name == "bonus" || shapeReference[i].name == "bomb") //Prefab names.
            {
                tmp = new ObjectPool(shapeReference[i], maxPoolSize / 5); //reduced spawn rate of special shapes
            }
            else
            {
                tmp = new ObjectPool(shapeReference[i], maxPoolSize); //reduced spawn rate of special shapes
            }

            pools.Add(tmp);
        }


        StartCoroutine(SpawnShapes()); //Start spawning away.
    }

    public MainPool() { Debug.Log("Main Pool created."); }


    IEnumerator SpawnShapes()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 8)); //Spawn shapes at random intervals.

            randomIndex = Random.Range(0, shapeReference.Length); //Choose random shape.

            Debug.Log("Spawning: " + shapeReference[randomIndex].name);
            spawnedShape = pools[randomIndex].GetPooledObject(); //Retrieve object from pool.

            if (spawnedShape != null)
            {
                shapeRenderer = spawnedShape.transform.GetChild(0).GetComponent<Renderer>();

                spawnedShape.GetComponent<ShapeStatus>().isGrounded = false; //Reset the shape's info
                spawnedShape.GetComponent<ShapeStatus>().isAttached = false;
                spawnedShape.GetComponent<ShapeStatus>().isFossilized = false;

                //Apply material and texture.
                if (spawnedShape.CompareTag("bomb") || spawnedShape.CompareTag("bonus"))
                {
                    shapeRenderer.material = shapeMaterial[0];
                    shapeRenderer.material.SetTexture("_MainTex", shapeTex[0]);
                }
                else if (spawnedShape.GetComponent<ShapeStatus>().initialMaterial != null) //Not the first time it spawned.
                { //Restore original material and texture.
                    Debug.Log("Material restored.");
                    shapeRenderer.material = spawnedShape.GetComponent<ShapeStatus>().initialMaterial;
                    shapeRenderer.material.mainTexture = spawnedShape.GetComponent<ShapeStatus>().initialTexture;
                }
                else
                {
                    Debug.Log("Saving initial material."); //First time it spawned. Store its material and texture.
                    spawnedShape.GetComponent<ShapeStatus>().initialMaterial = shapeRenderer.material;
                    spawnedShape.GetComponent<ShapeStatus>().initialTexture = shapeRenderer.material.mainTexture;
                }

                string sceneName = SceneManager.GetActiveScene().name;
                if (sceneName == "Level1")
                {
                    randomX = Random.Range(-2.75f, 2.75f); //Set spawn perimeter depending on arena specs.
                    randomZ = Random.Range(-1.8f, 1.8f);
                }
                else if (sceneName == "Level2")
                {
                    randomX = Random.Range(-2.1f, 2.1f);
                    randomZ = Random.Range(-2.0f, 2.0f);
                }

                else if (sceneName == "Level3")
                {

                }
                Rigidbody rigidBody = spawnedShape.GetComponent<Rigidbody>();
                rigidBody.constraints = RigidbodyConstraints.FreezeRotation; //To avoid any weird flips.

                spawnedShape.transform.position = new Vector3(randomX, 10f, randomZ); //Right from above.
                spawnedShape.SetActive(true);
                Debug.Log(spawnedShape.name + " successfully spawned.");
            }
            else
            {
                Debug.Log(shapeReference[randomIndex].name + " could not be spawned.");
            }

            //Draw a spawn map for every arena.
        }
    }
}
