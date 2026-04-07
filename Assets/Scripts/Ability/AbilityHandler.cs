using System.Collections;
using UnityEngine;

public class AbilityHandler
{
    private const int SecondStep = 1;

    private Vampirism _vampirism;
    private AbilityViewUI _viewUI;
    private MonoBehaviour _coroutineBehaviour;

    private float _activeTime = 6f;
    private float _cooldownTime = 4f;
    private WaitForSeconds _waitingOneSecond;

    private Collider2D[] _hits;
    private Coroutine _abilityCoroutine;

    public void Initialize(IHealable healable, Vampirism vampirism, AbilityViewUI viewUI,
        MonoBehaviour coroutineBehaviour)
    {
        _vampirism = vampirism;
        _viewUI = viewUI;
        _waitingOneSecond = new WaitForSeconds(SecondStep);
        _coroutineBehaviour = coroutineBehaviour;
        _vampirism.SetHealable(healable);
        Enable();
    }

    public void Activate()
    {
        if (_abilityCoroutine == null)
        {
            _vampirism.Activate();
            _vampirism.FindTargets();
            _viewUI.DisableTooltip();
            _viewUI.DisableProgress();
            _abilityCoroutine = _coroutineBehaviour.StartCoroutine(AbilityCoroutine());
        }
    }

    private IEnumerator AbilityCoroutine()
    {
        yield return StartExecuteCoroutine();

        yield return TimerCoroutine();
    }

    private IEnumerator StartExecuteCoroutine()
    {
        float elapsedTime = 0f;

        for (int i = 0; i < _activeTime; i += SecondStep)
        {
            _vampirism.Execute(SecondStep);
            yield return _waitingOneSecond;
        }
        
        Deactivate();
    }

    private IEnumerator TimerCoroutine()
    {
        float elapsedTime = 0;
        float timeTick = 0;

        while (elapsedTime <= _cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            _viewUI.ChangeCooldown(elapsedTime, _cooldownTime);
            yield return null;
        }

        Enable();
    }

    private void Enable()
    {
        _viewUI.EnableTooltip();
        _viewUI.EnableProgress();
        _abilityCoroutine = null;
    }

    private void Deactivate()
    {
        _viewUI.EnableProgress();
        _viewUI.ChangeCooldown(0, _cooldownTime);
        _vampirism.Deactivate();
    }
}