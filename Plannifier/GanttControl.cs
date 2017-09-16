
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Plannifier.Components;

namespace Plannifier
{
    public partial class GanttControl : UserControl
    {
        private enum EMouseOperation
        {
            None,
            MoveSheet,
            ScaleSheet,
            MoveTaskStart,
            MoveTaskEnd,
            MoveWholeTask
        }

        private enum ETaskPart
        {
            None,
            Start,
            Middle,
            End
        }

        private GanttProperties _Properties;
        private Workspace _TheWorkspace;
        private double _Scale;
        private EMouseOperation _Operation;
        private Point _MousePoint;
        private List<TaskVisuals> _VisibleTasks;
        private Project _SelectedProject;
        private ProjectTask _SelectedTask;

        private Brush _Brush_Background;
        private Brush _Brush_Name;
        private Brush _Brush_Container;
        private Brush _Brush_Project_Warning;
        private Brush _Brush_Task;
        private Brush _Brush_Task_Fixed;
        private Brush _Brush_Marker;
        private Brush _Brush_Marker_Warning;
        private Brush _Brush_Trigram;
        private Brush _Brush_Day_Light;
        private Brush _Brush_Day_Today;

        private Pen _Pen_Day;
        private Pen _Pen_Day_Light;
        private Pen _Pen_Selected_Task;

        private Font _Font_Days;
        private Font _Font_Name;

        private const int DateBarHeight = 40;

