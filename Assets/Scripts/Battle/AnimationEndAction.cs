using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Battle")]
public class AnimationEndAction : Action
{
    private Animator animator;

    [SerializeField] float animTime;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
        animTime = 999;
    }

    public override TaskStatus OnUpdate()
    {
        GetCurrentAnimTime();

        animTime -= Time.deltaTime;

        if (animTime <= 0)
        {
            animTime = 999;

            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }

    private void GetCurrentAnimTime()
    {
        if (animTime != 999) return;

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        animTime = (info.length / info.speed);
    }
}