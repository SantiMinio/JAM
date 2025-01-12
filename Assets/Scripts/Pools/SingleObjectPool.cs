using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


namespace DevelopTools
{
    public abstract class SingleObjectPool<T> :  MonoBehaviour, IEnumerable<T> where T : Component
    {
        public bool extendible = true;
        /// <summary>
        /// Prefab que se quiere poolear
        /// </summary>
        [Header("Si es un sonido, dejar vacio el prefab")]
        [SerializeField] protected T prefab = null;
        /// <summary>
        /// Cola donde se guardan los objetos pooleados
        /// </summary>
        protected Queue<T> objects = new Queue<T>();

        protected List<T> currentlyUsingObj = new List<T>();
        public List<T> Currents { get { return currentlyUsingObj; } } 

        public int InUse => currentlyUsingObj.Count;

        #region Auto exponential size
        bool auto_exp;
        int auto_size = 0;
        #endregion

        /// <summary>
        /// Le pido un objeto del pool y lo prendo.
        /// Si no tengo ninguno, creo uno y lo doy
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            T obj = null;

            if (objects.Count == 0)
            {
                if (!extendible)
                {
                    return obj;
                }
                else
                {
                    AddObject(auto_exp ? auto_size : 1);
                }
            }
            
            obj = objects.Dequeue();
            obj.gameObject.SetActive(true);
            currentlyUsingObj.Add(obj);
            OnGetObject(obj);
            return obj;
        }

        protected virtual void OnGetObject(T obj) {  }

       /// <summary>
       /// Initialize con configuracion previa
       /// </summary>
       /// <param name="prewarm">prewarm para crear pre-crear objetos y tenerlos listos para usar</param>
       /// <param name="autoExponential">activa el auto-escalado exponencial, para grandes volumenes de objetos</param>
        public void Initialize(int prewarm = 5, bool autoExponential = false)
        {
            auto_exp = autoExponential;
            for (int i = 0; i < prewarm; i++) AddObject(1);
        }

        /// <summary>
        /// Devuelvo el objeto al pool
        /// </summary>
        /// <param name="objectToReturn"></param>
        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            currentlyUsingObj.Remove(objectToReturn);
            objects.Enqueue(objectToReturn);
        }
        /// <summary>
        /// Creo un objeto del prefab y lo agrego al pool previo apagarlo
        /// </summary>
        /// <param name="amount"></param>
        protected virtual void AddObject(int amount = 5)
        {
            for (int i = 0; i < amount; i++)
            {
                var newObject = GameObject.Instantiate(prefab, transform);
                newObject.gameObject.SetActive(false);
                objects.Enqueue(newObject);
                if (auto_exp && objects.Count > auto_size) auto_size = objects.Count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in objects)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }    

}