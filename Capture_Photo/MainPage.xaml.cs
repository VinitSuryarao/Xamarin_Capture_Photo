using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Capture_Photo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsTakePhotoSupported && !CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Message","Photo Capture Not Supported","Ok");
                return;
            }
            else
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory="Images",
                    Name=DateTime.Now.ToString() +"_Test.jpg"
                });

                if (file == null)
                    return;
                await DisplayAlert("Message", file.Path, "ok");

                imgPhoto.Source = ImageSource.FromStream(()=>
                {
                    var stream = file.GetStream();
                    return stream;
                });

            }

        }
    }
}
