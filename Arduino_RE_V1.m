%% Debugging Version
    % clear all
    % close all

    % create arduino object
a = arduino('COM5','Mega2560'); % Right USB
 
sensorPin = 'A0';
ledPin = 'D13';
sensorValue = 0;


k = 0;  %index
v = 0;  %voltage
t = 0;  %time
max_index = 10000;
pause_time = 0.01;
while k < max_index
    k = k + 1;
    sensorValue = readVoltage(a,sensorPin);
    display(sensorValue)
    if sensorValue >= 2
        writeDigitalPin(a, ledPin, 1);
    end
    pause(pause_time);
    writeDigitalPin(a, ledPin, 0);

end