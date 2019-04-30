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

#include "VSTPlugin.h"
#include "VSTHost.h"

//cons
VSTPlugin::VSTPlugin(VSTHost *_pHost)
{
	pHost = _pHost;
	pEffect = NULL;
	hModule = NULL;	

	outBufs = NULL;
	outBufCount = 0;

	InitializeCriticalSection(&cs);
}

//destuct
VSTPlugin::~VSTPlugin()
{
	unload();

	enterCritical();

	if (outBufs)
	{
		for (int i = 0; i < outBufCount; i++)
			if (outBufs[i]) 
				delete[] outBufs[i];
		delete[] outBufs;
	}

	leaveCritical();
	DeleteCriticalSection(&cs);
}

bool VSTPlugin::load(const char *filename)
{
	//load VST dll into memory
	//printf("loading plugin %s\n",name);
	hModule = ::LoadLibrary(filename);		
	if (!hModule)
		return false;

	//get VST's entry point
	AEffect *(*pMain)(long (*audioMaster)(AEffect *effect, long opcode, long index, long value, void *ptr, float opt)) = NULL;
	pMain = (AEffect * (*)(long (*)(AEffect *,long,long,long,void *,float))) ::GetProcAddress(hModule, "VSTPluginMain");
	if (!pMain)
		pMain = (AEffect * (*)(long (*)(AEffect *,long,long,long,void *,float))) ::GetProcAddress(hModule, "main");

	if (!pMain)
		return false;

	//get effect struct from VST, pass host's callback func to VST
	pEffect = pMain(AudioMasterCallback);

	//check if valid effect
	if (pEffect && (pEffect->magic != kEffectMagic)) {
		pEffect = NULL;
		return false;
	}

	//store plugin obj 
	pEffect->user = this;

	//get output buffers
	if (pEffect->numOutputs)
	{
		int nAlloc = pEffect->numOutputs < 2 ? 2 : pEffect->numOutputs;		//min output = stereo channels
		outBufs = new float *[nAlloc];
		if (!outBufs)
			return false;
		for (int i = 0; i < pEffect->numOutputs; i++)
		{
			outBufs[i] = new float[OUTPUTBUFSIZE];		
			if (!outBufs[i])
				return false;
			for (int j = 0; j < OUTPUTBUFSIZE; j++)
				outBufs[i][j] = 0.0f;
		}
		outBufCount = nAlloc;
	}

	return true;
}

void VSTPlugin::unload()
{
	close();
	pEffect = NULL;

	if (hModule)
	{
		::FreeLibrary(hModule);
		hModule = NULL;       
	}
}

//- processing methods --------------------------------------------------------

float * VSTPlugin::getOutputBuffer(int bufIdx)
{
	if (bufIdx >= 0 && bufIdx < outBufCount)
		return outBufs[bufIdx];
	else
		return 0;
}

void VSTPlugin::doProcess(long sampleFrames)
{
	process(inBufs, outBufs, sampleFrames);
}

void VSTPlugin::doProcessReplacing(long sampleFrames)
{
	processReplacing(inBufs, outBufs, sampleFrames);
}

//- VST functions ----------------------------------------------------------

long VSTPlugin::dispatch(long opCode, long index, long value, void *ptr, float opt)
{
	if (!pEffect)
		return 0;

	return pEffect->dispatcher(pEffect, opCode, index, value, ptr, opt);
}

void VSTPlugin::setParameter(long index, float parameter)
{
	if (!pEffect)
		return;

	pEffect->setParameter(pEffect, index, parameter);
}

float VSTPlugin::getParameter(long index)
{
	if (!pEffect)
		return 0.0f;

	return pEffect->getParameter(pEffect, index);
}

void VSTPlugin::process(float **inputs, float **outputs, long sampleframes)
{
	if (!pEffect)
		return;

	pEffect->process(pEffect, inputs, outputs, sampleframes);
}

void VSTPlugin::processReplacing(float **inputs, float **outputs, long sampleframes)
{
	if ((!pEffect) ||
		(!(pEffect->flags & effFlagsCanReplacing)))
		return;

	pEffect->processReplacing(pEffect, inputs, outputs, sampleframes);
}

void VSTPlugin::processDoubleReplacing(double **inputs, double **outputs, long sampleFrames)
{
	if ((!pEffect) ||
		(!(pEffect->flags & effFlagsCanDoubleReplacing)))
		return;

	pEffect->processDoubleReplacing(pEffect, inputs, outputs, sampleFrames);
}

//- VST dispatcher functions -----------------------------------------------

void VSTPlugin::open() 
{ 
	dispatch(effOpen); 
}

void VSTPlugin::close() 
{ 
	dispatch(effClose); 
}

void VSTPlugin::setProgram(long lValue) { 
	dispatch(effSetProgram, 0, lValue); 
}

void VSTPlugin::getParamLabel(long index, char *ptr) 
{ 
	dispatch(effGetParamLabel, index, 0, ptr); 
}

void VSTPlugin::getParamDisplay(long index, char *ptr) 
{ 
	dispatch(effGetParamDisplay, index, 0, ptr); 
}

void VSTPlugin::getParamName(long index, char *ptr) 
{ 
	dispatch(effGetParamName, index, 0, ptr); 
}

void VSTPlugin::setSampleRate(float fSampleRate) 
{ 
	dispatch(effSetSampleRate, 0, 0, 0, fSampleRate); 
}

void VSTPlugin::setBlockSize(long value) 
{ 
	dispatch(effSetBlockSize, 0, value); 
}

void VSTPlugin::suspend() 
{ 
	dispatch(effMainsChanged, 0, false); 
}

void VSTPlugin::resume() 
{ 
	dispatch(effMainsChanged, 0, true); 
}

long VSTPlugin::editGetRect(ERect **ptr) 
{ 
	return dispatch(effEditGetRect, 0, 0, ptr); 
}

long VSTPlugin::editOpen(void *ptr) { 
	return dispatch(effEditOpen, 0, 0, ptr); 
}

void VSTPlugin::editClose() { 
	dispatch(effEditClose); 
}

long VSTPlugin::getProgramNameIndexed(long category, long index, char* text) { 
	return dispatch(effGetProgramNameIndexed, index, category, text); 
}

long VSTPlugin::getVendorString(char *ptr) 
{ 
	return dispatch(effGetVendorString, 0, 0, ptr); 
}

long VSTPlugin::getProductString(char *ptr) 
{ 
	return dispatch(effGetProductString, 0, 0, ptr); 
}

long VSTPlugin::getVstVersion() 
{ 
	return dispatch(effGetVstVersion); 
}

//- host callback functions ---------------------------------------------------

//callback func that gets passed to the plugin on loading
//use static host var that points to this instance to xlate this call to class method class

long VSTCALLBACK VSTPlugin::AudioMasterCallback
	(
	AEffect *effect,
	long opcode,
	long index,
	long value,
	void *ptr,
	float opt
	)
{
	if (opcode == audioMasterVersion)		//this is called before the user field is set
		return 2400;

	VSTPlugin* plugin = (VSTPlugin*)effect->user;
	return plugin->OnAudioMasterCallback(0, opcode, index, value, ptr, opt);
}

//class method version of above callback
long VSTPlugin::OnAudioMasterCallback
	(
	int nEffect,
	long opcode,
	long index,
	long value,
	void *ptr,
	float opt
	)
{
	return 0;			//to be implemented
}

//-----------------------------------------------------------------------------

void VSTPlugin::enterCritical()
{
	EnterCriticalSection(&cs);
}

void VSTPlugin::leaveCritical()
{
	LeaveCriticalSection(&cs);
}

