using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using YieldReturn;

namespace AsyncYieldReturn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await ProcessPeople();
        }

        private async Task ProcessPeople()
        {
            var people = GetPeople(1_000_000);

            // Only when people is being accessed GetPeople() will be called
            // We lazily iterate over the GetPeople() method
            // Thereby we only need to generate 1000 ppl instead of 1 million
            await foreach (var person in people)
            {
                if (person.Id < 1000)
                {
                    MyListbox.Items.Add(person.Name);
                }
                else
                {
                    break;
                }
            }
        }

        async IAsyncEnumerable<Person> GetPeople(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(300);
                yield return new Person { Id = i, Name = $"Person {i}" };
            }
        }
    }
}
