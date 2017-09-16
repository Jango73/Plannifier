namespace Plannifier
{
    partial class ProjectControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.DeliveryLimit = new System.Windows.Forms.DateTimePicker();
            this.NewTask = new System.Windows.Forms.Button();
            this.View = new System.Windows.Forms.ListView();
            this.Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTaskBeforeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTaskAfterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.View, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 500);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.DeliveryLimit);
            this.flowLayoutPanel1.Controls.Add(this.NewTask);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(994, 34);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delivery";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeliveryLimit
            // 
            this.DeliveryLimit.Location = new System.Drawing.Point(109, 3);
            this.DeliveryLimit.Name = "DeliveryLimit";
            this.DeliveryLimit.Size = new System.Drawing.Size(200, 20);
            this.DeliveryLimit.TabIndex = 1;
            this.DeliveryLimit.ValueChanged += new System.EventHandler(this.DeliveryLimit_ValueChanged);
            // 
            // NewTask
            // 
            this.NewTask.Location = new System.Drawing.Point(315, 3);
            this.NewTask.Name = "NewTask";
            this.NewTask.Size = new System.Drawing.Size(75, 23);
            this.NewTask.TabIndex = 2;
            this.NewTask.Text = "New task";
            this.NewTask.UseVisualStyleBackColor = true;
            this.NewTask.Click += new System.EventHandler(this.NewTask_Click);
            // 
            // View
            // 
            this.View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.View.Location = new System.Drawing.Point(3, 43);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(994, 454);
            this.View.TabIndex = 2;
            this.View.UseCompatibleStateImageBehavior = false;
            this.View.View = System.Windows.Forms.View.Details;
            this.View.DoubleClick += new System.EventHandler(this.View_DoubleClick);
            this.View.KeyDown += new System.Windows.Forms.KeyEventHandler(this.View_KeyDown);
            this.View.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.View_KeyPress);
            this.View.MouseDown += new System.Windows.Forms.MouseEventHandler(this.View_MouseDown);
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTaskBeforeToolStripMenuItem,
            this.addTaskAfterToolStripMenuItem,
            this.moveLeftToolStripMenuItem,
            this.moveRightToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(203, 158);
            // 
            // addTaskBeforeToolStripMenuItem
            // 
            this.addTaskBeforeToolStripMenuItem.Name = "addTaskBeforeToolStripMenuItem";
            this.addTaskBeforeToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addTaskBeforeToolStripMenuItem.Text = "Add task before";
            this.addTaskBeforeToolStripMenuItem.Click += new System.EventHandler(this.addTaskBeforeToolStripMenuItem_Click);
            // 
            // addTaskAfterToolStripMenuItem
            // 
            this.addTaskAfterToolStripMenuItem.Name = "addTaskAfterToolStripMenuItem";
            this.addTaskAfterToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addTaskAfterToolStripMenuItem.Text = "Add task after";
            this.addTaskAfterToolStripMenuItem.Click += new System.EventHandler(this.addTaskAfterToolStripMenuItem_Click);
            // 
            // moveLeftToolStripMenuItem
            // 
            this.moveLeftToolStripMenuItem.Name = "moveLeftToolStripMenuItem";
            this.moveLeftToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.moveLeftToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveLeftToolStripMenuItem.Text = "Move left";
            this.moveLeftToolStripMenuItem.Click += new System.EventHandler(this.moveLeftToolStripMenuItem_Click);
            // 
            // moveRightToolStripMenuItem
            // 
            this.moveRightToolStripMenuItem.Name = "moveRightToolStripMenuItem";
            this.moveRightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.moveRightToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveRightToolStripMenuItem.Text = "Move right";
            this.moveRightToolStripMenuItem.Click += new System.EventHandler(this.moveRightToolStripMenuItem_Click);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveUpToolStripMenuItem.Text = "Move up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveDownToolStripMenuItem.Text = "Move down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(1000, 500);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DeliveryLimit;
        private System.Windows.Forms.ListView View;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem addTaskAfterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTaskBeforeToolStripMenuItem;
        private System.Windows.Forms.Button NewTask;
        private System.Windows.Forms.ToolStripMenuItem moveLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
    }
}
