using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class playerAttack : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public int damage = 1;
    public GameObject hitBox;
    public float attackDelayStart = 0.5f;
    private float attackDelay;
    private Vector2 mousePos;
    private GameObject enemyTarg;
    private AICharacter AiEnemy;
    private bool canHit = true;
    private Feeding feeding;
    public GameObject BloodOnDamage;
    public GameObject destroyGrave;
    // public GameObject floatingDamage;

    [HideInInspector] public bool parrying;
    private bool canParry;
    public GameObject parryLight;
    public float parryFeedbackLength = 0.3f;
    private bool coolDownParry;
    private float parryCoolTime;
    private float parryCoolStart = 1f;
    public GameObject heavyHitBox; // New hit box for heavy attack
    public int heavyDamage = 3; // Damage for heavy attack
    public float heavyChargeTime = 1.5f; // Time to charge the heavy attack
    private float heavyChargeTimer;
    private bool isChargingAttack = false; // Flag to indicate if the heavy attack is charging
    [HideInInspector] public Collider2D parriedEnemyInRange;

    [SerializeField] TrailRenderer dashTrail;
    private bool isHeavyAttackReady = false;

    private Animator animator;
    private PlayerAnimationController animationController;

    public List<GameObject> enemyList = new List<GameObject>();

    public GameObject parrySword;
    public GameObject parrySparks;

    public GameObject GetParrySword() => parrySword;

    //enter collision, detects if has enemy tag, if true set enemy to attacking var
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if ((other.tag == "Villager") || (other.tag == "Hunter"))
    //    {
    //        if (!enemyList.Contains(other.gameObject))
    //        {
    //            enemyList.Add(other.gameObject);
    //        }
            
    //    }
    //}

    ////exit clears enemy target
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if ((other.tag == "Villager") || (other.tag == "Hunter"))
    //    {
    //        enemyList.Remove(other.gameObject);
    //        //Debug.Log(other.name);
    //    }
    //}

    public void onAttackEnter(Collider2D other)
    {
        if (!enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);
        }
    }

    public void onAttackExit(Collider2D other)
    {
        enemyList.Remove(other.gameObject);
    }


    //damages the enemy
    void damageEnemy()
    {
        foreach (GameObject obj in enemyList)
        {
            if (obj.TryGetComponent(out AICharacter AiEnemy))
            {
                Debug.Log("DAMAGE");
                AiEnemy.health = Mathf.Clamp(AiEnemy.health - damage, 1, 1000);
                AiEnemy.ShowFloatingDamage(damage); 

                EnemyKnockback enemyKnockback = obj.GetComponent<EnemyKnockback>();

                if (enemyKnockback != null)
                {
                    // Apply knockback to the enemy
                    enemyKnockback.ApplyKnockback(transform.position);
                }

                AudioManager.Manager.PlaySFX("NPC_TakeDamage");
                //Instantiate(floatingDamage, AiEnemy.transform.position, Quaternion.identity);
                Instantiate(BloodOnDamage, AiEnemy.transform.position, Quaternion.identity);
                if (AiEnemy.characterType != AICharacter.CharacterTypes.Boss)
                    AiEnemy.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.Hurt); //Plays the currently hit AIs take damage function
                //enemyTarg.GetComponentInChildren<AI_AnimationController>().ChangeAnimationState(AI_AnimationController.AnimationStates.Hurt);
            }

            if (obj.CompareTag("Grave") && Input.GetMouseButtonDown(0))
            {
                Instantiate(destroyGrave, obj.transform.position, Quaternion.identity);
                Destroy(obj);
            }

        }

        //for (int i = enemyList.Count -1; i >= 0; i--)
        //{
        //    GameObject o = enemyList[i];
        //    o.
        //}

        //if (enemyTarg != null)
        //{

        //    Debug.Log(enemyTarg.name);
        //    AiEnemy.health -= damage;
        //    AudioManager.Manager.PlaySFX("NPC_TakeDamage");
        //}


    }

    


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        attackDelay = attackDelayStart;
        parryCoolTime = parryCoolStart;

        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();

        feeding = GetComponent<Feeding>();

        //enemyHealth = AiEnemy.health;

        heavyChargeTimer = heavyChargeTime;

        parrySword.SetActive(false);
        parrySparks.SetActive(false);
    }


    void Update()
    {

        //gets mous pos (0, 0 at centre screen)
        mousePos.x = Input.mousePosition.x - (Screen.width / 2);
        mousePos.y = Input.mousePosition.y - (Screen.height / 2);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        hitBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //swap "angle" with another 0 if cam is another angle
        heavyHitBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //swap "angle" with another 0 if cam is another angle

        // Check for normal attack (left mouse button)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canHit && !feeding.currentlyFeeding && !isChargingAttack)
            {
                animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.SlashAttack);
                animator.SetTrigger("AttackSlash");
                AudioManager.Manager.PlaySFX("PlayerAttack");
                damageEnemy();
                canHit = false;
            }
        }

        // Check for heavy attack charge (holding left mouse button)
        if (Input.GetMouseButton(0))
        {
            if (!isChargingAttack && canHit && !feeding.currentlyFeeding)
            {
                isChargingAttack = true;
            }
        }
        else
        {
            if (isChargingAttack && isHeavyAttackReady)
            {
                animator.SetTrigger("HeavyAttackSlash");
                AudioManager.Manager.PlaySFX("PlayerHeavyAttack");
                animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.SlashAttack);
                ExecuteHeavyAttack();
                canHit = false;
                isChargingAttack = false;
                isHeavyAttackReady = false;
                spriteRenderer.color = Color.white;
            }
            else
            {
                isChargingAttack = false;
                heavyChargeTimer = heavyChargeTime;
            }
        }

        // Handle heavy attack charge
        if (isChargingAttack)
        {
            if (heavyChargeTimer > 0)
            {
                heavyChargeTimer -= Time.deltaTime;

                if (heavyChargeTimer <= 0)
                {
                    isHeavyAttackReady = true;
                    spriteRenderer.color = Color.yellow;
                }
            }
        }

        // Handle attack delay
        if (!canHit)
        {
            attackDelay -= Time.deltaTime;

            if (attackDelay <= 0)
            {
                canHit = true;
                attackDelay = attackDelayStart;
            }
        }


        if (Input.GetKeyDown(KeyCode.Mouse1) && gameObject.GetComponent<PlayerDeath>().recParryAttack && !coolDownParry)
        {
            parrying = true;
            StartCoroutine(parryFeedBack());
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            coolDownParry = true;
            Debug.Log("parry cooling down");
        }
        if (coolDownParry)
        {
            parryCoolTime -= Time.deltaTime;
            if (parryCoolTime <= 0)
            {
                coolDownParry = false;
                parryCoolTime = parryCoolStart;
            }
        }

    }
    IEnumerator parryFeedBack()
    {
        if (!feeding.IsBiteIconActive())
            parrySparks.SetActive(true);

        AudioManager.Manager.PlaySFX("Parry");
        parryLight.GetComponent<Light2D>().enabled = true;
        GameObject.FindWithTag("MainCamera").GetComponent<cameraFollow>().StartShake(parryFeedbackLength, 3.5f);
        GameObject.FindWithTag("MainCamera").GetComponent<cameraFollow>().CameraZoom(parryFeedbackLength / 4, 4f);
        GetComponent<KnockBack>().ApplyKnockback(parriedEnemyInRange);
        dashTrail.emitting = true;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(parryFeedbackLength);
        GameObject.FindWithTag("MainCamera").GetComponent<cameraFollow>().CameraZoom(parryFeedbackLength / 3, 0);
        //yield return new WaitForSeconds(parryFeedbackLength / 2);
        Time.timeScale = 1f;
        dashTrail.emitting = false;
        parryLight.GetComponent<Light2D>().enabled = false;
        parrySparks.SetActive(false);
    }
    

    void ExecuteHeavyAttack()
    {
        foreach (GameObject obj in enemyList)
        {
            if (obj.TryGetComponent(out AICharacter AiEnemy))
            {
                AiEnemy.health = Mathf.Clamp(AiEnemy.health - heavyDamage, 1, 1000);
                AiEnemy.ShowFloatingDamage(heavyDamage);
                EnemyKnockback enemyKnockback = obj.GetComponent<EnemyKnockback>();

                if (enemyKnockback != null)
                {
                    // Apply knockback to the enemy
                    enemyKnockback.ApplyKnockback(transform.position, 3.8f);
                }

                AudioManager.Manager.PlaySFX("NPC_TakeDamage");
                AiEnemy.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.Hurt);
                Instantiate(BloodOnDamage, AiEnemy.transform.position, Quaternion.identity);
                Instantiate(BloodOnDamage, AiEnemy.transform.position, Quaternion.identity);
                Instantiate(BloodOnDamage, AiEnemy.transform.position, Quaternion.identity);
            }

            if (obj.CompareTag("Grave"))
            {
                Instantiate(destroyGrave, obj.transform.position, Quaternion.identity);
                Destroy(obj);
            }
        }
    }
}