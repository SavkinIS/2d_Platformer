using System;
using System.Collections;
using Ability.Effects;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    private const int SecondStep = 1;
    
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private float _activeTime = 6f;
    [SerializeField] private float _cooldownTime = 4f;
    
    private WaitForSeconds _waitingOneSecond;
    private Collider2D[] _hits;
    private Coroutine _abilityCoroutine;
    
    public event Action Activated;
    public event Action Enabled;
    public event Action Deactivated;
    public event Action<float, float> CooldownChanged;

    public void Initialize(IHealable healable)
    {
        _waitingOneSecond = new WaitForSeconds(SecondStep);
        _vampirism.SetHealable(healable);
        Enable();
    }

    public void Activate()
    {
        if (_abilityCoroutine == null)
        {
            _vampirism.Activate();
            Activated?.Invoke();
            _abilityCoroutine = StartCoroutine(AbilityCoroutine());
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
            CooldownChanged?.Invoke(elapsedTime, _cooldownTime);
            yield return null;
        }

        Enable();
    }

    private void Enable()
    {
        Enabled?.Invoke();
        _abilityCoroutine = null;
    }

    private void Deactivate()
    {
        Deactivated?.Invoke();
        CooldownChanged?.Invoke(0, _cooldownTime);
    }
}