using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    public enum CameraMode
    {
        Quarterview,
        Aiming,
    }

    public enum PlayerState
    {
        Idle,
        Run,
        Die,
    }

    public enum EnemyState
    {
        Move,
        Find,
        Follow,
    }

    public enum CardKey
    {
        White,
        Red,
        Yellow,
        Green,
        Blue,
        MaxCount
    }

    public enum SequnceNumber
    {
        Opening_1,
        Opening_2
    }

    public enum Scene
    {

    }
}