using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TinyCore
{
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        //同步资源加载
        public T Load<T>(string path) where T : Object
        {
            T resource = Resources.Load<T>(path);
            return resource is GameObject ? Instantiate(resource) : resource;
        }

        //异步资源加载
        public void LoadAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
            StartCoroutine(LoadAsyncInternal(path, callback));
        }

        private IEnumerator LoadAsyncInternal<T>(string path, UnityAction<T> callback) where T : Object
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<T>(path);
            yield return resourceRequest;

            if (resourceRequest.asset is GameObject)
            {
                callback(Instantiate(resourceRequest.asset) as T);
            }
            else
            {
                callback(resourceRequest.asset as T);
            }
        }
    }
}


