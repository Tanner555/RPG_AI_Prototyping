using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;
using UnityEngine.AI;

namespace RPGPrototype {
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Provides Move Direction From Navigation Destination.")]
	public class MoveDirFromNavDestination : Action
	{
		#region Shared
		public SharedVector3 MyMoveDirection;
		public SharedVector3 MyNavDestination;
		public SharedBool bFinishedMoving;
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
		Vector3 MyNavDestination_Cached;
		bool bFinishedMoving_Cached;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			MyMoveDirection_Cached = MyMoveDirection.Value;
			MyNavDestination_Cached = MyNavDestination.Value;
			bFinishedMoving_Cached = bFinishedMoving.Value;
			var _taskStatus = behaviorActions.MoveDirFromNavDestination(ref MyMoveDirection_Cached,
				ref MyNavDestination_Cached, ref bFinishedMoving_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
            MyMoveDirection.Value = MyMoveDirection_Cached;
            MyNavDestination.Value = MyNavDestination_Cached;
            bFinishedMoving.Value = bFinishedMoving_Cached;
            return _taskStatus;
		}
		#endregion

	} 
}