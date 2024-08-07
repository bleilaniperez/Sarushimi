using UnityEngine;

public class SaruAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayBobbingAnimation()
    {
        animator.SetBool("IsBobbing", true); // Ensure bobbing starts
    }

    public void StopBobbingAnimation()
    {
        animator.SetBool("IsBobbing", false); // Stop bobbing
    }

    public void PlayHitAnimation()
    {
        animator.SetTrigger("Hit"); // Trigger Hit
        animator.SetBool("IsBobbing", false); // Stop bobbing
    }

    public void PlayMissAnimation()
    {
        animator.SetTrigger("Miss"); // Trigger Miss
        animator.SetBool("IsBobbing", false); // Stop bobbing
    }
}
