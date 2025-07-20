using UnityEngine;

namespace Shapes.Factories.Base
{
    public abstract class Creator<T> where T: IComponent<T>
    {
        public virtual T Create()
        {
            return default;
        }

        public virtual T Create(T component, Vector3 position)
        {
            return default;
        }

        public virtual T Create(T component, Vector3 position, Transform parent)
        {
            return default;
        }        
        
        public virtual T Create(T component, Vector3 position, Quaternion quaternion, Transform parent)
        {
            return default;
        }
    }
}