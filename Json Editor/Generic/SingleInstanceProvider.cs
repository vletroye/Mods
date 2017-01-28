
namespace ZTn.Json.Editor.Generic
{
    /// <summary>
    /// Simple singleton allowing unicity insurance of a generic type <typeparamref name="T"/> instance.
    /// </summary>
    /// <typeparam name="T">Type of the unique instance.</typeparam>
    sealed class SingleInstanceProvider<T> where T : class, new()
    {
        #region >> Fields

        static T instance;

        #endregion

        #region >> Properties

        /// <summary>
        /// Get the <typeparamref name="T"/> instance stored in this singleton.
        /// The instance is created on the first call.
        /// </summary>
        public static T Value
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }

        #endregion
    }
}
