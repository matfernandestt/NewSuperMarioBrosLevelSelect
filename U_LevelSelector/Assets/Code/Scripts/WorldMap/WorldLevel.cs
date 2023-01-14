using System;
using UnityEngine;

public class WorldLevel : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private LevelAndPath upConnection;
    [SerializeField] private LevelAndPath downConnection;
    [SerializeField] private LevelAndPath leftConnection;
    [SerializeField] private LevelAndPath rightConnection;

    public LevelAndPath GetLevelInThatDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return upConnection;
            case Direction.Down:
                return downConnection;
            case Direction.Left:
                return leftConnection;
            case Direction.Right:
                return rightConnection;
        }
        return null;
    }
}

[Serializable]
public class LevelAndPath
{
    public WorldLevel Level;
    public WorldMapPath Path;
    public bool isReverse;
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
