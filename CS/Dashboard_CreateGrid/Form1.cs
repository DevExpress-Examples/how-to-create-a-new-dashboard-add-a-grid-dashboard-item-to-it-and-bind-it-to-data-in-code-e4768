﻿using System;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess;
using DevExpress.DataAccess.Excel;
using DevExpress.XtraEditors;

namespace Dashboard_CreateGrid {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {

            // Creates the Excel data source.
            DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource();
            excelDataSource = CreateExcelDataSource();

            // Creates the Grid dashboard item and adds it to a dashboard.
            dashboardViewer1.Dashboard = new Dashboard();
            dashboardViewer1.Dashboard.DataSources.Add(excelDataSource);
            GridDashboardItem grid = CreateGrid(excelDataSource);
            dashboardViewer1.Dashboard.Items.Add(grid);

            // Reloads data in the data sources.
            dashboardViewer1.ReloadData();
        }
        private GridDashboardItem CreateGrid(DashboardExcelDataSource dataSource) {

            // Creates a grid dashboard item and specifies its data source.
            GridDashboardItem grid = new GridDashboardItem();
            grid.DataSource = dataSource;

            // Creates new grid columns of the specified type and with the specified dimension or
            // measure. Then, adds these columns to the grid's Columns collection.
            grid.Columns.Add(new GridDimensionColumn(new Dimension("Category")));
            grid.Columns.Add(new GridHyperlinkColumn(new Dimension("Product"), "https://www.google.com/search?q={0}"));
            grid.Columns.Add(new GridMeasureColumn(new Measure("Count")));
            grid.Columns.Add(new GridDeltaColumn(new Measure("Count"),
                                                 new Measure("TargetCount")));
            grid.Columns.Add(new GridSparklineColumn(new Measure("Count")));
            grid.SparklineArgument = new Dimension("Date", DateTimeGroupInterval.MonthYear);

            return grid;
        }

        public DashboardExcelDataSource CreateExcelDataSource() {

            // Generates the Excel Data Source.
            DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource();
            excelDataSource.FileName = @"Data\SimpleDataSource.xls";
            ExcelWorksheetSettings worksheetSettings = new ExcelWorksheetSettings("Simple Data", "A1:F12");
            excelDataSource.SourceOptions = new ExcelSourceOptions(worksheetSettings);
            excelDataSource.Fill();

            return excelDataSource;
        }
    }
}
