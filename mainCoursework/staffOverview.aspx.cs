﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

namespace mainCoursework
{
    public partial class overview : System.Web.UI.Page
	{
		struct dataEntryStruct : IComparable
		{
			public dataEntryStruct(string Xaxis, decimal Yaxis)
			{
				Xvalue = Xaxis;
				Yvalue = Yaxis;
			}
			public readonly string Xvalue;
			public readonly decimal Yvalue;

			int IComparable.CompareTo(object obj)
			{
				dataEntryStruct comparison = (dataEntryStruct)obj;
				if (comparison.Yvalue > this.Yvalue)
				{
					return 1;
				}
				else if (comparison.Yvalue < this.Yvalue)
				{
					return -1;
				}
				else
				{
					return 0;
				}
			}
		}

		defaultDataSetTableAdapters.ordersTableAdapter adaptor = new defaultDataSetTableAdapters.ordersTableAdapter();

		protected void Page_Load(object sender, EventArgs e)
        {
			
		}

		//If "forever" is selected, grey out the time length text box
		protected void timeLength_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (timeLength.SelectedValue == "forever")
			{
				dateBox.ReadOnly = true;
			}
			else
			{
				dateBox.ReadOnly = false;
			}
		}

		//If one of the time modes is selected, grey out the time period radio buttons and the time beginning box
		protected void dataFilterType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dataFilterType.SelectedIndex > 1)
			{
				timeLength.Enabled = false;
				timeLength.SelectedValue = "day";
				dateBox.ReadOnly = true;
				dateBox.Text = "";
			}
			else
			{
				timeLength.Enabled = true;
				dateBox.ReadOnly = false;
			}
		}

		//Apply the selected settings and render the graph with those settings
		protected void applyButton_Click(object sender, EventArgs e)
		{
			//Pull the date the user wants and deny them if it's not in a valid format (so long as it's needed)
			DateTime startTime;
			if (!DateTime.TryParse(dateBox.Text, out startTime) && timeLength.SelectedValue != "forever" && dataFilterType.SelectedIndex < 2)
			{
				returnLabel.Text = "That's not a valid date format!";
				return;
			}

			var data = getData(startTime);
			string[] xValues = new string[data.Length];
			decimal[] yValues = new decimal[data.Length];
			int i = 0;
			foreach (dataEntryStruct current in data)
			{
				xValues[i] = current.Xvalue;
				yValues[i] = current.Yvalue;
				i++;
			}
			mainChart.ChartAreas["chartArea"].AxisX = new Axis(mainChart.ChartAreas["chartArea"], AxisName.X);
			mainChart.ChartAreas["chartArea"].AxisY = new Axis(mainChart.ChartAreas["chartArea"], AxisName.Y);
			mainChart.Series["Default"].Points.DataBindXY(xValues, yValues);
		}

		//Get the data and format it ready to add to the chart
		private dataEntryStruct[] getData(DateTime startTime)
		{
			//Get the data
			DataTable data = adaptor.GetData();
			DateTime[] bounds = new DateTime[2];
			dataEntryStruct[] output;
			bounds[0] = startTime;

			//Filter out the unwanted data if the user isn't having time divisions on the X axis
			if (dataFilterType.SelectedIndex < 1 && timeLength.SelectedValue != "forever")
			{
				DataView view = new DataView(data);
				switch (timeLength.SelectedValue)
				{
					case "day":
						bounds[1] = startTime.AddDays(1);
						break;
					case "week":
						bounds[1] = startTime.AddDays(7);
						break;
					case "month":
						bounds[1] = startTime.AddMonths(1);
						break;
					case "6month":
						bounds[1] = startTime.AddMonths(6);
						break;
					case "year":
						bounds[1] = startTime.AddYears(1);
						break;
				}
				view.RowFilter = "#" + bounds[0].ToString() + "# < datePlaced < #" + bounds[1].ToString() + "#";
				data = view.ToTable();
			}

			//Group the values in the table into their needed format
			if (dataFilterType.SelectedValue == "customer")
			{
				List<dataEntryStruct> list = new List<dataEntryStruct>(); 
				var query = data.Rows.Cast<DataRow>().GroupBy(product => product["customer"]).Select(grouped => new {
					name = grouped.Key,
					total = grouped.Sum(product => (int)product["productAmount"])
				});

				foreach (var t in query)
				{
					list.Add(new dataEntryStruct(Convert.ToString(t.name), Convert.ToDecimal(t.total)));
				}
				output = list.ToArray();
			}
			else if (dataFilterType.SelectedValue == "product")
			{
				List<dataEntryStruct> list = new List<dataEntryStruct>();
				var query = data.Rows.Cast<DataRow>().GroupBy(product => product["product"]).Select(grouped => new {
					name = grouped.Key,
					total = grouped.Sum(product => (int)product["productAmount"])
				});

				foreach (var t in query)
				{
					list.Add(new dataEntryStruct(Convert.ToString(t.name), Convert.ToDecimal(t.total)));
				}
				output = list.ToArray();
			}
			else
			{
				output = new dataEntryStruct[0];
			}

			Array.Sort(output);
			return output;
		}
	}
}