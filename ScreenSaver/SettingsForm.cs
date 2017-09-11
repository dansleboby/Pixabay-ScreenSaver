/*
 * SettingsForm.cs
 * By Frank McCown
 * Summer 2010
 * 
 * Feel free to modify this code.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Permissions;

namespace ScreenSaver
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        /// <summary>
        /// Load display text from the Registry
        /// </summary>
        private void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ScreenSaverPixabay");
            if (key == null)
            {
                textBox.Text = "20";
            }
            else
            {
                if ((string)key.GetValue("seconds") == "")
                    textBox.Text = "20";
                else
                    textBox.Text = (string)key.GetValue("seconds");

                if ((string)key.GetValue("editors_choice") == "")
                    checkBoxEditorChoice.Checked = false;
                else
                    checkBoxEditorChoice.Checked = (string)key.GetValue("editors_choice") == "1" ? true : false;
            }

            if (key != null && key.GetValue("categories") != null)
            {
                string[] categories = key.GetValue("categories").ToString().Split('|');
                for (int count = 0; count < checkedListBox1.Items.Count; count++)
                {
                    if (categories.Contains(checkedListBox1.Items[count].ToString()))
                    {
                        checkedListBox1.SetItemChecked(count, true);
                    }
                }
            }
        }

        /// <summary>
        /// Save text into the Registry.
        /// </summary>
        private void SaveSettings()
        {
            // Create or get existing subkey
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ScreenSaverPixabay");

            key.SetValue("seconds", textBox.Text);

            string categories = "";
            foreach (var item in checkedListBox1.CheckedItems) {
                categories = categories + "|" + item.ToString();
            }
            key.SetValue("categories", categories.Substring(1));

            key.SetValue("editors_choice", (checkBoxEditorChoice.Checked ? "1" : "0"));

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
