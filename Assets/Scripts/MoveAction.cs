using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation;

[TaskCategory("Battle")]
public class MoveAction : Action
{
	public SharedString MoveAnimationName;
	public SharedBool IsMoveAnimStart;
    public SharedBool IsAttackAnimStart;

    [SerializeField] float moveSpeed;
	private Animator animator;
	private SpriteRenderer renderer;

    public override void OnAwake()
    {
		animator = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
    }

    public override TaskStatus OnUpdate()
	{
		if (IsMoveAnimStart.Value.Equals(false)) MoveAnimation();

		Move();

		return TaskStatus.Success;
	}

	private void Move()
	{
		if (IsAttackAnimStart.Value.Equals(true)) return;

		transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * (renderer.flipX ? -1 : 1 ));
		if(Camera.main.WorldToViewportPoint(transform.position).x > 1f)
		{
			Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
			pos.x = 1f;
			transform.position = pos;
        }
	}

	private void MoveAnimation()
	{
		// 움직이는 애니메이션 진행
		// 이게 계속하면 안되서 한번만 진행해야 함
		// 각 테스크 별로 작동을 해야하니 -> 공용 변수를 사용하여 조절
		animator.CrossFade(MoveAnimationName.Value, 0f);
		IsMoveAnimStart.Value = true;
        IsAttackAnimStart.Value = false;
    }
}