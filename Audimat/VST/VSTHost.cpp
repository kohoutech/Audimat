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

#include "VSTHost.h"
#include "VSTPlugin.h"
#include "WaveOutDevice.h"

//cons
VSTHost::VSTHost()
{
	sampleRate = 44100.0f;
	blockSize = 1024;

	for (int i = 0; i < 2; i++)
		pOutputs[i] = new float[22050];			//0.5 sec @ 44.1 kHz

	pluginMax = 10;
	plugins = (VSTPlugin**)malloc(pluginMax * sizeof(VSTPlugin*));
	pluginCount = 0;

	InitializeCriticalSection(&cs);
}

//destuct
VSTHost::~VSTHost()
{
	unloadAll();
	delete plugins;

	for (int i = 0; i < 2; i++)
		delete[] pOutputs[i];
}

void VSTHost::setSampleRate(int rate) 
{
	sampleRate = rate; 
}

void VSTHost::setBlockSize (int size) 
{ 
	blockSize = size; 
}

//- plugin methods ------------------------------------------------------------

int VSTHost::loadPlugin(const char * filename) 
{
	VSTPlugin *plug = new VSTPlugin(this);      
	if (!plug->load(filename))      
	{
		delete plug;        
		return -1;
	}

	if (pluginCount >= pluginMax) 
	{
		pluginMax += 10;
		plugins = (VSTPlugin**)realloc(plugins,pluginMax * sizeof(VSTPlugin*));
	}
	int plugNum = pluginCount++;
	plugins[plugNum] = plug;

	plug->open();
	plug->setSampleRate(sampleRate);
	plug->setBlockSize(blockSize);
	plug->suspend();                  
	plug->resume();                   
	return plugNum;
}

VSTPlugin* VSTHost::getPlugin(int idx) 
{ 
	if ((idx >= 0) && (idx < pluginCount)) 
		return plugins[idx]; 
	else 
		return NULL; 
}

void VSTHost::unloadPlugin(int idx)
{
	if ((idx >= 0) && (idx < pluginCount)) 
	{
		VSTPlugin *plug = plugins[idx]; 
		if (plug != NULL) {
			plug->unload();
			delete plug;
		}
		plugins[idx] = NULL;
	}	
}

void VSTHost::unloadAll()
{
	for (int i = 0; i < pluginCount; i++)
		unloadPlugin(i);
}

//- processing methods --------------------------------------------------------

void VSTHost::processAudio(float **pBuffer, int nLength, int nChannels, DWORD dwStamp)
{
	VSTPlugin *pEff, *pNextEff;
	float *pBuf1, *pBuf2;

	if (nLength > blockSize)               
		nLength = blockSize;

	//zero out output buf
	for (int j = 0; j < 2; j++)
	{
		pBuf1 = pOutputs[j];
		if (!pBuf1)
			break;
		for (int k = 0; k < nLength; k++)   
			*pBuf1++ = 0.0f;
	}

	float fMult = 1.0f / pluginCount;

	//for each plugin
	for (int i = 0; i < pluginCount; i++) {

		pEff = getPlugin(i);
		pEff->enterCritical();              
		pEff->doProcessReplacing(nLength);	

		//sum plugin output
		for (int j = 0; j < 2; j++)
		{
			pBuf1 = pOutputs[j];
			pBuf2 = pEff->getOutputBuffer(j);
			if ((!pBuf1) || (!pBuf2))
				break;
			for (int k = 0; k < nLength; k++)   
				*pBuf1++ += *pBuf2++ * fMult;
		}

		pEff->leaveCritical();              
	}

	// now that we've got a populated output buffer set, send it to 
	// the Wave Output device.
	waveOut->writeOut(pOutputs, nLength);
}
