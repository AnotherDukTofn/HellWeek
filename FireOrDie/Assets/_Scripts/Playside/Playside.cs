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
        enemySide.HandleEnemyFire(target, damage);
    }

    public bool CheckUnitHit(int position) {
        Cells[position].Hit();
        return Cells[position].IsOccupied();
    }

    public void HandleEnemyFire(int position, float damage) {
        if (CheckUnitHit(position)) {
            Debug.Log(allyUnit.name + " is hit, take " + damage + " damage.");
            allyUnit.TakeDamage(damage);
        }
    }

    public void MoveUnit(int pos, Action onComplete = null) {
        StopAllCoroutines();
        StartCoroutine(MoveUnitRoutine(pos, onComplete));
    }

    IEnumerator MoveUnitRoutine(int pos, Action onComplete = null) {
        foreach (Cell cell in Cells) {
            cell.SetOccupied(false);    
        }
        Cells[pos].SetOccupied(true);
    
        while (Vector3.Distance(allyUnit.transform.position, Cells[pos].transform.position) >= 0.01f) {
            allyUnit.transform.position = Vector3.MoveTowards(
                allyUnit.transform.position, 
                Cells[pos].transform.position, 
                moveSpeed * Time.deltaTime);
        
            yield return null;
        }
    
        allyUnit.transform.position = Cells[pos].transform.position;
        onComplete?.Invoke();
    }
    
    public Unit GetAllyUnit() {
        return allyUnit;
    }
}