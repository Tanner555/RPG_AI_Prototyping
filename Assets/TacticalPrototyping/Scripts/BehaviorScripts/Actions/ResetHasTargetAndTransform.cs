using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype {
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Resets bTargetEnemy and CurrentTargettedEnemy If Character Is Free Moving.")]
	public class ResetHasTargetAndTransform : Action
	{
		#region Shared
		public SharedBool bIsFreeMoving;
		public SharedBool bTargetEnemy;
		public SharedTransform CurrentTargettedEnemy;
		#endregion

		#region Overrides
		public override void OnStart()
		{
		
		}

		public override TaskStatus OnUpdate()
		{
			if (bIsFreeMoving.Value)
			{
				bTargetEnemy.Value = false;
				CurrentTargettedEnemy.Value = null;
			}
			return TaskStatus.Success;
		}
		#endregion

	} 
}