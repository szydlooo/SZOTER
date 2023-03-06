using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public GameObject bulletPreFab;
    public float bulletSpeed = 20;
    public float playerSpeed = 2;
    Vector2 movementVector;
    Transform BulletSpawn;
    void Start()
    {
        movementVector = Vector2.zero;
        BulletSpawn = transform.Find("BulletSpawn");
    }

    void Update()
    {
        transform.Rotate(Vector3.up * movementVector.x);
        transform.Translate(Vector3.forward * movementVector.y * Time.deltaTime);
            
    }
   void OnMove(InputValue inputValue)
    {
        movementVector = inputValue.Get<Vector2>();
        //Debug.Log(movementVector.ToString());

    }
    void OnFire()
    {
      GameObject bullet = Instantiate(bulletPreFab, BulletSpawn);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.VelocityChange);
        Destroy(bullet, 1);
    }
}
