using UnityEngine;

public class ProgressUIAnimator : MonoBehaviour
{
    private const string IS_PURCHASED = "IsPurchased";
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        animator.SetTrigger(IS_PURCHASED);
    }
}
