using System;
using UnityEngine;

public class Unit : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private Playside allySide;
    
    [Header("Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [Header("Weapon")]
    [SerializeField] private WeaponDataSO currentWeapon;
    private Weapon _weapon;
    
    public event Action OnDie;
    public event Action<float, int> OnFire;
    public event Action<float> OnHit;

    
    private void Start() {
        currentHealth = maxHealth;
        _weapon = new Weapon(currentWeapon);
g\h
    }
        currentHealth -= damage;g\
    public void TakeDamage(float damage) {
        OnHit?.Invoke(damage);
        Debug.Log($"{this.gameObject.name} damaged with {damage} damage");
        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
    }

    public void FireTo(int target, Action onComplete = null) {
        _weapon.Fire();
        OnFire?.Invoke(currentWeapon.damage, target);
        Debug.Log($"{this.gameObject.name} fire to {target}");
        
        onComplete?.Invoke();
    }

    public void Reload() {
        _weapon.Reload();
    }

    public void DodgeTo(int position, Action onComplete = null) {
        allySide.MoveUnit(position, onComplete);
    }

    private void Die() {
        Debug.Log($"{name} has died.");
        this.gameObject.SetActive(false);
        OnDie?.Invoke();
    }
}