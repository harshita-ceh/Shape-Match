using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    public List<Cluster> clusters;
    public Cluster tempCluster;
    public int clusterScoreTotal;

    public AudioSource audioData;

    public Renderer shapeRendererCollider;
    public Renderer shapeRendererCollidee; //I know it is not a real word. Please ignore it :)
    public Renderer fossilizer;
    public Renderer fossilizee; //Same here.

    public delegate void ShapesMatched();
    public static event ShapesMatched shapesMatchedInfo; //Increments score.

    public delegate void ShapeFellOff();
    public static event ShapeFellOff shapeFellOffInfo; //Lose health.

    public delegate void ShapeFossilized();
    public static event ShapeFellOff shapeFossilizedInfo; //Lose health.

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Ground")) //Check if the shape just landed.
        { //First contact with arena.
            Debug.Log("Grounded");
            audioData = other.gameObject.GetComponent<AudioSource>();
            audioData.Play(0);
            gameObject.GetComponent<ShapeStatus>().isGrounded = true;

        }

        else if (other.gameObject.CompareTag("Despawn")) //Check if the shape fell off.
        {
            audioData = other.gameObject.GetComponent<AudioSource>();
            audioData.Play(0);
            shapeFellOffInfo?.Invoke();
            gameObject.SetActive(false);
        }

        else if (gameObject.CompareTag("Despawn"))
        {
            audioData = gameObject.GetComponent<AudioSource>();
            audioData.Play(0);
            shapeFellOffInfo?.Invoke();
            other.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("bomb") || gameObject.CompareTag("bomb")) // && gameObject.GetComponent<ShapeStatus>().isGrounded == true
        { //An object touched the bomb, despawn all objects attached but do not invoke any event.
            Debug.Log("BOOM.");
            audioData = gameObject.GetComponent<AudioSource>();
            audioData.Play(0);
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

        else if (other.gameObject.CompareTag("bonus") || gameObject.CompareTag("bonus"))
        { //An object touched the bonus shape, despawn all objects attached and invoke shapeMatched event.
            shapesMatchedInfo?.Invoke();
            audioData = other.gameObject.GetComponent<AudioSource>();
            audioData.Play(0);
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }

        else if (gameObject.CompareTag(other.gameObject.tag)) //Matching shape scenario
        {
            Debug.Log("Tag matched.");
            audioData = other.gameObject.GetComponent<AudioSource>();
            audioData.Play(0);
            StartCoroutine(Matched());
        }

        else
        {
            Debug.Log("Fossilization started.");
            shapeFossilizedInfo?.Invoke();
            var renderer = other.gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.grey); //Supposed to turn the shape grey.
        }

    }

    IEnumerator Matched()
    {

        shapesMatchedInfo?.Invoke();
        //Matching scenario
        Debug.Log("It's a match!");
        gameObject.SetActive(false);
        
        yield return new WaitForSeconds(3);
       
    }

    IEnumerator Fossilized()
    {
        shapeFossilizedInfo?.Invoke();
        fossilizer = GameObject.Find("Fossilizer").GetComponent<Renderer>();
        fossilizee = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        fossilizee.material.mainTexture = null;
        fossilizee.material = fossilizer.material;

        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeAll; //Freeze until blown up.

        yield return new WaitForSeconds(2);
    }

}
