﻿using System;
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

namespace Microwave.Test.Integration
{
   [TestFixture] 
   class IT5_Button_UserIn
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
            _light = Substitute.For<ILight>();
            _buttonpower = new Button();
            _buttontime = new Button();
            _buttonstartcan = new Button();
            _door = new Door();
            _userinterface = new UserInterface(_buttonpower, _buttontime, _buttonstartcan, _door, _display, _light, _cookcontroller);
        }

        [Test]
        public void Press_onePressOnPower_correctOutput() 
        {
            _buttonpower.Press();

            _display.Received(1).ShowPower(50);
        }

        [Test]
        public void Press_twoPressOnPower_correctOutput()
        {
            _buttonpower.Press();
            _buttonpower.Press();

            _display.Received(1).ShowPower(100);
        }

        [Test]
        public void Press_fourteenPressOnPower_correctOutput()
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

            _display.Received(1).ShowPower(700);
        }

        [Test]
        public void Press_fifteenPressOnPower_correctOutput()
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

            _display.Received().ShowPower(50);
        }

        [Test]
        public void Press_onePressOnTime_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();

            _display.Received(1).ShowTime(01, 00);
        }

        [Test]
        public void Press_twoPressOnTime_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttontime.Press();

            _display.Received(1).ShowTime(02, 00);
        }

        [Test]
        public void Press_pressOnStartCancel_correctOutput()
        {
            _buttonpower.Press();
            _buttontime.Press();
            _buttonstartcan.Press();
            Thread.Sleep(1050);
            _buttonstartcan.Press();

            _display.Received().Clear();
        }
    }
}
