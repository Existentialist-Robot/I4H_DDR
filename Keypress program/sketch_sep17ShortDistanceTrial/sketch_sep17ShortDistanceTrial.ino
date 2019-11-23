#include "Adafruit_VL53L0X.h"
#include <Keyboard.h>
#include <Keyboard.h>
#define KEY_UP_ARROW    0xDA
#define KEY_DOWN_ARROW    0xD9
#define KEY_LEFT_ARROW    0xD8
#define KEY_RIGHT_ARROW   0xD7
#define KEY_SPACE       0x20


Adafruit_VL53L0X lox = Adafruit_VL53L0X();
float D;
float Dd;
float D1;
float S;
float Direction= 0; // -ve "down" , +ve "Up"
unsigned long t1;
unsigned long t;
unsigned long interval;

float SAvg= 0;
float SAvgUp = 0; // Up and down
float SAvgDown = 0;
int countUp =0;  // Up and down 
int countDown= 0;

int totalTUp = 0;
int totalTDown= 0;
char highKey = KEY_UP_ARROW;
char lowKey = KEY_DOWN_ARROW;
char slowdownKey = KEY_LEFT_ARROW;
char spaceKey = KEY_SPACE; // close to the sensor with right speed
char rightKey = KEY_RIGHT_ARROW; // close to the sensor with high speed
int incomingByte = 0;
float accceptableRangeUp = 0.10;
float acceptableRangeDown = 0.10;
int delayTime = 25;
int Closedistance = 277;
int Fardistance = 350 ;
float SRefUp = 0;
float SRefDown = 0;
int countAddUp = 2;
int countAddDown= 2;
float threshold = 0.15;


void setup() {
  Serial.begin(115200); 

  // wait until serial port opens for native USB devices
  while (! Serial) {
    delay(1);
  }
  
  Serial.println("Adafruit VL53L0X test");
  if (!lox.begin()) {
    Serial.println(F("Failed to boot VL53L0X"));
    while(1);
  }
  // power 
  Serial.println(F("VL53L0X API Simple Ranging example\n\n")); 
  Keyboard.begin();

  VL53L0X_RangingMeasurementData_t measure; // init range 
  lox.rangingTest(&measure, false); // pass in 'true' to get debug data printout!
  if (measure.RangeStatus != 4) {  // phase failures have incorrect data
    D=(measure.RangeMilliMeter);
  } 
  t1 = millis();
}

void calibrate(){
  Serial.println("Starting calibration.");
  int totalMotions = 0; //speed
  int totalMotionsUp= 0;
  int totalMotionsDown = 0;
  float totalSRef = 0;
  float totalSRefUp = 0;
  float totalSRefDown = 0;// refernce speed
  
  while(totalMotionsUp < 5 && totalMotionsDown < 5){
    VL53L0X_RangingMeasurementData_t measure; // init range 
    lox.rangingTest(&measure, false); // pass in 'true' to get debug data printout!
    if (measure.RangeStatus != 4) {  // phase failures have incorrect data
      D1=(measure.RangeMilliMeter);
    } 
    Dd = abs(D-D1);
    t = millis();
    Direction = D1-D;
    interval=(t-t1);
    S= (Dd /interval);
    D=D1;
    t1 = t;

    if (S> threshold && Direction > 0) {
      SAvgUp = SAvgUp +Dd;
      totalTUp = totalTUp + interval ; 
      countUp++;// to keep adding until one movement stops
    } else if (S<= threshold && countUp> countAddUp ) {
        SAvgUp= (SAvgUp/totalTUp);
        if(D1 >= Fardistance) {
          totalSRefUp += SAvgUp;
          totalMotionsUp++;
          countUp =0;
          SAvgUp = 0;
          totalTUp = 0;
          Keyboard.press(spaceKey);
          delay(2);
          Keyboard.release(spaceKey);
          Serial.println(D1);
          Serial.println(S);
          Serial.println("motion complete UP");
        }
    } else if (S> threshold && Direction < 0) {
         SAvgDown = SAvgDown + Dd;
         totalTDown = totalTDown + interval ; 
      countDown++;// to keep adding speed until one movement stops
    } else if (S<= threshold && countDown > countAddDown ) {
        SAvgDown= (SAvgDown/totalTDown);
        if(D1 <= Closedistance){
          totalSRefDown += SAvgDown;
          totalMotionsDown++;
          countDown =0;
          SAvgDown = 0;
          totalTDown = 0;
          Keyboard.press(rightKey);
          delay(2);
          Keyboard.release(rightKey);
          Serial.println(D1);
          Serial.println(S);
          Serial.println("motion complete DOWN ");
        }
    }
    delay(delayTime);
  }
  SRefUp = totalSRefUp/totalMotionsUp ;
  SRefDown = totalSRefDown/totalMotionsDown;
  Serial.println("Calibration done.");
  Serial.println(SRefUp);
  Serial.println(SRefDown);
}
  

