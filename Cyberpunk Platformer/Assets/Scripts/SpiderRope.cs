﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRope : MonoBehaviour
{
    private LineRenderer line;

    public Material mat;
    public Transform ropePosition;
    public float lineWidth = 0.1f;
    public float speed = 75;
    public float pullForce = 50;
    public float ropeTimer = 1f;

    private IEnumerator timer;
    private Vector3 velocity;
    private bool pull = false;
    private bool update = false;
    private Transform parent;
    private Rigidbody2D parentRb;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        if(!line)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.material = mat;

        
        parentRb = ropePosition.transform.parent.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = worldPos - (Vector2)ropePosition.position;
            dir = dir.normalized;
            velocity = dir * speed;
            transform.position = (Vector2)ropePosition.position + dir;
            pull = false;
            update = true;

            if (timer != null)
            {
                StopCoroutine(timer);
                timer = null;
            }
        }

        if (!update)
            return;

        if(pull)
        {
            Vector2 dir = (Vector2)transform.position - (Vector2)ropePosition.position;
            //dir = dir.normalized;
            parentRb.AddForce(dir * pullForce);
        }
        else
        {
            transform.position = transform.position + velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, (Vector2)ropePosition.position);
            if (distance > 10)
            {
                update = false;
                line.SetPosition(0, Vector2.zero);
                line.SetPosition(1, Vector2.zero);
                return;
            }
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, (Vector2)ropePosition.position);
    }

    IEnumerator Reset(float delay)
    {
        yield return new WaitForSeconds(delay);
        update = false;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        velocity = Vector2.zero;
        pull = true;
        timer = Reset(ropeTimer);
        StartCoroutine(timer);
    }
}
