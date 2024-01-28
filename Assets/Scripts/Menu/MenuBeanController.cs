using Assets.Scripts.Controllers;
using UnityEngine;

public class MenuBeanController : BeanController
{
    [SerializeField]
    private float maxX = 12f;

    void Update()
    {
        if (transform.position.x > maxX)
            Destroy(gameObject);
    }
}
