using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;
using UnityEngine;
using UnityEngine.AI;

namespace RPGPrototype
{
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Updates Waypoint When Command Navigation Moving.")]
	public class UpdateWaypointRenderer : Action
	{
		#region Shared
		public SharedMaterial waypointMaterial;
		public SharedFloat waypointStartWidth = 0.05f;
        public SharedFloat waypointEndWidth = 0.05f;
        public SharedColor waypointStartColor = Color.yellow;
        public SharedColor waypointEndColor = Color.yellow;
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

        Material waypointMaterial_Cached;
        Color waypointStartColor_Cached;
        Color waypointEndColor_Cached;
        #endregion

        #region Overrides
        public override void OnStart()
		{
            waypointMaterial_Cached = waypointMaterial.Value;
            waypointStartColor_Cached = waypointStartColor.Value;
            waypointEndColor_Cached = waypointEndColor.Value;
        }

		public override TaskStatus OnUpdate()
		{
            return behaviorActions.UpdateWaypointRenderer(ref waypointMaterial_Cached,
                ref waypointStartColor_Cached, ref waypointEndColor_Cached,
                waypointStartWidth.Value, waypointEndWidth.Value) ?
                TaskStatus.Success : TaskStatus.Failure;
        }
		#endregion
	}
}