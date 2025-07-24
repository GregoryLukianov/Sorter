using System.Collections;
using Events;
using UnityEngine;
using Zenject;

namespace UI
{
    public abstract class MenuPanel: MonoBehaviour
    {
        [SerializeField] private Animator _mainPanelAnimator;
        [SerializeField] private CanvasGroup _globalBackground;
        
        [Inject] protected EventBus _eventBus;
        
        protected virtual void Awake()
        {
            
        }
        
        protected void TurnOn()
        {
            _mainPanelAnimator.Play("Panel In");
            _globalBackground.alpha = 1;
            _globalBackground.blocksRaycasts = true;
        }

        protected void TurnOff()
        {
            _mainPanelAnimator.Play("Panel Out");
            _globalBackground.alpha = 0;
            _globalBackground.blocksRaycasts = false;
        }

        protected void TurnOnWithDelay(float delay)
        {
            StartCoroutine(TurnOnWithDelayRoutine(delay));
        }

        private IEnumerator TurnOnWithDelayRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            TurnOn();
        }
    }
}