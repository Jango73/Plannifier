namespace Plannifier
{
    partial class TaskDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.TaskName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ModuleName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AssignedWorker = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Priority = new System.Windows.Forms.ComboBox();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.DeliveryLimit = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DependsOnTask = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DaysToComplete = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Fixed = new System.Windows.Forms.CheckBox();
            this.DaysRemaining = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.End = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.PercentComplete = new System.Windows.Forms.TextBox();
            this.Marker = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // TaskName
            // 
            this.TaskName.Location = new System.Drawing.Point(181, 6);
            this.TaskName.Name = "TaskName";
            this.TaskName.Size = new System.Drawing.Size(284, 20);
            this.TaskName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Module";
            // 
            // ModuleName
            // 
            this.ModuleName.Location = new System.Drawing.Point(181, 32);
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.Size = new System.Drawing.Size(284, 20);
            this.ModuleName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Worker";
            // 
            // AssignedWorker
            // 
            this.AssignedWorker.Location = new System.Drawing.Point(181, 58);
            this.AssignedWorker.Name = "AssignedWorker";
            this.AssignedWorker.Size = new System.Drawing.Size(284, 20);
            this.AssignedWorker.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Priority";
            // 
            // Priority
            // 
            this.Priority.FormattingEnabled = true;
            this.Priority.Location = new System.Drawing.Point(181, 84);
            this.Priority.Name = "Priority";
            this.Priority.Size = new System.Drawing.Size(121, 21);
            this.Priority.TabIndex = 7;
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(309, 420);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 24;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(390, 420);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 25;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // DeliveryLimit
            // 
            this.DeliveryLimit.Location = new System.Drawing.Point(181, 111);
            this.DeliveryLimit.Name = "DeliveryLimit";
            this.DeliveryLimit.Size = new System.Drawing.Size(200, 20);
            this.DeliveryLimit.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Delivery limit";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Depends on";
            // 
            // DependsOnTask
            // 
            this.DependsOnTask.FormattingEnabled = true;
            this.DependsOnTask.Location = new System.Drawing.Point(181, 137);
            this.DependsOnTask.Name = "DependsOnTask";
            this.DependsOnTask.Size = new System.Drawing.Size(284, 21);
            this.DependsOnTask.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Days to complete";
            // 
            // DaysToComplete
            // 
            this.DaysToComplete.Location = new System.Drawing.Point(181, 164);
            this.DaysToComplete.Name = "DaysToComplete";
            this.DaysToComplete.Size = new System.Drawing.Size(121, 20);
            this.DaysToComplete.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Days remaining";
            // 
            // Fixed
            // 
            this.Fixed.AutoSize = true;
            this.Fixed.Location = new System.Drawing.Point(181, 190);
            this.Fixed.Name = "Fixed";
            this.Fixed.Size = new System.Drawing.Size(51, 17);
            this.Fixed.TabIndex = 14;
            this.Fixed.Text = "Fixed";
            this.Fixed.UseVisualStyleBackColor = true;
            // 
            // DaysRemaining
            // 
            this.DaysRemaining.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DaysRemaining.Location = new System.Drawing.Point(181, 210);
            this.DaysRemaining.Name = "DaysRemaining";
            this.DaysRemaining.Size = new System.Drawing.Size(121, 19);
            this.DaysRemaining.TabIndex = 17;
            this.DaysRemaining.Text = "0";
            this.DaysRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 242);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Start";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(181, 235);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(200, 20);
            this.Start.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 268);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "End";
            // 
            // End
            // 
            this.End.Location = new System.Drawing.Point(181, 261);
            this.End.Name = "End";
            this.End.Size = new System.Drawing.Size(200, 20);
            this.End.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 290);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Percent complete";
            // 
            // PercentComplete
            // 
            this.PercentComplete.Location = new System.Drawing.Point(181, 287);
            this.PercentComplete.Name = "PercentComplete";
            this.PercentComplete.Size = new System.Drawing.Size(121, 20);
            this.PercentComplete.TabIndex = 23;
            // 
            // Marker
            // 
            this.Marker.AutoSize = true;
            this.Marker.Location = new System.Drawing.Point(385, 190);
            this.Marker.Name = "Marker";
            this.Marker.Size = new System.Drawing.Size(59, 17);
            this.Marker.TabIndex = 15;
            this.Marker.Text = "Marker";
            this.Marker.UseVisualStyleBackColor = true;
            // 
            // TaskDialog
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(479, 455);
            this.ControlBox = false;
            this.Controls.Add(this.Marker);
            this.Controls.Add(this.PercentComplete);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.End);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.DaysRemaining);
            this.Controls.Add(this.Fixed);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DaysToComplete);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DependsOnTask);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DeliveryLimit);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Priority);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AssignedWorker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ModuleName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TaskName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskDialog";
            this.Text = "Task";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TaskName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ModuleName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AssignedWorker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Priority;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.DateTimePicker DeliveryLimit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox DependsOnTask;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DaysToComplete;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox Fixed;
        private System.Windows.Forms.Label DaysRemaining;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker Start;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker End;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox PercentComplete;
        private System.Windows.Forms.CheckBox Marker;
    }
}