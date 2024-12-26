#include <stdio.h>
#include <math.h>

int main() {
  int number = 127;
  int bit3 = ffs(number) - 1; // 3 - 1 = 2

  printf("bit3 = %d\n", bit3);

  return 0;
}
    


