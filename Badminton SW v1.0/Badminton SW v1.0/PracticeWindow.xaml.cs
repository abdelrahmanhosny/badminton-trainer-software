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
    /// Interaction logic for PracticeWindow.xaml
    /// </summary>
    public partial class PracticeWindow : Window
    {
        private const float RenderWidth = 640.0f;
        private const float RenderHeight = 520.0f;
        private const double JointThickness = 10.0;
        private const double BodyCenterThickness = 10;
        private const double ClipBoundsThickness = 10;
        private const double BOUNDARY = 30.00;

        private readonly Brush centerPointBrush = Brushes.DarkBlue;
        //private readonly Brush successBrush = Brushes.DarkBlue;
        private readonly Brush failBrush = Brushes.Red;

        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        private readonly Pen trackedBonePen = new Pen(Brushes.White, 9);
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
        private readonly Pen failedMotionPen = new Pen(Brushes.Red, 9);
        private readonly Pen notReadyPen = new Pen(Brushes.Yellow, 9);


        private KinectSensor sensor;
        private DrawingGroup drawingGroup;
        private DrawingImage imageSource;

        //private List<Skeleton> mySkeletonList;

        private List<Double> myRightHandAngleList;
        private List<Double> myRightShoulderCenterAngleList;
        private List<Double> myLeftShoulderCenterAngleList;
        private List<Double> myLeftHandAngleList;
        private List<Double> myHeadSpineAngleList;
        private List<Double> myRightLegAngleList;
        private List<Double> myRightHipAngleList;
        private List<Double> myLeftHipAngleList;
        //private List<Double> myBetweerHipsAngleList;
        private List<Double> myLeftLegAngleList;
        private List<Double> myLeftSpineThighAngleList;
        private List<Double> myRightSpineThighAngleList;
        private List<Double> myRightShoulderSpineAngleList;
        private List<Double> myLeftShoulderSpineAngleList;
        private List<Double> myRightHandFingerAngleList;
        private List<Double> myLeftWristSpineAngleList;
        private List<Double> myRightWristSpineAngleList;
        private List<Double> myLeftHandFingerAngleList;

        private DispatcherTimer recordTimer;
        private int recordingTime = 5;
        private int countDown = 0;


        private int rightHandCount = 0;
        private int leftHandCount = 0;
        private int leftShoulderCenterCount = 0;
        private int rightShoulderCenterCount = 0;
        private int leftShoulderSpineCount = 0;
        private int rightShoulderSpineCount = 0;
        private int rightHandFingerCount = 0;
        private int leftHandFingerCount = 0;
        private int leftWristSpineCount = 0;
        private int rightWristSpineCount = 0;
        private int headSpineAngleCount = 0;
        private int rightLegAngleCount = 0;
        private int leftLegAngleCount = 0;
        private int rightSpineThighAngleCount = 0;
        private int leftSpineThighAngleCount = 0;
        private int rightHipAngleCount = 0;
        private int leftHipAngleCount = 0;

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

        private int reportNumber = 1;

        private bool status;
        private String[] lines;
        private double roundedAngle;
        private int liveSkeletons;

        private ReplayWindowForPractice replayWindow;

        private List<Skeleton> skeletonsToBeReplayed = new List<Skeleton>();
        private List<Brush> brushesToBeReplayed = new List<Brush>();

        private void InitializeLists()
        {
            myRightHandAngleList = new List<Double>();
            myLeftHandAngleList = new List<Double>();
            myRightShoulderCenterAngleList = new List<Double>();
            myLeftShoulderCenterAngleList = new List<Double>();

            myRightLegAngleList = new List<Double>();
            myLeftLegAngleList = new List<Double>();
            myHeadSpineAngleList = new List<Double>();
            myLeftHipAngleList = new List<Double>();

            myRightHipAngleList = new List<Double>();
            myRightShoulderSpineAngleList = new List<Double>();
            myLeftShoulderSpineAngleList = new List<Double>();
            //myBetweerHipsAngleList = new List<Double>();

            myLeftSpineThighAngleList = new List<Double>();
            myRightSpineThighAngleList = new List<Double>();
            myRightHandFingerAngleList = new List<Double>();
            myLeftHandFingerAngleList = new List<Double>();
            myLeftWristSpineAngleList = new List<Double>();
            myRightWristSpineAngleList = new List<Double>();

            rightHandCount = 0;
            leftHandCount = 0;
            leftShoulderCenterCount = 0;
            rightShoulderCenterCount = 0;
            leftShoulderSpineCount = 0;
            rightShoulderSpineCount = 0;
            rightHandFingerCount = 0;
            leftHandFingerCount = 0;
            leftWristSpineCount = 0;
            rightWristSpineCount = 0;
            headSpineAngleCount = 0;
            rightLegAngleCount = 0;
            leftLegAngleCount = 0;
            rightSpineThighAngleCount = 0;
            leftSpineThighAngleCount = 0;
            rightHipAngleCount = 0;
            leftHipAngleCount = 0;

            recordingTime = 5;
            countDown = 0;

            copyFileDataToLists();
            liveSkeletons = 0;
        }
        public PracticeWindow()
        {
            InitializeComponent();
            this.Show();

            InitializeLists();
        }

        private void PracticeWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            this.drawingGroup = new DrawingGroup();
            this.imageSource = new DrawingImage(this.drawingGroup);
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
                this.sensor.ColorStream.Enable();
                this.sensor.SkeletonStream.Enable();// Turn on the skeleton stream to receive skeleton frames

                // Add an event handler to be called whenever there is new color frame data
                //a += b 	a = a + b
                this.sensor.ColorFrameReady += sensor_ColorFrameReady;
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                try
                {
                    this.sensor.Start();
                }// Start the sensor!
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                result.Content = "جهاز الكينكت غير جاهز";
            }
        }
        BitmapImage colorImage;
        void sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame == null)
                {
                    return;
                }

                byte[] pixels = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(pixels);

                int stride = colorFrame.Width * 4;
                BitmapSource temp =
                    BitmapSource.Create(colorFrame.Width, colorFrame.Height,
                    96, 96, PixelFormats.Bgr32, null, pixels, stride);
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                MemoryStream memoryStream = new MemoryStream();
                colorImage = new BitmapImage();

                encoder.Frames.Add(BitmapFrame.Create(temp));
                encoder.Save(memoryStream);

                colorImage.BeginInit();
                colorImage.StreamSource = new MemoryStream(memoryStream.ToArray());
                colorImage.EndInit();

                memoryStream.Close();
            }
        }
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }
        private bool IsYFootEqual(float right, float left)
        {
            //setting a tolernce of 0.01
            if (Math.Abs(right - left) <= 0.1)
                return true;
            else
                return false;
        }
        private int countFootError = 0;

        private float handX1 = 0;
        private float handY1 = 0;
        private float handZ1 = 0;
        private float handX2 = 0;
        private float handY2 = 0;
        private float handZ2 = 0;
        private bool recordFlag = false;

        private bool practiceMode = false;

        
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];
            int passedAngle = 0;

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }

                Skeleton skeleton = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                if (!status && skeleton != null && !recordFlag && practiceMode)
                {
                    if (handX1 == 0 && handY1 == 0 && handZ1 == 0 && handX2 == 0 && handY2 == 0 && handZ2 == 0)
                    {
                        if (PlayerHand == 1)
                        {
                            handX1 = skeleton.Joints[JointType.HandRight].Position.X;
                            handY1 = skeleton.Joints[JointType.HandRight].Position.Y;
                            handZ1 = skeleton.Joints[JointType.HandRight].Position.Z;
                        }
                        else
                        {
                            handX1 = skeleton.Joints[JointType.HandLeft].Position.X;
                            handY1 = skeleton.Joints[JointType.HandLeft].Position.Y;
                            handZ1 = skeleton.Joints[JointType.HandLeft].Position.Z;
                        }

                        handX2 = handX1;
                        handY2 = handY1;
                        handZ2 = handZ1;
                    }
                    else
                    {
                        handX1 = handX2;
                        handY1 = handY2;
                        handZ1 = handZ2;
                        if (PlayerHand == 1)
                        {
                            handX2 = skeleton.Joints[JointType.HandRight].Position.X;
                            handY2 = skeleton.Joints[JointType.HandRight].Position.Y;
                            handZ2 = skeleton.Joints[JointType.HandRight].Position.Z;
                        }
                        else
                        {
                            handX2 = skeleton.Joints[JointType.HandLeft].Position.X;
                            handY2 = skeleton.Joints[JointType.HandLeft].Position.Y;
                            handZ2 = skeleton.Joints[JointType.HandLeft].Position.Z;
                        }
                    }

                    // compare the velocity here
                    double velocity = Math.Sqrt(Math.Pow((handX2 - handX1), 2.0) + Math.Pow((handY2 - handY1), 2.0) + Math.Pow((handZ2 - handZ1), 2.0));
                    if (velocity > velocitySensitivty)
                    {
                        recordFlag = true;
                        handX1 = 0;
                        handX2 = 0;
                        handY1 = 0;
                        handY2 = 0;
                        handZ1 = 0;
                        handZ2 = 0;
                        InitializeLists();
                        flagEllipse.Fill = new SolidColorBrush(Colors.Green);
                        textBox1.Text = "";
                        status = true;
                        return;
                    }
                    //else { flagEllipse.Fill = new SolidColorBrush(Colors.Red); }
                }
                if (status && skeleton != null)
                {

                    if (!IsYFootEqual(skeleton.Joints[JointType.FootLeft].Position.Y, skeleton.Joints[JointType.FootRight].Position.Y) && checkFootFlag)
                    {
                        countFootError++;
                    }

                    double rightHandAngle = getSegmentAngle(skeleton, JointType.ShoulderRight, JointType.ElbowRight, JointType.WristRight);
                    double rightShoulderCenterAngle = getSegmentAngle(skeleton, JointType.ElbowRight, JointType.ShoulderRight, JointType.ShoulderCenter);
                    double leftHandAngle = getSegmentAngle(skeleton, JointType.ShoulderLeft, JointType.ElbowLeft, JointType.WristLeft);
                    double leftShoulderCenterAngle = getSegmentAngle(skeleton, JointType.ElbowLeft, JointType.ShoulderLeft, JointType.ShoulderCenter);
                    double betweenHandAngle = getSegmentAngle(skeleton, JointType.WristRight, JointType.ShoulderCenter, JointType.WristLeft);
                    double rightShoulderSpineAngle = getSegmentAngle(skeleton, JointType.ElbowRight, JointType.ShoulderRight, JointType.Spine);
                    double leftShoulderSpineAngle = getSegmentAngle(skeleton, JointType.ElbowLeft, JointType.ShoulderLeft, JointType.Spine);
                    double rightWristSpineAngle = getSegmentAngle(skeleton, JointType.WristRight, JointType.ShoulderCenter, JointType.Spine);
                    double leftWristSpineAngle = getSegmentAngle(skeleton, JointType.WristLeft, JointType.ShoulderCenter, JointType.Spine);


                    double headSpineAngle = getSegmentAngle(skeleton, JointType.Head, JointType.ShoulderCenter, JointType.Spine);
                    double rightLegAngle = getSegmentAngle(skeleton, JointType.AnkleRight, JointType.KneeRight, JointType.HipRight);
                    double rightHipAngle = getSegmentAngle(skeleton, JointType.KneeRight, JointType.HipRight, JointType.HipCenter);
                    //double betweenHipsAngle = getSegmentAngle(skeleton, JointType.HipLeft, JointType.HipCenter, JointType.HipRight);
                    double leftLegAngle = getSegmentAngle(skeleton, JointType.AnkleLeft, JointType.KneeLeft, JointType.HipLeft);
                    double leftHipAngle = getSegmentAngle(skeleton, JointType.KneeLeft, JointType.HipLeft, JointType.HipCenter);
                    double leftSpineThighAngle = getSegmentAngle(skeleton, JointType.KneeLeft, JointType.HipLeft, JointType.Spine);
                    double rightSpineThighAngle = getSegmentAngle(skeleton, JointType.KneeRight, JointType.HipRight, JointType.Spine);
                    double rightHandFingerAngle = getSegmentAngle(skeleton, JointType.HandRight, JointType.WristRight, JointType.ElbowRight);
                    double leftHandFingerAngle = getSegmentAngle(skeleton, JointType.HandLeft, JointType.WristLeft, JointType.ElbowLeft);

                    if (myRightHandAngleList.Count != 0)
                    {

                        liveSkeletons++;
                        result.Content = " " + liveSkeletons;

                        if (IsBetween(rightHandAngle, myRightHandAngleList.First() - BOUNDARY, myRightHandAngleList.First() + BOUNDARY))
                        {
                            rightHandCount++;
                            passedAngle++;
                        }

                        myRightHandAngleList.RemoveAt(0);

                        if (myRightShoulderCenterAngleList.Count != 0)
                        {
                            if (IsBetween(rightShoulderCenterAngle, myRightShoulderCenterAngleList.First() - BOUNDARY,
                                myRightShoulderCenterAngleList.First() + BOUNDARY))
                            {
                                rightShoulderCenterCount++;
                                passedAngle++;
                            }

                            myRightShoulderCenterAngleList.RemoveAt(0);
                        }
                        if (myLeftHandAngleList.Count != 0)
                        {
                            if (IsBetween(leftHandAngle, myLeftHandAngleList.First() - BOUNDARY, myLeftHandAngleList.First() + BOUNDARY))
                            {
                                leftHandCount++;
                                passedAngle++;
                            }
                            myLeftHandAngleList.RemoveAt(0);
                        }

                        if (myLeftShoulderCenterAngleList.Count != 0)
                        {
                            if (IsBetween(leftShoulderCenterAngle, myLeftShoulderCenterAngleList.First() - BOUNDARY,
                                myLeftShoulderCenterAngleList.First() + BOUNDARY))
                            {
                                leftShoulderCenterCount++;
                                passedAngle++;
                            }
                            myLeftShoulderCenterAngleList.RemoveAt(0);
                        }

                        if (myRightShoulderSpineAngleList.Count != 0)
                        {
                            if (IsBetween(rightShoulderSpineAngle, myRightShoulderSpineAngleList.First() - BOUNDARY,
                                myRightShoulderSpineAngleList.First() + BOUNDARY))
                            {
                                rightShoulderSpineCount++;
                                passedAngle++;
                            }

                            myRightShoulderSpineAngleList.RemoveAt(0);
                        }

                        if (myLeftShoulderSpineAngleList.Count != 0)
                        {
                            if (IsBetween(leftShoulderSpineAngle, myLeftShoulderSpineAngleList.First() - BOUNDARY,
                                myLeftShoulderSpineAngleList.First() + BOUNDARY))
                            {
                                leftShoulderSpineCount++;
                                passedAngle++;
                            }

                            myLeftShoulderSpineAngleList.RemoveAt(0);
                        }

                        if (myRightWristSpineAngleList.Count != 0)
                        {
                            if (IsBetween(rightWristSpineAngle, myRightWristSpineAngleList.First() - BOUNDARY,
                                myRightWristSpineAngleList.First() + BOUNDARY))
                            {
                                rightWristSpineCount++;
                                passedAngle++;
                            }

                            myRightWristSpineAngleList.RemoveAt(0);
                        }

                        if (myLeftWristSpineAngleList.Count != 0)
                        {
                            if (IsBetween(leftWristSpineAngle, myLeftWristSpineAngleList.First() - BOUNDARY,
                                myLeftWristSpineAngleList.First() + BOUNDARY))
                            {
                                leftWristSpineCount++;
                                passedAngle++;
                            }

                            myLeftWristSpineAngleList.RemoveAt(0);
                        }

                        if (myRightHandFingerAngleList.Count != 0)
                        {
                            if (IsBetween(rightHandFingerAngle, myRightHandFingerAngleList.First() - BOUNDARY,
                                myRightHandFingerAngleList.First() + BOUNDARY))
                            {

                                rightHandFingerCount++;
                                passedAngle++;
                            }

                            myRightHandFingerAngleList.RemoveAt(0);
                        }

                        if (myLeftHandFingerAngleList.Count != 0)
                        {
                            if (IsBetween(leftHandFingerAngle, myLeftHandFingerAngleList.First() - BOUNDARY,
                                myLeftHandFingerAngleList.First() + BOUNDARY))
                            {
                                leftHandFingerCount++;
                                passedAngle++;
                            }
                            myLeftHandFingerAngleList.RemoveAt(0);
                        }
                        //start
                        if (myHeadSpineAngleList.Count != 0)
                        {
                            if (IsBetween(headSpineAngle, myHeadSpineAngleList.First() - BOUNDARY, myHeadSpineAngleList.First() + BOUNDARY))
                            {
                                headSpineAngleCount++;
                                passedAngle++;
                            }
                            myHeadSpineAngleList.RemoveAt(0);

                        }

                        if (myRightLegAngleList.Count != 0)
                        {
                            if (IsBetween(rightLegAngle, myRightLegAngleList.First() - BOUNDARY, myRightLegAngleList.First() + BOUNDARY))
                            {
                                rightLegAngleCount++;
                                passedAngle++;
                            }
                            myRightLegAngleList.RemoveAt(0);
                        }

                        if (myLeftLegAngleList.Count != 0)
                        {
                            if (IsBetween(leftLegAngle, myLeftLegAngleList.First() - BOUNDARY, myLeftLegAngleList.First() + BOUNDARY))
                            {
                                leftLegAngleCount++;
                                passedAngle++;
                            }
                            myLeftLegAngleList.RemoveAt(0);
                        }

                        if (myRightSpineThighAngleList.Count != 0)
                        {
                            if (IsBetween(rightSpineThighAngle, myRightSpineThighAngleList.First() - BOUNDARY,
                                myRightSpineThighAngleList.First() + BOUNDARY))
                            {
                                rightSpineThighAngleCount++;
                                passedAngle++;
                            }

                            myRightSpineThighAngleList.RemoveAt(0);
                        }
                        if (myLeftSpineThighAngleList.Count != 0)
                        {
                            if (IsBetween(leftSpineThighAngle, myLeftSpineThighAngleList.First() - BOUNDARY,
                                myLeftSpineThighAngleList.First() + BOUNDARY))
                            {
                                leftSpineThighAngleCount++;
                                passedAngle++;
                            }

                            myLeftSpineThighAngleList.RemoveAt(0);
                        }

                        if (myRightHipAngleList.Count != 0)
                        {
                            if (IsBetween(rightHipAngle, myRightHipAngleList.First() - BOUNDARY, myRightHipAngleList.First() + BOUNDARY))
                            {
                                rightHipAngleCount++;
                                passedAngle++;
                            }
                            myRightHipAngleList.RemoveAt(0);
                        }

                        if (myLeftHipAngleList.Count != 0)
                        {
                            if (IsBetween(leftHipAngle, myLeftHipAngleList.First() - BOUNDARY, myLeftHipAngleList.First() + BOUNDARY))
                            {
                                leftHipAngleCount++;
                                passedAngle++;
                            }
                            myLeftHipAngleList.RemoveAt(0);
                        }


                    }
                    // else if (rightHandCount > (int)(0.6 * liveSkeletons)
                    //&& rightShoulderCenterCount > (int)(0.6 * liveSkeletons) && leftHandCount > (int)(0.6 * liveSkeletons)
                    //&& leftShoulderCenterCount > (int)(0.6 * liveSkeletons) && rightShoulderSpineCount > (int)(0.5 * liveSkeletons)
                    //&& leftShoulderSpineCount > (int)(0.5 * liveSkeletons) && rightWristSpineCount > (int)(0.5 * liveSkeletons)
                    //&& leftWristSpineCount > (int)(0.5 * liveSkeletons) && rightHandFingerCount > (int)(0.5 * liveSkeletons)
                    //&& leftHandFingerCount > (int)(0.5 * liveSkeletons) && headSpineAngleCount > (int)(0.7 * liveSkeletons)
                    //&& rightLegAngleCount > (int)(0.7 * liveSkeletons) && leftLegAngleCount > (int)(0.7 * liveSkeletons)
                    //&& rightSpineThighAngleCount > (int)(0.7 * liveSkeletons) && leftSpineThighAngleCount > (int)(0.7 * liveSkeletons)
                    //&& rightHipAngleCount > (int)(0.7 * liveSkeletons) && leftHipAngleCount > (int)(0.7 * liveSkeletons))
                    else if (rightHandCount > (int)(0.8 * liveSkeletons)
                       && rightShoulderCenterCount > (int)(0.8 * liveSkeletons) && leftHandCount > (int)(0.8 * liveSkeletons)
                       && leftShoulderCenterCount > (int)(0.8 * liveSkeletons) && rightShoulderSpineCount > (int)(0.8 * liveSkeletons)
                       && leftShoulderSpineCount > (int)(0.8 * liveSkeletons) && rightWristSpineCount > (int)(0.8 * liveSkeletons)
                       && leftWristSpineCount > (int)(0.8 * liveSkeletons) && rightHandFingerCount > (int)(0.8 * liveSkeletons)
                       && leftHandFingerCount > (int)(0.8 * liveSkeletons) && headSpineAngleCount > (int)(0.8 * liveSkeletons)
                       && rightLegAngleCount > (int)(0.8 * liveSkeletons) && leftLegAngleCount > (int)(0.8 * liveSkeletons)
                       && rightSpineThighAngleCount > (int)(0.8 * liveSkeletons) && leftSpineThighAngleCount > (int)(0.8 * liveSkeletons)
                       && rightHipAngleCount > (int)(0.8 * liveSkeletons) && leftHipAngleCount > (int)(0.8 * liveSkeletons))
                    {
                        flagEllipse.Fill = new SolidColorBrush(Colors.Red);
                        if (checkFootFlag)
                        {
                            if (countFootError > 5)
                            {
                                FootErrorLabel.Content = "أحد الرجلين فقد ملامسته للأرض أثناء اللعب";
                            }
                            else
                            {
                                FootErrorLabel.Content = "قمت بالتحكم في الرجلين أثناء الارسال بنجاح";
                            }
                        }
                        result.Content = "شكرا .. لقد قمت بالحركة بشكل صحيح ";
                        textBox1.Text = "";
                        textBox1.AppendText("\n العدد الكلي للمقاطع: " + liveSkeletons + "\t\t\tالنسبة المئوية");
                        textBox1.AppendText("\n");
                        textBox1.AppendText("\n الرسغ الأيمن والكتف الأيمن: " + rightHandCount + "\t\t\t\t" + Math.Round(((double)rightHandCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الرسغ الأيسر والكتف الأيسر: " + leftHandCount + "\t\t\t\t" + Math.Round(((double)leftHandCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n المرفق الأيمن والكتف الأيمن: " + rightShoulderCenterCount + "\t\t\t" + Math.Round(((double)rightShoulderCenterCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n المرفق الأيسر والكتف الأيسر: " + leftShoulderCenterCount + "\t\t\t" + Math.Round(((double)leftShoulderCenterCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الكتف الأيمن مع العمود الفقري: " + rightShoulderSpineCount + "\t\t" + Math.Round(((double)rightShoulderSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الكتف الأيسر مع العمود الفكري: " + leftShoulderSpineCount + "\t\t" + Math.Round(((double)leftShoulderSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الكف الأيمن مع العمود الفقري: " + rightWristSpineCount + "\t\t" + Math.Round(((double)rightWristSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الكف الأيسر مع العمود الفقري: " + leftWristSpineCount + "\t\t" + Math.Round(((double)leftWristSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الكف الأيمن والمرفق الأيمن: " + rightHandFingerCount + "\t\t\t" + Math.Round(((double)rightHandFingerCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الكف الأيسر والمرفق الأيسر " + leftHandFingerCount + "\t\t\t" + Math.Round(((double)leftHandFingerCount / liveSkeletons) * 100, 2) + "%");

                        textBox1.AppendText("\n الكاحل الأيمن والوسط: " + rightLegAngleCount + "\t\t\t" + Math.Round(((double)rightLegAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الكاحل الأيسر والوسط: " + leftLegAngleCount + "\t\t\t" + Math.Round(((double)leftLegAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الركبة اليمني والوسط " + rightHipAngleCount + "\t\t\t" + Math.Round(((double)rightHipAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الركبة اليسرى والوسط " + leftHipAngleCount + "\t\t\t" + Math.Round(((double)leftHipAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الفخذ الأيمن والعمود الفقري " + rightSpineThighAngleCount + "\t\t" + Math.Round(((double)rightSpineThighAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\n الفخذ الأيسر والعمود الفقري " + leftSpineThighAngleCount + "\t\t" + Math.Round(((double)leftSpineThighAngleCount / liveSkeletons) * 100, 2) + "%");



                        status = false;
                        recordFlag = false;

                        // Write the report to the text File
                        if (reportNumber == 1)
                        {
                            using (StreamWriter sw = File.CreateText("Trainee Reports/report.txt"))
                            {
                                sw.WriteLine("*** Badminton SW v1.0 ***");
                                sw.WriteLine("*** Trainee Practice Reports");
                                sw.WriteLine();
                            }
                        }

                        using (StreamWriter wr = File.AppendText("Trainee Reports/report.txt"))
                        {
                            wr.WriteLine("Practice Session: " + reportNumber);
                            reportNumber++;
                            wr.WriteLine(textBox1.Text);
                            wr.WriteLine();
                            wr.WriteLine();
                        }

                        //if (this.sensor != null)
                        //{
                        //    sensor.Stop();
                        //}
                    }
                    else
                    {
                        flagEllipse.Fill = new SolidColorBrush(Colors.Red);
                        if (checkFootFlag)
                        {
                            if (countFootError > 5)
                            {
                                FootErrorLabel.Content = "أحد الرجلين فقد ملامسته للأرض أثناء اللعب";
                            }
                            else
                            {
                                FootErrorLabel.Content = "قمت بالتحكم في الرجلين أثناء الارسال بنجاح";
                            }
                        }
                        result.Content = "للأسف! انتهي الوقت ولم تتمكن من تنفيذ الحركة بالدقة المطلوبة";
                        textBox1.Text = "";
                        textBox1.AppendText("\r\n العدد الكلي للمقاطع: " + liveSkeletons + "\t\t\tالنسبة المئوية");
                        textBox1.AppendText("\r\n");
                        textBox1.AppendText("\r\n الرسغ الأيمن والكتف الأيمن: " + rightHandCount + "\t\t\t\t" + Math.Round(((double)rightHandCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الرسغ الأيسر والكتف الأيسر: " + leftHandCount + "\t\t\t\t" + Math.Round(((double)leftHandCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n المرفق الأيمن والكتف الأيمن: " + rightShoulderCenterCount + "\t\t\t" + Math.Round(((double)rightShoulderCenterCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n المرفق الأيسر والكتف الأيسر: " + leftShoulderCenterCount + "\t\t\t" + Math.Round(((double)leftShoulderCenterCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكتف الأيمن مع العمود الفقري: " + rightShoulderSpineCount + "\t\t" + Math.Round(((double)rightShoulderSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكتف الأيسر مع العمود الفقري: " + leftShoulderSpineCount + "\t\t" + Math.Round(((double)leftShoulderSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكف الأيمن مع العمود الفقري: " + rightWristSpineCount + "\t\t" + Math.Round(((double)rightWristSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكف الأيسر مع العمود الفقري: " + leftWristSpineCount + "\t\t" + Math.Round(((double)leftWristSpineCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكف الأيمن والمرفق الأيمن: " + rightHandFingerCount + "\t\t\t" + Math.Round(((double)rightHandFingerCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكف الأيسر والمرفق الأيسر: " + leftHandFingerCount + "\t\t\t" + Math.Round(((double)leftHandFingerCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكاحل الأيمن والوسط: " + rightLegAngleCount + "\t\t\t" + Math.Round(((double)rightLegAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الكاحل الأيسر والوسط: " + leftLegAngleCount + "\t\t\t" + Math.Round(((double)leftLegAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الركبة اليمني والوسط " + rightHipAngleCount + "\t\t\t" + Math.Round(((double)rightHipAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الركبة اليسري والوسط " + leftHipAngleCount + "\t\t\t" + Math.Round(((double)leftHipAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الفخذ الأيمن والعمود الفقري " + rightSpineThighAngleCount + "\t\t" + Math.Round(((double)rightSpineThighAngleCount / liveSkeletons) * 100, 2) + "%");
                        textBox1.AppendText("\r\n الفخذ الأيسر والعمود الفقري " + leftSpineThighAngleCount + "\t\t" + Math.Round(((double)leftSpineThighAngleCount / liveSkeletons) * 100, 2) + "%");

                        status = false;
                        recordFlag = false;

                        if (!practiceMode)
                        {
                            if (rightHandCount < 80)
                            {
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer("Tutorials/right hand.wav");
                                player.Play();
                            }
                            else if (leftHandCount < 80)
                            {
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer("Tutorials/left hand.wav");
                                player.Play();
                            }
                            else if (rightShoulderCenterCount < 80 || rightShoulderSpineCount < 80)
                            {
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer("Tutorials/right shoulder.wav");
                                player.Play();
                            }
                            else if (leftShoulderCenterCount < 80 || leftShoulderSpineCount < 80)
                            {
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer("Tutorials/left shoulder.wav");
                                player.Play();
                            }
                        }

                        // Write the report to the text File
                        if (reportNumber == 1)
                        {
                            using (StreamWriter sw = File.CreateText("Trainee Reports/report.txt"))
                            {
                                sw.WriteLine("*** Badminton SW v1.0 ***");
                                sw.WriteLine("*** Trainee Practice Reports");
                                sw.WriteLine();
                            }
                        }


                        using (StreamWriter wr = File.AppendText("Trainee Reports/report.txt"))
                        {
                            wr.WriteLine("Practice Session: " + reportNumber);
                            reportNumber++;
                            wr.WriteLine(textBox1.Text);
                            wr.WriteLine();
                            wr.WriteLine();
                        }

                        //if (this.sensor != null)
                        //{
                        //    sensor.Stop();
                        //}

                    }
                }
                else if (skeleton == null)
                {
                    result.Content = "لم تبدأ المقارنة بعد";
                }
            }
            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                //dc.DrawRectangle(Brushes.Brown, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));
                dc.DrawImage(new BitmapImage(new Uri("Tutorials/court.JPG", UriKind.Relative)), new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                //dc.DrawImage(colorImage, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skeleton in skeletons)
                    {
                        RenderClippedEdges(skeleton, dc);

                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            if (passedAngle == 0)
                            {
                                this.DrawBonesAndJoints("Yellow", skeleton, dc);
                            }
                            else if (passedAngle >= 14)
                            {
                                this.DrawBonesAndJoints("White", skeleton, dc);
                                skeletonsToBeReplayed.Add(skeleton);
                                brushesToBeReplayed.Add(Brushes.White);
                            }
                            else if (passedAngle > 0 && passedAngle < 14)
                            {
                                this.DrawBonesAndJoints("Red", skeleton, dc);
                                skeletonsToBeReplayed.Add(skeleton);
                                brushesToBeReplayed.Add(Brushes.Red);
                            }
                        }
                        else if (skeleton.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                            this.centerPointBrush,
                            null,
                            this.SkeletonPointToScreen(skeleton.Position),
                            BodyCenterThickness,
                            BodyCenterThickness);
                        }
                        // prevent drawing outside of our render area
                        this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
                    }
                }
            }
        }
        
        private void DrawBonesAndJoints(string color, Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            this.DrawBone(color, skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(color, skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(color, skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(color, skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(color, skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(color, skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(color, skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(color, skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(color, skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(color, skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(color, skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(color, skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(color, skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(color, skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(color, skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(color, skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(color, skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(color, skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(color, skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);

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
            DepthImagePoint depthPoint = this.sensor.MapSkeletonPointToDepth(
                                                                             skelpoint,
                                                                             DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        private void DrawBone(string colour, Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
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
                if (colour.Equals("White", StringComparison.OrdinalIgnoreCase))
                {
                    drawPen = this.trackedBonePen;
                    //skeletonsToBeReplayed.Add(skeleton);
                    //brushesToBeReplayed.Add(Brushes.White);
                }

                else if (colour.Equals("Red", StringComparison.OrdinalIgnoreCase))
                {
                    drawPen = this.failedMotionPen;
                    //skeletonsToBeReplayed.Add(skeleton);
                    //brushesToBeReplayed.Add(Brushes.Red);
                }

                else if (colour.Equals("Yellow", StringComparison.OrdinalIgnoreCase))
                {
                    drawPen = this.notReadyPen;
                }

            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            skeletonsToBeReplayed = new List<Skeleton>();
            brushesToBeReplayed = new List<Brush>();
            if (this.sensor != null)
            {
                //status = false;
                InitializeLists();
                countDown = recordingTime + 5;
                recordTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
                recordTimer.Tick += new EventHandler(onTimeUpdate);
                recordTimer.Interval = TimeSpan.FromMilliseconds(1000);
                recordTimer.Start();
            }
            else
            {
                MessageBox.Show("جهاز الكينكت غير مفعل");
            }
        }

        public void onTimeUpdate(object source, EventArgs e)
        {
            if (countDown >= 5 & countDown <= 10)
            {
                flagEllipse.Fill = new SolidColorBrush(Colors.Yellow);
                result.Content = "استعد فى خلال: " + (countDown - recordingTime).ToString() + " ثواني.";
                //status = false;
            }
            else
            {
                flagEllipse.Fill = new SolidColorBrush(Colors.Green);
                textBox1.Text = "";
                status = true;
                recordTimer.Stop();
            }
            countDown--;
        }


        private void copyFileDataToLists()
        {
            CopyFileData(filePath1, myRightHandAngleList);
            CopyFileData(filePath2, myRightShoulderCenterAngleList);
            CopyFileData(filePath3, myLeftHandAngleList);
            CopyFileData(filePath4, myLeftShoulderCenterAngleList);
            CopyFileData(filePath13, myRightShoulderSpineAngleList);
            CopyFileData(filePath14, myLeftShoulderSpineAngleList);

            CopyFileData(filePath5, myHeadSpineAngleList);
            CopyFileData(filePath6, myRightLegAngleList);
            CopyFileData(filePath7, myRightHipAngleList);
            //CopyFileData(filePath8, myBetweerHipsAngleList);

            CopyFileData(filePath9, myLeftLegAngleList);
            CopyFileData(filePath10, myLeftHipAngleList);
            CopyFileData(filePath11, myLeftSpineThighAngleList);
            CopyFileData(filePath12, myRightSpineThighAngleList);
            CopyFileData(filePath15, myRightHandFingerAngleList);
            CopyFileData(filePath16, myRightWristSpineAngleList);
            CopyFileData(filePath17, myLeftWristSpineAngleList);
            CopyFileData(filePath18, myLeftHandFingerAngleList);

        }

        private void CopyFileData(string filePath, List<Double> myList)
        {
            try
            {
                lines = File.ReadAllLines(filePath);

                if (lines.Length != 0)
                {
                    foreach (string line in lines)
                    {
                        double val = Double.Parse(line);
                        myList.Add(val);
                    }
                }
                else
                {
                    MessageBox.Show("الملفات المشار إليها خالية");
                }
            }
            catch (IOException exp)
            {
                MessageBox.Show(exp.ToString());
            }

        }

        private bool IsBetween(double value, double lowerBound, double upperBound)
        {
            return (value >= lowerBound && value <= upperBound);
        }

        private double getSegmentAngle(Skeleton skeleton, JointType type0, JointType type1, JointType type2)
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

        private bool checkFootFlag = false;

        private void CheckFoot_Checked(object sender, RoutedEventArgs e)
        {

            checkFootFlag = true;
        }

        private void CheckFoot_Unchecked(object sender, RoutedEventArgs e)
        {
            checkFootFlag = false;
        }

        private void PracticeWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.sensor != null)
            {
                sensor.Stop();
            }
        }

        private int PlayerHand = 1;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            PlayerHand = 1;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            PlayerHand = 2;
        }

        double velocitySensitivty = .15;
        private void velocitySens_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            velocitySensitivty = e.NewValue;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            practiceMode = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            practiceMode = false;
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Skeleton> temp = new List<Skeleton>(skeletonsToBeReplayed);
            List<Brush> temp2 = new List<Brush>(brushesToBeReplayed);
            replayWindow = new ReplayWindowForPractice(temp, temp2);
            replayWindow.WindowLoaded(sender, e);
        }

        private ReplayWindow replayWindow2;
        private void anotherBtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            replayWindow2 = new ReplayWindow();
            replayWindow2.WindowLoaded(sender, e);
        }
    }
}
