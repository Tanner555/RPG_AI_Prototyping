using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Moves RPG Character From A Given Direction Using The Animator.")]
    public class MoveRPGCharacter : Action
	{
		#region Shared
		public SharedVector3 MyMoveDirection;
		public SharedBool bIsFreeMoving;
		#endregion

		#region BehaviorActions
		RPGBehaviorActions behaviorActions
		{
			get
			{
				if (_behaviorActions == null)
				{
					_behaviorActions = GetComponent<AIControllerRPG>().BehaviorActionsInstance as RPGBehaviorActions;
				}
				return _behaviorActions;
			}
		}
		RPGBehaviorActions _behaviorActions = null;

		Vector3 MyMoveDirection_Cached;
		bool bIsFreeMoving_Cached;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			MyMoveDirection_Cached = MyMoveDirection.Value;
			bIsFreeMoving_Cached = bIsFreeMoving.Value;
			var _taskStatus = behaviorActions.MoveRPGCharacter
				(ref MyMoveDirection_Cached, ref bIsFreeMoving_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			MyMoveDirection.Value = MyMoveDirection_Cached;
			bIsFreeMoving.Value = bIsFreeMoving_Cached;
			return _taskStatus;
		}
		#endregion
	}
}