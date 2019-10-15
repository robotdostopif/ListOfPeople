using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListOfPeople
{
    public class Person
    {
        public string name { get; set; }
        public int age { get; set; }
        public string country { get; set; }
        public Person(string Name, int Age, string Country)
        {
            name = Name;
            age = Age;
            country = Country;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void PersonToList()
        {
            string[] text = File.ReadAllLines("People.txt");
            string[] split = null;
            peopleList = new List<Person>();

            foreach (string t in text)
            {
                split = t.Split(',');
                Person h = new Person(split[0], int.Parse(split[1]), split[2]);
                peopleList.Add(h);
            }

        }


        public void PersonToFile()
        {
            string[] outputList = peopleList.Select(p => $"{p.name},{p.age},{p.country}").ToArray();
            File.WriteAllLines("People.txt", outputList);
        }

        public TextBox nameBox;
        public TextBox ageBox;
        public TextBox countryBox;
        public ComboBox peopleBox;
        public TextBox info;
        public Button submit;
        public List<Person> peopleList;
        public MainWindow()
        {
            InitializeComponent();
            PersonToList();
            Start();
        }

        public void Start()
        {
            Grid grid = (Grid)Content;

            grid.Margin = new Thickness(5);
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Auto) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300, GridUnitType.Auto) });


            peopleBox = new ComboBox()
            {

                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 20,
                Width = 250,


            };

            Grid.SetColumn(peopleBox, 0);
            Grid.SetRow(peopleBox, 1);

            foreach (var p in peopleList)
            {
                peopleBox.Items.Add(p.name);
            }

            grid.Children.Add(peopleBox);
            peopleBox.SelectionChanged += PeopleBox_SelectionChanged;


            info = new TextBox()
            {

                Height = 20,
                Width = 250,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            grid.Children.Add(info);
            Grid.SetColumn(info, 0);
            Grid.SetRow(info, 2);


            submit = new Button()
            {
                Content = "Enter",
                Height = 30,
                Width = 60,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10)
            };

            submit.Click += Submit_Click;
            grid.Children.Add(submit);
            Grid.SetColumn(submit, 0);
            Grid.SetRow(submit, 6);


            nameBox = Text_Box("Name");

            grid.Children.Add(nameBox);
            Grid.SetColumn(nameBox, 0);
            Grid.SetRow(nameBox, 3);


            ageBox = Text_Box("Age");

            grid.Children.Add(ageBox);
            Grid.SetColumn(ageBox, 0);
            Grid.SetRow(ageBox, 4);

            countryBox = Text_Box("Country");

            grid.Children.Add(countryBox);
            Grid.SetColumn(countryBox, 0);
            Grid.SetRow(countryBox, 5);


            Label nameLabel = Label_New("Name");

            grid.Children.Add(nameLabel);
            Grid.SetColumn(nameLabel, 0);
            Grid.SetRow(nameLabel, 3);


            Label ageLabel = Label_New("Age");

            grid.Children.Add(ageLabel);
            Grid.SetColumn(ageLabel, 0);
            Grid.SetRow(ageLabel, 4);


            Label countryLabel = Label_New("Country");

            grid.Children.Add(countryLabel);
            Grid.SetColumn(countryLabel, 0);
            Grid.SetRow(countryLabel, 5);

            grid.KeyDown += OnKeyDownHandler;
        }
        public TextBox Text_Box(string name)
        {
            TextBox text = new TextBox()
            {
                Name = name,
                Height = 20,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            return text;
        }
        public Label Label_New(string name)
        {
            Label label = new Label()
            {
                Content = name,
                Width = 55,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            return label;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person p = new Person(nameBox.Text, int.Parse(ageBox.Text), countryBox.Text);
                peopleList.Add(p);
                PersonToFile();
                Start();
            }

            catch
            {
                MessageBox.Show("You have to enter valid values, try again!");
            }
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Submit_Click(sender, e);
            }
        }
        private void PeopleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox personBox = (ComboBox)sender;

            var person = peopleList.Where(p => p.name == personBox.SelectedItem.ToString()).First();

            info.Text = $" {person.name} is {person.age} years old and lives in {person.country}";
        }
    }
}
