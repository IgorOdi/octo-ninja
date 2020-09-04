using System;
using System.Collections.Generic;
using Octoninja.Combat.Model;
using Octoninja.Utils;
using UnityEngine;

namespace Octoninja.Combat.Controller {

    public abstract class AttackController : MonoBehaviour {

        public List<Combo> Combos = new List<Combo> ();
        public bool IsAttacking { get { return DamagerController.IsColliderActive; } }
        public bool IsRecovering { get; private set; }

        [SerializeField]
        protected DamagerController DamagerController;
        protected int currentAtkIndex;

        public void Initialize () {

            DamagerController.DisableCollider ();
        }

        protected virtual bool DoAttack (Attack attack) {

            if (IsRecovering || DamagerController.IsColliderActive) return false;

            this.RunDelayed (attack.Delay, () => {

                //controller.Animator.Play (atkInfo.AnimationName);
                DamagerController.ActivateCollider (attack.Damager, attack.Duration, (success) => OnHit (success));

                if (attack.RecoveryTime > 0) {
                    IsRecovering = true;
                    this.RunDelayed (attack.RecoveryTime, () => IsRecovering = false);
                }
            });

            return true;
        }

        protected virtual bool NextComboAttack (int comboIndex) {

            if (IsRecovering || DamagerController.IsColliderActive) return false;

            DoAttack (GetCurrentAttackInfo (comboIndex));

            if (currentAtkIndex < Combos[comboIndex].AttackList.Count - 1)
                currentAtkIndex++;
            else
                currentAtkIndex = 0;

            return true;
        }

        protected virtual void OnHit (bool success) {

            if (success) {

                Debug.Log ("Hitou com sucesso");
            } else {

                Debug.Log ("Hit falhou");
            }
        }

        protected Attack GetCurrentAttackInfo (int index = 0) {

            return Combos[index].AttackList[currentAtkIndex];
        }
    }
}