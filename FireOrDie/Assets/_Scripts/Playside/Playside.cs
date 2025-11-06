using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Playside : MonoBehaviour {
    [SerializeField] private Cell[] cells;
    [SerializeField] private Unit allyUnit;
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private Playside enemySide;

    [SerializeField] private bool moveToMid;
    [SerializeField] private bool moveToTop;
    [SerializeField] private bool moveToBot;

    void Update() {
        if (moveToMid) {
            MoveUnit(1);
            moveToMid = false;
        }

        if (moveToTop) {
            MoveUnit(0);
            moveToTop = false;
        }

        if (moveToBot) {
            MoveUnit(2);
            moveToBot = false;
        }
    }

    private void OnEnable() {
        allyUnit.OnFireToPosition += NotifyHit;
    }

    private void OnDisable() {
        allyUnit.OnFireToPosition -= NotifyHit;
    }

    private void Start() {
        MoveUnit(1);
    }

    void NotifyHit(int target, float damage) {
        Debug.Log($"{this.gameObject.name} notified hit ({target}, {damage})");
        enemySide.HandleEnemyFire(target, damage);
    }

    public bool CheckUnitHit(int position) {
        return cells[position].IsOccupied();
    }

    public void HandleEnemyFire(int position, float damage) {
        Debug.Log($"HandleEnemyFire of {this.gameObject.name} is called");
        if (CheckUnitHit(position)) {
            Debug.Log(allyUnit.name + " is hit, take " + damage + " damage.");
            allyUnit.TakeDamage(damage);
        }
    }

    public void MoveUnit(int pos) {
        StopAllCoroutines();
        StartCoroutine(MoveUnitRoutine(pos));
        Debug.Log($"{allyUnit.name} moved to cell[{pos}]");

    }

    IEnumerator MoveUnitRoutine(int pos) {
        while (Vector3.Distance(allyUnit.transform.position, cells[pos].transform.position) >= 0.01f) {
            allyUnit.transform.position = Vector3.MoveTowards(
                allyUnit.transform.position, 
                cells[pos].transform.position, 
                moveSpeed * Time.deltaTime);
            
            yield return null;
        }
        
        allyUnit.transform.position = cells[pos].transform.position;
        
        foreach (Cell cell in cells) {
            cell.SetOccupied(false);    
        }
        
        cells[pos].SetOccupied(true);
    }
}
