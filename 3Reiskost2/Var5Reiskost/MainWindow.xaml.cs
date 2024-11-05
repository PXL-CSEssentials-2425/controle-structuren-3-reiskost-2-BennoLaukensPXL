using System;
using System.Collections.Generic;
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

namespace Var5Reiskost
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

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();


            float.TryParse(baseFlightTextBox.Text, out float baseFlight);

            float.TryParse(basePriceTextBox.Text, out float basePrice);
            float.TryParse(numberOfDaysTextBox.Text, out float numberOfDays);
            float.TryParse(numberOfPersonsTextBox.Text, out float numberOfPersons);
            float.TryParse(reductionPercentageTextBox.Text, out float reductionPercentage);
            float flightclass;

            float totalFlight = baseFlight * numberOfPersons;
            float totalStay = basePrice * numberOfDays * numberOfPersons;
            float totalPrice = totalStay + totalFlight;
            float totalDiscount = totalPrice * reductionPercentage / 100;

            float thirdPerson = basePrice * 0.5f;
            float moreThanThree = basePrice - basePrice * 0.7f;

            switch (flightClassTextBox.Text)
            {
                case "1":
                    flightclass = baseFlight * 0.3f;
                    totalFlight += flightclass;
                    break;
                case "3":
                    flightclass = baseFlight * 0.2f;
                    totalFlight -= flightclass;
                    break;
                default:
                    flightclass = 0;
                    break;
            }

            if (numberOfPersons == 3)
            {
                totalStay = basePrice * 2 + thirdPerson;
                totalStay = totalStay * numberOfDays;
            }
            if (numberOfPersons > 3)
            {
                numberOfPersons -= 3;
                moreThanThree = moreThanThree * numberOfPersons;
                totalStay = basePrice * 2 + thirdPerson + moreThanThree;
                totalStay = totalStay * numberOfDays;
            }


            sb.AppendLine($"Rijskost volgens bestelling naar {destinationTextBox.Text}");
            sb.AppendLine();
            sb.AppendLine($"Totale vluchtprijs: € {totalFlight:f2}");
            sb.AppendLine($"Totale verblijfprijs: € {totalStay:f2}");
            sb.AppendLine($"Totale reisprijs: € {totalPrice:f2}");
            sb.AppendLine($"Korting: € {totalDiscount:f2}");
            sb.AppendLine();
            sb.AppendLine($"Te betalen: € {totalPrice - totalDiscount:f2}");

            resultTextBox.Text = sb.ToString();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            destinationTextBox.Text = "London";
            baseFlightTextBox.Text = "200";
            flightClassTextBox.Text = "1";
            basePriceTextBox.Text = "60";
            numberOfDaysTextBox.Text = "7";
            numberOfPersonsTextBox.Text = "4";
            reductionPercentageTextBox.Text = "5";
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
