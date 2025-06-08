using NF.Main.Core;
using NF.Main.Core.GameStateMachine;
using NF.TD.PlayerCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NF.Main.Gameplay
{
    public class GameManager : SingletonPersistent<GameManager>
    {
        public GameState GameState;

        private StateMachine _stateMachine;
        
        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            _stateMachine.Update();

            if (PlayerStats.Lives <= 0) 
            {
                EndGame();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (GameState == GameState.Playing)
                {
                    PauseGame();
                }
                else if (GameState == GameState.Paused)
                {
                    ResumeGame();
                }
            }
        }

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            GameState = GameState.Playing;
            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            // State Machine
            _stateMachine = new StateMachine();

            // Declare states
            var pausedState = new GamePausedState(this, GameState.Paused);
            var playingState = new GamePlayingState(this, GameState.Playing);
            var gameOverState = new GameOverState(this, GameState.GameOver);


            // Define transitions
            At(playingState, pausedState, new FuncPredicate(() => GameState == GameState.Paused));
            At(playingState, gameOverState, new FuncPredicate(() => GameState == GameState.GameOver));
            
            Any(playingState, new FuncPredicate(() => GameState == GameState.Playing));

            // Set initial state
            _stateMachine.SetState(playingState);
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

        void EndGame()
        {
            if (GameState != GameState.GameOver)
            {
                GameState = GameState.GameOver;
            }
        }

        void PauseGame()
        {
            if(GameState != GameState.Paused)
            {
                GameState = GameState.Paused;
            }
        }

        // Made public so that it can be also accessed by the Resume/Continue Button in Pause UI
        public void ResumeGame()
        {
            if (GameState != GameState.Playing)
            {
                GameState = GameState.Playing;
            }
        }
    }
}