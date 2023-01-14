using UnityEngine;

public class WorldMapPlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    private static readonly int AnimHash_Idle = Animator.StringToHash("Idle");
    private static readonly int AnimHash_Running = Animator.StringToHash("Running");

    private void SetAnimation(int animId)
    {
        anim.SetTrigger(animId);
    }

    public void SetIdle()
    {
        SetAnimation(AnimHash_Idle);
    }

    public void SetRunning()
    {
        SetAnimation(AnimHash_Running);
    }
}
