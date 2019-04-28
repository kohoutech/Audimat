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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Transonic.VST
{
    public class Vashti
    {
        //communication with vashti.dll
        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiInit();

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiShutDown();

        //- host exports ------------------------------------------------------------

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiStartEngine();

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiStopEngine();

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int VashtiLoadPlugin(string filename);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiUnloadPlugin(int vstnum);

        //- plugin exports ------------------------------------------------------------

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiGetPluginInfo(int vstnum, ref PluginInfo pinfo);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern String VashtiGetParamName(int vstnum, int paramnum);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern float VashtiGetParamValue(int vstnum, int paramnum);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiSetParamValue(int vstnum, int paramnum, float paramval);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern String VashtiGetProgramName(int vstnum, int prognum);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiSetProgram(int vstnum, int prognum);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiOpenEditor(int vstnum, IntPtr hwnd);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiCloseEditor(int vstnum);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiHandleMidiMsg(int vstnum, int b1, int b2, int b3);

//-----------------------------------------------------------------------------

        public bool isEngineRunning;

        public Vashti()
        {
            VashtiInit();
        }

        public void shutDown()
        {
            VashtiShutDown();
        }

        //- host methods ----------------------------------------------------------

        public void startEngine()
        {
            VashtiStartEngine();
            isEngineRunning = true;
        }

        public void stopEngine()
        {
            VashtiStopEngine();
            isEngineRunning = false;
        }

        public int loadPlugin(string filename)
        {
            Console.WriteLine("vashti loading plugin " + filename);
            int plugid = VashtiLoadPlugin(filename);
            return plugid;
        }

        public void unloadPlugin(int plugid)
        {
            VashtiUnloadPlugin(plugid);
        }

        //- plugin methods ----------------------------------------------------------

        public void getPluginInfo(int plugid, ref PluginInfo pluginfo)
        {
            VashtiGetPluginInfo(plugid, ref pluginfo);
        }

        public String getPluginParamName(int plugid, int paramnum)
        {
            return VashtiGetParamName(plugid, paramnum);
        }

        public float getPluginParamValue(int plugid, int paramnum)
        {
            return VashtiGetParamValue(plugid, paramnum);
        }

        public void setPluginParam(int plugid, int paramnum, float paramval)
        {
            VashtiSetParamValue(plugid, paramnum, paramval);
        }

        public String getPluginProgramName(int plugid, int prognum)
        {
            return VashtiGetProgramName(plugid, prognum);
        }

        public void setPluginProgram(int plugid, int prognum)
        {
            VashtiSetProgram(plugid, prognum);
        }

        public void openEditorWindow(int plugid, IntPtr hwnd)
        {
            VashtiOpenEditor(plugid, hwnd);
        }

        public void closeEditorWindow(int plugid)
        {
            VashtiCloseEditor(plugid);
        }

        public void sendMidiMsg(int plugid, int b1, int b2, int b3)
        {
            VashtiHandleMidiMsg(plugid, b1, b2, b3);
        }
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
