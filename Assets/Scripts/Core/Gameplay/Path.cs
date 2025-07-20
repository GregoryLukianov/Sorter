using UnityEngine;

namespace Core.Gameplay
{
    public class Path: MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endpoint;
        
        public Transform StartPoint => _startPoint;
        public Transform EndPoint => _endpoint;
    }
}