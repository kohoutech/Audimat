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

#include "Vashti.h"

#include "VSTPlugin.h"
#include "VSTHost.h"
#include "WaveOutDevice.h"

#define DEFAULTSAMPLERATE 44100
#define WAVEBUFCOUNT  20			//num buffers / sec
#define WAVEBUFDURATION   50		//buf duration in ms

Vashti* Vashti::vashtiB;

//- Vashti iface exports ----------------------------------------------------

extern "C" __declspec(dllexport) void VashtiInit() {

	Vashti::vashtiB = new Vashti();
}

extern "C" __declspec(dllexport) void VashtiShutDown() {

	delete Vashti::vashtiB;
}

//- host exports ------------------------------------------------------------

extern "C" __declspec(dllexport) void VashtiStartEngine() {

	Vashti::vashtiB->startEngine();
}

extern "C" __declspec(dllexport) void VashtiStopEngine() {

	Vashti::vashtiB->stopEngine();
}

extern "C" __declspec(dllexport) int VashtiLoadPlugin(char* name) {

	return Vashti::vashtiB->loadPlugin(name);
}

extern "C" __declspec(dllexport) void VashtiUnloadPlugin(int vstnum) {

	Vashti::vashtiB->unloadPlugin(vstnum);
}

extern "C" __declspec(dllexport) void VashtiSetSampleRate(int rate) {

	Vashti::vashtiB->setSampleRate(rate);
}

extern "C" __declspec(dllexport) void VashtiSetBlockSize(int size) {

	Vashti::vashtiB->setBlockSize(size);
}

//- plugin exports ------------------------------------------------------------

extern "C" __declspec(dllexport) void VashtiSetPluginAudioIn(int vstnum, int idx) {

	Vashti::vashtiB->setPlugAudioIn(vstnum, idx);
}

extern "C" __declspec(dllexport) void VashtiSetPluginAudioOut(int vstnum, int idx) {

	Vashti::vashtiB->setPlugAudioOut(vstnum, idx);
}

extern "C" __declspec(dllexport) void VashtiGetPluginInfo(int vstnum, PlugInfo* pinfo) {

	Vashti::vashtiB->getPlugInfo(vstnum, pinfo);
}

extern "C" __declspec(dllexport) char* VashtiGetParamName(int vstnum, int paramnum){

	return Vashti::vashtiB->getParamName(vstnum, paramnum);
}

extern "C" __declspec(dllexport) float VashtiGetParamValue(int vstnum, int paramnum){

	return Vashti::vashtiB->getParamVal(vstnum, paramnum);
}

extern "C" __declspec(dllexport) void VashtiSetParamValue(int vstnum, int paramnum, float paramval){

	Vashti::vashtiB->setParamVal(vstnum, paramnum, paramval);
}

extern "C" __declspec(dllexport) char* VashtiGetProgramName(int vstnum, int prognum) {

	return Vashti::vashtiB->getProgramName(vstnum, prognum);
}

extern "C" __declspec(dllexport) void VashtiSetProgram(int vstnum, int prognum) {

	Vashti::vashtiB->setProgram(vstnum, prognum);
}

extern "C" __declspec(dllexport) void VashtiOpenEditor(int vstnum, void* hwnd) {

	Vashti::vashtiB->openEditor(vstnum, hwnd);
}

extern "C" __declspec(dllexport) void VashtiCloseEditor(int vstnum) {

	Vashti::vashtiB->closeEditor(vstnum);
}

extern "C" __declspec(dllexport) void VashtiHandleMidiMsg(int vstnum, int b1, int b2, int b3) {

	Vashti::vashtiB->handleMidiShortMsg(vstnum, b1, b2, b3);
}

//---------------------------------------------------------------------------

