using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShapeStatus : MonoBehaviour
{
    public bool isGrounded = false; //To check if the object has successfully landed on the arena.
    public bool isAttached = false; //To check if the shape already belongs to a cluster.
    public bool isFossilized = false; //To check if the shape has already "died" or fossilized.
    public int clusterID; //To identify which cluster the shape is attached to.

    public Material initialMaterial = null; //This is useful for objects that had their material changed due to Fossilization.
    public Texture initialTexture = null;

}