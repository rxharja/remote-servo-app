#include <Servo.h>
Servo myservo;
int pos = 0;

void setup()
{
        Serial.begin(9600);
        while (!Serial);
        myservo.attach(9);
        for(pos = 0; pos <= 180; pos += 1)
                myservo.write(0);
                delay(1000);
                myservo.write(180);
                delay(1000);
                myservo.write(90);
                delay(1000);
}

void loop() {
        if (Serial.available())
        {
                int state = Serial.read();
                Serial.print("value: ");
                Serial.println(state);
                myservo.write(state);
        }
}
