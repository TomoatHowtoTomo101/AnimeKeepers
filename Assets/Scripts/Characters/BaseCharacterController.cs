using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class BaseCharacterController : MonoBehaviour
{
    [System.Serializable]
    public enum CombatAnimationDirection
    {
        Forward,
        Backward,
        Left,
        Right
    }

    [System.Serializable]
    public class CombatAnimationClip
    {
        public AnimationClip combatClip;
        public CombatAnimationDirection movement;
        public float movementStrength;
    }

    [SerializeField] private AnimancerComponent animancer;

    [Header("Non Combat Animations")]
    [SerializeField] private AnimationClip[] nonCombatAnimationClips;

    [Header("Combat Animations")]
    [SerializeField] private CombatAnimationClip[] combatAnimationClips;


    private CharacterController controller;
    private Vector3 velocity, gravity;
    private float moveSpeed, scaleMoveSpeed, turnSpeed, jumpSpeed, fallSpeed, drag;
    private float vInput, hInput;
    private bool spaceKey, jumping, inCombat, checkForChainAttack, chainAttack, moveDuringAttack;

    private int comboCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        animancer.Play(nonCombatAnimationClips[0]);
        controller = gameObject.GetComponent<CharacterController>();
        velocity = Vector3.zero;
        gravity = new Vector3(0f, -3.3f, 0f);
        moveSpeed = 3f;
        scaleMoveSpeed = 1f;
        turnSpeed = 100.0f;
        jumpSpeed = 150.0f;
        fallSpeed = -4f;
        drag = 0.85f;
        jumping = false;
        inCombat = false;
        chainAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();
        UpdateInputs();

        if (controller.isGrounded && !jumping && !inCombat)
        {
            GroundAnimation();

            if (spaceKey)
            {
                StartJumpWindup();
            }
        }

        if (jumping && velocity.y > fallSpeed)
        {
            velocity.y += 3f;
        }

        if (vInput < 0)
        {
            scaleMoveSpeed = 0.4f;
        }
        else
        {
            scaleMoveSpeed = 1f;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (checkForChainAttack)
            {
                chainAttack = true;
            }
            else
            {
                Attack();
            }
        }

        if (!inCombat)
        {
            CharacterMovement();
        }
    }

    #region Constantly Updating Variables
    private void UpdateInputs()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        spaceKey = Input.GetKeyDown("space");
    }

    private void UpdateVelocity()
    {
        velocity *= drag;
        velocity += gravity;
    }
    #endregion

    #region Attacking
    private void Attack()
    {
        inCombat = true;

        animancer.Play(combatAnimationClips[comboCounter].combatClip);

        StartCoroutine(TranslatePositionDuringAttack());
    }

    public void CheckChainAttack()
    {
        if (chainAttack)
        {
            NextAttackChain();
        }
        else
        {
            RegainMovementControl();
        }
    }

    private void NextAttackChain()
    {
        comboCounter += 1;
        animancer.Play(combatAnimationClips[comboCounter].combatClip);
        chainAttack = false;
    }

    public void CheckForChainAttack()
    {
        checkForChainAttack = true;
    }

    IEnumerator TranslatePositionDuringAttack()
    {
        moveDuringAttack = true;
        while (moveDuringAttack)
        {
            Vector3 endPosition = transform.position + transform.forward * 5;
            transform.position = Vector3.MoveTowards(transform.position,
                                                     endPosition,
                                                     combatAnimationClips[comboCounter].movementStrength * Time.deltaTime);
            yield return null;
        }

        StopCoroutine(TranslatePositionDuringAttack());
    }
    #endregion

    #region Character Movement & Jump Variables
    private void CharacterMovement()
    {
        velocity += transform.forward * Input.GetAxis("Vertical") * moveSpeed * scaleMoveSpeed;
        controller.Move(velocity * Time.deltaTime);
        //Debug.Log(velocity.y);
        transform.Rotate(0, hInput * turnSpeed * Time.deltaTime, 0);
    }

    private void RegainMovementControl()
    {
        moveDuringAttack = false;
        checkForChainAttack = false;
        chainAttack = false;
        inCombat = false;
        comboCounter = 0;
    }

    private void StartJumpWindup()
    {
        jumping = true;
        animancer.Play(nonCombatAnimationClips[3]).OnEnd = Jump;
    }

    private void Jump()
    {
        velocity.y = jumpSpeed;
        animancer.Play(nonCombatAnimationClips[4]).OnEnd = JumpApex;
    }

    private void JumpApex()
    {
        /*
        if (controller.isGrounded)
        {
            Landing();
        }
        else if (velocity.y < fallSpeed)
        {
            StartFall();
        }
        else
        {
            animancer.Play(jump_Apex).OnEnd = JumpApex;
        }
        */

        animancer.Play(nonCombatAnimationClips[5]).OnEnd = StartFall;
    }

    private void StartFall()
    {
        /*
        if (controller.isGrounded)
        {
            Landing();
        }
        else
        {
            animancer.Play(fall_Start).OnEnd = MidFall;
        }
        */

        animancer.Play(nonCombatAnimationClips[6]).OnEnd = MidFall;
    }

    private void MidFall()
    {
        if (controller.isGrounded)
        {
            Landing();
        }
        else;
        {
            animancer.Play(nonCombatAnimationClips[7]).OnEnd = MidFall; 
        }
    }

    private void Landing()
    {
        jumping = false;
        animancer.Play(nonCombatAnimationClips[8]).OnEnd = GroundAnimation;
    }

    private void GroundAnimation()
    {
        if (vInput > 0)
        {
            if (!animancer.IsPlaying(nonCombatAnimationClips[1]))
            {
                animancer.CrossFade(nonCombatAnimationClips[1]);
            }
        }
        else if (vInput < 0)
        {
            if (!animancer.IsPlaying(nonCombatAnimationClips[2]))
            {
                animancer.CrossFade(nonCombatAnimationClips[2]);
            } 
        }
        else if (!animancer.IsPlaying(nonCombatAnimationClips[0]))
        {
            animancer.CrossFade(nonCombatAnimationClips[0]);
        }
    }
    #endregion
}
