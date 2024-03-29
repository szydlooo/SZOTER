using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    public float sightRange = 15f;
    public float hearRange = 5f;
    int hp = 10;

    GameObject player;
    NavMeshAgent agent;
   
    private bool playerVisible = false;
    private bool playerHearable = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //sprawdz czy widzimy gracza
        Vector3 raySource = transform.position + Vector3.up * 1.8f;
        Vector3 rayDirection = player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(raySource, rayDirection, out hit, sightRange))
        {

            if (hit.transform.CompareTag("Player"))
                playerVisible = true;
            else
                playerVisible = false;
    }

        //sprawdzamy widoczność
        Collider[] heardObjects = Physics.OverlapSphere(transform.position, hearRange);
        playerHearable = false;
        foreach (Collider collider in heardObjects)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                //slyszy gracza
                playerHearable = true;
               
            }
        }


        agent.isStopped = !playerVisible && !playerHearable;
        if (hp> 0)
        {
            //transform.LookAt(player.transform.position);
            //Vector3 playerDirection = transform.position - player.transform.position;

            //transform.Translate(Vector3.forward * Time.deltaTime);

            agent.destination = player.transform.position;
        }
        else
        {
            agent.speed = 0;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            hp--;
            if(hp <= 0)
            {
                transform.Translate(Vector3.up);
                transform.Rotate(Vector3.right * -90);
                GetComponent<BoxCollider>().enabled = false;
                Destroy(transform.gameObject, 5);
            }
        }
    }
}
