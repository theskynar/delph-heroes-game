using UnityEngine;
using UnityEngine.UI;

public class HealthSystem {
    public int health;
    private int healthMax;
    private GameObject hpBar;

    public HealthSystem(int health)
    {
        this.health = health;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;

        hpBar = GameObject.Find("HpBar");
        var image = hpBar.GetComponent<Image>();
        image.fillAmount = 100 / health;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
    }
}
