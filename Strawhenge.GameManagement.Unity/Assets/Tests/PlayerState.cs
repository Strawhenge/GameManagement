using Strawhenge.GameManagement.Unity;
using System;

class PlayerState : IPlayerState
{
    public event Action Died;

    public void InvokeDied() => Died?.Invoke();
}