void loop() { 

  VL53L0X_RangingMeasurementData_t measure; // init range 
  lox.rangingTest(&measure, false); // pass in 'true' to get debug data printout!
  if (measure.RangeStatus != 4) {  // phase failures have incorrect data
    D1=(measure.RangeMilliMeter);
  }
    Dd = abs(D-D1);
    t = millis();
    interval=(t-t1);
    S= (Dd /interval);
    Direction = D1-D;
    D=D1;
    t1 = t;
 
    if (S>threshold && Direction > 0 ) {
      SAvgUp = SAvgUp +Dd;
      totalTUp = totalTUp + interval ; 
      countUp++;// to keep adding speed until one movement stops

      //Serial.print(D1); Serial.print("........");Serial.print(count); Serial.print("........."); Serial.println(S); 
      //Serial.println((SAvg/totalT));
     
    } else if (S<=threshold && countUp> countAddUp) {
        SAvgUp= (SAvgUp/totalTUp);
            if (D1 >= Fardistance && SAvgUp >SRefUp + accceptableRangeUp){  //decide the +5 range during testing
              Serial.println( "high jump") ;
              Keyboard.press(highKey);
              delay(2);
              Keyboard.release(highKey);
              Serial.print("speed");
              Serial.println(SAvgUp);
              countUp =0;
              SAvgUp = 0;
              totalTUp = 0;
              
            } else if (D1>= Fardistance && SAvgUp < SRefUp - accceptableRangeUp){ //decide the -5 range during testing
              Serial.println( "low jump") ;
              Keyboard.press(lowKey);
              delay(2);
              Keyboard.release(lowKey);
              Serial.print("speed");
              Serial.println(SAvgUp);
              countUp =0;
              SAvgUp = 0;
              totalTUp = 0;
            } else if (D1>= Fardistance && SAvgUp >=SRefUp - 0.1 && SAvgUp <= SRefUp + accceptableRangeUp) {
              Serial.println( " jump") ;
              Keyboard.press(spaceKey);
              delay(2);
              Keyboard.release(spaceKey);
              Serial.print("speed");
              Serial.println(SAvgUp);
              countUp =0;
              SAvgUp = 0;
              totalTUp = 0;
            }
    } else if (S>threshold && Direction < 0) {
         SAvgDown = SAvgDown + Dd;
         totalTDown = totalTDown + interval ; 
      countDown++; // to keep adding speed until one movement stops
      
    } else if (S<=threshold && countDown > countAddDown) {
        SAvgDown= (SAvgDown/totalTDown);
            if (D1<=Closedistance && SAvgDown > SRefDown + acceptableRangeDown){  //decide the +5 range during testing
              Serial.println( "voice to slow down") ;
              Keyboard.press(slowdownKey);
              delay(2);
              Keyboard.release(slowdownKey);
              Serial.print("speed");
              Serial.println(SAvgDown);
              countDown =0;
              SAvgDown = 0;
              totalTDown = 0;
              
            } else if (D1<= Closedistance  && SAvgDown <= SRefDown + acceptableRangeDown) {
              Serial.println( " voice Great job ") ;
              Keyboard.press(rightKey);
              delay(2);
              Keyboard.release(rightKey);
              Serial.print("speed");
              Serial.println(SAvgDown);
              countDown =0;
              SAvgDown = 0;
              totalTDown = 0;
            }
      }
      if (Serial.available()>0){
      incomingByte= Serial.read();
      Serial.println("Read something");
      Serial.println(incomingByte);
      if (incomingByte==48){
        calibrate();
    } //  call calibrate  
      }
    delay(delayTime);
}
