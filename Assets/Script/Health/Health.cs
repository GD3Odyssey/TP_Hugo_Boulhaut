using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 100;

    public int CurrentHealth { get; private set; }
    public bool IsAlive => isActiveAndEnabled && CurrentHealth > 0;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - Mathf.Max(0, damage));

        Debug.Log($"{name} : {CurrentHealth} PV");
    }
}