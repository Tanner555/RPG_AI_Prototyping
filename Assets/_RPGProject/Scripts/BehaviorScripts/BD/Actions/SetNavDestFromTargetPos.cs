using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype {
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Sets Nav Destination To Target Position.")]
	public class SetNavDestFromTargetPos : Action
	{
		#region Shared
		public SharedVector3 MyNavDestination;
		public SharedBool bHasSetDestination;
		public SharedTransform CurrentTargettedEnemy;
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

		Vector3 MyNavDestination_Cached;
		bool bHasSetDestination_Cached;
		Transform CurrentTargettedEnemy_Cached;
		#endregion

		#region Overrides
        public override TaskStatus OnUpdate()
		{
			MyNavDestination_Cached = MyNavDestination.Value;
			bHasSetDestination_Cached = bHasSetDestination.Value;
			CurrentTargettedEnemy_Cached = CurrentTargettedEnemy.Value;
			var _taskStatus = behaviorActions.SetNavDestFromTargetPos
				(ref MyNavDestination_Cached, ref bHasSetDestination_Cached,
				ref CurrentTargettedEnemy_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			MyNavDestination.Value = MyNavDestination_Cached;
			bHasSetDestination.Value = bHasSetDestination_Cached;
			CurrentTargettedEnemy.Value = CurrentTargettedEnemy_Cached;
			return _taskStatus;
		}
		#endregion

	}
}