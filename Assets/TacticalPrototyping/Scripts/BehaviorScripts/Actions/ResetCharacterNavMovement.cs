using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;
using UnityEngine.AI;

namespace RPGPrototype
{
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Resets The Provided Character Navigation Movement BlackBoard Variables and NavMeshAgent Dest.")]
	public class ResetCharacterNavMovement : Action
	{
        #region Shared
        public SharedVector3 MyNavDestination;
		public SharedBool bHasSetDestination;
		public SharedBool bHasSetCommandMove;
        #endregion

        #region Properties
        NavMeshAgent navMeshAgent
		{
			get
			{
				if(_navMeshAgent == null)
				{
					_navMeshAgent = GetComponent<NavMeshAgent>();
				}
				return _navMeshAgent;
			}
		}
		NavMeshAgent _navMeshAgent = null;

        AllyMember allyMember
        {
            get
            {
                if (_allymember == null)
                {
                    _allymember = GetComponent<AllyMember>();
                }
                return _allymember;
            }
        }
        AllyMember _allymember = null;

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

        BehaviorTree AllyBehaviorTree
        {
            get
            {
                if (_AllyBehaviorTree == null)
                {
                    _AllyBehaviorTree = GetComponent<BehaviorTree>();
                }
                return _AllyBehaviorTree;
            }
        }
        BehaviorTree _AllyBehaviorTree = null;
        #endregion

        #region Overrides
        public override void OnStart()
		{
		
		}

		public override TaskStatus OnUpdate()
		{
			MyNavDestination.Value = Vector3.zero;
			bHasSetDestination.Value = false;
			bHasSetCommandMove.Value = false;
            navMeshAgent.SetDestination(transform.position);
            navMeshAgent.velocity = Vector3.zero;
			//AllyBehaviorTree.SetVariableValue(aiController.BBName_MyNavDestination, Vector3.zero);
			//AllyBehaviorTree.SetVariableValue(aiController.BBName_bHasSetDestination, false);
			//AllyBehaviorTree.SetVariableValue(aiController.BBName_bHasSetCommandMove, false);
			return TaskStatus.Success;
		}

		public override void OnReset()
		{
			MyNavDestination.Value = Vector3.zero;
			bHasSetDestination.Value = false;
			bHasSetCommandMove.Value = false;
            navMeshAgent.SetDestination(transform.position);
            navMeshAgent.velocity = Vector3.zero;
			//AllyBehaviorTree.SetVariableValue(aiController.BBName_MyNavDestination, Vector3.zero);
			//AllyBehaviorTree.SetVariableValue(aiController.BBName_bHasSetDestination, false);
			//AllyBehaviorTree.SetVariableValue(aiController.BBName_bHasSetCommandMove, false);
		}
		#endregion

	} 
}