using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;
using UnityStandardAssets.CrossPlatformInput;

namespace RPGPrototype
{
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Returns Success if Owner is Current Player and Movement Input Has Been Received.")]
    public class PlayerWantsFreeMovement : Conditional
	{
		#region Shared
		public SharedVector3 MyMoveDirection;
		public SharedBool bIsFreeMoving;
		public SharedBool bUseNewInputSystem = true;
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

		System.Action<Vector3, bool> SetterAction;

		Vector3 MyMoveDirection_Cached;
		bool bIsFreeMoving_Cached;
		#endregion

		#region Overrides
		public override void OnStart()
		{
			SetterAction = (MyMoveDirection, bIsFreeMoving) =>
			{
				this.MyMoveDirection.Value = MyMoveDirection;
				this.bIsFreeMoving.Value = bIsFreeMoving;
			};
		}

		public override TaskStatus OnUpdate()
		{
			MyMoveDirection_Cached = MyMoveDirection.Value;
			bIsFreeMoving_Cached = bIsFreeMoving.Value;
			return behaviorActions.PlayerWantsFreeMovement(ref MyMoveDirection_Cached,
				ref bIsFreeMoving_Cached, ref SetterAction, bUseNewInputSystem.Value) ?
				TaskStatus.Success : TaskStatus.Failure;
		}
		#endregion
	}
}