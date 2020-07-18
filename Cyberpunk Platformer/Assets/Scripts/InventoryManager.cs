using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool inventoryOpen = false;

    private CharacterController characterController;
    private DashAbility dashAbility;

    private SpriteRenderer feetSlot;

    public Sprite redShoes;
    public Sprite blueShoes;
    public Sprite greenGloves;

    private Camera theCamera;
    private float defaultCameraZoom;
    public float inventoryCameraZoom;

    private void Awake()
    {
        characterController = GetComponentInParent<CharacterController>();
        dashAbility = GetComponentInParent<DashAbility>();
        feetSlot = GameObject.Find("EquipedItemFeet").GetComponent<SpriteRenderer>();
        theCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        defaultCameraZoom = theCamera.orthographicSize;
    }

    void Update()
    {
        InventoryStart();
        InventoryZoom();
        Inventory();
    }

    void InventoryStart()
    {
        if (Input.GetKeyDown(KeyCode.I) && !inventoryOpen)
        {
            StartCoroutine(OpenInventory());
        } else if(Input.GetKeyDown(KeyCode.I) && inventoryOpen)
        {
            StartCoroutine(CloseInventory());
        }
    }

    IEnumerator OpenInventory()
    {
        characterController.isActive = false;

        yield return new WaitForSeconds(0.5f);
        inventoryOpen = true;
    }

    IEnumerator CloseInventory()
    {
        inventoryOpen = false;

        yield return new WaitForSeconds(0.5f);
        characterController.isActive = true;
    }

    void InventoryZoom()
    {
        if (inventoryOpen)
        {
            theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, inventoryCameraZoom, Time.deltaTime);
        }
        else
        {
            theCamera.orthographicSize = Mathf.Lerp(theCamera.orthographicSize, defaultCameraZoom, Time.deltaTime);
        }
    }

    void Inventory()
    {
        if (inventoryOpen && Input.anyKeyDown)
        {
            if (Input.inputString != "i")
            {
                Equipment(Input.inputString);
            }
        }
    }

    void Equipment(string equip)
    {
        switch (equip)
        {
            case "1":
                dashAbility.dashEnabled = true;
                dashAbility.airDash = false;
                feetSlot.sprite = redShoes;
                break;
            case "2":
                dashAbility.dashEnabled = true;
                dashAbility.airDash = true;
                feetSlot.sprite = blueShoes;
                break;
            default:
                dashAbility.dashEnabled = false;
                dashAbility.airDash = false;
                feetSlot.sprite = null;
                break;
        }
    }
}
