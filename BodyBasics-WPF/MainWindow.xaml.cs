//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// actualizado viernes 22 enero
// Modif 14/enero/16

//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.BodyBasics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;
    using System.Web.Script.Serialization;
    using MySql.Data.MySqlClient;

       
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Radius of drawn hand circles
        /// </summary>
        private const double HandSize = 30;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Constant for clamping Z values of camera space points from being negative
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as closed //instruccion para asignar color mano cerrada
        /// </summary>
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as opened //instruccion para asignar color mano abierta
        /// </summary>
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as in lasso (pointer) position //instruccion para asignar color a las uniones
        /// </summary>
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred // uniones de articulaciones
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently inferred // huesos que no alcanza a detectar.
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Magenta, 1);

        /// <summary>
        /// Drawing group for body rendering output // 
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Coordinate mapper to map one type of point to another //Coordinar mapeador para asignar un tipo de punto a otro
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// Reader for body frames
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary>
        /// Array for the bodies  ////// arreglo para los cuerpos
        /// </summary>
        private Body[] bodies = null;

        /// <summary>
        /// definition of bones
        /// </summary>
        private List<Tuple<JointType, JointType>> bones;

        /// <summary>
        /// Width of display (depth space)
        /// </summary>
        private int displayWidth;

        /// <summary>
        /// Height of display (depth space)
        /// </summary>
        private int displayHeight;

        /// <summary>
        /// List of colors for each body tracked
        /// </summary>
        private List<Pen> bodyColors;

        /// <summary>
        /// Current status text to display
        /// </summary>
       private string statusText = null;
       /// 
       private int counter = 0;
       private string nombre; // para describir o tener una referencia de la lectura.
       private string idlectura;
       private bool sesion = true;

       public CameraSpacePoint headx, heady, headz;
       public CameraSpacePoint neckx, necky, neckz;
       public CameraSpacePoint hipleftx, hiplefty, hipleftz;
       public CameraSpacePoint footleftx, footlefty, footleftz;
       public CameraSpacePoint handleftx, handlefty, handleftz;
       public CameraSpacePoint hiprightx, hiprighty, hiprightz;
       public CameraSpacePoint kneeleftx, kneelefty, kneeleftz;
       public CameraSpacePoint spinemidx, spinemidy, spinemidz;
       public CameraSpacePoint ankleleftx, anklelefty, ankleleftz;
       public CameraSpacePoint elbowleftx, elbowlefty, elbowleftz;
       public CameraSpacePoint footrightx, footrighty, footrightz;
       public CameraSpacePoint handrightx, handrighty, handrightz;
       public CameraSpacePoint kneerightx, kneerighty, kneerightz;
       public CameraSpacePoint spinebasex, spinebasey, spinebasez;
       public CameraSpacePoint thumbleftx, thumblefty, thumbleftz;
       public CameraSpacePoint wristleftx, wristlefty, wristleftz;
       public CameraSpacePoint anklerightx, anklerighty, anklerightz;
       public CameraSpacePoint elbowrightx, elbowrighty, elbowrightz;
       public CameraSpacePoint thumbrightx, thumbrighty, thumbrightz;
       public CameraSpacePoint wristrightx, wristrighty, wristrightz;
       public CameraSpacePoint handtipleftx, handtiplefty, handtipleftz;
       public CameraSpacePoint handtiprightx, handtiprighty, handtiprightz;
       public CameraSpacePoint shoulderleftx, shoulderlefty, shoulderleftz;
       public CameraSpacePoint shoulderrightx, shoulderrighty, shoulderrightz;
       public CameraSpacePoint spineshoulderx, spineshouldery, spineshoulderz;


        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        /// 

        public MainWindow()
        {
            idlectura = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
          

            // one sensor is currently supported
            this.kinectSensor = KinectSensor.GetDefault();

            // get the coordinate mapper
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            // get the depth (display) extents
            FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            // get size of joint space
            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;

            // open the reader for the body frames
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // a bone defined as a line between two joints
            this.bones = new List<Tuple<JointType, JointType>>();

            // Torso
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            // Right Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Left Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            //// Right Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            //// Left Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));

            // populate body colors, one for each BodyIndex
            this.bodyColors = new List<Pen>();
            
            this.bodyColors.Add(new Pen(Brushes.Red, 6));
            this.bodyColors.Add(new Pen(Brushes.Orange, 6));
            this.bodyColors.Add(new Pen(Brushes.Green, 6));
            this.bodyColors.Add(new Pen(Brushes.Blue, 6));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 6)); // indigo
            this.bodyColors.Add(new Pen(Brushes.Violet, 6));

            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            // set the status text
            this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.NoSensorStatusText;

            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // use the window object as the view model in this simple example
            this.DataContext = this;

            // initialize the components (controls) of the window
            this.InitializeComponent();
        }

        //liga conexion BD.
        //BD BASED = new BD();

      //  BodyBasics.Connection1 conn = new BodyBasics.Connection1();
        
        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
        }

        /// <summary>
        /// Gets or sets the current status text to display
        /// </summary> 
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        }

        /// <summary>
        /// Execute start up tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                // BodyFrameReader is IDisposable
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        /// <summary>
        /// Handles the body frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
     
        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            if (counter % 30 == 0)
            {
                 
                bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                    // As long as those body objects are not disposed and not set to null in the array,
                    // those body objects will be re-used.
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                using (DrawingContext dc = this.drawingGroup.Open())
                {
                    // Draw a transparent background to set the render size
                    dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                    int penIndex = 0;
                    foreach (Body body in this.bodies)
                    {
                        Pen drawPen = this.bodyColors[penIndex++];

                        if (body.IsTracked && sesion == false)
                        {
                            this.DrawClippedEdges(body, dc);

                            IReadOnlyDictionary<JointType, Joint> joints = body.Joints;

                            // convert the joint points to depth (display) space
                            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                            //MySqlCommand command = new MySqlCommand();
                            //command.CommandType = System.Data.CommandType.Text;
                            //command.CommandText = "INSERT INTO frame(headX,headY,headZ) VALUES(@headx, @heady, @headz);";

                            BD BASED = new BD();
                            //Modificado para mandar consulta posicion

                            float headx = joints[JointType.Head].Position.X;
                            float heady = joints[JointType.Head].Position.Y;
                            float headz = joints[JointType.Head].Position.Z;
                            float neckx = joints[JointType.Neck].Position.X;
                            float necky = joints[JointType.Neck].Position.Y;
                            float neckz = joints[JointType.Neck].Position.Z;
                            float hipleftx = joints[JointType.HipLeft].Position.X;
                            float hiplefty = joints[JointType.HipLeft].Position.Y;
                            float hipleftz = joints[JointType.HipLeft].Position.Z;
                            float footleftx = joints[JointType.FootLeft].Position.X;
                            float footlefty = joints[JointType.FootLeft].Position.Y;
                            float footleftz = joints[JointType.FootLeft].Position.Z;
                            float handleftx = joints[JointType.HandLeft].Position.X;
                            float handlefty = joints[JointType.HandLeft].Position.Y;
                            float handleftz = joints[JointType.HandLeft].Position.Z;
                            float hiprightx = joints[JointType.HipRight].Position.X;
                            float hiprighty = joints[JointType.HipRight].Position.Y;
                            float hiprightz = joints[JointType.HipRight].Position.Z;
                            float kneeleftx = joints[JointType.KneeLeft].Position.X;
                            float kneelefty = joints[JointType.KneeLeft].Position.Y;
                            float kneeleftz = joints[JointType.KneeLeft].Position.Z;
                            float spinemidx = joints[JointType.SpineMid].Position.X;
                            float spinemidy = joints[JointType.SpineMid].Position.Y;
                            float spinemidz = joints[JointType.SpineMid].Position.Z;
                            float ankleleftx = joints[JointType.KneeLeft].Position.X;
                            float anklelefty = joints[JointType.KneeLeft].Position.Y;
                            float ankleleftz = joints[JointType.KneeLeft].Position.Z;
                            float elbowleftx = joints[JointType.ElbowLeft].Position.X;
                            float elbowlefty = joints[JointType.ElbowLeft].Position.Y;
                            float elbowleftz = joints[JointType.ElbowLeft].Position.Z;
                            float footrightx = joints[JointType.FootRight].Position.X;
                            float footrighty = joints[JointType.FootRight].Position.Y;
                            float footrightz = joints[JointType.FootRight].Position.Z;
                            float handrightx = joints[JointType.HandRight].Position.X;
                            float handrighty = joints[JointType.HandRight].Position.Y;
                            float handrightz = joints[JointType.HandRight].Position.Z;
                            float kneerightx = joints[JointType.KneeRight].Position.X;
                            float kneerighty = joints[JointType.KneeRight].Position.Y;
                            float kneerightz = joints[JointType.KneeRight].Position.Z;
                            float spinebasex = joints[JointType.SpineBase].Position.X;
                            float spinebasey = joints[JointType.SpineBase].Position.Y;
                            float spinebasez = joints[JointType.SpineBase].Position.Z;
                            float thumbleftx = joints[JointType.ThumbLeft].Position.X;
                            float thumblefty = joints[JointType.ThumbLeft].Position.Y;
                            float thumbleftz = joints[JointType.ThumbLeft].Position.Z;
                            float wristleftx = joints[JointType.WristLeft].Position.X;
                            float wristlefty = joints[JointType.WristLeft].Position.Y;
                            float wristleftz = joints[JointType.WristLeft].Position.Z;
                            float anklerightx = joints[JointType.AnkleRight].Position.X;
                            float anklerighty = joints[JointType.AnkleRight].Position.Y;
                            float anklerightz = joints[JointType.AnkleRight].Position.Z;
                            float elbowrightx = joints[JointType.ElbowRight].Position.X;
                            float elbowrighty = joints[JointType.ElbowRight].Position.Y;
                            float elbowrightz = joints[JointType.ElbowRight].Position.Z;
                            float thumbrightx = joints[JointType.ThumbRight].Position.X;
                            float thumbrighty = joints[JointType.ThumbRight].Position.Y;
                            float thumbrightz = joints[JointType.ThumbRight].Position.Z;
                            float wristrightx = joints[JointType.WristRight].Position.X;
                            float wristrighty = joints[JointType.WristRight].Position.Y;
                            float wristrightz = joints[JointType.WristRight].Position.Z;
                            float handtipleftx = joints[JointType.HandTipLeft].Position.X;
                            float handtiplefty = joints[JointType.HandTipLeft].Position.Y;
                            float handtipleftz = joints[JointType.HandTipLeft].Position.Z;
                            float handtiprightx = joints[JointType.HandTipRight].Position.X;
                            float handtiprighty = joints[JointType.HandTipRight].Position.Y;
                            float handtiprightz = joints[JointType.HandTipRight].Position.Z;
                            float shoulderleftx = joints[JointType.ShoulderLeft].Position.X;
                            float shoulderlefty = joints[JointType.ShoulderLeft].Position.Y;
                            float shoulderleftz = joints[JointType.ShoulderLeft].Position.Z;
                            float shoulderrightx = joints[JointType.ShoulderRight].Position.X;
                            float shoulderrighty = joints[JointType.ShoulderRight].Position.Y;
                            float shoulderrightz = joints[JointType.ShoulderRight].Position.Z;
                            float spineshoulderx = joints[JointType.SpineShoulder].Position.X;
                            float spineshouldery = joints[JointType.SpineShoulder].Position.Y;
                            float spineshoulderz = joints[JointType.SpineShoulder].Position.Z;

                           // nombre = Nomb.Text;
                                                      
                            label.Content = nombre;
                            BASED.insertar(idlectura, nombre ,headx, heady, headz, neckx, necky, neckz, hipleftx, hiplefty, hipleftz, footleftx, footlefty, footleftz, handleftx, handlefty, handleftz,
                            hiprightx, hiprighty, hiprightz, kneeleftx, kneelefty, kneeleftz, spinemidx, spinemidy, spinemidz, ankleleftx, anklelefty, ankleleftz, elbowleftx, elbowlefty,
                            elbowleftz, footrightx, footrighty, footrightz, handrightx, handrighty, handrightz, kneerightx, kneerighty, kneerightz, spinebasex, spinebasey, spinebasez,
                            thumbleftx, thumblefty, thumbleftz, wristleftx, wristlefty, wristleftz, anklerightx, anklerighty, anklerightz, elbowrightx, elbowrighty, elbowrightz, thumbrightx,
                            thumbrighty, thumbrightz, wristrightx, wristrighty, wristrightz, handtipleftx, handtiplefty, handtipleftz, handtiprightx, handtiprighty, handtiprightz, shoulderleftx,
                            shoulderlefty, shoulderleftz, shoulderrightx, shoulderrighty, shoulderrightz, spineshoulderx, spineshouldery, spineshoulderz); 

                            label2.Content = idlectura;




                            foreach (JointType jointType in joints.Keys)
                            {
                                // sometimes the depth(Z) of an inferred joint may show as negative
                                // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                                CameraSpacePoint position = joints[jointType].Position;
                                if (position.Z < 0)
                                {
                                    position.Z = InferredZPositionClamp;
                                }

                                DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
                            }

                            this.DrawBody(joints, jointPoints, dc, drawPen);

                            this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                            this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);
                        }

                    }

                    // prevent drawing outside of our render area
                    this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
               
                } //using
            } 
            
        } 
         counter++;
    }
       


          // Boton Pausa/  clave unica.
        public void Button_Click(object sender, RoutedEventArgs e)
        {
                    

            if (sesion == true) //re  true
            {
                nombre = Nomb.Text;
                idlectura = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                sesion = false;
                Nomb.Clear();
            }

            else 
            {

                sesion = true;     
            //System.Windows.Application.Current.Shutdown();
            
            }


    
            }

        private void Ref_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //nombre = Nomb.Text;
            //Nomb.Clear();

        }
       
        
