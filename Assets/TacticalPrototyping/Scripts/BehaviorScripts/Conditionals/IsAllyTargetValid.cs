using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype {
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Simple Valid Target Check using bTargetEnemy and CurrentTargettedEnemy.")]
	public class IsAllyTargetValid : Conditional
	{
		#region Shared
		public SharedBool bTargetEnemy;
		public SharedTransform CurrentTargettedEnemy;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			if(bTargetEnemy.Value && CurrentTargettedEnemy.Value != null)
			{
				return TaskStatus.Success;
			}
			else
			{
				return TaskStatus.Failure;
			}			
		}
		#endregion
	}
}