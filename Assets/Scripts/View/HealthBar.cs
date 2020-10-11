using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();

    }

    private void Start()
    {
        setHealth(GameController.Instance.playerHealthSystem.GetHealthNormalized());
        GameController.Instance.playerHealthSystem.onDamage += healthSystem_onDamage;
        GameController.Instance.playerHealthSystem.onHeal += healthSystem_onHeal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            GameController.Instance.playerHealthSystem.Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameController.Instance.playerHealthSystem.Heal(10);
        }
    }

    private void healthSystem_onHeal(object sender, System.EventArgs e) {
        setHealth(GameController.Instance.playerHealthSystem.GetHealthNormalized());
    }

    private void healthSystem_onDamage(object sender, System.EventArgs e) {
        setHealth(GameController.Instance.playerHealthSystem.GetHealthNormalized());
    }

    private void setHealth(float healthNormalized) {
        barImage.fillAmount = healthNormalized;
    }

    public float GetHealthSystem()
    {
        return GameController.Instance.playerHealthSystem.GetHealthNormalized();
    }

    public void GetDamage(int damage)
    {
        GameController.Instance.playerHealthSystem.Damage(damage);
    }
}
