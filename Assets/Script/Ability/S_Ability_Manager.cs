using System.Collections.Generic;
using UnityEngine;

public class S_AbilityManager : MonoBehaviour
{
    [SerializeField] private List<S_Ability> abilities = new List<S_Ability>();

    private sealed class AbilityState
    {
        public S_Ability Data;
        public float ReadyAt;
    }

    private readonly Dictionary<string, AbilityState> states =
        new Dictionary<string, AbilityState>();

    private void Awake()
    {
        foreach (S_Ability ability in abilities)
        {
            if (ability == null || string.IsNullOrWhiteSpace(ability.abilityName))
                continue;

            if (states.ContainsKey(ability.abilityName))
            {
                Debug.LogWarning(
                    $"Nom de compétence dupliqué : {ability.abilityName}", this);
                continue;
            }

            states.Add(ability.abilityName, new AbilityState
            {
                Data = ability
            });
        }
    }
    public float GetRemainingCooldown(string abilityName)
    {
        if (abilityName == null ||
            !states.TryGetValue(abilityName, out AbilityState state))
            return 0f;

        return Mathf.Max(0f, state.ReadyAt - Time.time);
    }
    public bool IsAvailable(string abilityName)
    {
        return isActiveAndEnabled
            && abilityName != null
            && states.TryGetValue(abilityName, out AbilityState state)
            && Time.time >= state.ReadyAt;
    }
    // Méthode à associer au OnClick du bouton.
    public void UseAbility(string abilityName)
    {
        if (!isActiveAndEnabled)
            return;

        if (abilityName == null ||
            !states.TryGetValue(abilityName, out AbilityState state))
        {
            Debug.LogWarning($"Compétence introuvable : {abilityName}", this);
            return;
        }

        if (!IsAvailable(abilityName))
        {
            Debug.Log(
                $"{abilityName} : recharge restante " +
                $"{GetRemainingCooldown(abilityName):0.0} s", this);
            return;
        }

        state.ReadyAt = Time.time + Mathf.Max(0f, state.Data.coolDown);

        Debug.Log($"Compétence lancée : {state.Data.abilityName} !", this);
    }
    // Alternative pour déclencher depuis la ressource directement.
    public void UseAbilityFromAsset(S_Ability ability)
    {
        if (ability == null)
            return;

        if (ability.abilityName != null &&
            states.TryGetValue(ability.abilityName, out AbilityState state) &&
            state.Data == ability)
        {
            UseAbility(ability.abilityName);
        }
        else
        {
            Debug.LogWarning("Cette compétence n'appartient pas au manager.", this);
        }
    }
}
