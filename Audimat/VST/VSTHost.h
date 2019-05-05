/* ----------------------------------------------------------------------------
Transonic VST Library
Copyright (C) 2005-2019  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

#if !defined(VSTHOST_H)
#define VSTHOST_H

#include "vstsdk2.4/audioeffectx.h"
#include <windows.h>                    

class VSTPlugin;
class WaveOutDevice;

class VSTHost
{
public:
	VSTHost();
	~VSTHost();

	void setSampleRate(int rate);
	void setBlockSize (int size);
	void setWaveOut (WaveOutDevice* _waveOut) { waveOut = _waveOut; }

	//plugin
	int loadPlugin(const char * filename);
	VSTPlugin* getPlugin(int idx);
    void unloadPlugin(int idx);
    void unloadAll();

	//audio
	void processAudio(float **pBuffer, int nLength, int nChannels = 2, DWORD dwStamp = 0);

protected:
    float sampleRate;
    long blockSize;

	VSTPlugin** plugins;		//array of plugins
	int pluginMax;				//array size
	int pluginCount;			//num of loaded plugins

	WaveOutDevice* waveOut;

	float * pOutputs[2];
	//CRITICAL_SECTION cs;
};

#endif // VSTHOST_H
