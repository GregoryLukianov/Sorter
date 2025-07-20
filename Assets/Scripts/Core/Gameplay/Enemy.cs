using System;
using System.Collections;
using Shapes.Factories.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Core.Gameplay
{
    public class Enemy: MonoBehaviour, IProduct<Enemy>, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        private EnemyType _enemyType;
        private float _speed;
        private Vector2 _target;
        private Coroutine _returningRoutine;
        private Vector3 _returningTargetPosition;
        
        public bool IsDragging { get; private set; }
        public bool IsReturningToPath { get; private set; }
        public bool IsInitialized { get; private set; }


        public void Initialize(EnemyType enemyType, float speed, Vector2 target)
        {
            _enemyType = enemyType;
            _speed = speed;
            _target = target;
            IsInitialized = true;
        }

        private void Update()
        {
            if(!IsInitialized)
                return;
            
            if(transform.position.x > _target.x)
                Destroy(gameObject);
        }
        
        private void FixedUpdate() 
        {
            if(!IsInitialized || IsReturningToPath)
                return;
            
            if (!IsDragging) {
                _rigidbody.linearVelocity = new Vector2(_speed, _rigidbody.linearVelocity.y);
            }
        }

        private IEnumerator Returning()
        {
            IsReturningToPath = true;
            
            while (Vector2.Distance(_rigidbody.position, _returningTargetPosition) > 0.01f)
            {
                Vector2 newPos = Vector2.Lerp(_rigidbody.position, _returningTargetPosition, 0.025f);
                _rigidbody.MovePosition(newPos);
                yield return null;
            }
            
            _rigidbody.MovePosition(_returningTargetPosition);
            IsReturningToPath = false;
            
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        

        #region Drag

        public void OnPointerDown(PointerEventData eventData)
        {
            if(IsReturningToPath)
                return;
            
            IsDragging = true;
            _returningTargetPosition = transform.position;
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.bodyType = RigidbodyType2D.Static;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            mousePos.z = 0;
            _rigidbody.MovePosition(mousePos);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            IsDragging = false;
            _returningRoutine = StartCoroutine(Returning());
        }

        #endregion
    }
}