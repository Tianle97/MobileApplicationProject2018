using MySoundsBoard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MySoundsBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> Sounds;

        private List<String> Suggestions;

        private List<MenuItem> Menus;
       
        public MainPage()
        {
            this.InitializeComponent();
            Sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(Sounds);

            Menus = new List<MenuItem>();
            Menus.Add(new MenuItem { IconFile = "Assets/Icon/animals.png", Category = SoundCategory.Animals });
            Menus.Add(new MenuItem { IconFile = "Assets/Icon/cartoon.png", Category = SoundCategory.Cartoons });
            Menus.Add(new MenuItem { IconFile = "Assets/Icon/people.png", Category = SoundCategory.Life });
            Menus.Add(new MenuItem { IconFile = "Assets/Icon/warning.png", Category = SoundCategory.Warnings });

            BackButton.Visibility = Visibility.Collapsed;

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //Click `SearchAutoSuggestBox` will show the all of sound's name
            SoundManager.GetAllSounds(Sounds);

            //if enter the first letter it will show a list which startwith the first letter
            Suggestions = Sounds.Where(p => p.Name.StartsWith(sender.Text)).Select(p => p.Name).ToList();
            SearchAutoSuggestBox.ItemsSource = Suggestions;
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SoundManager.GetSuundsByName(Sounds, sender.Text);
            CategoryTextBlock.Text = sender.Text;
            MenuItem.SelectedItem = null;
            BackButton.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.GetAllSounds(Sounds);
            CategoryTextBlock.Text = "All Sounds";
            MenuItem.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menus = (MenuItem)e.ClickedItem;

            //Filter on category
            CategoryTextBlock.Text = menus.Category.ToString();
            SoundManager.GetSoundsByCAtegory(Sounds, menus.Category);
            BackButton.Visibility = Visibility.Visible;

        }

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Sound)e.ClickedItem;
            MyMediaElement.Source = new Uri(this.BaseUri, sound.AudioFile);
        }

        private async void SoundGridView_Drop(object sender, DragEventArgs e)
        {
            //add somrthing what you want to sounds
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    var contentType = storageFile.ContentType;

                    StorageFolder folder = ApplicationData.Current.LocalFolder;

                    if (contentType == "audio/wav" || contentType == "audio/mpeg")
                    {
                        StorageFile file = await storageFile.CopyAsync(folder, storageFile.Name, NameCollisionOption.GenerateUniqueName);

                        MyMediaElement.SetSource(await storageFile.OpenAsync(FileAccessMode.Read), contentType);
                        MyMediaElement.Play();
                    }
                }
            }
        }

        private void SoundGridView_DragOver(object sender, DragEventArgs e)
        {
            //get the sound
            e.AcceptedOperation = DataPackageOperation.Copy;

            e.DragUIOverride.Caption = "drop to create a custom sound and tile";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }
    }
}

