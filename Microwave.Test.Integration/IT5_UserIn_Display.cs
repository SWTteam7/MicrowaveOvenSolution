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
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
   [TestFixture]
   class IT5_UserIn_Display
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
      }


   }
}