/// <summary>
        /// Draws a body
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
            }
        }

        /// <summary>
        /// Draws one bone of a body (joint to joint)
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="jointType0">first joint of bone to draw</param>
        /// <param name="jointType1">second joint of bone to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// /// <param name="drawingPen">specifies color to draw a specific bone</param>
        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        /// <summary>
        /// Draws a hand symbol if the hand is tracked: red circle = closed, green circle = opened; blue circle = lasso
        /// </summary>
        /// <param name="handState">state of the hand</param>
        /// <param name="handPosition">position of the hand</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawHand(HandState handState, Point handPosition, DrawingContext drawingContext)
        {
            switch (handState)
            {
                case HandState.Closed:
                    drawingContext.DrawEllipse(this.handClosedBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Open:
                    drawingContext.DrawEllipse(this.handOpenBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Lasso:
                    drawingContext.DrawEllipse(this.handLassoBrush, null, handPosition, HandSize, HandSize);
                    break;
            }
            //Console.Write(handPosition);
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping body data
        /// </summary>
        /// <param name="body">body to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>   // dibuja los bordes recortados
        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, this.displayHeight - ClipBoundsThickness, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, this.displayHeight));
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(this.displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, this.displayHeight));
            }
        }

        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            // on failure, set the status text
            this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.SensorNotAvailableStatusText;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

      
 
    }
  




    

}
