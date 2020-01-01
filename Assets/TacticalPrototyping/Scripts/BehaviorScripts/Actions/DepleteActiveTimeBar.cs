using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
	[TaskDescription("Empties The Active Time Bar.")]
	public class DepleteActiveTimeBar : Action
	{
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
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			if(allyMember != null && allyMember.ActiveTimeBarIsFull())
			{
				allyMember.DepleteActiveTimeBar();
			}
			return TaskStatus.Success;
		}
		#endregion
	}
}