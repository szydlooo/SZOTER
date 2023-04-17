using UnityEngine;
using System.Collections;
using UnityEditor.PackageManager;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    void Start()
    {
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 4)
        {
            Instantiate(Enemy);
        }
    }
}