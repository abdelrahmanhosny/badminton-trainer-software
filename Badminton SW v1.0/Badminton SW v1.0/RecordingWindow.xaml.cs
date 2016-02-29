using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Timers;
using System.Windows.Threading;

namespace Badminton_SW_v1._0
{
    /// <summary>
    /// Interaction logic for RecordingWindow.xaml
    /// </summary>
    public partial class RecordingWindow : Window
    {
        private const float RenderWidth = 650.0f;
        private const float RenderHeight = 510.0f;

        private const double JointThickness = 8.0;
        private const double BodyCenterThickness = 10;
        private const double ClipBoundsThickness = 10;
        private readonly Brush centerPointBrush = Brushes.Black;
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private readonly Brush inferredJointBrush = Brushes.Yellow;
        private readonly Pen trackedBonePen = new Pen(Brushes.White, 9);
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
        private KinectSensor sensor;

        private DrawingGroup drawingGroup;
        private DrawingImage imageSource;

        private Skeleton mySkeleton;
        private List<Skeleton> mySkeletonList;
        private float timeStamp = 0.0f;
        private int skeletonCount = 0;

        //timer function
        private DispatcherTimer recordTimer;
        private int recordingTime = 5;         //record skeleton motion for 5 second long

        private bool recordingStatus;           //trigger serialization
        private int countDown;
        private Stream stream;

        private ReplayWindow replayWindow;
        public RecordingWindow()
        {
            InitializeComponent();
            this.Show();
            mySkeletonList = new List<Skeleton>();
            mySkeleton = new Skeleton();
            recordingStatus = false;
        }
        public void WindowLoaded(object sender, RoutedEventArgs e)
        {
            drawingGroup = new DrawingGroup();
            imageSource = new DrawingImage(this.drawingGroup);
            imgLiveImage.Source = this.imageSource;

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                try
                {
                    // Start the sensor!
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            { /*this.statusBarText.Text = Properties.Resources.NoKinectReady;  */         }
        }
        private void RecordingWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }

                // Find first tracked skeleton, if any
                //s is an element of skeletons, which is skeleton.
                Skeleton skeleton = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                if (skeleton != null & recordingStatus)
                {
                    mySkeleton = new Skeleton();
                    timeStamp = skeletonFrame.Timestamp;
                    mySkeleton = skeleton;
                    mySkeletonList.Add(mySkeleton);
                }

            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        RenderClippedEdges(skel, dc);

                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.DrawBonesAndJoints(skel, dc);
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(this.centerPointBrush, null, this.SkeletonPointToScreen(skel.Position), BodyCenterThickness, BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }

        #region DrawSkeletonRegion
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            { drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness)); }
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            { drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, RenderWidth, ClipBoundsThickness)); }
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            { drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, ClipBoundsThickness, RenderHeight)); }
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            { drawingContext.DrawRectangle(Brushes.Red, null, new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight)); }
        }
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);

            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                { drawBrush = this.trackedJointBrush; }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                { drawBrush = this.inferredJointBrush; }

                if (drawBrush != null)
                { drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness); }
            }
        }
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.MapSkeletonPointToDepth(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }

        #endregion DrawSekeltonRegion

        private void recordButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.sensor != null)
            {
                stream = new FileStream("Recordings/skeletonData.mot1", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                countDown = recordingTime + 3;      // 3 seconds for standby

                recordTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);

                recordTimer.Tick += new EventHandler(onTimeUpdate);

                recordTimer.Interval = TimeSpan.FromMilliseconds(1000);

                //recordTimer.Enabled = true;//start the timer
                recordTimer.Start();
            }
            else
            {
                MessageBox.Show("Kinect برجاء توصيل الـ", "غير متصل", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        public void onTimeUpdate(object source, EventArgs e)
        {

            if (countDown >= 5 & countDown <= 8)
            {
                lblRecordingStatus.Content = "استعد: " + (countDown - recordingTime).ToString() + " ثواني.";
            }
            else if (countDown > 0 & countDown <= 10)
            {
                recordingStatus = true;//trigger capture skeleton data @WindowLoaded
                this.lblRecordingStatus.Content = "جاري التسجيل الآن ...";
            }
            else
            { // CountDownTime at 0,
                // 1. stop saving data >recordingStatus==false
                // 2. serialize data on HERE
                // 3. stop the timer
                //
                lblRecordingStatus.Content = "تم التسجيل بنجاح!";
                recordingStatus = false;
                FileManipulation.serialize(mySkeletonList, stream);
                recordTimer.Stop();
                if (this.sensor != null)
                {
                    sensor.Stop();
                }
            }
            countDown--;
        }

        private void replayButton_Click(object sender, RoutedEventArgs e)
        {
            replayWindow = new ReplayWindow();
            replayWindow.WindowLoaded(sender, e);
        }
    }
}
