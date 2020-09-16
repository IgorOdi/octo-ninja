using System.Collections.Generic;
using System.Linq;
using Octoninja.Global;
using UnityEngine;

namespace Octoninja.Pooling {

    public class PoolManager : MonoBehaviour {

        private List<Pool> pools = new List<Pool> ();
        private Vector2 spawnPosition = new Vector2 (5000, 5000);

        public void Initialize () => this.SubscribeAsSingleton ();

        public Pool CreatePool (string name, GameObject reference, int amount) {

            var parent = new GameObject (name);
            parent.transform.position = spawnPosition;

            List<GameObject> objects = new List<GameObject> ();
            for (int i = 0; i < amount; i++) {
                var obj = Instantiate (reference, spawnPosition, Quaternion.identity, parent.transform);
                objects.Add (obj);
            }
            var newPool = new Pool (name, objects);
            pools.Add (newPool);
            return newPool;
        }

        public Pool GetPool (string poolName) {

            return pools.Find (p => p.PoolName.Equals (poolName));
        }
    }
}