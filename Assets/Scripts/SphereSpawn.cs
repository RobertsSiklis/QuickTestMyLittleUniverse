using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform sphere;
    private float distance = 3f;
    void Start()
    {
        for (int i = 0; i < 15; i++) {
            Instantiate(sphere, new Vector3(i * distance, 0f, 0f), Quaternion.identity);
            for (int j = 0; j < 15; j++) {
                Instantiate(sphere, new Vector3(i * distance, j * distance, 0f), Quaternion.identity);
                for (int k = 0; k < 15; k++) {
                    Instantiate(sphere, new Vector3(i * distance, j * distance, k * distance), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
