#include "Plantilla.h"

int main(int argc, char* argv[]) {
	Plantilla ejer = Plantilla();
	
	while (ejer.playing()) {
		ejer.update();
	}
	return 0;
}