using System.Collections.Generic;
using Octoninja.Combat.Model;
using Octoninja.Global;
using Octoninja.Pooling;
using Octoninja.Utils;
using UnityEngine;

namespace Octoninja.Combat.Controller {

    public abstract class AttackController : MonoBehaviour {

        public List<Combo> Combos = new List<Combo> ();
        public bool IsAttacking { get { return DamagerController.IsColliderActive; } }
        public bool IsRecovering { get; private set; }

        [SerializeField]
        protected DamagerController DamagerController;
        protected Pool attackPool;
        protected int currentAtkIndex;

        protected Coroutine recoveryCoroutine;

        void Start () => Initialize ();
        public void Initialize () {

            DamagerController.DisableCollider ();
            return;
            for (int i = 0; i < Combos[0].AttackList.Count; i++) {

                attackPool = SingletonManager.GetSingleton<PoolManager> ().CreatePool (Combos[0].AttackList[i].AttackName,
                    Combos[0].AttackList[i].projectile, 5);
            }
        }

        protected virtual bool DoAttack (Attack attack) {

            if (IsAttacking || IsRecovering) return false;

            this.RunDelayed (attack.Delay, () => {

                //controller.Animator.Play (atkInfo.AnimationName);
                if (attack.AttackType.Equals (AttackType.MELEE))
                    DamagerController.ActivateCollider (attack.Damager, attack.Duration, (success, damager) => OnHit (success, damager));
                else
                    attackPool.Spawn (DamagerController.transform.position, DamagerController.transform.eulerAngles);

                if (attack.RecoveryTime > 0) {
                    IsRecovering = true;
                    if (recoveryCoroutine != null) StopCoroutine (recoveryCoroutine);
                    recoveryCoroutine = this.RunDelayed (attack.RecoveryTime, () => IsRecovering = false);
                }
            });

            return true;
        }

        protected virtual bool NextComboAttack (int comboIndex) {

            if (IsAttacking || IsRecovering) return false;

            DoAttack (GetCurrentAttackInfo (comboIndex));

            if (currentAtkIndex < Combos[comboIndex].AttackList.Count - 1)
                currentAtkIndex++;
            else
                currentAtkIndex = 0;

            return true;
        }

        protected virtual void OnHit (bool success, Damager damager) {

            if (success) {

                if (damager.ShakeScreen)
                    Global.SingletonManager.GetSingleton<Cameras.CameraManager> ().ShakeCurrentCamera (damager.ScreenShakeDuration,
                        damager.ScreenShakeIntensity, damager.ScreenShakeIntensity, true);
            } else {

                Debug.Log ("Hit falhou");
            }
        }

        protected Attack GetCurrentAttackInfo (int index = 0) {

            return Combos[index].AttackList[currentAtkIndex];
        }
    }
}