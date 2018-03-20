using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework.Internal;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
   [TestFixture] //FÆRDIG
   class IT1_CookContr_Power
   {
      private ITimer _timer;
      private CookController _uut;
      private IPowerTube _powerTube;
      private IDisplay _display;
      private IOutput _output;

      [SetUp]
      public void SetUp()
      {
         _output = Substitute.For<IOutput>();
         _powerTube=new PowerTube(_output);
         _timer = new Timer();
         _display = new Display(_output);
         _uut = new CookController(_timer, _display, _powerTube);
      }

      [Test]
      public void StartCooking_50_output()
      {
         _uut.StartCooking(50,100);

         _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("50 %")));
      }

      [Test]
      public void StartCooking_minus1_exception()
      {
         
         Assert.Throws<ArgumentOutOfRangeException>(() => _uut.StartCooking(-1,100));

      }

      [Test]
      public void StartCooking_101_exception()
      {

         Assert.Throws<ArgumentOutOfRangeException>(() => _uut.StartCooking(101, 100));

      }

      [Test]
      public void StartCooking_isAlreadyOn_exception()
      {
         _uut.StartCooking(50,100);

         Assert.Throws<ApplicationException>(() => _uut.StartCooking(60, 100));
      }


      [Test]
      public void Stop_isOn_output()
      {
         _uut.StartCooking(50,100);
         _uut.Stop();

         _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
      }


      [Test]
      public void OnTimerExpired_turnOff_output()
      {
         _uut.StartCooking(50,1);

         Thread.Sleep(1050);

         _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
      }
   }
}
