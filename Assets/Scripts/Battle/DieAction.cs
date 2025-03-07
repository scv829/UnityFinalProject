using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

[TaskCategory("Battle")]
public class DieAction : Action
{
	// 사망했을 때 작동할 내용
	[Tooltip("사망했을 때 변경될 레이어")]
	public SharedLayerMask DieLayer;
	[Tooltip("사망했을 때 동작할 애니메이션")]
	public SharedString DieAnimationName;
    [Tooltip("살아 있는지 확인 여부")]
    public SharedBool IsAlive;

    protected Animator animator;

    public override void OnAwake()
    {
		animator = GetComponent<Animator>();
    }

	public override TaskStatus OnUpdate()
	{
		// 사망 애니메이션 실행
		StartDieAnimation();
		// 사망 레이어로 변경
		ChangeDieLayer();

		IsAlive.Value = false;

		// 행동 트리의 반복을 중단하기 위해 실패 반환
		return TaskStatus.Success;
	}

	private void StartDieAnimation() => animator.CrossFade(DieAnimationName.Value, 0f);

	private void ChangeDieLayer() => gameObject.layer = DieLayer.Value;

}