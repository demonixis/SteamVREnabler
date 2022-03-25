using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

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

        private readonly string _SteamInstallPath = $@"{Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam").GetValue("SteamPath")?? @"C:/Program Files (x86)/Steam"}/steamapps/common"; //:Gets SteamDirectory from windows registry or sets default if not found
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
                        _SteamInstallPath = item.First.ToString().Slice(0,"SteamVR")?? item.First.ToString(); //:both cases ("SteamVR" and "SteamVR/") are taken into account while udsing this function
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

    public static class Functions
    {
	    public static string Slice(this string s, int Start, string EndsWith)
	    {
		    var end =s.LastIndexOf(EndsWith);
		    if (end < 0) return null;

		    if (Start > end) throw new ArgumentException($"start ({Start}) is be bigger than end ({end})");

		    return s.Slice(Start, end);

	    }

	    /// <summary>
	    /// Slices the string form Start to End not including End
	    /// </summary>
	    public static string Slice(this string s, int Start = 0, int End = Int32.MaxValue)
	    {
		    if (Start < 0) throw new ArgumentOutOfRangeException($"Start is {Start}");
		    if (Start > End) throw new ArgumentException($"start ({Start}) is be bigger than end ({End})");
		    if (End > s.Length) End = s.Length;
		    return s.Substring(Start, End - Start);
	    }
    }
}
