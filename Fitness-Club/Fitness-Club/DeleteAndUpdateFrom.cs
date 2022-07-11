﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitness_Club
{
    public partial class DeleteAndUpdateFrom : Form
    {
        public DeleteAndUpdateFrom()
        {
            InitializeComponent();
        }

        private void DeleteAndUpdateFrom_Load(object sender, EventArgs e)
        {
            loadTheme();
        }
        private void loadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))        //if btn prv is button
                {
                    Button btn = (Button)btns;
                    btns.BackColor = ThemColor.primaryColor;
                    btns.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemColor.secondColor;
                }
            }
            lblTitle.ForeColor = ThemColor.secondColor;

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
