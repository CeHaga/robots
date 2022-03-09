using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour, IFighter {
    [Header("Starting Attributes")]
    [SerializeField] private int maxLifePoints;
    [SerializeField] private int minAttackPoints;
    [SerializeField] private int maxDefensePoints;

    private int lifePoints;
    private int attackPoints;
    private int defensePoints;
    private bool isDefending;
    private bool isDodging;
    private bool dodgedLastRound;
    private bool healedLastRound;
    private bool chargedLastRound;

    private void Start() {
        ResetAttributes();
    }

    public void ResetAttributes() {
        lifePoints = maxLifePoints;
        attackPoints = minAttackPoints;
        defensePoints = maxDefensePoints;
        healedLastRound = chargedLastRound = isDefending = isDodging = dodgedLastRound = false;
    }

    public int getMaxLifePoints() {
        return maxLifePoints;
    }

    public bool attack(Fighter enemy) {
        if (!this.dodgedLastRound) {
            enemy.ReceiveAttack(attackPoints);
        }
        this.attackPoints = minAttackPoints;
        if (this.dodgedLastRound) return false;
        return true;
    }

    public bool defend() {
        if (defensePoints > 0) {
            isDefending = true;
            return true;
        }
        isDefending = false;
        return false;
    }

    public bool charge() {
        attackPoints += chargedLastRound ? 2 : 1;
        chargedLastRound = !chargedLastRound;
        return true;
    }
    public bool dodge() {
        dodgedLastRound = true;
        return true;
    }

    public bool heal() {
        lifePoints += healedLastRound ? 2 : 1;
        lifePoints = Mathf.Min(lifePoints, maxLifePoints);
        healedLastRound = !healedLastRound;
        return true;
    }

    public bool ReceiveAttack(int attackPoints) {
        if (isDodging) return false;
        if (isDefending) {
            defensePoints -= 1;
            return false;
        }
        this.lifePoints -= Mathf.Max(attackPoints, 0);
        return true;
    }

    public bool executeAction(Commands action, Fighter enemy) {
        bool success = true;
        switch (action) {
            case Commands.ATTACK:
                success = attack(enemy);
                isDefending = dodgedLastRound = healedLastRound = chargedLastRound = false;
                break;
            case Commands.DEFEND:
                success = defend();
                dodgedLastRound = healedLastRound = chargedLastRound = false;
                break;
            case Commands.CHARGE:
                charge();
                isDefending = dodgedLastRound = healedLastRound = false;
                break;
            case Commands.DODGE:
                dodge();
                isDefending = healedLastRound = chargedLastRound = false;
                break;
            case Commands.HEAL:
                heal();
                isDefending = dodgedLastRound = chargedLastRound = false;
                break;
        }
        return success;
    }

    public bool isDead() {
        return this.lifePoints <= 0;
    }

    public int GetChargePoints() {
        return Mathf.Max(attackPoints - minAttackPoints);
    }

    public int GetLifePoints() {
        return Mathf.Max(lifePoints, 0);
    }

    public int GetDefensePoints() {
        return Mathf.Max(defensePoints, 0);
    }
}
