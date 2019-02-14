
int gLed = 10;
char myCol[20];

void setup() {  
   Serial.begin (9600);  
   pinMode(gLed, OUTPUT);   
   digitalWrite(gLed, LOW);

 
}


void loop() {
  int lf = 10;
  Serial.readBytesUntil(lf, myCol, 1);
  if(strcmp(myCol,"y")==0){
  
       digitalWrite(gLed, LOW); 
   }
  if(strcmp(myCol,"g")==0){

       digitalWrite(gLed, HIGH); 
   }
}

