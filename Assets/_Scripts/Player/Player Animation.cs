using System.Collections.Generic;
using NUnit.Framework;
using SHG.AnimatorCoder;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : AnimatorCoder
{
    private readonly AnimationData RUN_LEFT = new(Animations.runLeft);
    private readonly AnimationData RUN_RIGHT = new(Animations.runRight);
    private readonly AnimationData RUN_UP = new(Animations.runUp);
    private readonly AnimationData RUN_DOWN = new(Animations.runDown);
    private readonly AnimationData RESET = new(Animations.RESET);


    public static PlayerAnimation Instance;
    Vector2 _movementVector;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void OnMovement(InputValue value) => _movementVector = value.Get<Vector2>();

    private void Update()
    {
        DefaultAnimation(0);
    }

    public override void DefaultAnimation(int layer)
    {
        if (_movementVector.magnitude < .05f)
            Play(RUN_DOWN);
        else if (_movementVector.y > 0.5f)
            Play(RUN_UP);
        else if (_movementVector.y < -0.5f)
            Play(RUN_DOWN);
        else if (_movementVector.x != 0)
            Play(RUN_LEFT);
        else
            Play(RUN_DOWN);

        GetComponent<SpriteRenderer>().flipX = _movementVector.x > 0;
    }
}