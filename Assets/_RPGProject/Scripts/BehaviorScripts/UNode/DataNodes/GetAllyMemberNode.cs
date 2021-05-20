using MaxyGames.uNode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGPrototype.Behaviors
{
    [NodeMenu("RPGPrototype/AllyMember", "GetAllyMember")] //Add the node to the menu
    [MaxyGames.Events.BlockMenu("RPGPrototype/AllyMember", "GetAllyMember")] //Add the node to the block menu
    public class GetAllyMemberNode : DataNode<AllyMemberRPG>
    {
        public Transform myTransform;

        private AllyMemberRPG _allymember = null;

        public override AllyMemberRPG GetValue(object graph)
        {
            if (_allymember == null)
                _allymember = myTransform.GetComponent<AllyMemberRPG>();

            return _allymember;
        }
    }
}