using GameClientNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class SingletonGameState
    {
        private static SingletonGameState instance = null;
        private static readonly object padlock = new object();
        private GameState gameState = null;
        private UserCommand userCommand = null;

        private SingletonGameState()
        {
            gameState = new GameState();
            userCommand = new UserCommand();
        }

        public static SingletonGameState GetInstance()
        {
            lock (padlock)
            {
                if (SingletonGameState.instance == null)
                {
                    instance = new SingletonGameState();
                }

                return instance;
            }
        }

        public void SetGameState(GameState _gameState)
        {
            lock (padlock)
            {
                gameState = _gameState;
            }
        }

        public GameState GetGameState()
        {
            return gameState;
        }

        public void SetUserCommand(UserCommand _userCommand)
        {
            lock (padlock)
            {
                userCommand = _userCommand;
            }
        }

        public UserCommand GetUserCommand()
        {
            return userCommand;
        }
    }
}
