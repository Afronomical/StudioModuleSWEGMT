using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation manager is a script that goes inside of a prefab e.g. (Player or NPCs), which
/// is to be animated.
/// To get a reference to this script component in the a centralized script e.g. (Player Controller)
/// You have to create a variable to hold it and then during start get its component:
/// 
/// AnimationManager animationManager;
/// 
/// animationManager = GetComponent<AnimationManager>();
/// 
/// </summary>
public class AIAnimationController : MonoBehaviour
{
    public enum AnimationStates
    {
        Idle,
        Walk,
        Hurt,
        Downed,
        Death,
        BowAttack,
        SwordAttack
    }

    // GOs Animator Component:
    private Animator animator;
    private AnimationStates currentState;

    // INFO: The position of each state and string should match in both lists, otherwise the state will point to a completely different animation

    // List of all the GOs animation states that will be used as a key later to access instead of calling via strings
    public List<AnimationStates> animationStatesList = new();
    // List of all the names of animations that relate to this specific GO
    [SerializeField] private List<string> animationStringsList = new();

    // Dictionary that holds each animation with a relevant key that is used to access it to prevent mispelled animation strings
    [SerializeField] private Dictionary<AnimationStates, string> animationsDictionary = new();

    private void Start()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i < animationStatesList.Count; i++)
        {
            animationsDictionary.Add(animationStatesList[i], animationStringsList[i]);
        }
    }

    private void Update()
    {
        /*if (AnimationStates.SwordAttack == currentState)
        {
            AudioManager.Manager.PlaySFX("NPC_MeleeAttack");
        /*}

        /*if (AnimationStates.BowAttack == currentState)
        {
            AudioManager.Manager.PlaySFX("NPC_RangedAttack");
        }*/
    }

    public void ChangeAnimationState(AnimationStates newState)
    {
        if (currentState == newState) return;

        if (animationsDictionary.ContainsKey(newState))
        {
            animator.Play(animationsDictionary[newState]);
            currentState = newState;
        }
        else
        {
            Debug.Log("Animation not found!");
        }
    }

    public bool IsAnimationPlaying(Animator animator, AnimationStates state)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationsDictionary[state])
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

