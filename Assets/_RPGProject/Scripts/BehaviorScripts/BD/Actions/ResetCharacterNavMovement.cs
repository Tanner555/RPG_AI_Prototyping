using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;
using UnityEngine.AI;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Resets The Provided Character Navigation Movement BlackBoard Variables and NavMeshAgent Dest. Optional OnlyResetIfHasSetDestination Checkbox Will Only Reset Variables If Destination Has Been Set.")]
	public class ResetCharacterNavMovement : Action
	{
        #region Shared
        public SharedVector3 MyNavDestination;
		public SharedBool bHasSetDestination;
		public SharedBool bHasSetCommandMove;
        public SharedBool OnlyResetIfHasSetDestination = false;
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

        Vector3 MyNavDestination_Cached;
        bool bHasSetDestination_Cached;
        bool bHasSetCommandMove_Cached;
        #endregion

        #region Overrides
        public override TaskStatus OnUpdate()
		{
            MyNavDestination_Cached = MyNavDestination.Value;
            bHasSetDestination_Cached = bHasSetDestination.Value;
            bHasSetCommandMove_Cached = bHasSetCommandMove.Value;
            var _taskStatus = behaviorActions.ResetCharacterNavMovement(ref MyNavDestination_Cached,
                ref bHasSetDestination_Cached, ref bHasSetCommandMove_Cached, 
                OnlyResetIfHasSetDestination.Value) ?
                TaskStatus.Success : TaskStatus.Failure;
            MyNavDestination.Value = MyNavDestination_Cached;
            bHasSetDestination.Value = bHasSetDestination_Cached;
            bHasSetCommandMove.Value = bHasSetCommandMove_Cached;
            return _taskStatus;
        }

		public override void OnReset()
		{
            MyNavDestination_Cached = MyNavDestination.Value;
            bHasSetDestination_Cached = bHasSetDestination.Value;
            bHasSetCommandMove_Cached = bHasSetCommandMove.Value;
            behaviorActions.ResetCharacterNavMovement(ref MyNavDestination_Cached,
                            ref bHasSetDestination_Cached, ref bHasSetCommandMove_Cached, false);
        }
		#endregion
	} 
}