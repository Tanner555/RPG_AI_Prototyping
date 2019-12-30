using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;
using UnityStandardAssets.CrossPlatformInput;

namespace RPGPrototype
{
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Returns Success if Owner is Current Player and Movement Input Has Been Received.")]
    public class PlayerWantsFreeMovement : Conditional
	{
		#region Fields
		private float myHorizontalMovement, myForwardMovement = 0.0f;
		Vector3 myDirection = Vector3.zero;
		#endregion

		#region Properties
		AllyMember allyMember
		{
			get
			{
				if(_allymember == null)
				{
					_allymember = GetComponent<AllyMember>();
				}
				return _allymember;
			}
		}
		AllyMember _allymember = null;

		AllyEventHandler myEventHandler
		{
			get
			{
				if(_myEventhandler == null)
				{
					_myEventhandler = GetComponent<AllyEventHandler>();
				}
				return _myEventhandler;
			}
		}
		AllyEventHandler _myEventhandler = null;

		bool bIsAlive => allyMember != null && allyMember.IsAlive;
		#endregion

		public override TaskStatus OnUpdate()
		{
			if (bIsAlive == false ||
                allyMember == null ||
                allyMember.bIsCurrentPlayer == false) return TaskStatus.Failure;

			myHorizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");
            myForwardMovement = CrossPlatformInputManager.GetAxis("Vertical");
			myDirection = Vector3.zero;
            myDirection.x = myHorizontalMovement;
            myDirection.z = myForwardMovement;
            myDirection.y = 0;

			if (myDirection.sqrMagnitude > 0.05f)
            {
                if (myEventHandler.bIsNavMoving)
                {
                    myEventHandler.CallEventFinishedMoving();
                }
                if (myEventHandler.bIsFreeMoving == false)
                {
                    myEventHandler.CallEventTogglebIsFreeMoving(true);
                }
                return TaskStatus.Success;
            }
            else
            {
                if (myEventHandler.bIsFreeMoving)
                {
                    myEventHandler.CallEventTogglebIsFreeMoving(false);
                }
                return TaskStatus.Failure;
            }

		}
	}
}