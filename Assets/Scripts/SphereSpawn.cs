using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    private float distance = 3f;
    private float timeDecrease = 0.02f;
    ChangeSphereColor changeSphereColor;
    void Start()
    {
        for (int i = 0; i < 15; i++) {
            if (changeSphereColor != null) {
                changeSphereColor.SetColorTransitionTime(timeDecrease);
            }
            for (int j = 0; j < 15; j++) {
                for (int k = 0; k < 15; k++) {
                    Instantiate(sphere, new Vector3(i * distance, j * distance, k * distance), Quaternion.identity);
                    changeSphereColor = sphere.GetComponent<ChangeSphereColor>();
                }
            }
        }
    }
}
