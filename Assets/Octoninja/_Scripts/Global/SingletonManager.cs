﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Octoninja.Global {

    public static class SingletonManager {

        private static Dictionary<Type, object> singletonList = new Dictionary<Type, object> ();

        public static void SubscribeAsSingleton (this object singleton, bool allowOverride = false) {

            Type singletonType = singleton.GetType ();
            if (singletonList.ContainsKey (singletonType)) {

                if (!allowOverride) {
                    throw new System.Exception ($"Class {singletonType} already registered");
                } else {
                    singletonList[singletonType] = singleton;
                    Debug.Log ($"Subscribed {singleton} as a {singletonType} singleton overriding an old object");
                    return;
                }
            }

            singletonList.Add (singletonType, singleton);
            Debug.Log ($"Subscribed {singleton} as a singleton");
        }

        public static void UnsubscribeSingleton (this object singleton) {

            singletonList.Remove (singleton.GetType ());
        }

        public static T GetSingleton<T> () {

            if (singletonList.TryGetValue (typeof (T), out object singleton))
                return (T) singleton;

            throw new Exception ($"Singleton of type {typeof(T)} not found");
        }

        public static void ClearSingletons () {

            singletonList.Clear ();
        }
    }
}