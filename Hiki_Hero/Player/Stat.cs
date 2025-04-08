using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stat : MonoBehaviour
{
    [SerializeField] protected int _level;
    [SerializeField] protected int _atk;
    [SerializeField] protected int _hp;

    public int Level { get { return _level; } set { _level = value; } }
    public int Atk { get { return _atk; } set { _atk = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
}
