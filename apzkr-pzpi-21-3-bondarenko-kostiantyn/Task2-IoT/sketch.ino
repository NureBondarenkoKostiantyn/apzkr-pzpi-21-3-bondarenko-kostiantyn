#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define SCREEN_WIDTH 128 // Ширина дисплея OLED (в пікселях)
#define SCREEN_HEIGHT 64 // Висота дисплея OLED (в пікселях)

// Оголошення дисплея SSD1306, підключеного до I2C (піни SDA, SCL)
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, -1);

const int MPU_ADDRESS = 0x68; // Адреса MPU6050 по I2C
float AccX, AccY, AccZ;
float GyroX, GyroY, GyroZ;
float accAngleX, accAngleY, gyroAngleX, gyroAngleY, gyroAngleZ;
float roll, pitch, yaw;
float elapsedTime, currentTime, previousTime;

void setup() {
  Wire.begin();                      // Ініціалізація зв'язку
  Wire.beginTransmission(MPU_ADDRESS);       // Початок зв'язку з MPU6050
  Wire.write(0x6B);                  // Розмова з регістром 6B
  Wire.write(0x00);                  // Скидання - записуємо 0 в регістр 6B
  Wire.endTransmission(true);        // Завершення передачі
  delay(20);

  // SSD1306_SWITCHCAPVCC = генерування напруги дисплея з 3.3V внутрішньо
  if(!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    for(;;);
  }
  display.display();
  delay(2000);
  display.clearDisplay();
  display.setTextSize(1);      // Звичайний масштаб 1:1 пікселів
  display.setTextColor(SSD1306_WHITE); // Білий колір тексту
  display.setCursor(0, 0);     // Початок в лівому верхньому куті
}

void loop() {
  // Зчитування даних з акселерометра
  Wire.beginTransmission(MPU_ADDRESS);
  Wire.write(0x3B); // Починаємо з регістра 0x3B (ACCEL_XOUT_H)
  Wire.endTransmission(false);
  Wire.requestFrom(MPU_ADDRESS, 6, true); // Зчитуємо 6 регістрів, значення кожної вісі зберігається в 2 регістрах

  AccX = (Wire.read() << 8 | Wire.read()) / 16384.0; // Значення по осі X
  AccY = (Wire.read() << 8 | Wire.read()) / 16384.0; // Значення по осі Y
  AccZ = (Wire.read() << 8 | Wire.read()) / 16384.0; // Значення по осі Z

  // Обчислення кутів нахилу за допомогою акселерометра
  accAngleX = (atan(AccY / sqrt(pow(AccX, 2) + pow(AccZ, 2))) * 180 / PI); 
  accAngleY = (atan(-1 * AccX / sqrt(pow(AccY, 2) + pow(AccZ, 2))) * 180 / PI); 

  // Зберігання попереднього часу перед зчитуванням поточного
  previousTime = currentTime;
  currentTime = millis(); // Зчитування поточного часу
  elapsedTime = (currentTime - previousTime) / 1000; // Перетворення мілісекунд в секунди

  // Зчитування даних з гіроскопа
  Wire.beginTransmission(MPU_ADDRESS);
  Wire.write(0x43); // Початок даних з гіроскопа - адреса першого регістра 0x43
  Wire.endTransmission(false);
  Wire.requestFrom(MPU_ADDRESS, 6, true); // Зчитуємо 6 регістрів, значення кожної вісі зберігається в 2 регістрах

  GyroX = (Wire.read() << 8 | Wire.read()) / 131.0; // Значення гіроскопа по осі X
  GyroY = (Wire.read() << 8 | Wire.read()) / 131.0; // Значення гіроскопа по осі Y
  GyroZ = (Wire.read() << 8 | Wire.read()) / 131.0; // Значення гіроскопа по осі Z

  // Оновлення кутів нахилу за допомогою гіроскопа
  gyroAngleX = gyroAngleX + GyroX * elapsedTime; // deg/s * s = deg
  gyroAngleY = gyroAngleY + GyroY * elapsedTime;
  yaw =  yaw + GyroZ * elapsedTime;

  // Об'єднання даних акселерометра та гіроскопа для отримання кінцевих значень кутів нахилу
  // Значення кутів нахилу обчислюються, приділяючи 96% ваги значенням гіроскопа та 4% - значенням акселерометра
  roll = 0.96 * gyroAngleX + 0.04 * accAngleX; // Кут нахилу в плоскості X
  pitch = 0.96 * gyroAngleY + 0.04 * accAngleY; // Кут нахилу в плоскості Y

  // Відображення значень на OLED дисплеї
  display.clearDisplay();

  display.setCursor(0, 0);
  display.print("Roll: ");
  display.print(roll);

  display.setCursor(0, 10);
  display.print("Pitch: ");
  display.print(pitch);

  display.setCursor(0, 20);
  display.print("Yaw: ");
  display.print(yaw);

  display.display();

  delay(100);
}
