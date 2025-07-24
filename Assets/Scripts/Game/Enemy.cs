using System;
using System.Collections;
using Events.Handlers;
using Factory;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using EventBus = Events.EventBus;

namespace Game
{
    public class Enemy: MonoBehaviour, IProduct<Enemy>, IPointerDownHandler, IDragHandler, IPointerUpHandler, IGameOverHandler, IVictoryHandler
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _explosionParticleSystem;
        [SerializeField] private ParticleSystem _collectParticleSystem;
        private EventBus _eventBus;

        public EnemyType Type { get; private set; }
        private float _speed;
        private Vector2 _endPoint;
        private Coroutine _movingRoutine;
        private Vector3 _returningTargetPosition;
        private bool _isActingFinalMove;
        private bool _isAlive;
        
        public bool IsDragging { get; private set; }
        public bool IsGrabbed;
        public bool IsMovingToTarget { get; private set; }
        public bool IsInitialized { get; private set; }
        public Rigidbody2D Rigidbody => _rigidbody;

        public event Action<Enemy> onEnemyDrag;


        public void Initialize(EnemyType enemyType, float speed, Vector2 target, EventBus eventBus)
        {
            _isAlive = true;
            _eventBus = eventBus;
            _eventBus.Subscribe(this);
            Type = enemyType;
            _speed = speed;
            _endPoint = target;
            IsInitialized = true;
        }

        private void Update()
        {
            if(!IsInitialized || IsDragging || IsMovingToTarget)
                return;

            if (transform.position.x > _endPoint.x && _isAlive)
            {
                Explode();
                _eventBus.RaiseEvent<IGetDamageHandler>(h=>h.HandleGetDamage());
                _isAlive = false;
            }
                
        }
        
        private void FixedUpdate() 
        {
            if(!IsInitialized || IsMovingToTarget)
                return;
            
            if (!IsDragging) {
                _rigidbody.linearVelocity = new Vector2(_speed, _rigidbody.linearVelocity.y);
            }
        }

        public void Explode()
        {
            ParticleSystem particleInstance = Instantiate(_explosionParticleSystem, 
                transform.position+ new Vector3(0,0,-2), Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance.gameObject, particleInstance.main.duration);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe(this);
            _eventBus.RaiseEvent<IEnemyDeathHandler>(h=>h.HandleEnemyDeath());
        }

        public void MoveToTarget(Vector2 target, bool isFinalMove = false)
        {
            _isActingFinalMove = isFinalMove;
            _movingRoutine = StartCoroutine(MovingToTarget(target, isFinalMove));
        }
        

        private IEnumerator MovingToTarget(Vector2 target, bool isFinalMove = false )
        {
            IsMovingToTarget = true;
            
            while (Vector2.Distance(_rigidbody.position, target) > 0.01f)
            {
                Vector2 newPos = Vector2.Lerp(_rigidbody.position, target, 0.05f);
                _rigidbody.MovePosition(newPos);
                yield return null;
            }
            
            _rigidbody.MovePosition(target);
            IsMovingToTarget = false;
            
            
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            
            if(!isFinalMove)
                yield break;

            _spriteRenderer.gameObject.SetActive(false);
            ParticleSystem particleInstance = Instantiate(_collectParticleSystem, 
                transform.position+ new Vector3(0,0,-2), Quaternion.identity);
            particleInstance.Play();
            Destroy(particleInstance.gameObject, particleInstance.main.duration);
            _eventBus.RaiseEvent<IAddScoreHandler>(h=>h.HandleAddScore());
            Destroy(gameObject);
        }
        

        #region Drag

        public void OnPointerDown(PointerEventData eventData)
        {
            if(IsMovingToTarget)
                return;
            
            _returningTargetPosition = transform.position;
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.linearVelocity = Vector2.zero;
            IsDragging = true;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if(IsMovingToTarget)
                return;
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            mousePos.z = 0;
            _rigidbody.MovePosition(mousePos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(_isActingFinalMove)
                return;
            
            IsDragging = false;
            if (_movingRoutine != null)
            {
                StopCoroutine(_movingRoutine);
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
                _rigidbody.linearVelocity = Vector2.zero;
                MoveToTarget(_returningTargetPosition);
            }else
                MoveToTarget(_returningTargetPosition);
        }

        #endregion

        public void HandleGameOver()
        {
            Destroy(gameObject);
        }

        public void HandleVictory(int score)
        {
            Destroy(gameObject);
        }
    }
}