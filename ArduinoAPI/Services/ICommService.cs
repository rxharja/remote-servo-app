using System;
namespace ArduinoAPI.Services
{
    public interface ICommService
    {
      T Read<T>();

      bool Write(int value);
    }
}
