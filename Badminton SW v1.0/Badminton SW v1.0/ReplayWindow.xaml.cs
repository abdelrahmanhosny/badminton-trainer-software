using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Xna.Framework.Design;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Threading;

namespace Badminton_SW_v1._0
{
    /// <summary>
    /// Interaction logic for ReplayWindow.xaml
    /// </summary>
    public partial class ReplayWindow : Window
    {
        private const float RenderWidth = 640.0f;
        private const float RenderHeight = 480.0f;
        private const double JointThickness = 10.0;
        private const double BodyCenterThickness = 12;
        private const double ClipBoundsThickness = 14;
        private readonly Brush centerPointBrush = Brushes.Black;
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private readonly Brush inferredJointBrush = Brushes.Yellow;
        private readonly Pen trackedBonePen = new Pen(Brushes.White, 8);
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
        private KinectSensor sensor;

        private DrawingGroup drawingGroup;
        private DrawingImage imageSource;

        private Skeleton mySkeleton;
        private List<Skeleton> mySkeletonList;
        //private List<Double> rightHandAngleList;
        private DispatcherTimer replayTimer;
        private int skeletonCount = 0;
        private double roundedAngle = 0.0;
        private int replayTime = 5;
        private int countDown = 0;

        private string filePath1 = "Recordings/rightHand.txt";
        private string filePath2 = "Recordings/rightShoulderCenterCount.txt";
        private string filePath3 = "Recordings/leftHand.txt";
        private string filePath4 = "Recordings/leftShoulderCenter.txt";
        private string filePath5 = "Recordings/headSpine.txt";
        private string filePath6 = "Recordings/rightLeg.txt";
        private string filePath7 = "Recordings/rightHip.txt";
        private string filePath8 = "Recordings/betweenHips.txt";
        private string filePath9 = "Recordings/leftLeg.txt";
        private string filePath10 = "Recordings/leftHip.txt";
        private string filePath11 = "Recordings/leftSpineThigh.txt";
        private string filePath12 = "Recordings/rightSpineThing.txt";
        private string filePath13 = "Recordings/rightShoulderSpine.txt";
        private string filePath14 = "Recordings/leftShoulderSpine.txt";
        private string filePath15 = "Recordings/rightHandFingers.txt";
        private string filePath16 = "Recordings/rightWristSpine.txt";
        private string filePath17 = "Recordings/leftWristSpine.txt";
        private string filePath18 = "Recordings/leftHandFingers.txt";

