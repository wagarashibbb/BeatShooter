using UnityEngine;

namespace _MyAssets.Scripts.Base
{
    /// <summary>
    /// http://tsubakit1.hateblo.jp/entry/20140709/1404915381
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        protected static T instance;
        public static T Instance {
            get {
                if (instance == null) {
                    instance = (T)FindObjectOfType (typeof(T));
				
                    if (instance == null) {
                        Debug.LogWarning (typeof(T) + "is nothing");
                    }
                }
			
                return instance;
            }
        }
	
        protected void Awake()
        {
            CheckInstance();
        }
	
        protected bool CheckInstance()
        {
            if( instance == null)
            {
                instance = (T)this;
                return true;
            }else if( Instance == this )
            {
                return true;
            }

            Destroy(this);
            return false;
        }
    }
}