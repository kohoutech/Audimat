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
    public class VSTHost
    {
        //- host exports ------------------------------------------------------------

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiStartEngine();

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiStopEngine();

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int VashtiLoadPlugin(string filename);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiUnloadPlugin(int vstnum);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiSetSampleRate(int rate);

        [DllImport("Vashti.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void VashtiSetBlockSize(int size);

        //- host methods ----------------------------------------------------------

        public Vashti vashti;
        public bool isEngineRunning;

        public List<VSTPlugin> plugins;

        public VSTHost(Vashti _vashti)
        {
            vashti = _vashti;
            isEngineRunning = false;
            plugins = new List<VSTPlugin>();
        }

        public void shutdown()
        {
        }

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
            int plugid = VashtiLoadPlugin(filename);
            return plugid;
        }

        public void unloadPlugin(int plugid)
        {
            VashtiUnloadPlugin(plugid);
        }

        public void setSampleRate(int rate)
        {
            VashtiSetSampleRate(rate);
        }

        public void setBlockSize(int blocksize)
        {
            VashtiSetBlockSize(blocksize);
        }
    }
}
