using Iot.Device.Hcsr501;
using System;
using System.Device.Gpio;
using System.Threading;

namespace Blinky
{
    public class Blinky
    {
        static void Main(string[] args)
        {
            var ledPinNumber = 11;

            var lightTimeInMilliseconds = 200;
            var dimTimeInMilliseconds = 200;

            Console.WriteLine($"Let's blink a LED");
            using (GpioController gpioController = new GpioController(PinNumberingScheme.Board))
            {
                gpioController.OpenPin(ledPinNumber, PinMode.Output);

                Console.WriteLine($"The current Pinout Numbering Schema: {PinNumberingScheme.Board}");
                Console.WriteLine($"GPIO pin enable for use: {ledPinNumber}");

                Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs consoleCancelEventArgs) =>
                {
                    gpioController.Dispose();
                };

                while (true)
                {
                    Console.WriteLine($"Red Lights on for: {lightTimeInMilliseconds}");
                    gpioController.Write(ledPinNumber, PinValue.High);
                    Thread.Sleep(lightTimeInMilliseconds);
                    Console.WriteLine($"Red Lights on for: {dimTimeInMilliseconds}");
                    gpioController.Write(ledPinNumber, PinValue.Low);
                    Thread.Sleep(dimTimeInMilliseconds);
                }
            }
        }
    }
}
