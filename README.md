# remote-servo-app
A small proof-of-concept application that allows a user to control the angle of a servo motor through a web UI remotely over a local network.

Front-End
A tiny react app containing a single knob which upon moving sends an message to servo motor

RESTful API
an ASP.NET RESTful api containing the necessary controllers to abstract the communication between the serial port listening Arduino and the web UI.

Arduino Code
Arduino logic that listens for inputs from the serial port and sets the value for the servo motor, adjusting its angle.

At a high level:
![Component and Connector Diagram of High Level Diagram](https://github.com/rxharja/remote-servo-app/blob/master/Images/c_and_c_diagram.png)
