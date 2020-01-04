using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
	public class AddEnergyPoints : Action
	{
		#region Shared
		public SharedInt EnergyRegenPointsPerSec;
		#endregion

		#region Properties
		protected AllyMember allymember
		{
			get
			{
				if (_allymember == null)
					_allymember = GetComponent<AllyMember>();

				return _allymember;
			}
		}
		AllyMember _allymember = null;
		#endregion

		#region Overrides
		public override TaskStatus OnUpdate()
		{
			allymember.AllyRegainStamina(EnergyRegenPointsPerSec.Value);
			return TaskStatus.Success;
		}
		#endregion
	}
}