using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using RTSCoreFramework;

namespace RPGPrototype
{
    [TaskCategory("RPGPrototype/AllyMember")]
    [TaskDescription("Returns True if Evaluating Tactics Successfully Returns A Tactics Item.")]
    public class EvaluateTacticsSuccessful : Conditional
	{
        #region Shared
        public SharedTacticsItem CurrentExecutionItem;
        #endregion

        #region FieldsAndProperties
        protected List<AllyTacticsItem> evalTactics = new List<AllyTacticsItem>();
        protected List<AllyTacticsItem> AllyTacticsList => aiController.AllyTacticsList;

        protected PartyManager myPartyManager { get { return allyMember ? allyMember.partyManager : null; } }
        protected AllyMember allyInCommand
        {
            get
            {
                return myPartyManager != null ? myPartyManager.AllyInCommand : null;
            }
        }

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
        #endregion

        #region Overrides
        public override TaskStatus OnUpdate()
        {
            //Temporary Fix for PartyManager Delaying Initial AllyInCommand Methods
            if (allyInCommand == null)
            {
                CurrentExecutionItem.Value = null;
                return TaskStatus.Failure;
            }

            evalTactics.Clear();
            foreach (var _tactic in AllyTacticsList)
            {
                //If Condition is True and 
                //Can Perform The Given Action
                if (_tactic.condition.action(allyMember) &&
                    _tactic.action.canPerformAction(allyMember))
                {
                    evalTactics.Add(_tactic);
                }
            }

            if (evalTactics.Count > 0)
            {            
                CurrentExecutionItem.Value = EvaluateTacticalConditionOrders(evalTactics);
                return TaskStatus.Success;
            }
            else
            {
                CurrentExecutionItem.Value = null;
                return TaskStatus.Failure;
            }                
        }
        #endregion

        #region HelperMethods
        protected virtual AllyTacticsItem EvaluateTacticalConditionOrders(List<AllyTacticsItem> _tactics)
        {
            int _order = int.MaxValue;
            AllyTacticsItem _exeTactic = null;
            foreach (var _tactic in _tactics)
            {
                if (_tactic.order < _order)
                {
                    _order = _tactic.order;
                    _exeTactic = _tactic;
                }
            }
            return _exeTactic;
        }
        #endregion
    }
}