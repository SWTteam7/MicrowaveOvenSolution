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
using NUnit.Framework.Internal;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT7_UserIn_Display
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
        private ILight _light;
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
        public void OnPowerPressed_onePress_correctOutput()
        {
            _buttonpower.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));

        }

        [Test]
        public void OnPowerPressed_twoPress_correctOutput()
        {
            _buttonpower.Press();
            _buttonpower.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("100 W")));

        }

        [Test]
        public void OnPowerPressed_fourteenPress_correctOutput()
        {
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("700 W")));
        }

        [Test]
        public void OnPowerPressed_fifteenPress_correctOutput()
        {
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();
            _buttonpower.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
        }

        [Test]
        public void OnTimePressed_onePress_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void OnTimePressed_twoPress_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttontime.Press();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("02:00")));
        }

        [Test]
        public void OnStartCancelPresses_press_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttonstartcan.Press();
            Thread.Sleep(1050); //Disse to linjer er ikke nødvendige - skal vi tage dem med? (man canceller før man starter)
            _buttonstartcan.Press(); // (eller også clearer den bare automatisk før den starter med at tælle ned?)

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

        [Test]
        public void OnDoorOpened_openDoor_correctOutput()
        {
            _buttonpower.Press();
            _door.Open();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }

        [Test]
        public void CookingIsDone_doneCooking_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttonstartcan.Press();
            Thread.Sleep(60050);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }
    }
}
