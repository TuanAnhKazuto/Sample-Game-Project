using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATK : MonoBehaviour
{
    [SerializeField] private Transform arrowTransform;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 25f;
    private bool isMovingRight = true;
    private int diretion = 1;
    private void Start()
    {
    }
    void Update()
    {
        Shoot();
    }

    public void IsMovingRight()
    {
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            isMovingRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            isMovingRight = false;
        }
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowTransform.position, Quaternion.identity);

            if(isMovingRight)
            {
                diretion = 1;
            }
            else
            {
                diretion = -1;
            }
            arrow.GetComponent<Rigidbody2D>().velocity = arrowTransform.right * arrowSpeed * diretion;
        }
    }   
}
