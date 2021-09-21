using System;
using System.IO;
using System.IO.Ports;
using ArduinoAPI.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArduinoAPI.Services
{
    public class SerialService : ICommService, IDisposable
    {
        private readonly ILogger<SerialService> _logger;

        private readonly SerialPort _serialPort;

        private readonly PortConfiguration _portConfig;

        public SerialService(ILogger<SerialService> logger, IOptions<PortConfiguration> options)
        {
            _logger = logger;

            _portConfig = options.Value;

            _serialPort = new SerialPort(_portConfig.PortName, _portConfig.BaudRate);

			TryOpen();
        }

        public void Dispose()
        {
		    TryClose();

		    _serialPort.Dispose();
        }

        public T Read<T>()
        {
			string output = _serialPort.ReadLine();

			try
			{
			    return (T)(object)output;
			}
			catch (InvalidCastException)
			{
			    _logger.LogError("Invalid Casting Operation");
			}

            return default;
        }

        public bool Write(int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            try
            {
                _serialPort.Write(buffer, 0, 1);
                _logger.LogInformation("Successfully written to port");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"An Invalid Operation was caught: {e.Message}");
                return false;
            }

            return true;
        }

		private void TryOpen() {
			if (!_serialPort.IsOpen) {
				try 
				{
				    _serialPort.Open();
				    _logger.LogInformation("Serial Port Opened");
			    }
				catch (UnauthorizedAccessException e)
				{
		           _logger.LogError($"An unauthorized access exception was caught: {e.Message}");
			    }
				catch (IOException e) 
				{
					_logger.LogError($"IO Exception was caught, ensure the port is supported: {e.Message}");
				}
			}
	    }

		private void TryClose() 
		{
			if (_serialPort.IsOpen) {
				try 
				{
				    _serialPort.Close();
				    _logger.LogInformation("Serial Port Closed");
			    }
				catch (UnauthorizedAccessException e)
				{
		           _logger.LogError($"An unauthorized access exception was caught: {e.Message}");
			    }
			}
	    }

    }
}
