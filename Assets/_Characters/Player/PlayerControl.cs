using UnityEngine;
using System.Collections;
using RPG.CameraUI; // for mouse events
using BaseFramework;
using RPGPrototype;

namespace RPG.Characters
{
    public class PlayerControl : MonoBehaviour
    {
        #region Fields
        Character character;
        SpecialAbilities abilities;
        WeaponSystem weaponSystem;
        #endregion

        #region Properties
        RPGGameMaster gamemaster
        {
            get { return RPGGameMaster.thisInstance; }
        }
        #endregion

        #region UnityMessages
        void Start()
        {
            character = GetComponent<Character>();
            abilities = GetComponent<SpecialAbilities>();
            weaponSystem = GetComponent<WeaponSystem>();

            RegisterForMouseEvents();
        }

        void Update()
        {
            //ScanForAbilityKeyDown();
        }

        private void OnDisable()
        {
            DeregisterForMouseEvents();
        }
        #endregion

        #region MyHandlers
        //Custom
        void OnKeyPress(int _key)
        {
            if (_key == 0 ||
                _key >= abilities.GetNumberOfAbilities()) return;
            abilities.AttemptSpecialAbility(_key);
        }

        void OnMouseOverPotentiallyWalkable(Vector3 destination)
        {
            if (Input.GetMouseButton(0))
            {
                weaponSystem.StopAttacking();
                character.SetDestination(destination);
            }
        }

        void OnMouseOverEnemy(EnemyAI enemy)
        {
            if (Input.GetMouseButton(0) && IsTargetInRange(enemy.gameObject))
            {
                weaponSystem.AttackTarget(enemy.gameObject);
            }
            else if (Input.GetMouseButton(0) && !IsTargetInRange(enemy.gameObject))
            {
                StartCoroutine(MoveAndAttack(enemy));
            }
            else if (Input.GetMouseButtonDown(1) && IsTargetInRange(enemy.gameObject))
            {
                abilities.AttemptSpecialAbility(0, enemy.gameObject);
            }
            else if (Input.GetMouseButtonDown(1) && !IsTargetInRange(enemy.gameObject))
            {
                StartCoroutine(MoveAndPowerAttack(enemy));
            }
        }
        #endregion

        #region OldCode
        void ScanForAbilityKeyDown()
        {
            for (int keyIndex = 1; keyIndex < abilities.GetNumberOfAbilities(); keyIndex++)
            {
                if (Input.GetKeyDown(keyIndex.ToString()))
                {
                    abilities.AttemptSpecialAbility(keyIndex);
                }
            }
        }
        #endregion

        #region Initialization
        private void RegisterForMouseEvents()
        {
            var cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            if (cameraRaycaster != null)
            {
                cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
                cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
            }
            //Register Custom Events
            gamemaster.OnNumberKeyPress += OnKeyPress;
        }

        private void DeregisterForMouseEvents()
        {
            var cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            if (cameraRaycaster != null)
            {
                cameraRaycaster.onMouseOverEnemy -= OnMouseOverEnemy;
                cameraRaycaster.onMouseOverPotentiallyWalkable -= OnMouseOverPotentiallyWalkable;
            }
            //Deregister Custom Events
            gamemaster.OnNumberKeyPress -= OnKeyPress;
        }
        #endregion

        bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
        }      

        IEnumerator MoveToTarget(GameObject target)
        {
            character.SetDestination(target.transform.position);
            while (!IsTargetInRange(target))
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }

        IEnumerator MoveAndAttack(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            weaponSystem.AttackTarget(enemy.gameObject);
        }

        IEnumerator MoveAndPowerAttack(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            abilities.AttemptSpecialAbility(0, enemy.gameObject);
        }
    }
}