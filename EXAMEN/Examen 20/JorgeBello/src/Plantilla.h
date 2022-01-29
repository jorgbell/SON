#include "fmod.hpp"
#include "fmod_errors.h"
#include "conio.h"
#include <iostream>
#include <string>

using namespace FMOD;
using namespace std;

class Exam20 {
public:
	Exam20() { init(); }

	void init() {
		result = System_Create(&_sys); // Creamos el objeto system
		ERRCHECK(result);
		// 128 canales (numero maximo que podremos utilizar simultaneamente)
		result = _sys->init(128, FMOD_INIT_NORMAL, 0); // Inicializacion de FMOD
		ERRCHECK(result);
		float doppler = 1.0f, rolloff = 1.0f;
		_sys->set3DSettings(doppler, 1.0, rolloff);
		FMOD_VECTOR
			listenerPos = { 0,0,0 }, // posicion del listener
			listenerVel = { 0,0,0 }, // velocidad del listener
			up = { 0,1,0 }, // vector up: hacia la ``coronilla''
			at = { 1,0,0 }; // vector at: hacia donde mira
			// colocamos listener
		_sys->set3DListenerAttributes(0, &listenerPos, &listenerVel, &up, &at);
		_playing = true;



		//PRUEBA
		Sound* sound = loadSound("Battle.wav", FMOD_DEFAULT);
		channel = playSound(sound);
		volume = 0.7;
		TogglePaused(channel);
	}

	void update() {

		//INPUT
		if (_kbhit()) {
			char c = _getch();

			if (c == 'q' || c == 'Q')
				close();
			else if (c == 'V') {
				if (volume < 1.0) {
					volume = volume + 0.1;
					result = channel->setVolume(volume); ERRCHECK(result);
					printf("Volume: %f\n", volume);
				}
			}
			else if (c == 'v') {
				if (volume > 0) {
					volume = volume - 0.1;
					result = channel->setVolume(volume); ERRCHECK(result);
					printf("Volume: %f\n", volume);
				}
			}

		}

		_sys->update();
	}


	void close() {
		result = _sys->release();
		ERRCHECK(result);
		_playing = false;
		//result = sound->release(); ERRCHECK(result);


	}

	bool playing() { return _playing; }
private:
	System* _sys;
	bool _playing;
	FMOD_RESULT result;
	float volume;
	Channel* channel;

	// para salidas de error
	void ERRCHECK(FMOD_RESULT result) {
		if (result != FMOD_OK) {
			std::cout << FMOD_ErrorString(result) << std::endl;
			// printf("FMOD error %d - %s", result, FMOD_ErrorString(result));
			exit(-1);
		}
	}

	Sound* loadSound(const char* filename, FMOD_MODE mode) {
		Sound* sound1;
		result = _sys->createSound(
			filename, // path al archivo de sonido
			mode, // valores (por defecto en este caso: sin loop, 2D)
			0, // informacion adicional (nada en este caso)
			&sound1); // handle al buffer de sonido

		return sound1;
	}

	Channel* playSound(Sound* sound) {
		Channel* c;
		result = _sys->playSound(
			sound, // buffer que se "engancha" a ese canal
			0, // grupo de canales, 0 sin agrupar (agrupado en el master)
			true, // arranca sin "pause" (se reproduce directamente)
			&c); // devuelve el canal que asigna
			// el sonido ya se esta reproduciendo
		channel = c;
		return c;
	}

	void TogglePaused(FMOD::Channel* channel) {
		bool paused;
		channel->getPaused(&paused);
		channel->setPaused(!paused);
	}

};


