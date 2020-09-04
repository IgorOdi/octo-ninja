using System.Collections.Generic;
using UnityEngine;

namespace Octoninja.Combat.Model {

    [CreateAssetMenu (menuName = "Player/Combo", fileName = "New Player Combo")]
    public class Combo : ScriptableObject {

        public List<Attack> AttackList = new List<Attack> ();
    }
}