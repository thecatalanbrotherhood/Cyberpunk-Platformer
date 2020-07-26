using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeFromDoor : MonoBehaviour
{
    [SerializeField] int scene;
    [SerializeField] bool inDoor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inDoor)
        {
            if (scene != 0)
            {
                SceneManager.LoadScene(scene);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            inDoor = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            inDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            inDoor = false;
        }
    }
}
