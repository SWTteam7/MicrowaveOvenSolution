using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
   [TestFixture] 
   class IT4_Door_UserIn
   {
        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powertube;
        private ITimer _timer;
        private ICookController _cookcontroller;
        private ILight _light;
        private IButton _buttonpower;
        private IButton _buttontime;
        private IButton _buttonstartcan;
        private IDoor _door;
        private IUserInterface _userinterface;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _display = Substitute.For<IDisplay>();
            _powertube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();
            _cookcontroller = Substitute.For<ICookController>();
            _light= Substitute.For<ILight>();
            _buttonpower = new Button();
            _buttontime = new Button();
            _buttonstartcan = new Button();
            _door = new Door();
            _userinterface = new UserInterface(_buttonpower, _buttontime, _buttonstartcan, _door, _display, _light, _cookcontroller);
        }

        [Test]
        public void Open_openDoor_lightIsTurnedOn()
        {
            _door.Open();
            _light.Received(1).TurnOn();
        }

        [Test]
        public void Close_closeDoor_lightIsTurnedOff()
        {
            _door.Open();
            _door.Close();
            _light.Received(1).TurnOff();
        }
    }
}
