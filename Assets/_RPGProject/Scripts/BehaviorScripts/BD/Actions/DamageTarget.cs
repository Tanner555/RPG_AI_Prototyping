using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Damages The Ally Target. Doesn't Use Any Animations.")]
	public class DamageTarget : Action
	{
		#region Shared
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

		Transform CurrentTargettedEnemy_Cached;
		#endregion

		#region Properties
		AllyMemberRPG allyMember
		{
			get
			{
				if (_allyMember == null)
				{
					_allyMember = GetComponent<AllyMemberRPG>();
				}
				return _allyMember;
			}
		}
		AllyMemberRPG _allyMember = null;

		AIControllerRPG aiController
		{
			get
			{
				if (_aiController == null)
				{
					_aiController = (AIControllerRPG)allyMember.aiController;
				}
				return _aiController;
			}
		}
		AIControllerRPG _aiController = null;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			CurrentTargettedEnemy_Cached = CurrentTargettedEnemy.Value;
			var _taskStatus = behaviorActions.DamageTarget(ref CurrentTargettedEnemy_Cached) ?
				TaskStatus.Success : TaskStatus.Failure;
			CurrentTargettedEnemy.Value = CurrentTargettedEnemy_Cached;
			return _taskStatus;
		}
		#endregion
	}
}