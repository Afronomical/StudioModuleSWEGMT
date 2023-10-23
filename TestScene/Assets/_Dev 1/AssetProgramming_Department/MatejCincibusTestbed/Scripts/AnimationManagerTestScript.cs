using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerTestScript : MonoBehaviour
{
    AnimationManager animationManager;

    private void Start()
    {
        animationManager = GetComponent<AnimationManager>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    animationManager.ChangeAnimationState(AnimationManager.AnimationStates.MoveHorizontal);
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    animationManager.ChangeAnimationState(AnimationManager.AnimationStates.MoveVertical);
        //}
    }
}