        StreamWriter sw1 = null;
        StreamWriter sw2 = null;
        StreamWriter sw3 = null;
        StreamWriter sw4 = null;
        StreamWriter sw5 = null;
        StreamWriter sw6 = null;
        StreamWriter sw7 = null;
        StreamWriter sw8 = null;
        StreamWriter sw9 = null;
        StreamWriter sw10 = null;
        StreamWriter sw11 = null;
        StreamWriter sw12 = null;
        StreamWriter sw13 = null;
        StreamWriter sw14 = null;
        StreamWriter sw15 = null;
        StreamWriter sw16 = null;
        StreamWriter sw17 = null;
        StreamWriter sw18 = null;
        public ReplayWindow()
        {
            InitializeComponent();
            this.Show();
            mySkeleton = new Skeleton();
            mySkeletonList = new List<Skeleton>();
            mySkeletonList = (List<Skeleton>)FileManipulation.diserialise();
        }
        public void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.drawingGroup = new DrawingGroup();
            this.imageSource = new DrawingImage(this.drawingGroup);
            imgReplayImage.Source = this.imageSource;

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }
        }
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(Brushes.Red, null, new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

        //replay Part
        private void replaySkeleton(Skeleton replaySkeleton)
        {
            sw1 = new StreamWriter(filePath1, true);
            sw2 = new StreamWriter(filePath2, true);
            sw3 = new StreamWriter(filePath3, true);
            sw4 = new StreamWriter(filePath4, true);
            sw5 = new StreamWriter(filePath5, true);
            sw6 = new StreamWriter(filePath6, true);
            sw7 = new StreamWriter(filePath7, true);
            sw8 = new StreamWriter(filePath8, true);
            sw9 = new StreamWriter(filePath9, true);
            sw10 = new StreamWriter(filePath10, true);
            sw11 = new StreamWriter(filePath11, true);
            sw12 = new StreamWriter(filePath12, true);
            sw13 = new StreamWriter(filePath13, true);
            sw14 = new StreamWriter(filePath14, true);
            sw15 = new StreamWriter(filePath15, true);
            sw16 = new StreamWriter(filePath16, true);
            sw17 = new StreamWriter(filePath17, true);
            sw18 = new StreamWriter(filePath18, true);



            Skeleton[] skeletons = new Skeleton[1];
            skeletons[0] = replaySkeleton;

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.DarkBlue, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));


                if (skeletons.Length != 0)
                {

                    foreach (Skeleton skeleton in skeletons)
                    {
                        RenderClippedEdges(skeleton, dc);
                        this.DrawBonesAndJoints(skeleton, dc);
                        double rightWristShoulderAngle = getAngle(skeleton, JointType.WristRight, JointType.ElbowRight, JointType.ShoulderRight);
                        double rightShoulderCenterAngle = getAngle(skeleton, JointType.ElbowRight, JointType.ShoulderRight, JointType.ShoulderCenter);
                        double leftWristShoulderAngle = getAngle(skeleton, JointType.WristLeft, JointType.ElbowLeft, JointType.ShoulderLeft);
                        double leftShoulderCenterAngle = getAngle(skeleton, JointType.ElbowLeft, JointType.ShoulderLeft, JointType.ShoulderCenter);
                        double headSpineAngle = getAngle(skeleton, JointType.Head, JointType.ShoulderCenter, JointType.Spine);
                        double rightLegAngle = getAngle(skeleton, JointType.AnkleRight, JointType.KneeRight, JointType.HipRight);
                        double rightHipAngle = getAngle(skeleton, JointType.KneeRight, JointType.HipRight, JointType.HipCenter);
                        double betweenHipsAngle = getAngle(skeleton, JointType.HipLeft, JointType.HipCenter, JointType.HipRight);
                        double leftLegAngle = getAngle(skeleton, JointType.AnkleLeft, JointType.KneeLeft, JointType.HipLeft);
                        double leftHipAngle = getAngle(skeleton, JointType.KneeLeft, JointType.HipLeft, JointType.HipCenter);
                        double leftSpineThighAngle = getAngle(skeleton, JointType.KneeLeft, JointType.HipLeft, JointType.Spine);
                        double rightSpineThighAngle = getAngle(skeleton, JointType.KneeRight, JointType.HipRight, JointType.Spine);
                        double rightShoulderSpineAngle = getAngle(skeleton, JointType.ElbowRight, JointType.ShoulderRight, JointType.Spine);
                        double leftShoulderSpineAngle = getAngle(skeleton, JointType.ElbowLeft, JointType.ShoulderLeft, JointType.Spine);
                        double rightWristSpineAngle = getAngle(skeleton, JointType.WristRight, JointType.ShoulderCenter, JointType.Spine);
                        double leftWristSpineAngle = getAngle(skeleton, JointType.WristLeft, JointType.ShoulderCenter, JointType.Spine);
                        double rightHandFingerAngle = getAngle(skeleton, JointType.HandRight, JointType.WristRight, JointType.ElbowRight);
                        double leftHandFingerAngle = getAngle(skeleton, JointType.HandLeft, JointType.WristLeft, JointType.ElbowLeft);

                        FileManipulation.writeToTextFile(rightWristShoulderAngle, sw1);
                        FileManipulation.writeToTextFile(rightShoulderCenterAngle, sw2);
                        FileManipulation.writeToTextFile(leftWristShoulderAngle, sw3);
                        FileManipulation.writeToTextFile(leftShoulderCenterAngle, sw4);
                        FileManipulation.writeToTextFile(headSpineAngle, sw5);
                        FileManipulation.writeToTextFile(rightLegAngle, sw6);
                        FileManipulation.writeToTextFile(rightHipAngle, sw7);
                        FileManipulation.writeToTextFile(betweenHipsAngle, sw8);
                        FileManipulation.writeToTextFile(leftLegAngle, sw9);
                        FileManipulation.writeToTextFile(leftHipAngle, sw10);
                        FileManipulation.writeToTextFile(leftSpineThighAngle, sw11);
                        FileManipulation.writeToTextFile(rightSpineThighAngle, sw12);
                        FileManipulation.writeToTextFile(rightShoulderSpineAngle, sw13);
                        FileManipulation.writeToTextFile(leftShoulderSpineAngle, sw14);
                        FileManipulation.writeToTextFile(rightHandFingerAngle, sw15);
                        FileManipulation.writeToTextFile(rightWristSpineAngle, sw16);
                        FileManipulation.writeToTextFile(leftWristSpineAngle, sw17);
                        FileManipulation.writeToTextFile(leftHandFingerAngle, sw18);




                        //angleLabel.Content = " " + rightWristShoulderAngle;
                        frameNo.Content = "عدد الهياكل: " + skeletonCount;
                        if (skeleton.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(this.centerPointBrush, null, this.SkeletonPointToScreen(skeleton.Position), BodyCenterThickness, BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
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
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
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
        public double getAngle(Skeleton skeleton, JointType type0, JointType type1, JointType type2)
        {
            Microsoft.Xna.Framework.Vector3 crossProduct;
            Microsoft.Xna.Framework.Vector3 joint0Tojoint1;
            Microsoft.Xna.Framework.Vector3 joint1Tojoint2;

            Joint joint0 = skeleton.Joints[type0];
            Joint joint1 = skeleton.Joints[type1];
            Joint joint2 = skeleton.Joints[type2];

            joint0Tojoint1 = new Microsoft.Xna.Framework.Vector3(joint0.Position.X - joint1.Position.X, joint0.Position.Y - joint1.Position.Y,
                joint0.Position.Z - joint1.Position.Z);
            joint1Tojoint2 = new Microsoft.Xna.Framework.Vector3(joint2.Position.X - joint1.Position.X, joint2.Position.Y - joint1.Position.Y,
                joint2.Position.Z - joint1.Position.Z);

            joint0Tojoint1.Normalize();
            joint1Tojoint2.Normalize();

            double dotProduct = Microsoft.Xna.Framework.Vector3.Dot(joint0Tojoint1, joint1Tojoint2);
            crossProduct = Microsoft.Xna.Framework.Vector3.Cross(joint0Tojoint1, joint1Tojoint2);
            double crossProdLength = crossProduct.Length();

            double angleFormed = Math.Atan2(crossProdLength, dotProduct);
            double angleInDegree = angleFormed * (180 / Math.PI);
            roundedAngle = Math.Round(angleInDegree, 2);

            return roundedAngle;
        }

        private void clearAllFiles()
        {
            clearTextFile(filePath1);
            clearTextFile(filePath2);
            clearTextFile(filePath3);
            clearTextFile(filePath4);
            clearTextFile(filePath5);
            clearTextFile(filePath6);
            clearTextFile(filePath7);
            clearTextFile(filePath8);
            clearTextFile(filePath9);
            clearTextFile(filePath10);
            clearTextFile(filePath11);
            clearTextFile(filePath12);
            clearTextFile(filePath13);
            clearTextFile(filePath14);
            clearTextFile(filePath15);
            clearTextFile(filePath16);
            clearTextFile(filePath17);
            clearTextFile(filePath18);

        }
        private void clearTextFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.WriteAllText(filePath, string.Empty);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void btnReplay_Click(object sender, RoutedEventArgs e)
        {
            countDown = replayTime + 30; //3second for standby
            clearAllFiles();
            replayTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            replayTimer.Tick += new EventHandler(onTimeUpdate);
            replayTimer.Interval = TimeSpan.FromMilliseconds(20);//will update after in onTimeUpdate
            replayTimer.Start();
        }
        private int currentFrame = 0;
        private int numberOfFrames = 0;

        public void onTimeUpdate(object source, EventArgs e)
        {
            try
            {
                if (countDown >= 5 & countDown <= 195)
                {
                    angleLabel.Content = "سيتم اعادة العرض خلال: " + (countDown - replayTime).ToString() + " ثواني.";
                }
                else
                {

                    if (currentFrame < mySkeletonList.Count)
                    {
                        mySkeleton = mySkeletonList[currentFrame];
                        skeletonCount++;
                        replaySkeleton(mySkeleton);
                        currentFrame++;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            countDown--;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (replayTimer != null)
            {
                if (replayTimer.IsEnabled)
                {
                    replayTimer.Stop();
                    pauseBtn.Content = "Resume";
                }
                else
                {
                    replayTimer.Start();
                    pauseBtn.Content = "Pause";
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            currentFrame -= 1;
            skeletonCount -= 1;
            if (currentFrame < 0)
            {
                currentFrame = 0;
            }
            if (skeletonCount < 0)
            {
                skeletonCount = 0;
            }
        }
    }
}
