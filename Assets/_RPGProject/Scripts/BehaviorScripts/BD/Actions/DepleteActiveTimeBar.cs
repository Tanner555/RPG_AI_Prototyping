using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Empties The Active Time Bar. OnlyDepleteIfAboveMinimum Checkbox Will Only Deplete Time Bar if Amount is Greater Than 0.")]
	public class DepleteActiveTimeBar : Action
	{
		#region Shared
		public SharedBool OnlyDepleteIfAboveMinimum = false;
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
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			return behaviorActions.DepleteActiveTimeBar(OnlyDepleteIfAboveMinimum.Value) ?
				TaskStatus.Success : TaskStatus.Failure;
		}
		#endregion
	}
}