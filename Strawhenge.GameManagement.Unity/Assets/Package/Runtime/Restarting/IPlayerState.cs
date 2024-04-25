using System;

namespace Strawhenge.GameManagement.Unity
{
    public interface IPlayerState
    {
        public event Action Died; 
    }
}