using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IdleAction : Action
{
	public Animator animator;
	[SerializeField] string animName;
	[SerializeField] int animHash;

	public SharedBool isStart;

    public override void OnAwake()
    {
		animator = GetComponent<Animator>();
		animHash = Animator.StringToHash(animName);
    }

    public override void OnStart()
    {
		animator.CrossFade(animHash, 0.01f);
    }

    public override TaskStatus OnUpdate()
	{

		return (isStart.Value) ? TaskStatus.Success : TaskStatus.Failure; 
	}
}
