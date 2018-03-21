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
    class IT3_CookContr_Timer
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
          _powerTube = new PowerTube(_output);
          _timer = new Timer();
          _display = new Display(_output);
          _uut = new CookController(_timer, _display, _powerTube);
       }

      [Test]
       public void StartCooking_start_correctOutput()
       {
         _uut.StartCooking(50, 2);

          Thread.Sleep(1050);

          _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:01")));

      }

       [Test]
       public void StopCooking_stop_correctOutput()
       {
         _uut.StartCooking(50,10);

         Thread.Sleep(2050);
         _uut.Stop();

         Thread.Sleep(1000);

         _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:08")));
       }

      

   }
}
