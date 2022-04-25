using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cluster : MonoBehaviour
{
    public int netScore;
    public int multiplier;
    private GameObject shape;
    public List<GameObject> attachedShapes;


    public int CalculateCluster() //Tallies all shapes in a cluster and despawns them all at once.
    { //Returns the total tallied score.
        netScore = 0;
        multiplier = 0;
        for (int i = 0; i < attachedShapes.Count; i++)
        {
            shape = attachedShapes[i];
            if (shape.CompareTag("bonus"))
            {
                multiplier++;
            }

            else
            {
                netScore += 100;
            }

            shape.SetActive(false);
        }

        netScore = netScore * 2 * multiplier;

        return netScore;
    }

    public void Attach(GameObject attachedObject)
    {
        attachedShapes.Add(attachedObject);
        attachedObject.GetComponent<ShapeStatus>().isAttached = true;
    }

    public Cluster(GameObject firstObject) //New cluster constructor.
    {
        attachedShapes = new List<GameObject>();
        attachedShapes.Add(firstObject);
        firstObject.GetComponent<ShapeStatus>().isAttached = true;
    }
}
