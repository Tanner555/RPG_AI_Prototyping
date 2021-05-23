using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;
using UnityEngine.AI;

namespace RPGPrototype {
	[TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Disables Waypoint Renderer If It Exists and Is Enabled.")]
	public class ResetWaypointRenderer : Action
	{
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
        #endregion

        #region Overrides
        public override TaskStatus OnUpdate()
		{
            return behaviorActions.ResetWaypointRenderer() ?
                TaskStatus.Success : TaskStatus.Failure;            
		}
		#endregion
	}
}