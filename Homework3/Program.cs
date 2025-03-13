using System;
using System.Windows.Forms;

namespace Homework3
{
    // Define the Product
    public class Car
    {
        public string Engine { get; set; }
        public string Wheels { get; set; }
        public string Color { get; set; }
        public string Series { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }

        public override string ToString()
        {
            return $"Engine: {Engine}\nWheels: {Wheels}\nColor: {Color}\nSeries: {Series}\nTransmission: {Transmission}\nFuel Type: {FuelType}";
        }
    }

    // Define the Builder Interface
    public interface ICarBuilder
    {
        void SetEngine(string engine);
        void SetWheels(string wheels);
        void SetColor(string color);
        void SetSeries(string series);
        void SetTransmission(string transmission);
        void SetFuelType(string fuelType);
        Car Build();
    }

    // Implement the Concrete Builder
    public class ConcreteCarBuilder : ICarBuilder
    {
        private Car _car = new Car();

        public void SetEngine(string engine) => _car.Engine = engine;
        public void SetWheels(string wheels) => _car.Wheels = wheels;
        public void SetColor(string color) => _car.Color = color;
        public void SetSeries(string series) => _car.Series = series;
        public void SetTransmission(string transmission) => _car.Transmission = transmission;
        public void SetFuelType(string fuelType) => _car.FuelType = fuelType;

        public Car Build()
        {
            Car builtCar = _car;
            _car = new Car(); // Reset for next build
            return builtCar;
        }
    }

    // Define the Director
    public class Director
    {
        private ICarBuilder _builder;

        public Director(ICarBuilder builder)
        {
            _builder = builder;
        }

        public Car Construct(string engine, string wheels, string color, string series, string transmission, string fuelType)
        {
            _builder.SetEngine(engine);
            _builder.SetWheels(wheels);
            _builder.SetColor(color);
            _builder.SetSeries(series);
            _builder.SetTransmission(transmission);
            _builder.SetFuelType(fuelType);
            return _builder.Build();
        }
    }

    // Windows Forms UI
    public partial class MainForm : Form
    {
        private ComboBox cbEngine, cbWheels, cbColor, cbSeries, cbTransmission, cbFuelType;
        private TextBox txtOutput;
        private Button btnBuild;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Car Builder";
            this.Size = new System.Drawing.Size(400, 400);

            Label lblEngine = new Label { Text = "Engine:", Left = 20, Top = 20 };
            cbEngine = new ComboBox { Left = 120, Top = 20, Width = 150 };
            cbEngine.Items.AddRange(new string[] { "V6", "V8", "V10","Electric" });

            Label lblWheels = new Label { Text = "Wheels:", Left = 20, Top = 60 };
            cbWheels = new ComboBox { Left = 120, Top = 60, Width = 150 };
            cbWheels.Items.AddRange(new string[] { "17-inch", "18-inch", "19-inch" });

            Label lblColor = new Label { Text = "Color:", Left = 20, Top = 100 };
            cbColor = new ComboBox { Left = 120, Top = 100, Width = 150 };
            cbColor.Items.AddRange(new string[] { "Red", "Blue", "Black", "Pink" });

            Label lblSeries = new Label { Text = "Series:", Left = 20, Top = 140 };
            cbSeries = new ComboBox { Left = 120, Top = 140, Width = 150 };
            cbSeries.Items.AddRange(new string[] { "2022", "2023", "2024", "Gray" });

            Label lblTransmission = new Label { Text = "Transmission:", Left = 20, Top = 180 };
            cbTransmission = new ComboBox { Left = 120, Top = 180, Width = 150 };
            cbTransmission.Items.AddRange(new string[] { "Automatic", "Manual" });

            Label lblFuelType = new Label { Text = "Fuel Type:", Left = 20, Top = 220 };
            cbFuelType = new ComboBox { Left = 120, Top = 220, Width = 150 };
            cbFuelType.Items.AddRange(new string[] { "Petrol", "Diesel", "Electric" });

            btnBuild = new Button { Text = "Build Car", Left = 120, Top = 260, Width = 150 };
            btnBuild.Click += BtnBuild_Click;

            txtOutput = new TextBox { Left = 20, Top = 300, Width = 350, Multiline = true, Height = 80 };

            this.Controls.Add(lblEngine);
            this.Controls.Add(cbEngine);
            this.Controls.Add(lblWheels);
            this.Controls.Add(cbWheels);
            this.Controls.Add(lblColor);
            this.Controls.Add(cbColor);
            this.Controls.Add(lblSeries);
            this.Controls.Add(cbSeries);
            this.Controls.Add(lblTransmission);
            this.Controls.Add(cbTransmission);
            this.Controls.Add(lblFuelType);
            this.Controls.Add(cbFuelType);
            this.Controls.Add(btnBuild);
            this.Controls.Add(txtOutput);
        }

        private void BtnBuild_Click(object sender, EventArgs e)
        {
            ICarBuilder builder = new ConcreteCarBuilder();
            Director director = new Director(builder);

            Car myCar = director.Construct(
                cbEngine.SelectedItem?.ToString() ?? "Unknown Engine",
                cbWheels.SelectedItem?.ToString() ?? "Unknown Wheels",
                cbColor.SelectedItem?.ToString() ?? "Unknown Color",
                cbSeries.SelectedItem?.ToString() ?? "Unknown Series",
                cbTransmission.SelectedItem?.ToString() ?? "Unknown Transmission",
                cbFuelType.SelectedItem?.ToString() ?? "Unknown Fuel Type"
            );

            txtOutput.Text = myCar.ToString();
        }
    }

    // Entry Point
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
