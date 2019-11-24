#include <Adafruit_NeoPixel.h>
#include <Keyboard.h>

#define KEY_V       0x76
#define KEY_B       0x62
#define KEY_N       0x6E
#define KEY_H       0x68
#define KEY_G       0x67
#define KEY_F       0x66
#define KEY_Y       0x79
#define KEY_T       0x74
#define KEY_R       0x72

#define LED_PIN    6
#define LED_COUNT 4
Adafruit_NeoPixel strip(LED_COUNT, LED_PIN, NEO_GRB + NEO_KHZ800);

char padKey1 = KEY_N;
char padKey2 = KEY_B; 
char padKey3 = KEY_N; 
char padKey4 = KEY_H; 
char padKey5 = KEY_G; 
char padKey6 = KEY_F; 
char padKey7 = KEY_Y; 
char padKey8 = KEY_T; 
char padKey9 = KEY_R; 

int fsrPin1 = 0;     // the FSR and 10K pulldown are connected to a0
int fsrReading1;     // the analog reading from the FSR resistor divider
int fsrPin2 = 1;     // the FSR and 10K pulldown are connected to a0
int fsrReading2;     // the analog reading from the FSR resistor divider
int fsrPin3 = 2;     // the FSR and 10K pulldown are connected to a0
int fsrReading3;     // the analog reading from the FSR resistor divider
int fsrPin4 = 3;     // the FSR and 10K pulldown are connected to a0
int fsrReading4;     // the analog reading from the FSR resistor divider
int fsrPin5 = 4;     // the FSR and 10K pulldown are connected to a0
int fsrReading5;     // the analog reading from the FSR resistor divider
int fsrPin6 = 5;     // the FSR and 10K pulldown are connected to a0
int fsrReading6;     // the analog reading from the FSR resistor divider
int fsrPin7 = 6;     // the FSR and 10K pulldown are connected to a0
int fsrReading7;     // the analog reading from the FSR resistor divider
int fsrPin8 = 7;     // the FSR and 10K pulldown are connected to a0
int fsrReading8;     // the analog reading from the FSR resistor divider
int fsrPin9 = 8;     // the FSR and 10K pulldown are connected to a0
int fsrReading9;     // the analog reading from the FSR resistor divider

int thresh = 50;
int change = 0;

int gameState = 1; // 1 == idle, 2 == in play
int val = 0;
int delayVal = 500;

uint32_t black = strip.Color(0, 0 ,0);
uint32_t white = strip.Color(200, 200 ,200);
// colours 1-3
uint32_t colour_1a = strip.Color(100, 0 ,100);
uint32_t colour_1b = strip.Color(200, 0 ,200);
uint32_t colour_2a = strip.Color(100, 0 ,0);
uint32_t colour_2b = strip.Color(200, 0 ,0);
uint32_t colour_3a = strip.Color(0, 0 ,100);
uint32_t colour_3b = strip.Color(0, 0 ,200);
// colour 4-6
uint32_t colour_4a = strip.Color(0, 100,0);
uint32_t colour_4b = strip.Color(0, 200,0);
uint32_t colour_5a = strip.Color(100, 0 ,100);
uint32_t colour_5b = strip.Color(200, 0 ,200);
uint32_t colour_6a = strip.Color(100, 0 ,0);
uint32_t colour_6b = strip.Color(200, 0 ,0);
// colours 7-9
uint32_t colour_7a = strip.Color(0, 0 ,100);
uint32_t colour_7b = strip.Color(0, 0 ,200);
uint32_t colour_8a = strip.Color(0, 100,0);
uint32_t colour_8b = strip.Color(0, 200,0);
uint32_t colour_9a = strip.Color(100, 0 ,100);
uint32_t colour_9b = strip.Color(200, 0 ,200);


/*
int gameState = 1; // 1 == off, 2 == loading, 3 == in play
int LEDpin = 13;      // All LEDs to pin 13 (PWM pin)
int padReading[] = {0,0,0,0,0,0,0,0,0}    // 
int padAnalogPin[] = {0,1,2,3,4,5,6,7,8}  // physical pin of each pad
int padStates[] = {0,0,0,0,0,0,0,0,0}     // where a pad is being use in game
int padNum[] = 9                          // number of different pads
int padLEDstart = {0,20,40,60,80,100,120,140,160}
int padLEDstop = {19,39,59,79,99,119,139,159,179}
int touchedPads{0,0,0,0,0,0,0,0,0}
 */
int activePads[] = {0,0,0,0,0,0,0,0,0}    // where a pad has been touched
int padLEDstart = {0,20,40,60,80,100,120,140,160}
//int padLEDstop = {19,39,59,79,99,119,139,159,179}
int LEDppad = 20

void setup() {
  // We'll send debugging information via the Serial monitor
  Serial.begin(9600); 
  strip.begin();           // INITIALIZE NeoPixel strip object (REQUIRED)
  strip.show();            // Turn OFF all pixels ASAP
  strip.setBrightness(60); // Set BRIGHTNESS to about 1/5 (max = 255)
}

