using UnityEngine;

public class DamageTracker : MonoBehaviour {
    [SerializeField] private float damageTaken;
    [SerializeField] private float damageDealt;
    
    [SerializeField] private Unit allyUnit;

    void OnEnable() {
        allyUnit.OnFire += TrackDamageDealt;
        allyUnit.OnHit += TrackDamageTaken;
    }

    void OnDisable() {
        allyUnit.OnFire -= TrackDamageDealt;
        allyUnit.OnHit -= TrackDamageTaken;
    }

    private void TrackDamageDealt(float damage, int target) {
        damageDealt += damage;
    }

    private void TrackDamageTaken(float damage) {
        damageTaken += damage;
    }
    
    public float GetDamageDealt() => damageDealt;
    public float GetDamageTaken() => damageTaken;
}
