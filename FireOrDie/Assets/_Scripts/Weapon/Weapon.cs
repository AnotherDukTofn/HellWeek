using Mono.Cecil;
using UnityEngine;

public class Weapon {
    private WeaponDataSO _weaponData;
    private int _currentAmmo;
    
    public int GetCurrentAmmo() => _currentAmmo;
    public int GetMaxAmmo() => _weaponData.maxAmmo;

    public Weapon(WeaponDataSO weaponData) {
        _weaponData = weaponData;
    }

    public void Fire() {
        _currentAmmo -= _weaponData.fireAmount;
    }

    public void Reload() {
        _currentAmmo += _weaponData.reloadAmount;
    }

    public void Init() {
        _currentAmmo = _weaponData.maxAmmo;
    }
}
