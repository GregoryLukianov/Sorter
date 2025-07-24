using UnityEngine;

namespace Factory
{
    public interface IProduct<T> : IComponent<T> where T : MonoBehaviour
    {
    }
}