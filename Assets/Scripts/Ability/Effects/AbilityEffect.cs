using System;
using UnityEngine;

namespace Ability.Effects
{
    public class AbilityEffect : MonoBehaviour
    {
        [SerializeField] private AbilityHandler _abilityHandler;
        [SerializeField] private SpriteRenderer _renderer;

        private void Awake()
        {
            Deactivated();
        }

        private void OnEnable()
        {
            _abilityHandler.Activated += Activated;
            _abilityHandler.Deactivated += Deactivated;
        }

        private void OnDisable()
        {
            _abilityHandler.Activated -= Activated;
            _abilityHandler.Deactivated -= Deactivated;
        }

        private void Activated()
        {
            _renderer.enabled = true;
        }

        private void Deactivated()
        {
            _renderer.enabled = false;
        }
    }
}