using System.Collections.Generic;
using UnityEngine;

namespace Octoninja.Pooling {

    public class Pool {

        public string PoolName { get; set; }
        private Queue<GameObject> pooled = new Queue<GameObject> ();

        public Pool (string name, List<GameObject> startingPool) {

            PoolName = name;

            for (int i = 0; i < startingPool.Count; i++)
                AddToPool (startingPool[i]);
        }

        public void AddToPool (GameObject gameObject) {

            pooled.Enqueue (gameObject);
        }

        public GameObject Spawn (Vector3 position, Vector3 eulerAngles) {

            var p = pooled.Dequeue ();
            p.transform.SetParent (null);
            p.transform.position = position;
            p.transform.eulerAngles = eulerAngles;
            p.transform.localScale = Vector3.one;
            return p;
        }

        public GameObject Spawn (Vector3 position) {

            var p = pooled.Dequeue ();
            p.transform.SetParent (null);
            p.transform.position = position;
            p.transform.eulerAngles = Vector3.zero;;
            p.transform.localScale = Vector3.one;
            return p;
        }
    }
}