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
        public ControlPanel controlPanel;
        public VSTRack rack;

        public List<VSTPanel> panels;
        public List<Patch> patches;

        public Patch currentPatch;
        public Patch newPatch;

        public bool hasChanged;

        //---------------------------------------------------------------------

        public static VSTRig loadFromFile(String path, ControlPanel controlPanel)
        {
            VSTRig rig = null;

            SerialData rigData = new SerialData(path);      //load data from file

            if (rigData != null)
            {
                rig = new VSTRig(controlPanel);

                //load plugins
                List<String> plugs = rigData.getSubpathKeys("plugin-list");
                Dictionary<String, VSTPanel> plugList = new Dictionary<string, VSTPanel>();      //temp dict for matching plugins to patches
                foreach (String plug in plugs)
                {
                    String plugPath = rigData.getStringValue("plugin-list." + plug + ".path", "");
                    String plugAudioOut = rigData.getStringValue("plugin-list." + plug + ".audio-out", "");
                    String plugMidiIn = rigData.getStringValue("plugin-list." + plug + ".midi-in", "");

                    VSTPanel panel = rig.addPanel(plugPath);
                    //panel.plugin.setAudioOut(plugAudioOut);
                    panel.setMidiIn(plugMidiIn);

                    plugList[plug] = panel;
                }

                //load patches
                List<String> pats = rigData.getSubpathKeys("patch-list");
                foreach (String pat in pats)
                {
                    String patchName = rigData.getStringValue("patch-list." + pat + ".name", "");
                    Patch patch = new Patch(patchName);
                    foreach (String plug in plugs)
                    {
                        int patnum = rigData.getIntValue("patch-list." + pat + "." + plug, 0);
                        patch.addPanel(plugList[plug], patnum);
                    }
                    rig.patches.Add(patch);
                }
                rig.setCurrentPatch(0);
            }

            return rig;
        }

        public VSTRig(ControlPanel _controlPanel)
        {
            controlPanel = _controlPanel;
            rack = controlPanel.auditwin.rack;

            panels = new List<VSTPanel>();
            patches = new List<Patch>();

            currentPatch = null;
            newPatch = null;
            hasChanged = false;
        }

        //return rig to empty state
        public void shutDown()
        {
            currentPatch = null;
            patches.Clear();
            removeAllPanels();
            hasChanged = false;
        }

        //- plugin management ------------------------------------------------------------

        public VSTPanel addPanel(string pluginPath)
        {
            VSTPanel panel = new VSTPanel(this, panels.Count);
            bool result = panel.loadPlugin(pluginPath);
            if (!result) return null;

            panels.Add(panel);
            rack.addPanel(panel);

            foreach (Patch patch in patches)            //add panel to each patch
            {
                patch.addPanel(panel, 0);
            }

            hasChanged = true;
            controlPanel.rigPluginsChanged();         //broadcast rack change

            return panel;
        }

        public void removePanel(VSTPanel panel)
        {
            panel.unloadPlugin();           //disconnect panel & shut down plugin
            rack.removePanel(panel);
            panels.Remove(panel);

            foreach (Patch patch in patches)            //add panel to each patch
            {
                patch.removePanel(panel);
            }


            hasChanged = true;
            controlPanel.rigPluginsChanged();         //broadcast rack change            
        }

        public void removeAllPanels()
        {
            List<VSTPanel> tmpList = new List<VSTPanel>(panels);
            foreach (VSTPanel panel in tmpList)                         
            {
                removePanel(panel);                 //remove panels from rack
            }
        }

        //- patch management --------------------------------------------------

        public void setCurrentPatch(int p)
        {
            if (p < patches.Count)
            {
                currentPatch = patches[p];
                foreach (VSTPanel panel in panels)
                {                    
                    int patchNum = (currentPatch.panels.ContainsKey(panel) ? currentPatch.panels[panel] : 0);
                    panel.cbxProgList.SelectedIndex = patchNum;
                }
            }
        }

        //add a new patch to the patch list
        public void addNamedPatch(String patchName)
        {
            Patch newPatch = new Patch(patchName);
            foreach (VSTPanel panel in panels)
            {
                int patchNum = panel.cbxProgList.SelectedIndex;
                newPatch.addPanel(panel, patchNum);
            }
            patches.Add(newPatch);
            currentPatch = newPatch;
            hasChanged = true;
            controlPanel.rigPatchesChanged();
        }

        //add a new patch, but don't add it to the list until the user gives it a name
        public void addNewPatch()
        {
            newPatch = new Patch("new patch");
            foreach (VSTPanel panel in panels)
            {
                newPatch.addPanel(panel, 0);
                panel.cbxProgList.SelectedIndex = 0;
            }
            currentPatch = newPatch;
            controlPanel.rigPatchesChanged();
        }

        public void updatePatch()
        {
            foreach (VSTPanel panel in panels)
            {
                int patchNum = panel.cbxProgList.SelectedIndex;
                if (currentPatch.panels.ContainsKey(panel))
                {
                    currentPatch.panels[panel] = patchNum;
                }
            }
        }

        //- saving ------------------------------------------------------------

        public void saveToFile(String filename)
        {
            SerialData rigData = new SerialData();
            rigData.setStringValue("version", Settings.VERSION);

            Dictionary<VSTPanel, String> plugList = new Dictionary<VSTPanel, string>();
            int count = 1;
            foreach (VSTPanel panel in panels)
            {
                String plugName = "plugin-" + count.ToString().PadLeft(3, '0');
                rigData.setStringValue("plugin-list." + plugName + ".path", panel.plugPath);
                rigData.setStringValue("plugin-list." + plugName + ".audio-out", panel.audioOut);
                rigData.setStringValue("plugin-list." + plugName + ".midi-in", 
                    ((panel.midiInDevice != null) ? panel.midiInDevice.devName : "no input"));
                plugList[panel] = plugName;
                count++;
            }
            count = 1;
            foreach (Patch patch in patches)
            {
                String patname = "patch-" + count.ToString().PadLeft(3, '0');
                rigData.setStringValue("patch-list." + patname + ".name", patch.name);
                foreach (VSTPanel panel in patch.panels.Keys)
                {
                    rigData.setIntValue("patch-list." + patname + "." + plugList[panel], patch.panels[panel]);
                }
                count++;
            }
            rigData.saveToFile(filename);
            hasChanged = false;
        }
    }

    //-------------------------------------------------------------------------

    public class Patch
    {
        public string name;
        public Dictionary<VSTPanel, int> panels;

        public Patch(string _name)
        {            
            name = _name;
            panels = new Dictionary<VSTPanel, int>();
        }

        public void addPanel(VSTPanel panel, int patchNum)
        {
            panels.Add(panel, patchNum);
        }

        public void removePanel(VSTPanel panel)
        {
            panels.Remove(panel);
        }
    }
}

//  Console.WriteLine(" there's no sun in the shadow of the wizard");