        /// <summary>
        /// 
        /// </summary>
        public GanttProperties Properties
        {
            get { return _Properties; }
            set { _Properties = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GanttControl()
        {
            InitializeComponent();

            _Properties = new GanttProperties();
            _Properties.StartDay = Utils.GetMidnightDateTime(DateTime.Now);
            _Properties.StartRow = 0;
            _Scale = 15.0;
            _Operation = EMouseOperation.None;
            _VisibleTasks = new List<TaskVisuals>();
            _SelectedProject = null;
            _SelectedTask = null;

            _Brush_Background = new SolidBrush(Color.White);
            _Brush_Name = new SolidBrush(Color.Black);
            _Brush_Container = new SolidBrush(Color.Black);
            _Brush_Project_Warning = new SolidBrush(Color.Red);
            _Brush_Task = new SolidBrush(Color.Blue);
            _Brush_Task_Fixed = new SolidBrush(Color.Gray);
            _Brush_Marker = new SolidBrush(Color.Red);
            _Brush_Marker_Warning = new SolidBrush(Color.Orange);
            _Brush_Trigram = new SolidBrush(Color.White);
            _Brush_Day_Light = new SolidBrush(Color.LightGray);
            _Brush_Day_Today = new SolidBrush(Color.LightGreen);

            _Pen_Day = new Pen(Color.Black, 1.0f);
            _Pen_Day_Light = new Pen(Color.LightGray, 1.0f);
            _Pen_Selected_Task = new Pen(Color.Red, 2.0f);

            ScaleFonts();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ScaleFonts()
        {
            int FontSize = (int)(_Scale * 0.6);
            if (FontSize > 10) FontSize = 10;
            if (FontSize < 2) FontSize = 2;

            _Font_Days = new Font("Arial", FontSize);
            _Font_Name = new Font("Arial", 10);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        public void setWorkspace(Workspace AWorkspace)
        {
            _TheWorkspace = AWorkspace;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // base.OnPaintBackground(e);

            e.Graphics.FillRectangle(_Brush_Background, ClientRectangle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int SizeY = 40;
            int SpacingY = 10;
            int CurrentY = _Properties.StartRow + DateBarHeight + SpacingY;

            PaintDayBars(e);

            _VisibleTasks.Clear();

            foreach (Project AProject in _TheWorkspace.Projects)
            {
                if (CurrentY + SizeY > 0)
                {
                    Rectangle AProjectRectangle = new Rectangle(0, CurrentY, ClientSize.Width, SizeY);
                    PaintProjectBar(e, AProjectRectangle, AProject);
                }

                CurrentY += SizeY + SpacingY;

                foreach (ProjectTask ATask in AProject.Tasks)
                {
                    if (CurrentY > ClientSize.Height) break;

                    if (CurrentY + SizeY > 0)
                    {
                        Rectangle ATaskRectangle = new Rectangle(0, CurrentY, ClientSize.Width, SizeY);

                        PaintTaskBar(e, ATaskRectangle, AProject, ATask);
                    }

                    CurrentY += SizeY + SpacingY;
                }
            }

            PaintDayLabels(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PaintDayBars(PaintEventArgs e)
        {
            DateTime Day = _Properties.StartDay;

            while (true)
            {
                int X1 = GetPositionForTime(Day);
                int X2 = GetPositionForTime(Day.AddDays(1.0));
                if (X1 > ClientSize.Width) break;

                if (!(X1 == 0 && X2 == 0))
                {
                    if (Day.Date == DateTime.Now.Date)
                    {
                        e.Graphics.FillRectangle(_Brush_Day_Today, X1, 0, X2 - X1, ClientSize.Height);
                    }
                    else if (Day.DayOfWeek == DayOfWeek.Saturday || Day.DayOfWeek == DayOfWeek.Sunday)
                    {
                        e.Graphics.FillRectangle(_Brush_Day_Light, X1, 0, X2 - X1, ClientSize.Height);
                    }

                    if (Day.DayOfWeek == DayOfWeek.Monday)
                    {
                        e.Graphics.DrawLine(_Pen_Day, X1, 0, X1, ClientSize.Height);
                    }
                    else
                    {
                        e.Graphics.DrawLine(_Pen_Day_Light, X1, 0, X1, ClientSize.Height);
                    }
                }

                Day = Day.AddDays(1.0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void PaintDayLabels(PaintEventArgs e)
        {
            DateTime Day = _Properties.StartDay.AddDays(-7.0);

            while (true)
            {
                int X1 = GetPositionForTime(Day);
                int X2 = GetPositionForTime(Day.AddDays(7.0));
                int Y1 = 0;
                int Y2 = DateBarHeight;
                if (X1 > ClientSize.Width) break;

                if (Day.DayOfWeek == DayOfWeek.Monday)
                {
                    Rectangle AWeekRectangle = new Rectangle(X1, Y1, X2 - X1, Y2 - Y1);

                    e.Graphics.FillRectangle(_Brush_Day_Light, AWeekRectangle);
                    e.Graphics.DrawRectangle(_Pen_Day, AWeekRectangle);

                    AWeekRectangle = new Rectangle(X1, Y1, X2 - X1, (Y2 - Y1) / 2);

                    String Text1 = Day.ToShortDateString();

                    DrawCenteredText(e, Text1, AWeekRectangle, _Font_Days, _Brush_Name);
                }

                X2 = GetPositionForTime(Day.AddDays(1.0));
                Rectangle ADayRectangle = new Rectangle(X1, Y1 + ((Y2 - Y1) / 2), X2 - X1, (Y2 - Y1) / 2);

                e.Graphics.FillRectangle(_Brush_Day_Light, ADayRectangle);
                e.Graphics.DrawRectangle(_Pen_Day, ADayRectangle);

                String Text2 = Day.DayOfWeek.ToString().Substring(0, 1);

                DrawCenteredText(e, Text2, ADayRectangle, _Font_Days, _Brush_Name);

                Day = Day.AddDays(1.0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AText"></param>
        /// <param name="ARectangle"></param>
        /// <param name="AFont"></param>
        /// <param name="ABrush"></param>
        public void DrawCenteredText(PaintEventArgs e, String AText, Rectangle ARectangle, Font AFont, Brush ABrush)
        {
            SizeF TextSize = e.Graphics.MeasureString(AText, AFont);
            PointF TextPosition = new PointF(
                ((float)ARectangle.Left + ((float)ARectangle.Width / 2.0f) - (TextSize.Width / 2.0f)),
                ((float)ARectangle.Top + ((float)ARectangle.Height / 2.0f) - (TextSize.Height / 2.0f))
                );
            e.Graphics.DrawString(AText, AFont, ABrush, TextPosition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ARectangle"></param>
        /// <param name="AProject"></param>
        public void PaintProjectBar(PaintEventArgs e, Rectangle ARectangle, Project AProject)
        {
            int SX = GetPositionForTime(AProject.Start);
            int EX = GetPositionForTime(AProject.End);

            PointF TextPosition = new PointF(SX, ARectangle.Top);

            e.Graphics.DrawString(AProject.Name, _Font_Name, _Brush_Name, TextPosition);

            int BarSizeY = ARectangle.Height / 4;

            Rectangle Bar = new Rectangle(SX, ARectangle.Bottom - BarSizeY, EX - SX, BarSizeY);

            if (AProject.End <= AProject.Delivery)
            {
                e.Graphics.FillRectangle(_Brush_Container, Bar);
            }
            else
            {
                e.Graphics.FillRectangle(_Brush_Project_Warning, Bar);

                SX = GetPositionForTime(AProject.Delivery);

                int MarkerSize = ARectangle.Height / 2;
                int MiddleY = ARectangle.Top + ARectangle.Height / 2;

                Rectangle MarkerRectangle = new Rectangle(SX - MarkerSize / 2, MiddleY, MarkerSize, MarkerSize);

                Point[] Triangle = new Point[3];

                Triangle[0] = new Point(SX - MarkerSize / 2, MiddleY + MarkerSize / 2);
                Triangle[1] = new Point(SX + MarkerSize / 2, MiddleY + MarkerSize / 2);
                Triangle[2] = new Point(SX, MiddleY - MarkerSize / 2);

                e.Graphics.FillPolygon(_Brush_Marker_Warning, Triangle);
                e.Graphics.DrawPolygon(_Pen_Day, Triangle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ARectangle"></param>
        /// <param name="ATask"></param>
        public void PaintTaskBar(PaintEventArgs e, Rectangle ARectangle, Project AProject, ProjectTask ATask)
        {
            int SX = GetPositionForTime(ATask.Start);
            int EX = GetPositionForTime(ATask.End);

            PointF TextPosition = new PointF(SX, ARectangle.Top);

            e.Graphics.DrawString(ATask.Name, _Font_Name, _Brush_Name, TextPosition);

            if (ATask.Marker == true)
            {
                int MarkerSize = ARectangle.Height / 2;
                int MiddleY = ARectangle.Top + ARectangle.Height / 2;

                Rectangle MarkerRectangle = new Rectangle(SX - MarkerSize / 2, MiddleY, MarkerSize, MarkerSize);

                e.Graphics.FillEllipse(_Brush_Marker, MarkerRectangle);
            }
            else
            {
                if (AProject.TaskHasChildren(ATask) == true)
                {
                    int BarSizeY = ARectangle.Height / 4;

                    Rectangle Bar = new Rectangle(SX, ARectangle.Bottom - BarSizeY, EX - SX, BarSizeY);

                    e.Graphics.FillRectangle(_Brush_Container, Bar);
                }
                else
                {
                    int BarSizeY = ARectangle.Height / 2;

                    Brush FillBrush = _Brush_Task;

                    if (ATask.Fixed)
                    {
                        FillBrush = _Brush_Task_Fixed;
                    }

                    Rectangle Bar = new Rectangle(SX, ARectangle.Bottom - BarSizeY, EX - SX, BarSizeY);
                    PointF TrigramPosition = new PointF(Bar.Left, Bar.Top);

                    _VisibleTasks.Add(new TaskVisuals(AProject, ATask, Bar));

                    e.Graphics.DrawString(ATask.AssignedWorker, _Font_Name, _Brush_Name, TrigramPosition);
                    e.Graphics.FillRectangle(FillBrush, Bar);

                    EX = SX + (int)((((double)(EX - SX)) / 100.0) * ATask.PercentComplete);
                    Rectangle CompleteBar = new Rectangle(SX, ARectangle.Bottom - BarSizeY + 4, EX - SX, BarSizeY - 8);
                    e.Graphics.FillRectangle(_Brush_Container, CompleteBar);

                    e.Graphics.DrawString(ATask.AssignedWorker, _Font_Name, _Brush_Trigram, new PointF(TrigramPosition.X + 1.0f, TrigramPosition.Y + 1.0f));

                    if (_SelectedTask == ATask)
                    {
                        e.Graphics.DrawRectangle(_Pen_Selected_Task, Bar);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ADate"></param>
        /// <returns></returns>
        public int GetPositionForTime(DateTime ADate)
        {
            return (int)((double)(Utils.GetMidnightDateTime(ADate) - _Properties.StartDay).Days * _Scale);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="APoint"></param>
        private ETaskPart SelectTask(Point APoint)
        {
            foreach (TaskVisuals ATask in _VisibleTasks)
            {
                if (ATask.Rect.Contains(APoint))
                {
                    _SelectedTask = ATask.Task;
                    _SelectedProject = ATask.Project;

                    Invalidate();

                    int DistanceFromStart = Math.Abs(APoint.X - ATask.Rect.Left);
                    int DistanceFromEnd = Math.Abs(APoint.X - ATask.Rect.Right);

                    if (DistanceFromStart < 10) return ETaskPart.Start;
                    if (DistanceFromEnd < 10) return ETaskPart.End;

                    return ETaskPart.Middle;
                }
            }

            return ETaskPart.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AProject"></param>
        /// <param name="ATask"></param>
        private void EditTask(Project AProject, ProjectTask ATask)
        {
            TaskDialog Dialog = new TaskDialog(AProject, ATask);

            if (Dialog.ShowDialog(this) == DialogResult.OK)
            {
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AProject"></param>
        /// <param name="ATask"></param>
        private void ToggleTaskFixed(Project AProject, ProjectTask ATask)
        {
            if (AProject.SetTaskFixed(ATask, !ATask.Fixed))
            {
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AProject"></param>
        /// <param name="ATask"></param>
        private void DeleteTask(Project AProject, ProjectTask ATask)
        {
            if (AProject.DeleteTask(ATask))
            {
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GanttControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ETaskPart Part = SelectTask(new Point(e.X, e.Y));

                if (Part == ETaskPart.Start)
                {
                    _Operation = EMouseOperation.MoveTaskStart;
                }
                else if (Part == ETaskPart.End)
                {
                    _Operation = EMouseOperation.MoveTaskEnd;
                }
                else if (Part == ETaskPart.Middle)
                {
                    _Operation = EMouseOperation.MoveWholeTask;
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                SelectTask(new Point(e.X, e.Y));

                if (_SelectedTask != null)
                {
                    Menu.Show(Cursor.Position);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                _Operation = EMouseOperation.MoveSheet;
            }

            _MousePoint = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GanttControl_MouseUp(object sender, MouseEventArgs e)
        {
            _Operation = EMouseOperation.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GanttControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Operation != EMouseOperation.None)
            {
                Point Delta = new Point(e.X - _MousePoint.X, e.Y - _MousePoint.Y);

                switch (_Operation)
                {
                    case EMouseOperation.MoveSheet:
                        {
                            double Days = (double)Delta.X / _Scale;
                            _Properties.StartDay = _Properties.StartDay.AddDays(-Days);

                            _Properties.StartRow += Delta.Y;
                            if (_Properties.StartRow > 0) _Properties.StartRow = 0;

                            Invalidate();
                        }
                        break;

                    case EMouseOperation.MoveTaskStart:
                        {
                            if (_SelectedProject != null && _SelectedTask != null)
                            {
                                double Days = (double)Delta.X / _Scale;

                                _SelectedProject.SetTaskStart(_SelectedTask, _SelectedTask.Start.AddDays(Days));

                                Invalidate();
                            }
                        }
                        break;

                    case EMouseOperation.MoveTaskEnd:
                        {
                            if (_SelectedProject != null && _SelectedTask != null)
                            {
                                double Days = (double)Delta.X / _Scale;

                                _SelectedProject.SetTaskEnd(_SelectedTask, _SelectedTask.End.AddDays(Days));

                                Invalidate();
                            }
                        }
                        break;

                    case EMouseOperation.MoveWholeTask:
                        {
                            if (_SelectedProject != null && _SelectedTask != null)
                            {
                                double Days = (double)Delta.X / _Scale;
                                DateTime NewStart = _SelectedTask.Start.AddDays(Days);
                                DateTime NewEnd = _SelectedTask.End.AddDays(Days);

                                _SelectedProject.SetTaskStart(_SelectedTask, NewStart);
                                _SelectedProject.SetTaskEnd(_SelectedTask, NewEnd);

                                Invalidate();
                            }
                        }
                        break;
                }

                _MousePoint = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GanttControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Add:
                    {
                        _Scale *= 1.5;
                        ScaleFonts();
                        Invalidate();
                    }
                    break;

                case Keys.Subtract:
                    {
                        _Scale /= 1.5;
                        ScaleFonts();
                        Invalidate();
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_SelectedProject != null && _SelectedTask != null)
            {
                EditTask(_SelectedProject, _SelectedTask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fixedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_SelectedProject != null && _SelectedTask != null)
            {
                ToggleTaskFixed(_SelectedProject, _SelectedTask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_SelectedProject != null && _SelectedTask != null)
            {
                DeleteTask(_SelectedProject, _SelectedTask);
            }
        }
    }

    public class TaskVisuals
    {
        private Project _Project;
        private ProjectTask _Task;
        private Rectangle _Rect;

        /// <summary>
        /// 
        /// </summary>
        public Project Project
        {
            get { return _Project; }
            set { _Project = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ProjectTask Task
        {
            get { return _Task; }
            set { _Task = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Rectangle Rect
        {
            get { return _Rect; }
            set { _Rect = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewTask"></param>
        /// <param name="NewRect"></param>
        public TaskVisuals(Project NewProject, ProjectTask NewTask, Rectangle NewRect)
        {
            _Project = NewProject;
            _Task = NewTask;
            _Rect = NewRect;
        }
    }

    public class GanttProperties
    {
        private DateTime _StartDay;
        private int _StartRow;

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDay
        {
            get { return _StartDay; }
            set { _StartDay = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartRow
        {
            get { return _StartRow; }
            set { _StartRow = value; }
        }
    }
}
