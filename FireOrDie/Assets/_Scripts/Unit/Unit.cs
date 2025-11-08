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

    #region JustForDebug

    [SerializeField] private bool fireToTop;
    [SerializeField] private bool fireToMid;
    [SerializeField] private bool fireToBot;

    void Update() {
        if (fireToTop) {
            FireTo(0);
            fireToTop = false;
        }

        if (fireToMid) {
            FireTo(1);
            fireToMid = false;
        }

        if (fireToBot) {
            FireTo(2);
            fireToBot = false;
        }
    }
    
    #endregion

    private void Start() {
        currentHealth = maxHealth;
        _weapon = new Weapon(currentWeapon);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        Debug.Log($"{this.gameObject.name} damaged with {damage} damage");
        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
    }

    public void FireTo(int target, Action onComplete = null) {
        _weapon.Fire();
        allySide.NotifyHit(target, currentWeapon.damage);
        Debug.Log($"{this.gameObject.name} fire to {target}");
        
        // Fire là instant action, gọi callback ngay
        // Nếu có animation thì đợi animation xong mới gọi
        onComplete?.Invoke();
    }

    public void Reload() {
        _weapon.Reload();
    }

    public void DodgeTo(int position, Action onComplete = null) {
        // Pass callback xuống Playside để gọi sau khi movement xong
        allySide.MoveUnit(position, onComplete);
    }

    private void Die() {
        Debug.Log($"{name} has died.");
        this.gameObject.SetActive(false);
    }
}