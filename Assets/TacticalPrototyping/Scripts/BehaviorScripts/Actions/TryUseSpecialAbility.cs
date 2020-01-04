using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Try Using The Ability In The Ability To Use Slot.")]
	public class TryUseSpecialAbility : Action
	{
		#region Shared
		public SharedObject AbilityToUse;
		#endregion

		#region Properties
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
			myEventHandler.CallOnTrySpecialAbility(AbilityToUse.Value.GetType());
			return TaskStatus.Success;
		}
		#endregion
	}
}