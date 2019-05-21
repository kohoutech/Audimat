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
using Transonic.VST;
using Transonic.MIDI.System;
using Origami.Serial;

//Mrs. Peel, we're needed

namespace Audimat.Graph
{
    public class VSTRig
    {
        ControlPanel controlPanel;
        public MidiSystem midiDevices;

        public List<Module> modules;
        public List<Patch> patches;

        public Patch currentPatch;

        public bool hasChanged;

        public static VSTRig loadFromFile(String path, ControlPanel controlPanel)
        {
            VSTRig rig = null;

            SerialData rigData = new SerialData(path);

            if (rigData != null)
            {

                rig = new VSTRig(controlPanel);

                //load modules
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

                //load patches
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
            }

            return rig;
        }

        //---------------------------------------------------------------------

        public VSTRig(ControlPanel _controlPanel)
        {
            controlPanel = _controlPanel;
            midiDevices = controlPanel.midiDevices;

            modules = new List<Module>();
            patches = new List<Patch>();

            currentPatch = null;
            hasChanged = false;
        }

        ////load new rig from file and update rack to match rig
        //public bool loadRig(string rigPath)
        //{

        //    //VSTRig newRig = VSTRig.loadFromFile(rigPath);
        //    //if (newRig != null)
        //    //{
        //    //    //remove prev rig
        //    //    currentRig.shutDown();
        //    //    removeAllPanels();

        //    //    //install new rig
        //    //    currentRig = newRig;
        //    //    foreach (Module mod in currentRig.modules)      //add panels to rack
        //    //    {
        //    //        VSTPanel panel = addPanel(mod.path, false);
        //    //        if (panel != null)
        //    //        {
        //    //            mod.panel = panel;
        //    //            panel.setMidiIn(mod.midiIn);
        //    //        }
        //    //    }
        //    //    auditwin.rackPatchesChanged(null);             //broadcast patch change

        //    //    hasChanged = false;
        //    //    return true;
        //    //}
        //    return false;
        //}

        //public void clearRig()
        //{
        //    //currentRig.shutDown();
        //    //removeAllPanels();

        //    //currentRig = new VSTRig();
        //    //hasChanged = false;
        //}

        //public void saveRig(string rigPath)
        //{
        //    //currentRig.saveToFile(rigPath);
        //    //hasChanged = false;
        //}

        public void AddModule(Module module)
        {
            modules.Add(module);
        }

        public void AddPatch(Patch patch)
        {
            patches.Add(patch);
        }

        private void removeAllPanels()
        {
            foreach (VSTPanel panel in panels)            //remove panels from rack
            {
                panelSpace.Controls.Remove(panel);
                panel.unloadPlugin();
            }
            panels.Clear();
            panelSpace.Size = new Size(0, 0);
            updateScrollBar();
            Invalidate();

            //broadcast rack change
            auditwin.rackPatchesChanged(null);
            auditwin.rackModulesChanged();
        }

        public List<VSTPlugin> getPluginList()
        {
            return host.plugins;
        }

        //- patching ----------------------------------------------------------

        public void setCurrentPatch(int p)
        {
            //currentPatch = currentRig.patches[p];
            //foreach (Module mod in currentRig.modules)
            //{
            //    int patchNum = (currentPatch.modules.ContainsKey(mod) ? currentPatch.modules[mod] : 0);
            //    mod.panel.cbxProgList.SelectedIndex = patchNum;
            //}
        }

        public void clearPatch()
        {
            currentPatch = null;
            auditwin.rackPatchesChanged(null);
            foreach (Module mod in currentRig.modules)
            {
                mod.panel.cbxProgList.SelectedIndex = 0;
            }
        }

        public void updatePatch()
        {
            foreach (Module mod in currentRig.modules)
            {
                int patchNum = mod.panel.cbxProgList.SelectedIndex;
                if (currentPatch.modules.ContainsKey(mod))
                {
                    currentPatch.modules[mod] = patchNum;
                }
            }
        }

        public void addNewPatch(String patchName)
        {
            Patch newPatch = new Patch(patchName);
            foreach (Module mod in currentRig.modules)
            {
                int patchNum = mod.panel.cbxProgList.SelectedIndex;
                newPatch.AddModule(mod, patchNum);
            }
            currentRig.AddPatch(newPatch);
            currentPatch = newPatch;
            auditwin.rackPatchesChanged(currentPatch.name);
        }

        //- midi i/o connections ----------------------------------------------

        public void connectMidiInput(InputDevice indev, PanelMidiIn pluginMidiIn)
        {
            //InputDevice indev = midiDevices.inputDevices[idx];
            try
            {
                indev.open();
                indev.connectUnit(pluginMidiIn);
                indev.start();
            }
            catch
            {
                //Console.WriteLine("error connecting midi input");
            }
        }

        public void disconnectMidiInput(InputDevice indev, PanelMidiIn pluginMidiIn)
        {
            //InputDevice indev = midiDevices.inputDevices[idx];
            indev.disconnectUnit(pluginMidiIn);
        }

        public void connectMidiOutput(int idx, PanelMidiOut pluginMidiOut)
        {
        }

        public void disconnectMidiOutput(int idx, PanelMidiOut pluginMidiOut)
        {
        }


        public void saveToFile(String filename)
        {
            SerialData rigData = new SerialData();
            rigData.setStringValue("version", Settings.VERSION);

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
            hasChanged = false;
            //    removeAllPanels();
        }

        public bool addPanel(string pluginPath)
        {
            //        panel.unloadPlugin();           //disconnect panel & shut down plugin
            //auditwin.rackModulesChanged();         //broadcast rack change

            //        auditwin.rackModulesChanged();         //broadcast rack change
            //if (updateRig)
            //{
            //    Module mod = new Module(plugPath, "no output", "no input");
            //    mod.panel = panel;
            //    currentRig.AddModule(mod);
            //}

            //        VSTPanel panel = new VSTPanel(this, panels.Count);
            //bool result = panel.loadPlugin(plugPath);
            //if (!result) return null;
            return true;
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
