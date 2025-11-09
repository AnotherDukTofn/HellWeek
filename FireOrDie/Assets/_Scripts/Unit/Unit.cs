using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour {
    [Header("References")] 
    [SerializeField] private Playside allySide;
    
    [Header("Statuss")]
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    [Header("Weapon")]
    [SerializeField] private WeaponDataSO currentWeapon;
    public Weapon weapon;
    
    public event Action OnDie;
    public event Action<float, int> OnFire;
    public event Action<float> OnHit;

    
    private void Start() {
        CurrentHealth = MaxHealth;
        weapon = new Weapon(currentWeapon);
        weapon.Init();
    }
        
    public void TakeDamage(float damage) {
        CurrentHealth -= damage;
        OnHit?.Invoke(damage);
        if (CurrentHealth <= 0) {
            CurrentHealth = 0;
            Die();
        }
    }

    public void FireTo(int target, Action onComplete = null) {
        weapon.Fire();
        OnFire?.Invoke(currentWeapon.damage, target);
        StopAllCoroutines();
        StartCoroutine(WaitForAnimation(onComplete));
    }

    public void Reload(Action onComplete = null) {
        weapon.Reload();
        StopAllCoroutines();
        StartCoroutine(WaitForAnimation(onComplete));
    }

    public void DodgeTo(int position, Action onComplete = null) {
        allySide.MoveUnit(position, onComplete);
    }

    private void Die() {
        Debug.Log($"{name} has died.");
        this.gameObject.SetActive(false);
        OnDie?.Invoke();
    }
    
    private IEnumerator WaitForAnimation(Action onComplete) {
        yield return new WaitForSeconds(1f);
        onComplete?.Invoke();
    }
}