using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSphereColor : MonoBehaviour
{
    [SerializeField] private Color[] sphereColors;
    [SerializeField] private float time = 1f;
    private MeshRenderer sphereMeshRenderer;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetTime;

    private void Awake() {
        sphereMeshRenderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        ColorTransition();
    }

    private void ColorTransition() {
        targetTime += Time.deltaTime / time;
        sphereMeshRenderer.material.color = Color.Lerp(sphereColors[currentColorIndex], sphereColors[targetColorIndex], targetTime);

        if (targetTime >= 1f) {
            targetTime = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex++;
            if (targetColorIndex == sphereColors.Length) {
                targetColorIndex = 0;
            }
        }
    }

    public void SetColorTransitionTime(float time) {
        if (time < 0.02f) {
            time = 1;
        }
        this.time -= time;
    }
}
