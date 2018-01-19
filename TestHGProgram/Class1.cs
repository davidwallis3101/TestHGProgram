using System.Net;
using System.Threading;
using Innovative.Geometry;
using Innovative.SolarCalculator;
using Raspberry;
using Raspberry.Timers;
using Raspberry.IO;
using Raspberry.IO.Components.Controllers.Pca9685;
using Raspberry.IO.Components.Controllers.Tlc59711;
using Raspberry.IO.Components.Converters.Mcp3002;
using Raspberry.IO.Components.Converters.Mcp3008;
using Raspberry.IO.Components.Converters.Mcp4822;
using Raspberry.IO.Components.Displays.Hd44780;
using Raspberry.IO.Components.Displays.Ssd1306;
using Raspberry.IO.Components.Displays.Ssd1306.Fonts;
using Raspberry.IO.Components.Displays.Sda5708;
using Raspberry.IO.Components.Expanders.Mcp23017;
using Raspberry.IO.Components.Expanders.Pcf8574;
using Raspberry.IO.Components.Expanders.Mcp23008;
using Raspberry.IO.Components.Leds.GroveBar;
using Raspberry.IO.Components.Leds.GroveRgb;
using Raspberry.IO.Components.Sensors;
using Raspberry.IO.Components.Sensors.Distance.HcSr04;
using Raspberry.IO.Components.Sensors.Pressure.Bmp085;
using Raspberry.IO.Components.Sensors.Temperature.Dht;
using Raspberry.IO.Components.Sensors.Temperature.Tmp36;
using Raspberry.IO.Components.Devices.PiFaceDigital;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.GeneralPurpose.Behaviors;
using Raspberry.IO.GeneralPurpose.Configuration;
using Raspberry.IO.InterIntegratedCircuit;
using Raspberry.IO.SerialPeripheralInterface;

# pragma warning disable 0168 // variable declared but not used.
# pragma warning disable 0219 // variable assigned but not used.
# pragma warning disable 0414 // private field assigned but not used.

using System;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using HomeGenie;
//using HomeGenie.Service;
//using HomeGenie.Service.Logging;
using HomeGenie.Automation;
using HomeGenie.Data;
using MIG;

namespace HomeGenie.Automation.Scripting
{
    [Serializable]
    public class ScriptingInstance : ScriptingHost
    {
        private void RunCode(string PROGRAM_OPTIONS_STRING) // Program Code
        {
            {
                var lights = Modules.InGroup("Porch");
                if (lights.IsOff)
                {
                    lights.On();
                }

                When.SystemStarted(() => {
                    Program.Say("HomeGenie is now ready!");
                    return true;
                });
            }

        }

        #pragma warning disable 0162 //unreachable code 
        private bool EvaluateConditionBlock() // Startup Block
        {
            Program.AddOption(
                "Location", // <-- identifier name of the option
                "autoip", // <-- default value
                "City name", // <-- description
                "text"
            ); // <-- UI field type and parameters
            return false;
        }
        #pragma warning restore 0162 //unreachable code 

        private MethodRunResult Run(string PROGRAM_OPTIONS_STRING)
        {
            Exception ex = null;
            try
            {
                RunCode(PROGRAM_OPTIONS_STRING);
            }
            catch (Exception e)
            {
                ex = e;
            }
            return new MethodRunResult() { Exception = ex, ReturnValue = null };
        }

        private MethodRunResult EvaluateCondition()
        {
            Exception ex = null;
            bool retval = false;
            try
            {
                retval = EvaluateConditionBlock();
            }
            catch (Exception e)
            {
                ex = e;
            }
            return new MethodRunResult() { Exception = ex, ReturnValue = retval };
        }

        public ScriptingHost hg => (ScriptingHost)this;
    }
}
