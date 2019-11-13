%% Debugging Version
    % clear all
    % close all

    % create arduino object
a = arduino('COM7','Mega2560'); % Right USB
 
sensorPin = 'A0';
ledPin = 'D13';
sensorValue = 0;

k = 0;  %index
v = 0;  %voltage
t = 0;  %time
max_index = 10000;

s_pause = 0.1;
l_pause = 0.2;

writeDigitalPin(a, ledPin, 0)

while k < max_index
    k = k + 1;
    sensorValue = readVoltage(a,sensorPin);
    if sensorValue > 2.5
        writeDigitalPin(a, ledPin, 1);
        if sensorValue > 3
            pause(l_pause);
        else
            pause(s_pause);
        end
        writeDigitalPin(a, ledPin, 0);
    end
    display(sensorValue)
end