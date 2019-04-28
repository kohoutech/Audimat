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

#define WAVEBUFCOUNT  10
#define WAVEBUFDURATION   100		//buf duration in ms

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

//- plugin exports ------------------------------------------------------------

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

	Vashti::vashtiB->handleMidiShortMsg(b1,b2,b3);
}

//---------------------------------------------------------------------------

Vashti::Vashti() 
{
	vstHost = new VSTHost();

	timerID = 0;                              
	timeGetDevCaps(&tc, sizeof(tc));       

	for (int i = 0; i < 2; i++)            
	{
		emptyBuf[i] = new float[44100];
		if (emptyBuf[i])
			for (int j = 0; j < 44100; j++)
				emptyBuf[i][j] = 0.0f;
	}

	//default vals
	sampleRate = 44100;
	blockSize = sampleRate / 10;		//default duration = 100ms
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
	int timerDuration = (blockSize * 1000) / sampleRate;
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

	timerID = timeSetEvent(timerDuration, (resolution > 1) ? resolution / 2 : 1, timerCallback, (DWORD)this, TIME_PERIODIC);

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
		((Vashti *)dwUser)->handleTimer();
}

void Vashti::handleTimer()
{
	static DWORD now;                
	static DWORD dwOffset;                 
	static WORD  wClocks;                  
	static WORD  i;                        

	now = timeGetTime();             
	dwOffset = now - dwLastTime;
	if (dwOffset > now)              
	{
		dwLastTime = 0;                    
		dwOffset = now;              
	}                                   

	dwOffset += dwRest;                 
	if (dwOffset > timerDuration)         
	{
		dwLastTime = now;            
		dwRest = dwOffset - timerDuration;     
		vstHost->audioOut(emptyBuf, (44100 * timerDuration / 1000), 2, now - dwStartStamp);
	}
}

//- device methods ------------------------------------------------------------

BOOL Vashti::loadWaveOutDevice	(int devID)
{
	BOOL result = FALSE;

	waveOut = new WaveOutDevice();
	waveOut->setBufferCount(WAVEBUFCOUNT);
	waveOut->setBufferDuration(WAVEBUFDURATION);
	result = waveOut->open(devID, 44100, 16, 2);		//stereo out

	vstHost->setWaveOut(waveOut);
	vstHost->setBlockSize(4410);

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
}

void Vashti::getPlugInfo(int vstnum, PlugInfo* pinfo) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	if (vst == NULL)
		return;

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

//- plugin params -------------------------------------------------------------

char* Vashti::getParamName(int vstnum, int paramnum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	char* paramname = (char*) CoTaskMemAlloc(kVstMaxNameLen);
	vst->getParamName(paramnum, paramname);
	return paramname;
}

float Vashti::getParamVal(int vstnum, int paramnum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	return vst->getParameter(paramnum);
}

void Vashti::setParamVal(int vstnum, int paramnum, float paramval) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstnum);
	vst->setParameter(paramnum, paramval);
}

//- plugin programs -----------------------------------------------------------

char* Vashti::getProgramName(int vstNum, int prognum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	char* progname = (char*) CoTaskMemAlloc(kVstMaxNameLen);
	vst->getProgramNameIndexed(0, prognum, progname);		
	return progname;
}

void Vashti::setProgram(int vstNum, int prognum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	vst->setProgram(prognum);
}

//- plugin editor -------------------------------------------------------------

void Vashti::openEditor(int vstNum, void * hwnd) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	vst->editOpen(hwnd);
}

void Vashti::closeEditor(int vstNum) 
{
	VSTPlugin* vst = vstHost->getPlugin(vstNum);
	vst->editClose();
}

void Vashti::handleMidiShortMsg(int b1, int b2, int b3) 
{
}

//printf("there's no sun in the shadow of the wizard.\n");
