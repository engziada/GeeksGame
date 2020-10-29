using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GeeksGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image newFace;
        private Image activeFace;
        //private Storyboard dropdownStoryboard;
        private Point prevPosition;
        private bool dragStart = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void playground_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            dropNewFace();
        }

        private void dropNewFace()
        {
            //double centerX = (Canvas.GetRight(this)-Canvas.GetLeft(this))/2 ;
            // mainGrid.ActualWidth/2;//((this.Width - sideMenu.Width) / 2) + sideMenu.Width;
            //playground.TransformToAncestor(this).Transform(new Point(0, 0)).X

            newFace = new Image();
            newFace.Name = "img_Face_" + DateTime.Now.Ticks;
            newFace.Source = new BitmapImage(new Uri(@"/Images/ThaiBox.png", UriKind.Relative));
            newFace.Width = 150;
            newFace.Cursor = Cursors.Hand;

            //newFace.MouseDown += NewFace_MouseDown;
            //newFace.MouseUp += NewFace_MouseUp;
            //newFace.MouseMove += NewFace_MouseMove;
            //newFace.MouseEnter += NewFace_MouseEnter; 
            //newFace.MouseLeave += NewFace_MouseLeave;
            newFace.PreviewMouseLeftButtonDown += NewFace_PreviewMouseLeftButtonDown;
            newFace.PreviewMouseLeftButtonUp += NewFace_PreviewMouseLeftButtonUp;
            newFace.PreviewMouseMove += NewFace_PreviewMouseMove;
            
            playground.Children.Add(newFace);
            this.RegisterName(newFace.Name, newFace);

            double left = (playground.ActualWidth - newFace.ActualWidth) / 2;
            newFace.SetValue(Canvas.LeftProperty, left);
            newFace.SetValue(Canvas.TopProperty, 0.0);

            //activeFace = newFace;


            move(newFace,0,0,0,mainGrid.ActualHeight,3);

        }


        private double FirstXPos, FirstYPos, FirstArrowXPos, FirstArrowYPos;
        private Image MovingObject;

        private void NewFace_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                (MovingObject as FrameworkElement).SetValue(Canvas.LeftProperty,
                     e.GetPosition((MovingObject as FrameworkElement).Parent as FrameworkElement).X - FirstXPos);

                (MovingObject as FrameworkElement).SetValue(Canvas.TopProperty,
                     e.GetPosition((MovingObject as FrameworkElement).Parent as FrameworkElement).Y - FirstYPos);
            }
        }

        private void NewFace_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MovingObject = null;
        }

        private void NewFace_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image selImage=sender as Image;
            FirstXPos = e.GetPosition(selImage).X;
            FirstYPos = e.GetPosition(selImage).Y;
            FirstArrowXPos = e.GetPosition(selImage.Parent as Canvas).X - FirstXPos;
            FirstArrowYPos = e.GetPosition(selImage.Parent as Canvas).Y - FirstYPos;
            MovingObject = selImage;
        }


        private void NewFace_MouseLeave(object sender, MouseEventArgs e)
        {
            activeFace = null;
        }

        private void NewFace_MouseEnter(object sender, MouseEventArgs e)
        {
            activeFace = sender as Image;
        }

        //public void startDropdownStoryboard(Image target, double oldX, double oldY, double newX, double newY, int seconds)
        //{
        //    Storyboard dropdownStoryboard=new Storyboard();
        //    Duration dropdownDuration = new Duration(TimeSpan.FromSeconds(seconds));
        //    Point fromPoint = new Point(oldX, oldY);
        //    Point toPoint = new Point(newX, newY);
        //    DoubleAnimation dropdownAnimation = new DoubleAnimation(0, this.Height, dropdownDuration);
        //    dropdownStoryboard = new Storyboard();
        //    Storyboard.SetTargetName(dropdownAnimation, newFace.Name);
        //    Storyboard.SetTargetProperty(dropdownAnimation, new PropertyPath(Canvas.TopProperty));
        //    dropdownStoryboard.Children.Add(dropdownAnimation);
        //    dropdownStoryboard.Begin(this);

        //}
        public void move(Image target, double oldX, double oldY, double newX, double newY, int seconds)
        {
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(oldY, newY, TimeSpan.FromSeconds(seconds));
            DoubleAnimation anim2 = new DoubleAnimation(oldX, newX, TimeSpan.FromSeconds(seconds));
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }




        private void NewFace_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //    activeFace = sender as Image;
            
            //if (activeFace != null && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    DataObject data = new DataObject(typeof(ImageSource), activeFace.Source);
            //    DragDrop.DoDragDrop(activeFace, data, DragDropEffects.Move);
            //}

            //var location = activeFace.TransformToAncestor(playground).Transform(new Point(0, 0));
            //location.Offset(e.GetPosition(playground).X - prevPosition.X, e.GetPosition(playground).Y - prevPosition.Y);
            ////activeFace.Location = location;
            //Canvas.SetTop(activeFace,location.X);
            //Canvas.SetLeft(activeFace,location.Y);

        }

        private void NewFace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //activeFace = null;
        }

        private void NewFace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //activeFace = sender as Image;

            //activeFace = sender as Image;
            //prevPosition = e.GetPosition(playground);

            //activeFace.SetValue(TranslateTransform.YProperty,0.0);
            //dropdownStoryboard.Freeze();
            //Point activeFacePoint = activeFace.TransformToAncestor(this).Transform(new Point(0, 0));
            //moveToBox(activeFace,activeFacePoint.X,activeFacePoint.Y,0,0,2);



        }





        private void playground_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void playground_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void playground_PreviewMouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
