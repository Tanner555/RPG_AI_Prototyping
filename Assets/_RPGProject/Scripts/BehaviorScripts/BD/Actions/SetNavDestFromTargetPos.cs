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

		System.Action<Vector3, bool, Transform> SetterAction;
        #endregion

        #region Overrides
        public override void OnStart()
        {
            SetterAction = (MyNavDestination, bHasSetDestination, CurrentTargettedEnemy) =>
			{
				this.MyNavDestination.Value = MyNavDestination;
				this.bHasSetDestination.Value = bHasSetDestination;
				this.CurrentTargettedEnemy.Value = CurrentTargettedEnemy;
			};
		}

        public override TaskStatus OnUpdate()
		{
			return behaviorActions.SetNavDestFromTargetPos
				(MyNavDestination.Value, bHasSetDestination.Value,
				CurrentTargettedEnemy.Value, ref SetterAction) ?
				TaskStatus.Success : TaskStatus.Failure;
		}
		#endregion

	}
}