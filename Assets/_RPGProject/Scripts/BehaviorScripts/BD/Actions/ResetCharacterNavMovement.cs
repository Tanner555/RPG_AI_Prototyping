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

        System.Action<Vector3, bool, bool> SetterAction;
        #endregion

        #region Overrides
        public override void OnStart()
        {
            SetterAction = (MyNavDestination, bHasSetDestination, bHasSetCommandMove) =>
            {
                this.MyNavDestination.Value = MyNavDestination;
                this.bHasSetDestination.Value = bHasSetDestination;
                this.bHasSetCommandMove.Value = bHasSetCommandMove;
            };
        }

        public override TaskStatus OnUpdate()
		{
            return behaviorActions.ResetCharacterNavMovement(MyNavDestination.Value, 
                bHasSetDestination.Value, bHasSetCommandMove.Value, 
                ref SetterAction, OnlyResetIfHasSetDestination.Value) ?
                TaskStatus.Success : TaskStatus.Failure;
        }

		public override void OnReset()
		{
            behaviorActions.ResetCharacterNavMovement(MyNavDestination.Value,
                bHasSetDestination.Value, bHasSetCommandMove.Value,
                ref SetterAction, false);
        }
		#endregion
	} 
}