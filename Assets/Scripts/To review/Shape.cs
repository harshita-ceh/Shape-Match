using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
 
    [SerializeField]
    private float moveForce = 10f;

    private Vector3 movement;
    private Rigidbody shapeBody;

    private bool _fossilized = false;
    private int _attachedShapes = 0;

    private string GROUND_TAG = "Ground";
    private string BOMB_TAG = "shape4";
    private string BONUS_TAG = "shape5";
    //private string[] SHAPE_TAGS;

    public bool Fossilized
    {
        get
        {
            return _fossilized;
        }

        set
        {
            _fossilized = value;
        }
    }

    public int AttachedShapes
    {
        get
        {
            return _attachedShapes;
        }

        set
        {
            _attachedShapes = value;
        }
    }

    void ShapeMove()
    {
        movement = Input.mousePosition;
        movement.z = movement.y; //Force movement to comply with mouse input's x-y coordinate system.
        transform.position += movement * Time.deltaTime * moveForce;
    }

    private void OnCollisionEnter(Collider other)
    {
        if (gameObject.name == other.name)
        {
            Debug.Log("It's a match!");
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

    }


    void InvokeFossilization()
    {
        Fossilized = true;
        //change color
    }

    public Shape() { }

    private void Awake()
    {
        shapeBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //ShapeMove();

    }
}
