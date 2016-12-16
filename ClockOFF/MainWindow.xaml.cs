using ClockOFF.IServices;
using ClockOFF.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ClockOFF
{
    public partial class MainWindow : Window
    {
        private List<int> _timeIntervals = new List<int> { 1, 15, 30, 45, 60, 90, 120, 240 };

        private readonly ISwitcherService _switchService;

        public MainWindow()
        {
            InitializeComponent();
            StartCustomComponents();

            _switchService = new SwitchService();
        }

        private void StartCustomComponents()
        {
            rbCrono.IsChecked = true;            
        }

        private void TypeProgrammingChanged(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "rbCrono":
                    SetComboForCrono();
                    break;

                case "rbTimed":
                    SetComboForTimed();
                    break;
            }
        }

        private void SendSelectionToMachine(object sender, RoutedEventArgs e)
        {
            if (rbTimed.IsChecked.Value)
            {
                if (cbTimeSelector.SelectedIndex > -1)
                {
                    var tTimeSelectedSplitted = cbTimeSelector.SelectedItem.ToString().Split(':');
                    var tTimeSelected = new TimeSpan(int.Parse(tTimeSelectedSplitted[0]), int.Parse(tTimeSelectedSplitted[1]), 0);

                    if (_switchService.TurnComputerOffAtProgrammedTime(tTimeSelected))
                        NotifyAndCloseApplication();
                }                    
                else
                    MessageBox.Show("Selecciona una hora para apagar el equipo");
            }
            else if (rbCrono.IsChecked.Value)
            {
                if (cbTimeSelector.SelectedIndex > -1)
                {
                    if(_switchService.TurnComputerOffInXMinutes((int)this.cbTimeSelector.SelectedItem))
                        NotifyAndCloseApplication();
                }
                else
                    MessageBox.Show("Selecciona unos minutos para apagar el equipo");
            }
        }

        private void NotifyAndCloseApplication()
        {
            MessageBox.Show("El equipo se apagará en el momento solicitado");
            CloseWindow(null, null);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetComboForCrono()
        {
            ClearComboForSelectionIfShould();
            foreach (var iItem in _timeIntervals)
            {
                cbTimeSelector.Items.Add(iItem);
            }
        }

        private void SetComboForTimed()
        {
            ClearComboForSelectionIfShould();
            for (var iHour = 1; iHour < 24; iHour++) {
                for (var iMinute = 0; iMinute < 60; iMinute += 15) {
                    cbTimeSelector.Items.Add(iHour + ":" + iMinute);
                }
            }
        }

        private void ClearComboForSelectionIfShould()
        {
            if (cbTimeSelector.Items.Count > 0)
                cbTimeSelector.Items.Clear();
        }
    }
}
