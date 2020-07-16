using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTeleportation : MonoBehaviour
{
    private bool inTeleporter;
    private Rigidbody2D objectInTeleporter;

    public GameObject teleporter;

    private void Update()
    {
        if (Input.GetKeyDown("e") && inTeleporter)
        {
            StartCoroutine(Teleport());
        }
    }
    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.5f);
        if (objectInTeleporter != null)
        {
            objectInTeleporter.transform.position = new Vector2(teleporter.transform.position.x, teleporter.transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inTeleporter = true;
        objectInTeleporter = collision.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTeleporter = false;
        objectInTeleporter = null;
    }
}