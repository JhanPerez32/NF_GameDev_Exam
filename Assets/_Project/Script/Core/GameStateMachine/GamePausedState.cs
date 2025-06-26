using NF.Main.Gameplay;
using NF.TD.UICore;
using UnityEngine;

namespace NF.Main.Core.GameStateMachine
{
    public class GamePausedState : GameBaseState
    {
        public GamePausedState(GameManager gameManager, GameState gameState) : base(gameManager, gameState)
        {
        
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game paused state");

            Time.timeScale = 0f;

            UIManager.Instance.ShowPauseScreen();
        }

        public override void OnExit()
        {
            base.OnExit();

            Time.timeScale = 1f;

            // Optional: Hide UI if leaving GameOver state for any reason
            UIManager.Instance.HidePauseScreen();
        }
    }
}