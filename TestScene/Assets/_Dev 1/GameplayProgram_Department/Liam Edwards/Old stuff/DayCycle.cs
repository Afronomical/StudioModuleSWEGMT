using System.Collections;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Gradient colorGradient; // A gradient to hold the color
    public float changeSpeed; // The speed at which the color changes
    public float timeInSun = 0; // A float to calculate the amount of time the player has spent in the sun
    public float nightTime = 120f; // The amount of time remaining in the night
    public int sunBurn; // Damage inflicted by the sun
    public float damageDelay = 1.5f; // Num of seconds between the delay in damage when the player is in the sun

    public bool canBurn = true; // Bool to check if the player can take damage

    public CountdownTimer countdownTimer;
    public PlayerDeath playerDeath;

    private void Start()
    {
        InitializeDayCycle();
    }

    private void Update()
    {
        UpdateNightTime();

        if (nightTime <= 0)
        {
            HandleSunExposure();
        }
    }

    private void InitializeDayCycle()
    {
        canBurn = true;
        gameObject.transform.localScale = new Vector2(1000, 1000);
        transform.position = new Vector3(0, 0, 0);
        nightTime = 120;
    }

    private void UpdateNightTime()
    {
        nightTime -= Time.deltaTime;
    }

    private void HandleSunExposure()
    {
        timeInSun += Time.deltaTime;

        if (IsPlayerInSunlight())
        {
            if (timeInSun < 5 && canBurn)
            {
                sunBurn = 1;
                StartCoroutine(SunDamage());
            }
            else if (timeInSun >= 5 && timeInSun < 15 && canBurn)
            {
                sunBurn = 3;
                StartCoroutine(SunDamage());
            }
            else if (timeInSun >= 15 && timeInSun < 30 && canBurn)
            {
                sunBurn = 5;
                StartCoroutine(SunDamage());
            }
        }
    }

    private bool IsPlayerInSunlight()
    {
        return transform.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

    private IEnumerator SunDamage()
    {
        canBurn = false;
        playerDeath.currentHealth -= sunBurn;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().SunRiseDamage();
        yield return new WaitForSeconds(damageDelay);
        canBurn = true;
    }
}



