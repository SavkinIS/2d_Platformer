using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _textGo;
    
    private Vampirism _vampirism;

    private void Awake()
    {
        _fillImage.type = Image.Type.Filled;
        _fillImage.fillMethod = Image.FillMethod.Vertical;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void SetAbility(Vampirism vampirism)
    {
        _vampirism = vampirism;
        Subscribe();
    }

    private void Subscribe()
    {
        if (_vampirism != null)
        {
            _vampirism.StateChanged += ChangeState;
            _vampirism.CooldownChanged += ChangeCooldown;
        }
    }

    private void ChangeCooldown(float current, float max)
    {
        _fillImage.fillAmount = current / max;
    }

    private void Unsubscribe()
    {
        if (_vampirism != null)
        {
            _vampirism.StateChanged -= ChangeState;
            _vampirism.CooldownChanged -= ChangeCooldown;
        }
    }
    
    private void ChangeState(AbilityState state)
    {
        _fillImage.gameObject.SetActive(state != AbilityState.Active);
        _textGo.SetActive(state == AbilityState.Waiting);
    }
}