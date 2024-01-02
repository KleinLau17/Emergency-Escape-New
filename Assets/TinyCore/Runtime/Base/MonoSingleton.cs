using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyCore
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                
            }
        }
    }

}