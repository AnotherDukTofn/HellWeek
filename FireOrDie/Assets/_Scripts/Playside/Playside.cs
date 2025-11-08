using System;
using System.Collections;
using UnityEngine;

public class Playside : MonoBehaviour {
    [field: SerializeField] public Cell[] Cells { get; private set; }
    [SerializeField] private Unit allyUnit;
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private Playside enemySide;

    private void OnEnable() {
        allyUnit.OnFire += NotifyHit;
    }

    private void OnDisable() {
        allyUnit.OnFire -= NotifyHit;
    }

    private void Start() {
        MoveUnit(1);
    }

    public void NotifyHit(float damage, int target) {
        Debug.Log($"{this.gameObject.name} notified hit ({target}, {damage})");
        enemySide.HandleEnemyFire(target, damage);
    }

    public bool CheckUnitHit(int position) {
        return Cells[position].IsOccupied();
    }

    public void HandleEnemyFire(int position, float damage) {
        Debug.Log($"HandleEnemyFire of {this.gameObject.name} is called");
        if (CheckUnitHit(position)) {
            Debug.Log(allyUnit.name + " is hit, take " + damage + " damage.");
            allyUnit.TakeDamage(damage);
        }
    }

    public void MoveUnit(int pos, Action onComplete = null) {
        StopAllCoroutines();
        StartCoroutine(MoveUnitRoutine(pos, onComplete));
        Debug.Log($"{allyUnit.name} moved to cell[{pos}]");
    }

    IEnumerator MoveUnitRoutine(int pos, Action onComplete = null) {
        while (Vector3.Distance(allyUnit.transform.position, Cells[pos].transform.position) >= 0.01f) {
            allyUnit.transform.position = Vector3.MoveTowards(
                allyUnit.transform.position, 
                Cells[pos].transform.position, 
                moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        
        allyUnit.transform.position = Cells[pos].transform.position;
        
        foreach (Cell cell in Cells) {
            cell.SetOccupied(false);    
        }
        
        Cells[pos].SetOccupied(true);
        
        // GỌI CALLBACK SAU KHI MOVEMENT HOÀN THÀNH
        onComplete?.Invoke();
    }
}