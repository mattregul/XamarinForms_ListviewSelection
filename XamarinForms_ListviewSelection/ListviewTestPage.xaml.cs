using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;


namespace XamarinForms_ListviewSelection
{
	// Another good Listview sample
	// https://github.com/xamarin/xamarin-forms-samples/tree/master/UserInterface/ListView/Interactivity


	/// <summary>
	/// Employee Class
	/// </summary>
	class Employee
	{

		public Color BoxColor { get; set; }
		public string Name { get; set; }
		public string Job { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XamarinForms_ListviewSelection.Employee"/> class.
		/// </summary>
		/// <param name="_boxcolor">Color.</param>
		/// <param name="_name">Name.</param>
		/// <param name="_job">Job.</param>
		public Employee(Color _boxcolor, string _name, string _job)
		{
			BoxColor = _boxcolor;
			Name = _name;
			Job = _job;
		}
	}


	public partial class ListviewTestPage : ContentPage
	{
		// our global employee list
		List<Employee> employeeList;

		public ListviewTestPage()
		{
			InitializeComponent();

			employeeList = new List<Employee>();

			// Create a few sample employees
			employeeList.Add(new Employee(Color.Lime, "Matt", "Developer"));
			employeeList.Add(new Employee(Color.Navy, "Meredith", "Sales"));
			employeeList.Add(new Employee(Color.Purple, "Dave", "HR"));
			employeeList.Add(new Employee(Color.Yellow, "John", "Developer"));
			employeeList.Add(new Employee(Color.Red, "Sandy", "HR"));
			employeeList.Add(new Employee(Color.Blue, "Chris", "Director"));

			// Set the source
			EmployeeListview.ItemsSource = employeeList;

			// Add on our custom Tapped and Selected actions
			EmployeeListview.ItemSelected += OnSelection;
			EmployeeListview.ItemTapped += OnTap;

		}

		/// <summary>
		/// On Tap - Called when you tap on a new item in the listview
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnTap(object sender, ItemTappedEventArgs e)
		{
			// Grab the currently tapped employee from our ItemTappedEventArgs
			var employee = (Employee)e.Item;

			string tmpString = $"You *tapped* on {employee.Name}";
			//DisplayAlert("Listitem Tapped!", tmpString, "OK");

			Debug.WriteLine(tmpString);
			CurrentSelection.Text = tmpString;

			AnimateLabel();
		}

		/// <summary>
		/// On Selection - Called when you select a new item in the listview
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null

			// Grab the currently tapped employee from our SelectedItemChangedEventArgs
			var employee = (Employee)e.SelectedItem;

			string tmpString1 = $"You *selected* {employee.Name}";
			//DisplayAlert("Listitem Selected!", tmpString1, "OK");

			Debug.WriteLine(tmpString1);
			CurrentSelection.Text = tmpString1;

			AnimateLabel();

			// Uncomment if you want to clear the current selection
			//ListView lst = (ListView)sender;
			//lst.SelectedItem = null;
		}

		/// <summary>
		/// Selects the second Employee in the list
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void SelectSecondEmp(object sender, EventArgs args)
		{
			// make sure we have at least 2 employees
			if (employeeList.Count <= 1)
				return;
			
			// Select the second employee in the list
			// (The list is zero-indexed, so the first item is index 0, second is 1, etc)
			EmployeeListview.SelectedItem = employeeList[1];

			// ***************
			// This will call our OnSelection() automatically!
			// ***************
		}


		/// <summary>
		/// Unselects the currently selected employee in the listview
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void SelectNone(object sender, EventArgs args)
		{
			// Clear the current listview selection
			EmployeeListview.SelectedItem = null;	

			CurrentSelection.Text = "No one is selected";

			AnimateLabel();
		}

		/// <summary>
		/// Spin the label around in a circle!
		/// </summary>
		void AnimateLabel()
		{
			// Animation from http://xfcomplete.net/animation/2016/01/18/compound-animations/
			var animation = new Animation(callback: d => CurrentSelection.Rotation = d,
							  start: 0,
							  end: 360,
							  easing: Easing.SpringOut);
			animation.Commit(CurrentSelection, "Loop", length: 800);
		}


		/// <summary>
		/// Check to see who is currently selected
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void WhoIsSelected(object sender, EventArgs args)
		{
			string tmpString = "No one is selected";

			if (EmployeeListview.SelectedItem != null)
			{
				// store the selected Index number
				int selectedEmployeeIndex = employeeList.IndexOf((Employee)EmployeeListview.SelectedItem);

				// Grab the currently selected employee from the Listview
				Employee itemFromListview = (Employee)EmployeeListview.SelectedItem;
				tmpString = $"{itemFromListview.Name} is currently selected, which is Index #{selectedEmployeeIndex} in employeeList[]";


				// ** Another way to grab the currently selected employee from the List datasource **
				//Employee itemFromDataSource = (Employee)employeeList[selectedEmployeeIndex];
				//tmpString = $"{itemFromDataSource.Name} is currently selected, which is Index #{selectedEmployeeIndex} in employeeList[]";

			}

			Debug.WriteLine(tmpString);
			CurrentSelection.Text = tmpString;

			AnimateLabel();

		}

	}
}

