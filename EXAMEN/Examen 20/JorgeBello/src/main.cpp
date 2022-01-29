#include "fmod.hpp"
#include "fmod_errors.h"
#include "conio.h"
#include <iostream>
#include <string>

using namespace FMOD;
using namespace std;

class Exam20 {
public:
	Exam20() { init(); part1(); }

	void init() {
		result = System_Create(&_sys); // Creamos el objeto system
		ERRCHECK(result);
		// 128 canales (numero maximo que podremos utilizar simultaneamente)
		result = _sys->init(128, FMOD_INIT_NORMAL, 0); // Inicializacion de FMOD
		ERRCHECK(result);
		float doppler = 1.0f, rolloff = 1.0f;
		_sys->set3DSettings(doppler, 1.0, rolloff);

		_sys->createChannelGroup("grupo1", &game);
		_playing = true;

	}


	void part1() {
		//PRUEBA
		Sound* sound = loadSound("musica.ogg", FMOD_DEFAULT);
		Channel*  channel = playSound(sound);
		// se repite indefinidamente
		result = channel->setLoopCount(-1); ERRCHECK(result);
		volume = 0.1;
		result = channel->setVolume(volume); ERRCHECK(result);
		result = channel->setChannelGroup(game); ERRCHECK(result);
		TogglePaused(channel);

		part2();
	}

	void part2() {
		/*Situar el listener en la posición(0, 0, 0), orientado en posición vertical y mirando
			hacia adelante.Cargar la muestra de latido.wav en loop 3D y asociarla a un canal
			emisor latido.Situar dicho emisor en la posición(0, 0, 4) y reproducir la muestra en
			loop.Para ello implementaremos un bucle principal que mantiene la reproducción del
			canal hasta detectar la pulsación ’q’, que terminará dicho bucle(también terminará
				la reproducción del canal música).*/

		_sys->set3DListenerAttributes(0, &listenerPos, &listenerVel, &up, &at);

		Sound* sound = loadSound("latido.wav", (FMOD_3D | FMOD_LOOP_NORMAL));
		emmiterChannel = playSound(sound);

		result = emmiterChannel->set3DAttributes(&emmiterPos, &emmiterVel); ERRCHECK(result);
		result = emmiterChannel->setChannelGroup(game); ERRCHECK(result);
		TogglePaused(emmiterChannel);

		changeUI(); //primer visionado

		part5();
	}

	void part5() {
		/*Añadir un cono al emisor con ángulos 15º(interior) y 30º(exterior) y cuya direc -
			ción será la del eje Z.Al mover el listener en el plano debe percibirse el efecto de
			atenuación de este cono.*/

		FMOD_VECTOR dir = { 0.0f, 0.0f, 1.0f }; // vector de direccion de los conos
		emmiterChannel->set3DConeOrientation(&dir);
		emmiterChannel->set3DConeSettings(15.0f, 30.0f,1.0f); // angulos en grados
		float minDistance = 1.0f, maxDistance = 10.0f;
		emmiterChannel->set3DMinMaxDistance(minDistance, maxDistance);

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
					result = game->setVolume(volume); ERRCHECK(result);
					printf("Volume: %f\n", volume);
				}
			}
			else if (c == 'v') {
				if (volume > 0) {
					volume = volume - 0.1;
					result = game->setVolume(volume); ERRCHECK(result);
					printf("Volume: %f\n", volume);
				}
			}
			//parte 3
			/*A continuación, extender la funcionalidad del bucle permitiendo mover el listener
				en el plano(X - Z) en saltos de 0.5 unidades con las teclas habituales ”asdw”, que
				detectaremos con las funciones _kbhit() y _getch() mencionadas arriba.*/
			else if (c == 'a' || c == 'A') {
				listenerPos.x -= 0.5;
				_sys->set3DListenerAttributes(0, &listenerPos, &listenerVel, &up, &at);

				changeUI();
			}
			else if (c == 'd' || c == 'D') {
				listenerPos.x += 0.5;
				_sys->set3DListenerAttributes(0, &listenerPos, &listenerVel, &up, &at);

				changeUI();
			}
			else if (c == 's' || c == 'S') {
				listenerPos.z -= 0.5;
				_sys->set3DListenerAttributes(0, &listenerPos, &listenerVel, &up, &at);

				changeUI();
			}
			else if (c == 'w' || c == 'W') {
				listenerPos.z += 0.5;
				_sys->set3DListenerAttributes(0, &listenerPos, &listenerVel, &up, &at);
				changeUI();
			}


			//parte4
			/*Extender la funcionalidad permitiendo subir el pitch del canal latido con la tecla ’P’
				o bajarlo con ’p’, en saltos de 0.1 unidades.*/
			else if (c == 'p') {
				if (pitch > 0) {
					pitch = pitch - 0.1;
					if (pitch < 0)
						pitch = 0;
					result = emmiterChannel->setPitch(pitch); ERRCHECK(result);
					changeUI();
				}
			}
			else if (c == 'P') {
				pitch = pitch + 0.1;
				result = emmiterChannel->setPitch(pitch); ERRCHECK(result);
				changeUI();
			}

		}

		_sys->update();

	}


	void close() {
		result = game->stop(); ERRCHECK(result);
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
	float volume = 0;
	float pitch = 0;
	ChannelGroup* game;
	Channel* emmiterChannel;
	FMOD_VECTOR
		listenerPos = { 0,0,0 }, // posicion del listener
		listenerVel = { 0,0,0 }, // velocidad del listener
		up = { 0,1,0 }, // vector up: hacia la ``coronilla''
		at = { 0,0,1 }; // vector at: hacia donde mira
	FMOD_VECTOR
		emmiterPos = { 0,0,4 }, // posicion
		emmiterVel = { 0,0,0 }; // velocidad

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
		return c;
	}

	void TogglePaused(FMOD::Channel* channel) {
		bool paused;
		channel->getPaused(&paused);
		channel->setPaused(!paused);
	}


	void changeUI() {
		cout << "----------------------\n";
		cout << "LISTENER POS: { ";
		cout << listenerPos.x<<" , " << listenerPos.y << " , " <<listenerPos.z<< " }\n";
		cout << "EMMITTER POS: { ";
		cout << emmiterPos.x << " , " << emmiterPos.y << " , " << emmiterPos.z << " }\n";
		cout << "PITCH: " << pitch << '\n';

	}
};

















//---------------------------------------------------------------------------//
int main(int argc, char* argv[]) {
	Exam20 ejer = Exam20();
	
	while (ejer.playing()) {
		ejer.update();
	}
	return 0;
}