Vashti::Vashti() 
{
	vstHost = new VSTHost();

	timerID = 0;                              
	timeGetDevCaps(&tc, sizeof(tc));		//get timer capabilities
	dwRest = 0;
	dwLastTime = 0;

	for (int i = 0; i < 2; i++)            
	{
		emptyBuf[i] = new float[DEFAULTSAMPLERATE];
		if (emptyBuf[i])
			for (int j = 0; j < DEFAULTSAMPLERATE; j++)
				emptyBuf[i][j] = 0.0f;
	}

	//default vals
	sampleRate = DEFAULTSAMPLERATE;
	blockSize = sampleRate / WAVEBUFCOUNT;		//default duration = 100ms
	timerDuration = WAVEBUFDURATION;

	//use default in & out for now
	loadWaveOutDevice(WAVE_MAPPER);		// open output device

	isRunning = false;
}

Vashti::~Vashti() 
{
	stopEngine();
	waveOut->close();

	isRunning = false;
	vstHost->unloadAll();  

	for (int i = 0; i < 2; i++)             //delete empty buffers
		if (emptyBuf[i])
			delete[] emptyBuf[i];

	delete vstHost;
}

//- host engine methods -------------------------------------------------------

void Vashti::startEngine() 
{
	if (isRunning)
		return;

	//start output device and timer to send track data to it
	waveOut->start();		
	int timerDuration = (blockSize * 1000) / sampleRate;		//timer len in msec
	startTimer(timerDuration);

	isRunning = TRUE;
}

void Vashti::stopEngine() 
{
	if (!isRunning)
		return;

	//stop output device
	stopTimer(); 
	waveOut->stop();

	isRunning = FALSE;
}

//- timer methods -------------------------------------------------------------

BOOL Vashti::startTimer(UINT msSec)
{
	if (msSec < tc.wPeriodMin)          
		msSec = tc.wPeriodMin;

	timerDuration = msSec;
	int resolution = timerDuration / 10;
	timeBeginPeriod(resolution);        

	//timer will call <timerCallback> func every <timerDuration> millisecs
	timerID = timeSetEvent(timerDuration, (resolution > 1) ? resolution / 2 : 1, timerCallback, (DWORD)this, 
		TIME_PERIODIC || TIME_KILL_SYNCHRONOUS);

	return (timerID != NULL);
}

void Vashti::stopTimer() 
{
	if (timerID != NULL)
	{
		timeKillEvent(timerID);
		timerID = 0;
		timeEndPeriod(tc.wPeriodMin);
	}
}

void CALLBACK Vashti::timerCallback(UINT uID,	UINT uMsg,	DWORD dwUser,	DWORD dw1,	DWORD dw2)
{
	if (dwUser)                             
		((Vashti *)dwUser)->handleTimer();		//use dwUser field to xlate Windows call back to class method
}

void Vashti::handleTimer()
{
	static DWORD now;                
	static DWORD dwOffset;                 
	static WORD  i;                        

	now = timeGetTime();             
	dwOffset = now - dwLastTime;			//time since last timer call
	if (dwOffset > now)              
	{
		dwLastTime = 0;                    
		dwOffset = now;              
	}                                   

	dwOffset += dwRest;						//ofs from last timer duration - compensate for timer drift
	if (dwOffset > timerDuration)			//if we've passed the next timer duration
	{
		dwLastTime = now;            
		dwRest = dwOffset - timerDuration;     
		vstHost->processAudio(emptyBuf, (sampleRate * timerDuration / 1000), 2, now - dwStartStamp);
	}
}

//- device methods ------------------------------------------------------------

BOOL Vashti::loadWaveOutDevice	(int devID)
{
	BOOL result = FALSE;

	waveOut = new WaveOutDevice();
	waveOut->setBufferCount(WAVEBUFCOUNT);
	waveOut->setBufferDuration(WAVEBUFDURATION);
	result = waveOut->open(devID, sampleRate, 16, 2);		//stereo out

	vstHost->setWaveOut(waveOut);
	vstHost->setBlockSize(blockSize);

	return result;
}