void loop() {
  if (gameState == 1){  
    strip.clear();
//    idleLED();
    for(int i=0; i<LED_COUNT; i++) { // For each pixel...
      strip.setPixelColor(i, white);
      strip.show(); 
      delay(delayVal);
    }
    if(Serial.available() > 0){
      val = Serial.read();         // read it and store it in 'val'
      if( val == '9' ){
        gameState = 2;        // if '9' was received start the game
        strip.clear()
        strip.show();
      }
    }
  }
   else if (gameState == 2) { 
    if(Serial.available() > 0){
      val = Serial.read();         // read it and store it in 'val'
      if( val == '0' ){
        activePads[0] = 1;
      }
      else if( val == '1' ){
        activePads[1] = 1;
      }
      else if( val == '2' ){
        activePads[2] = 1;
      }
      else if( val == '3' ){
        activePads[3] = 1;
      }      
      else if( val == '4' ){
        activePads[4] = 1;
      }
      else if( val == '5' ){
        activePads[5] = 1;
      }
      else if( val == '6' ){
        activePads[6] = 1;
      }
      else if( val == '7' ){
        activePads[7] = 1;
      }
      else if( val == '8' ){
        activePads[8] = 1;
      }
      else if( val == '9' ){
         gameState = 3;
        }
      }
   }

 
  else if (gameState == 3) { 
    // Pad #1
    if (activePads[0] = 1){
      fsrReading1 = analogRead(fsrPin1);
      if (fsrReading1 > thresh) {
        Serial.println("Pressure Sensor 1 Touched");
        strip.fill(colour_1b, padLEDstart[0], LEDppad);
        Keyboard.press(padKey1);
      }
      else{
        strip.fill(colour_1a, padLEDstart[0], LEDppad);
      }
      strip.show();
      Serial.print("Analog reading 1 = ");
      Serial.println(fsrReading1);
    }
    // Pad #2
      if (activePads[1] = 1){
        fsrReading2 = analogRead(fsrPin2);
        if (fsrReading2 > thresh) {
          Serial.println("Pressure Sensor 2 Touched");
          strip.fill(colour_2b, padLEDstart[1], LEDppad);
          Keyboard.press(padKey2);
        }
        else{
          strip.fill(colour_2a, padLEDstart[1], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 1 = ");
        Serial.println(fsrReading2);  
      }
    // Pad #3
      if (activePads[2] = 1){
        fsrReading3 = analogRead(fsrPin3);
        if (fsrReading3 > thresh) {
          Serial.println("Pressure Sensor 3 Touched");
          strip.fill(colour_3b, padLEDstart[2], LEDppad);
          Keyboard.press(padKey3);
        }
        else{
          strip.fill(colour_3a, padLEDstart[2], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 3 = ");
        Serial.println(fsrReading3); 
      }
    // Pad #4
      if (activePads[3] = 1){
        fsrReading4 = analogRead(fsrPin4);
        if (fsrReading4 > thresh) {
          Serial.println("Pressure Sensor 2 Touched");
          strip.fill(colour_4b, padLEDstart[3], LEDppad);
          Keyboard.press(padKey4);
        }
        else{
          strip.fill(colour_4a, padLEDstart[3], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 4 = ");
        Serial.println(fsrReading4);  
      }
    // Pad #5
      if (activePads[4] = 1){
        fsrReading5 = analogRead(fsrPin5);
        if (fsrReading5 > thresh) {
          Serial.println("Pressure Sensor 2 Touched");
          strip.fill(colour_5b, padLEDstart[4], LEDppad);
          Keyboard.press(padKey5);
        }
        else{
          strip.fill(colour_5a, padLEDstart[4], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 5 = ");
        Serial.println(fsrReading5);  
      }
    // Pad #6
      if (activePads[5] = 1){
        fsrReading6 = analogRead(fsrPin6);
        if (fsrReading6 > thresh) {
          Serial.println("Pressure Sensor 2 Touched");
          strip.fill(colour_6b, padLEDstart[5], LEDppad);
          Keyboard.press(padKey6);
        }
        else{
          strip.fill(colour_6a, padLEDstart[5], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 6 = ");
        Serial.println(fsrReading6);  
      }
    // Pad #7
      if (activePads[6] = 1){
        fsrReading7 = analogRead(fsrPin7);
        if (fsrReading7 > thresh) {
          Serial.println("Pressure Sensor 2 Touched");
          strip.fill(colour_7b, padLEDstart[6], LEDppad);
          Keyboard.press(padKey7);
        }
        else{
          strip.fill(colour_7a, padLEDstart[6], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 7 = ");
        Serial.println(fsrReading7);  
      }
    // Pad #8
      if (activePads[7] = 1){
        fsrReading8 = analogRead(fsrPin8);
        if (fsrReading8 > thresh) {
          Serial.println("Pressure Sensor 2 Touched");
          strip.fill(colour_8b, padLEDstart[7], LEDppad);
          Keyboard.press(padKey8);
        }
        else{
          strip.fill(colour_8a, padLEDstart[7], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 8 = ");
        Serial.println(fsrReading8);  
      }
    // Pad #8
      if (activePads[8] = 1){
        fsrReading9 = analogRead(fsrPin9);
        if (fsrReading9 > thresh) {
          Serial.println("Pressure Sensor 2 Touched");
          strip.fill(colour_9b, padLEDstart[8], LEDppad);
          Keyboard.press(padKey9);
        }
        else{
          strip.fill(colour_9a, padLEDstart[8], LEDppad);
        }
        strip.show();
        Serial.print("Analog reading 9 = ");
        Serial.println(fsrReading9);  
      }

    Keyboard.release(padKey1);         
    Keyboard.release(padKey2); 
    Keyboard.release(padKey3); 
    Keyboard.release(padKey4);         
    Keyboard.release(padKey5); 
    Keyboard.release(padKey6);
    Keyboard.release(padKey7);         
    Keyboard.release(padKey8); 
    Keyboard.release(padKey9); 
   
    Serial.print(fsrReading1);
    Serial.print(fsrReading2);
    Serial.print(fsrReading3);
    Serial.print(fsrReading4);
    Serial.print(fsrReading5);
    Serial.print(fsrReading6);
    Serial.print(fsrReading7);
    Serial.print(fsrReading8);
    Serial.print(fsrReading9);

    delay(10); 
  }
}
