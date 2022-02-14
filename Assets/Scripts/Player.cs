using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player
{
    public int score;

    public string name;

    public Player()
    {
        score = 100;
        name = "Friend";
    }

    public Player(string name)
    {
        score = 100;
        this.name = name;
    }
}
