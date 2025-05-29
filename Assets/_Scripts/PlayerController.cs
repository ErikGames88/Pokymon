using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 input;
    private bool isMoving;
    private Animator _animator;


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            // input.x = Input.GetAxisRaw("Horizontal");
            // input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                _animator.SetFloat("Move X", input.x);
                _animator.SetFloat("Move Y", input.y);

                var targetPosition = transform.position;

                targetPosition.x += input.x;
                targetPosition.y += input.y;

                StartCoroutine(MoveTowards(targetPosition));
            }
        }


    }

    IEnumerator MoveTowards(Vector3 destination)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }
}
