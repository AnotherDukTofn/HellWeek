using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private ChoosingManager enemyChoosingManager;
    [SerializeField] private Playside enemySide;
    [SerializeField] private Playside playerSide;
    [SerializeField] private Unit enemyUnit;
    [SerializeField] private Unit playerUnit;

    private bool lowAmmo;      
    private bool lowHealth;
    private bool enemyLowAmmo;  
    private bool enemyLowHealth;
    private bool outOfAmmo;

    public void MakeDecision() {
        EvaluateStatus();
        DecideAction();
        DecideTarget();
    }

    private void EvaluateStatus() {
        var eW = enemyUnit.weapon;
        var pW = playerUnit.weapon;

        lowAmmo = eW.GetCurrentAmmo() < eW.GetMaxAmmo() * 0.5f;
        enemyLowAmmo = pW.GetCurrentAmmo() < pW.GetMaxAmmo() * 0.5f;

        lowHealth = enemyUnit.CurrentHealth < enemyUnit.MaxHealth * 0.5f;
        enemyLowHealth = playerUnit.CurrentHealth < playerUnit.MaxHealth * 0.5f;

        outOfAmmo = eW.GetCurrentAmmo() == 0;
    }

    private void DecideAction() {
        int roll = Random.Range(1, 10);

        if (outOfAmmo) {
            enemyChoosingManager.SetActionType(roll <= 5 
                ? ChoosingManager.ActionType.Reload 
                : ChoosingManager.ActionType.Dodge);
            return;
        }

        if ((!lowAmmo && (!lowHealth || enemyLowHealth))
            || (enemyLowAmmo && !lowAmmo)
            || (lowHealth && enemyLowHealth)) 
        {
            enemyChoosingManager.SetActionType(
                roll <= 6 ? ChoosingManager.ActionType.Fire :
                roll <= 8 ? ChoosingManager.ActionType.Dodge :
                ChoosingManager.ActionType.Reload
            );
            return;
        }

        if (enemyLowAmmo && lowAmmo) {
            enemyChoosingManager.SetActionType(
                roll <= 6 ? ChoosingManager.ActionType.Reload :
                roll <= 8 ? ChoosingManager.ActionType.Dodge :
                ChoosingManager.ActionType.Fire
            );
            return;
        }

        if ((lowHealth && !enemyLowHealth) || (lowHealth && lowAmmo)) {
            enemyChoosingManager.SetActionType(
                roll <= 6 ? ChoosingManager.ActionType.Dodge :
                roll <= 8 ? ChoosingManager.ActionType.Fire :
                ChoosingManager.ActionType.Reload
            );
            return;
        }

        enemyChoosingManager.SetActionType(
            roll <= 4 ? ChoosingManager.ActionType.Fire :
            roll < 8 ? ChoosingManager.ActionType.Dodge :
            ChoosingManager.ActionType.Reload
        );
    }

    private void DecideTarget() {
        var action = enemyChoosingManager.actionType;

        switch (action) {
            case ChoosingManager.ActionType.Fire:
                SetFireTarget();
                break;

            case ChoosingManager.ActionType.Dodge:
                SetDodgeTarget();
                break;

            case ChoosingManager.ActionType.Reload:
                enemyChoosingManager.SetTarget(1);
                break;
        }
    }

    private void SetFireTarget() {
        int playerPos = FindOccupiedCell(playerSide);
        if (playerPos < 0) playerPos = 1; 

        int roll = Random.Range(1, 10);

        if (roll <= 6) {
            enemyChoosingManager.SetTarget(playerPos);
            Debug.Log($"[ENEMY FIRE] Nhắm trực tiếp người chơi ở vị trí: {playerPos}");
            return;
        }

        int target;
        do {
            target = Random.Range(0, 3);
        } while (target == playerPos);

        enemyChoosingManager.SetTarget(target);
        Debug.Log($"[ENEMY FIRE] Không bắn trực tiếp. Player ở {playerPos} nhưng enemy chọn bắn ô {target}");
    }


    private void SetDodgeTarget() {
        int currentPos = FindOccupiedCell(enemySide);
        if (currentPos < 0) currentPos = 1;

        int target;
        do {
            target = Random.Range(0, 3);
        } while (target == currentPos);

        enemyChoosingManager.SetTarget(target);
    }

    private int FindOccupiedCell(Playside side) {
        for (int i = 0; i < 3; i++) {
            if (side.Cells[i].IsOccupied())
                return i;
        }
        return 1;
    }
}
