using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame
{
    public class DownCommand : ICommand
    {
        private PlayerCharacter player;

        public DownCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {
            player.Down();
        }
    }
    public class UpCommand : ICommand
    {
        private PlayerCharacter player;

        public UpCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {
            player.Up();
        }
    }

    public class LeftCommand : ICommand
    {
        private PlayerCharacter player;

        public LeftCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {
            player.Left();
        }
    }

    public class RightCommand : ICommand
    {
        private PlayerCharacter player;

        public RightCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {
            player.Right();
        }
    }

    public class ExitCommand : ICommand
    {
        private DespicableGame game;

        public ExitCommand(DespicableGame game)
        {
            this.game = game;
        }

        public void Execute(Gamepad pad)
        {
            game.Exit();
        }
    }

    public class PauseCommand : ICommand
    {
        private DespicableGame game;

        public PauseCommand(DespicableGame game)
        {
            this.game = game;
        }

        public void Execute(Gamepad pad)
        {
            game.PauseButtonPressAction();
        }
    }

    public class ACommand : ICommand
    {
        private PlayerCharacter player;

        public ACommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {

        }
    }

    public class BCommand : ICommand
    {
        private PlayerCharacter player;

        public BCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {

        }
    }

    public class YCommand : ICommand
    {
        private PlayerCharacter player;

        public YCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {

        }
    }

    public class XCommand : ICommand
    {
        private PlayerCharacter player;

        public XCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {

        }
    }

    public class LeftShoulderCommand : ICommand
    {
        private PlayerCharacter player;

        public LeftShoulderCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {

        }
    }

    public class RightShoulderCommand : ICommand
    {
        private PlayerCharacter player;

        public RightShoulderCommand(PlayerCharacter f)
        {
            player = f;
        }

        public void Execute(Gamepad pad)
        {

        }
    }


}
