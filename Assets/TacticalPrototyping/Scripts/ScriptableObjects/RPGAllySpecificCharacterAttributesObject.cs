using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSCoreFramework;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using RPG.Characters;

namespace RPGPrototype 
{
    [CreateAssetMenu(menuName = "RTSPrototype/RPGAllySpecificCharacterAttributes")]
    public class RPGAllySpecificCharacterAttributesObject : ScriptableObject
    {
        #region RPG Character Attributes
        //Used For Character Death
        [FoldoutGroup("RPG Character Attributes")]
        [Header("Character Death")]
        [SerializeField] AudioClip[] damageSounds;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] AudioClip[] deathSounds;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float deathVanishSeconds = 2.0f;

        [FoldoutGroup("RPG Character Attributes")]
        const string DEATH_TRIGGER = "Death";

        [FoldoutGroup("RPG Character Attributes")]
        AudioSource audioSource;

        [FoldoutGroup("RPG Character Attributes")]
        [Header("Animator")] [SerializeField] RuntimeAnimatorController animatorController;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] AnimatorOverrideController animatorOverrideController;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] Avatar characterAvatar;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] [Range(.1f, 1f)] float animatorForwardCap = 1f;

        [FoldoutGroup("RPG Character Attributes")]
        [Header("Audio")]
        [SerializeField] float audioSourceSpatialBlend = 0.5f;

        [FoldoutGroup("RPG Character Attributes")]
        [Header("Capsule Collider")]
        [SerializeField] Vector3 colliderCenter = new Vector3(0, 1.03f, 0);
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float colliderRadius = 0.2f;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float colliderHeight = 2.03f;

        [FoldoutGroup("RPG Character Attributes")]
        [Header("Movement")]
        [SerializeField] float moveSpeedMultiplier = .7f;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float animationSpeedMultiplier = 1.5f;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float movingTurnSpeed = 360;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float stationaryTurnSpeed = 180;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float moveThreshold = 1f;

        [FoldoutGroup("RPG Character Attributes")]
        [Header("Nav Mesh Agent")]
        [SerializeField] float navMeshAgentSteeringSpeed = 1.0f;
        [FoldoutGroup("RPG Character Attributes")]
        [SerializeField] float navMeshAgentStoppingDistance = 1.3f;
        #endregion

        #region RPG Special Abilites Attributes
        [FoldoutGroup("RPG Special Abilites Attributes")]
        [SerializeField] AbilityConfig[] abilities;
        [FoldoutGroup("RPG Special Abilites Attributes")]
        [SerializeField] Image energyBar;
        [FoldoutGroup("RPG Special Abilites Attributes")]
        [SerializeField] AudioClip outOfEnergy;
        #endregion

        #region WeaponSystemAttributes
        [Header("WeaponSystemAttributes")]
        [FoldoutGroup("WeaponSystemAttributes")]
        [SerializeField] float baseDamage = 10f;
        [FoldoutGroup("WeaponSystemAttributes")]
        [SerializeField] WeaponConfig currentWeaponConfig = null;
        #endregion
    }
}