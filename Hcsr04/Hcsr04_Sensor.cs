using System;
using System.Device.Gpio;
using System.Threading;
using Iot.Device.Hcsr04;
using UnitsNet;

namespace Hcsr04_Sensor
{
    class Hcsr04_Sensor
    {
        static void Main(string[] args)
        {
            int ledPinNumber = 13;

            Console.WriteLine("Hello Hcsr04 Sample!");

            using (Hcsr04 sonar = new Hcsr04(4, 17))
            {
                using (GpioController gpioController = new GpioController(PinNumberingScheme.Board))
                {
                    gpioController.OpenPin(ledPinNumber, PinMode.Output);

                    Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs consoleCancelEventArgs) =>
                    {
                        sonar.Dispose();
                        gpioController.Dispose();
                    };

                    while (true)
                    {
                        if (sonar.TryGetDistance(out Length distance))
                        {
                            Console.WriteLine($"Distance: {distance.Centimeters} cm");

                            if (distance.Centimeters < 15)
                            {
                                gpioController.Write(ledPinNumber, PinValue.High);
                            }

                            if (distance.Centimeters >= 15)
                            {                                
                                gpioController.Write(ledPinNumber, PinValue.Low);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error reading sensor");
                        }

                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
