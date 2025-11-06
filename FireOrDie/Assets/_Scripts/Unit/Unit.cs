using System;
using UnityEngine;

public class Unit : MonoBehaviour {
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Weapon")]
    [SerializeField] private WeaponDataSO _currentWeapon;
    private Weapon weapon;

    public Action<int, float> OnFireToPosition;

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

    private void Start() {
        currentHealth = maxHealth;
        weapon = new Weapon(_currentWeapon);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        Debug.Log($"{this.gameObject.name} damaged with {damage} damage");
        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
    }

    public void FireTo(int position) {
        weapon.Fire();
        OnFireToPosition?.Invoke(position, _currentWeapon.damage); 
        Debug.Log($"{this.gameObject.name} fire to {position}");
    }

    public void Reload() {
        weapon.Reload();
    }

    public void DodgeTo(int position, Playside allySide) {
        allySide.MoveUnit(position);
    }

    private void Die() {
        Debug.Log($"{name} has died.");
        Destroy(gameObject);
    }
}