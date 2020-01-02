using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Attempts To Use Weapon And Attack Enemy. Bind WeaponAttackRate Variable For Wait Node.")]
	public class TryAttackTarget : Action
	{
		#region Shared
		public SharedFloat WeaponAttackRate;
		public SharedTransform CurrentTargettedEnemy;
		#endregion

		#region Properties
		AllyMember allyMember
		{
			get
			{
				if (_allyMember == null)
				{
					_allyMember = GetComponent<AllyMember>();
				}
				return _allyMember;
			}
		}
		AllyMember _allyMember = null;

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

		AllyEventHandler myEventHandler
		{
			get
			{
				if (_myEventhandler == null)
				{
					_myEventhandler = GetComponent<AllyEventHandler>();
				}
				return _myEventhandler;
			}
		}
		AllyEventHandler _myEventhandler = null;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			WeaponAttackRate.Value = aiController.GetAttackRate();
			myEventHandler.CallOnTryUseWeapon(CurrentTargettedEnemy.Value);
			return TaskStatus.Success;
		}
		#endregion

	}
}