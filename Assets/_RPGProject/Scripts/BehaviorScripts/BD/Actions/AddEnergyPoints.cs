using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Adds Energy Points To The Ally On Every Tick.")]
	public class AddEnergyPoints : Action
	{
		#region Shared
		public SharedInt EnergyRegenPointsPerSec;
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
			var _taskStatus = behaviorActions.AddEnergyPoints
				(EnergyRegenPointsPerSec.Value) ?
				TaskStatus.Success : TaskStatus.Failure;
			return _taskStatus;
		}
		#endregion
	}
}