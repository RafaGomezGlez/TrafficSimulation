using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Car
{
    public int id;
    public int direction;
    public float x,y,z; //Invertir z con y en movimiento
}
