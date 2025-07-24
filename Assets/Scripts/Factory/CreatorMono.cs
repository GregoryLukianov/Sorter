using UnityEngine;

namespace Factory
{
    public abstract class CreatorMono<T> : Creator<T> where T : MonoBehaviour, IProduct<T>
    {
        public override T Create(T prefab, Vector3 position)
        {
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }
        
        public override T Create(T prefab, Vector3 position, Transform parent)
        {
            return Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }
        
        public override T Create(T prefab, Vector3 position, Quaternion quaternion, Transform parent)
        {
            return Object.Instantiate(prefab, position, quaternion, parent);
        }
    }
}