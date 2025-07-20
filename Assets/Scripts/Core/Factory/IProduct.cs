using UnityEngine;

namespace Shapes.Factories.Base
{
    public interface IProduct<T> : IComponent<T> where T : MonoBehaviour
    {
    }
}