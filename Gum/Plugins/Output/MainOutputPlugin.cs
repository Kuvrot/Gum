﻿using Gum.Managers;
using Gum.Plugins.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gum.Plugins.Output
{
    [Export(typeof(PluginBase))]
    class MainOutputPlugin : InternalPlugin
    {
        public override void StartUp()
        {
            InitializeOutputTextBox();

        }

        private void InitializeOutputTextBox()
        {
            var outputTextBox = new System.Windows.Forms.RichTextBox();

            // 
            // OutputTextBox
            // 
            outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            outputTextBox.Location = new System.Drawing.Point(3, 3);
            outputTextBox.Name = "OutputTextBox";
            outputTextBox.Size = new System.Drawing.Size(526, 78);
            outputTextBox.TabIndex = 0;
            outputTextBox.Text = "";

            GumCommands.Self.GuiCommands.AddWinformsControl(outputTextBox, "Output", TabLocation.RightBottom);
            OutputManager.Self.Initialize(outputTextBox);
        }
    }
}
