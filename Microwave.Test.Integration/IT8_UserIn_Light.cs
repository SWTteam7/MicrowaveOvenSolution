using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
   class IT8_UserIn_Light
   {
        private ITimer _timer;
        private CookController _cookController;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private IOutput _output;
        private IUserInterface _uut;
        private IButton _buttonpower;
        private IButton _buttontime;
        private IButton _buttonstartcan;
        private Light _light;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _buttonpower = new Button();
            _buttontime = new Button();
            _buttonstartcan = new Button();
            _light = new Light(_output);
            _door = new Door();

            _uut = new UserInterface(_buttonpower, _buttontime, _buttonstartcan, _door, _display, _light, _cookController);
            _cookController.UI = _uut;
        }

        [Test]
        public void OnStartCancelPressed_lightWasOff_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttonstartcan.Press();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void OnStartCancelPressed_lightWasOn_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttonstartcan.Press();
            Thread.Sleep(1050);
            _buttonstartcan.Press(); 

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void OnDoorOpened_lightWasOff_correctOutput()
        {
            _door.Open();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void OnDoorClosed_lightWasOn_correctOutput()
        {
            _door.Open();
            _door.Close();

            _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void CookingIsDone_lightWasOn_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttonstartcan.Press();
            Thread.Sleep(60500);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }
    }
}
