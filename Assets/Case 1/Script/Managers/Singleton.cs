using UnityEngine;

namespace Case_1
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        #region Fields

        private static T instance;

        #endregion

        #region Properties

        public static T Instance=> instance;

        #endregion

        #region Func

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
//                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}