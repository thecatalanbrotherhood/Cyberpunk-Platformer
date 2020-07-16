using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickUp : MonoBehaviour
{
    private bool inPickupRange;
    private bool pickedUp;
    private bool overObject;
    private GameObject player;
    private float objectMass;

    private void Awake()
    {
        objectMass = gameObject.GetComponent<Rigidbody2D>().mass;
    }

    private void Update()
    {
        PickUpObject();
    }

    void PickUpObject()
    {
        if (Input.GetMouseButtonDown(1) && inPickupRange && !pickedUp)
        {
            StartCoroutine(PickUp());
        }
        else if (Input.GetMouseButtonDown(1) && pickedUp)
        {
            Debug.Log("Droped");
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            gameObject.transform.SetParent(null);
            pickedUp = false;
        }
    }

    IEnumerator PickUp()
    {
        Debug.Log("PickedUp");
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.transform.SetParent(player.transform);
        //gameObject.transform.position = GameObject.Find("GrabPoint").transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f, gameObject.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        pickedUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            inPickupRange = true;
            player = collision.gameObject;
            Debug.Log("inRange");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            inPickupRange = false;
            player = null;
            Debug.Log("outRange");
        }
    }
}
