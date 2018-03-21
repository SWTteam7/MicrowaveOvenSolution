using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup all the objects, 
            var door = new Door();
            var buttonpower = new Button();
            var buttontime = new Button();
            var buttonstartcancel = new Button();
            var output = new Output();
            var timer = new Timer();
            var light = new Light(output);
            var display = new Display(output);
            var powertube = new PowerTube(output);
            var cookcontroller = new CookController(timer, display, powertube);
            var userinterface = new UserInterface(buttonpower, buttontime, buttonstartcancel, door, display, light, cookcontroller);

            // Simulate user activities
            door.Open();
            door.Close();
            buttonpower.Press();
            buttonpower.Press();
            buttonpower.Press();
            buttonpower.Press(); //Den kører på 200 W
            buttontime.Press(); //Den kører 1 minut
            buttonstartcancel.Press();

            // Wait while the classes, including the timer, do their job
            //System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
         // test kommentar
        }
    }
}