//- plugin methods ------------------------------------------------------------

int Vashti::loadPlugin(LPCSTR fileName) 
{ 
	BOOL wasEngineRunning = isRunning;         

	if (isRunning) stopEngine();                           

	int plugid = vstHost->loadPlugin(fileName);

	if (wasEngineRunning) startEngine();
	return plugid;
}

void Vashti::unloadPlugin(int vstNum) 
{
	BOOL wasEngineRunning = isRunning;         

	if (isRunning) stopEngine();                           

	vstHost->unloadPlugin(vstNum);

	if (wasEngineRunning) startEngine();
}

void Vashti::setSampleRate(int rate)
{
	if (isRunning) stopEngine();                           

	vstHost->setSampleRate(rate);
}

void Vashti::setBlockSize(int size)
{
	if (isRunning) stopEngine();                           

	vstHost->setBlockSize(size);
}

//- plugin methods ------------------------------------------------------------

void Vashti::setPlugAudioIn(int vstnum, int idx)
{
}

void Vashti::setPlugAudioOut(int vstnum, int idx)
{
}


void Vashti::getPlugInfo(int vstnum, PlugInfo* pinfo) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	if (vst != NULL)
	{
		pinfo->name = (char*) CoTaskMemAlloc(kVstMaxNameLen);
		vst->getProductString(pinfo->name);
		pinfo->vendor = (char*) CoTaskMemAlloc(kVstMaxNameLen);
		vst->getVendorString(pinfo->vendor);
		pinfo->version = vst->getVstVersion();
		pinfo->numPrograms = vst->pEffect->numPrograms;
		pinfo->numParameters = vst->pEffect->numParams;
		pinfo->numInputs = vst->pEffect->numInputs;
		pinfo->numOutputs = vst->pEffect->numOutputs;
		pinfo->flags = vst->pEffect->flags;
		pinfo->uniqueID = vst->pEffect->uniqueID;

		if (vst->pEffect->flags && effFlagsHasEditor != 0) {
			ERect* pRect;
			vst->editGetRect(&pRect);
			pinfo->editorWidth = pRect->right - pRect->left;
			pinfo->editorHeight = pRect->bottom - pRect->top;
		} else {
			pinfo->editorWidth = 0;
			pinfo->editorHeight = 0;
		}
	}
}

//- plugin params -------------------------------------------------------------

char* Vashti::getParamName(int vstnum, int paramnum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	if (vst) 
	{
		char* paramname = (char*) CoTaskMemAlloc(kVstMaxNameLen);
		vst->getParamName(paramnum, paramname);
		return paramname;
	}
}

float Vashti::getParamVal(int vstnum, int paramnum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	if (vst) 
	{
		return vst->getParameter(paramnum);
	}
}

void Vashti::setParamVal(int vstnum, int paramnum, float paramval) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	if (vst) 
	{	
		vst->setParameter(paramnum, paramval);
	}
}

//- plugin programs -----------------------------------------------------------

char* Vashti::getProgramName(int vstNum, int prognum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	if (vst) 
	{
		char* progname = (char*) CoTaskMemAlloc(kVstMaxNameLen);
		vst->getProgramNameIndexed(0, prognum, progname);		
		return progname;
	}
}

void Vashti::setProgram(int vstNum, int prognum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	if (vst) 
	{
		vst->setProgram(prognum);
	}
}

//- plugin editor -------------------------------------------------------------

void Vashti::openEditor(int vstNum, void * hwnd) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	if (vst) 
	{
		vst->editOpen(hwnd);
	}
}

void Vashti::closeEditor(int vstNum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	if (vst) 
	{
		vst->editClose();
	}
}

void Vashti::handleMidiShortMsg(int vstnum, int b1, int b2, int b3) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	if (vst) 
	{
		vst->storeMidiShortMsg(b1, b2, b3);
	}
}

//printf("there's no sun in the shadow of the wizard.\n");
