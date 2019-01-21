using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SteamVRSwitcher
{
    public partial class Form1 : Form
    {
        private const string Version = "1.0.0.0";

        private readonly string[] _SteamVRProcesses =
        {
            "vrdashboard", "vrserver",
            "vrmonitor", "vrcompositor",
            "steamvr_tutorial", "steamtours",
            "vrwebhelper"
        };

        private readonly string _SteamInstallPath = @"D:\Jeux\Steam\steamapps\common";
        private readonly string _SteamVREnabledPath;
        private readonly string _SteamVRDisabledPath;

        public bool IsEnabled => Directory.Exists(Path.Combine(_SteamInstallPath, "SteamVR"));
        public bool IsDisabled => Directory.Exists(Path.Combine(_SteamInstallPath, "_SteamVR"));

        public Form1()
        {
            InitializeComponent();

            var openvrpaths = $@"c:\Users\{Environment.UserName}\AppData\Local\openvr\openvrpaths.vrpath";

            if (File.Exists(openvrpaths))
            {
                var content = File.ReadAllText(openvrpaths);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                if (json.ContainsKey("runtime"))
                {
                    var item = (JArray)json["runtime"];
                    if (item != null)
                        _SteamInstallPath = item.First.ToString().Replace("SteamVR\\", "");
                }
            }

            _SteamVRDisabledPath = Path.Combine(_SteamInstallPath, "_SteamVR");
            _SteamVREnabledPath = Path.Combine(_SteamInstallPath, "SteamVR");

            LabelToggleStatus.Text = IsEnabled ? "Enabled" : "Disabled";
            LabelSteamVRStatus.Text = IsSteamVRActive() ? "Active" : "Stopped";

            Text += $" {Version}";
        }

        private void OnToggleButtonClick(object sender, EventArgs e)
        {
            var enabledPath = IsEnabled;
            var disabledPath = IsDisabled;

            if (!enabledPath && !disabledPath)
            {
                MessageBox.Show("SteamVR is not installed on this PC.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            KillSteamVR();

            try
            {
                if (enabledPath)
                    Directory.Move(_SteamVREnabledPath, _SteamVRDisabledPath);
                else
                    Directory.Move(_SteamVRDisabledPath, _SteamVREnabledPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during change", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LabelToggleStatus.Text = !enabledPath ? "Enabled" : "Disabled";
        }

        private void OnKillButtonClick(object sender, EventArgs e) => KillSteamVR();

        private void KillSteamVR()
        {
            foreach (var name in _SteamVRProcesses)
            {
                var processes = Process.GetProcessesByName(name);
                if (processes.Length == 0)
                    continue;

                foreach (var process in processes)
                    process.Kill();
            }

            LabelSteamVRStatus.Text = "Stopped";
        }

        private bool IsSteamVRActive()
        {
            foreach (var name in _SteamVRProcesses)
            {
                var processes = Process.GetProcessesByName(name);
                if (processes.Length > 0)
                    return true;
            }

            return false;
        }
    }
}
