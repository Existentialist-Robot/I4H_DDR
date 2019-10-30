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

while k < max_index
    k = k + 1;
    sensorValue = readVoltage(a,sensorPin);
    writeDigitalPin(a, ledPin, 1);
    display(sensorValue)
    pause(sensorValue);
    writeDigitalPin(a, ledPin, 0);
    pause(sensorValue);
end