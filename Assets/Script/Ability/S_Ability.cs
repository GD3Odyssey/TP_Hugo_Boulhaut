using UnityEditor;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewAbility",
    menuName = "Ability/New"
)]

public class S_Ability : ScriptableObject
{

    public string abilityName;
    public string description;
    public Sprite icon;
    public int damage;
    public float coolDown;
    public float projectileSpeed;
    public Color color;
    public GameObject projectiflePrefab;
    public GameObject effectPrefab;
    public string triggerName = "Attack";

}
