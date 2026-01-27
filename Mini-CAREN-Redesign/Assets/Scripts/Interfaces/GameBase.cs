using UnityEngine;

public abstract class GameBase : MonoBehaviour
{
    public abstract void GameStart();
    public abstract void GameStop();
    public abstract void GamePause();
    public abstract void GameUnpause();
}