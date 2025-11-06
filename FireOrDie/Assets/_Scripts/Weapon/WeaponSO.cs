using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Scriptable Object/ New Weapon")]
public class WeaponDataSO : ScriptableObject {
    [field: SerializeField] public float damage;
    [field: SerializeField] public int fireAmount;
    [field: SerializeField] public int maxAmmo;
    [field: SerializeField] public int reloadAmount;
}