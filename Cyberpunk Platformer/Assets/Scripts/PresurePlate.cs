using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    private GameObject topComponent;
    [SerializeField] bool onPresurePlate;
    [SerializeField] GameObject objectToToggle;

    void Awake()
    {
        topComponent = GameObject.Find("TopSprite");
        onPresurePlate = false;
    }

    private void Update()
    {
        if (onPresurePlate == true)
        {
            topComponent.SetActive(!onPresurePlate);
            objectToToggle.SetActive(onPresurePlate);
        }
        else if(onPresurePlate == false)
        {
            topComponent.SetActive(!onPresurePlate);
            objectToToggle.SetActive(onPresurePlate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onPresurePlate = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        onPresurePlate = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onPresurePlate = false;
    }
}
