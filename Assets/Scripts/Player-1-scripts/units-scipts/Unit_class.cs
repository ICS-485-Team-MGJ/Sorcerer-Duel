using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit_class : MonoBehaviour
{
    protected float health = 100f;
    protected float damge = 10f;
    protected float speed = 4.5f;

    public abstract void dead();
    public abstract void attack(float damge);
    public abstract void move();
    public abstract void scan();
    public abstract void takeDamge(float damge);
}
