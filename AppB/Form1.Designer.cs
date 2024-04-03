namespace AppB
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openDoorButton = new System.Windows.Forms.Button();
            this.closeDoorButton = new System.Windows.Forms.Button();
            this.doorControlPanelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openDoorButton
            // 
            this.openDoorButton.BackColor = System.Drawing.Color.DarkGreen;
            this.openDoorButton.ForeColor = System.Drawing.Color.White;
            this.openDoorButton.Location = new System.Drawing.Point(89, 141);
            this.openDoorButton.Name = "openDoorButton";
            this.openDoorButton.Size = new System.Drawing.Size(244, 112);
            this.openDoorButton.TabIndex = 0;
            this.openDoorButton.Text = "Open Door";
            this.openDoorButton.UseVisualStyleBackColor = false;
            this.openDoorButton.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // closeDoorButton
            // 
            this.closeDoorButton.BackColor = System.Drawing.Color.Maroon;
            this.closeDoorButton.ForeColor = System.Drawing.Color.Snow;
            this.closeDoorButton.Location = new System.Drawing.Point(444, 141);
            this.closeDoorButton.Name = "closeDoorButton";
            this.closeDoorButton.Size = new System.Drawing.Size(240, 112);
            this.closeDoorButton.TabIndex = 1;
            this.closeDoorButton.Text = "Close Door";
            this.closeDoorButton.UseVisualStyleBackColor = false;
            this.closeDoorButton.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // doorControlPanelLabel
            // 
            this.doorControlPanelLabel.AutoSize = true;
            this.doorControlPanelLabel.Location = new System.Drawing.Point(337, 23);
            this.doorControlPanelLabel.Name = "doorControlPanelLabel";
            this.doorControlPanelLabel.Size = new System.Drawing.Size(96, 13);
            this.doorControlPanelLabel.TabIndex = 2;
            this.doorControlPanelLabel.Text = "Door Control Panel";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.doorControlPanelLabel);
            this.Controls.Add(this.closeDoorButton);
            this.Controls.Add(this.openDoorButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openDoorButton;
        private System.Windows.Forms.Button closeDoorButton;
        private System.Windows.Forms.Label doorControlPanelLabel;
    }
}

