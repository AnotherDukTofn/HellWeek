using UnityEngine;

public class DamageTracker : MonoBehaviour {
    [field: SerializeField] public float PlayerDamageTaken { get; private set; }
    [field: SerializeField] public float PlayerDamageDealt { get; private set; }
    
    
    [SerializeField] private Unit playerUnit;

    void OnEnable() {
        playerUnit.OnFire += TrackPlayerDamageDealt;
        playerUnit.OnHit += TrackPlayerDamageTaken;
    }

    void OnDisable() {
        playerUnit.OnFire -= TrackPlayerDamageDealt;
        playerUnit.OnHit -= TrackPlayerDamageTaken;
    }

    private void TrackPlayerDamageDealt(float damage, int target) {
        PlayerDamageDealt += damage;
    }

    private void TrackPlayerDamageTaken(float damage) {
        PlayerDamageTaken += damage;
    }
}
