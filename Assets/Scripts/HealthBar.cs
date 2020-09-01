using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image barImage;
    private HealthSystem healthSystem;

    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();

    }

    private void Start()
    {
        healthSystem = new HealthSystem(100);
        setHealth(healthSystem.GetHealthNormalized());
        healthSystem.onDamage += healthSystem_onDamage;
        healthSystem.onHeal += healthSystem_onHeal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            healthSystem.Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthSystem.Heal(10);
        }
        if (healthSystem.GetHealthAmount() == 0)
        {

        }
    }

    private void healthSystem_onHeal(object sender, System.EventArgs e) {
        setHealth(healthSystem.GetHealthNormalized());
    }

    private void healthSystem_onDamage(object sender, System.EventArgs e) {
        setHealth(healthSystem.GetHealthNormalized());
    }

    private void setHealth(float healthNormalized) {
        barImage.fillAmount = healthNormalized;
    }
}
