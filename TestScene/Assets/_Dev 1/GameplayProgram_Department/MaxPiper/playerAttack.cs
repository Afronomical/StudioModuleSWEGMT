using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitBox;
    public float attackDelayStart = 0.5f;
    private float attackDelay;
    private Vector2 mousePos;
    private GameObject enemyTarg;
    private AICharacter AiEnemy;
    private bool canHit = true;
    private Feeding feeding;
    public GameObject heavyHitBox; // New hit box for heavy attack
    public int heavyDamage = 3; // Damage for heavy attack
    public float heavyChargeTime = 1.5f; // Time to charge the heavy attack
    private float heavyChargeTimer;
    private bool isChargingAttack = false; // Flag to indicate if the heavy attack is charging

    private Animator animator;
    private PlayerAnimationController animationController;

    public List<GameObject> enemyList = new List<GameObject>();

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
                AiEnemy.health = Mathf.Clamp(AiEnemy.health - damage, 1, 1000);

                AudioManager.Manager.PlaySFX("NPC_TakeDamage");
                AiEnemy.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.Hurt); //Plays the currently hit AIs take damage function
                //enemyTarg.GetComponentInChildren<AI_AnimationController>().ChangeAnimationState(AI_AnimationController.AnimationStates.Hurt);
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
        attackDelay = attackDelayStart;

        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();

        feeding = GetComponent<Feeding>();

        //enemyHealth = AiEnemy.health;

        heavyChargeTimer = heavyChargeTime;
    }


    void Update()
    {

        //gets mous pos (0, 0 at centre screen)
        mousePos.x = Input.mousePosition.x - (Screen.width / 2);
        mousePos.y = Input.mousePosition.y - (Screen.height / 2);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        hitBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //swap "angle" with another 0 if cam is another angle
        heavyHitBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //swap "angle" with another 0 if cam is another angle
        //calls damage enemy when LMB is pressed
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.SlashAttack);

            AudioManager.Manager.PlaySFX("PlayerAttack");
            if (canHit )//&& //feeding.currentlyFeeding == false)
            {
               
                damageEnemy();
                canHit = false;
                Debug.Log("ATTACKED HDBGSUYHGBK");
            }
            
            
        }

        if (Input.GetKey(KeyCode.Mouse1)) // Assuming Mouse1 is the input for heavy attack
        {
            // Start charging the heavy attack
            if (!isChargingAttack)
            {
                isChargingAttack = true;
            }
        }

        if (isChargingAttack)
        {
            if (heavyChargeTimer > 0)
            {
                heavyChargeTimer -= Time.deltaTime;

                // Charging animation or effects can be included here

                if (heavyChargeTimer <= 0)
                {
                    ExecuteHeavyAttack();
                    heavyChargeTimer = heavyChargeTime;
                    isChargingAttack = false;
                    canHit = false;
                }
            }
        }

        if (!canHit)
        {
            attackDelay -= Time.deltaTime;

            if (attackDelay <= 0)
            {
                canHit = true;
                attackDelay = attackDelayStart;
            }
        }
    }

    void ExecuteHeavyAttack()
    {
        foreach (GameObject obj in enemyList)
        {
            if (obj.TryGetComponent(out AICharacter AiEnemy))
            {
                AiEnemy.health = Mathf.Clamp(AiEnemy.health - heavyDamage, 1, 1000);

                AudioManager.Manager.PlaySFX("NPC_TakeDamage");
                AiEnemy.GetComponentInChildren<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.Hurt);
            }
        }
    }

}