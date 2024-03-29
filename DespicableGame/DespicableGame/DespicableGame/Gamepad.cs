﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework.Content;
namespace DespicableGame
{
    public class Gamepad
    {
        ICommand gamePadDown;
        ICommand gamePadUp;
        ICommand gamePadRight;
        ICommand gamePadLeft;
        ICommand gamePadExit;
        ICommand gamePadPause;
        ICommand gamePadPowerup;
        ICommand gamePadUnleashMinions;
	    private Dictionary<Buttons, ICommand> inputMappings;
        private List<Buttons> pressedButtons = new List<Buttons>();

	    public Gamepad(PlayerCharacter Gru, DespicableGame game)
	    {
            gamePadDown = new DownCommand(Gru);
            gamePadUp = new UpCommand(Gru);
            gamePadRight = new RightCommand(Gru);
            gamePadLeft = new LeftCommand(Gru);
            gamePadPowerup = new PowerupCommand(Gru);
            gamePadUnleashMinions = new UnleashMinionsCommand(Gru);
            gamePadExit = new ExitCommand(game);
            gamePadPause = new PauseCommand(game);
            inputMappings = new Dictionary<Buttons, ICommand>();
	    }

	    public void RegisterCommand(Buttons button, ICommand command)
	    {
            inputMappings.Add(button, command);
	    }

	    public void Update()
	    {
            try
            {
                foreach (Buttons button in pressedButtons)
                {
                    inputMappings[button].Execute(this);
                }
            }
            catch
            {
                //Do nothing, that button is not used...    
            }

            pressedButtons.Clear();
	    }

        public void GetPressedButtons(GamePadState gamePadState)
        {

            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (gamePadState.IsButtonDown(button))
                {
                    pressedButtons.Add(button);
                }
            }
        }

        public void RegisterKeyMapping()
        {
            if (File.Exists("Controls.xml"))
            {
                XmlDocument xd = new System.Xml.XmlDocument();
                xd.Load("Controls.xml");

                XmlNodeList nl = xd.SelectNodes("Table");
                XmlNode root = nl[0];
                foreach (XmlNode xnode in root.ChildNodes)
                {
                    if (xnode.Name == "Command")
                    {
                        foreach (XmlNode subNode in xnode.ChildNodes)
                        {
                            if (subNode.Name == "Action")
                            {
                                RegisterCommand(GetButton(subNode.NextSibling.InnerText), GetCommand(subNode.InnerText));
                            }
                        }
                    }
                }
            }
        }

        protected Buttons GetButton(string value)
        {
            Buttons associatedButton = Buttons.A;
            switch (value)
            {
                case "DPadUp":
                    associatedButton = Buttons.DPadUp;
                    break;
                case "DPadDown":
                    associatedButton = Buttons.DPadDown;
                    break;
                case "DPadLeft":
                    associatedButton = Buttons.DPadLeft;
                    break;
                case "DPadRight":
                    associatedButton = Buttons.DPadRight;
                    break;
                case "Back":
                    associatedButton = Buttons.Back;
                    break;
                case "Start":
                    associatedButton = Buttons.Start;
                    break;
                case "A":
                    associatedButton = Buttons.A;
                    break;
                case "B":
                    associatedButton = Buttons.B;
                    break;
                case "Y":
                    associatedButton = Buttons.Y;
                    break;
                case "X":
                    associatedButton = Buttons.X;
                    break;
                case "RightShoulder":
                    associatedButton = Buttons.RightShoulder;
                    break;
                case "LeftShoulder":
                    associatedButton = Buttons.LeftShoulder;
                    break;
            }
            return associatedButton;
        }

        protected ICommand GetCommand(string value)
        {
            ICommand associatedCommand = null;
            switch (value)
            {
                case "Up":
                    associatedCommand = gamePadUp;
                    break;
                case "Down":
                    associatedCommand = gamePadDown;
                    break;
                case "Left":
                    associatedCommand = gamePadLeft;
                    break;
                case "Right":
                    associatedCommand = gamePadRight;
                    break;
                case "Exit":
                    associatedCommand = gamePadExit;
                    break;
                case "Pause":
                    associatedCommand = gamePadPause;
                    break;
                case "Powerup":
                    associatedCommand = gamePadPowerup;
                    break;
                case "UnleashMinions":
                    associatedCommand = gamePadUnleashMinions;
                    break;
            }
            return associatedCommand;
        }
    }
}
