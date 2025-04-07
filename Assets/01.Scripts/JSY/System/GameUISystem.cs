using JSY;
using UnityEngine;

public enum Speed : int
{
    normalSpeed = 1,
    twoSpeed = 2,
    fourSpeed = 4,
}

public static class GameUISystem
{
    public static Speed GameSpeed = Speed.normalSpeed;

    public static Speed ChangeSpeed()
    {
        GameSpeed = (Speed)((int)GameSpeed * 2);
        if ((int)GameSpeed == 8) GameSpeed = Speed.normalSpeed;
        return GameSpeed;
    }
}
