/* ----------------------------------------------------------------------------
Audimat : an audio plugin host
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

using Audimat.UI;
using Origami.Serial;

//Mrs. Peel, we're needed

namespace Audimat.Graph
{
    public class VSTRig
    {
        public List<Module> modules;
        public List<Patch> patches;

        public static VSTRig loadFromFile(String path)
        {
            SerialData rigData = new SerialData(path);

            VSTRig rig = new VSTRig();

            List<String> mods = rigData.getSubpathKeys("module-list");
            Dictionary<String, Module> modList = new Dictionary<string, Module>();
            foreach (String mod in mods)
            {
                String modPath = rigData.getStringValue("module-list." + mod + ".path", "");
                String modAudioOut = rigData.getStringValue("module-list." + mod + ".audio-out", "");
                String modMidiIn = rigData.getStringValue("module-list." + mod + ".midi-in", "");
                Module module = new Module(modPath, modAudioOut, modMidiIn);
                rig.AddModule(module);
                modList[mod] = module;
            }

            List<String> pats = rigData.getSubpathKeys("patch-list");
            foreach (String pat in pats)
            {
                String patname = rigData.getStringValue("patch-list." + pat + ".name", "");
                Patch patch = new Patch(patname);
                foreach (String mod in mods)
                {
                    int patnum = rigData.getIntValue("patch-list." + pat + "." + mod, 0);
                    patch.AddModule(modList[mod], patnum);
                }
                rig.AddPatch(patch);
            }

            return rig;
        }

        //---------------------------------------------------------------------

        public VSTRig()
        {
            modules = new List<Module>();
            patches = new List<Patch>();
        }

        public void AddModule(Module module)
        {
            modules.Add(module);
        }

        public void AddPatch(Patch patch)
        {
            patches.Add(patch);
        }

        public void saveToFile(String filename)
        {
            SerialData rigData = new SerialData();
            rigData.setStringValue("version", AudimatWindow.VERSION);

            Dictionary<Module, String> modList = new Dictionary<Module, string>();
            int count = 1;
            foreach (Module module in modules)
            {
                String modname = "module-" + count.ToString().PadLeft(3, '0');
                rigData.setStringValue("module-list." + modname + ".path", module.path);
                rigData.setStringValue("module-list." + modname + ".audio-out", module.audioOut);
                rigData.setStringValue("module-list." + modname + ".midi-in", module.midiIn);
                modList[module] = modname;
                count++;
            }
            count = 1;
            foreach (Patch patch in patches)
            {
                String patname = "patch-" + count.ToString().PadLeft(3, '0');
                rigData.setStringValue("patch-list." + patname + ".name", patch.name);
                foreach (Module module in patch.modules.Keys)
                {
                    rigData.setIntValue("patch-list." + patname + "." + modList[module], patch.modules[module]);
                }
                count++;
            }
            rigData.saveToFile(filename);
        }

        //return rig to empty state
        public void shutDown()
        {
            modules.Clear();
            patches.Clear();
        }
    }

    //-------------------------------------------------------------------------

    public class Module
    {
        public string path;
        public string audioOut;
        public string midiIn;
        public VSTPanel panel;

        public Module(string _path, string _audioOut, string _midiIn)
        {
            path = _path;
            audioOut = _audioOut;
            midiIn = _midiIn;
            panel = null;
        }
    }

    //-------------------------------------------------------------------------

    public class Patch
    {
        public string name;
        public Dictionary<Module, int> modules;

        public Patch(string _name)
        {            
            name = _name;

            modules = new Dictionary<Module, int>();
        }

        public void AddModule(Module mod, int patchNum)
        {
            modules.Add(mod, patchNum);
        }
    }
}
