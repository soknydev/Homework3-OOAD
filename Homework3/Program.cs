using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework3
{
    // Product
    public class Car
    {
        public string Engine { get; set; }
        public string Wheels { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"Engine: {Engine}\nWheels: {Wheels}\nColor: {Color}";
        }
    }

    // Builder Interface
    public interface ICarBuilder
    {
        void SetEngine(string engine);
        void SetWheels(string wheels);
        void SetColor(string color);
        Car Build();
    }

    // Concrete Builder
    public class ConcreteCarBuilder : ICarBuilder
    {
        private Car _car = new Car();

        public void SetEngine(string engine) => _car.Engine = engine;
        public void SetWheels(string wheels) => _car.Wheels = wheels;
        public void SetColor(string color) => _car.Color = color;

        public Car Build()
        {
            Car builtCar = _car;
            _car = new Car(); // Reset for next build
            return builtCar;
        }
    }

    // Director
    public class Director
    {
        private ICarBuilder _builder;

        public Director(ICarBuilder builder)
        {
            _builder = builder;
        }

        public Car Construct(string engine, string wheels, string color)
        {
            _builder.SetEngine(engine);
            _builder.SetWheels(wheels);
            _builder.SetColor(color);
            return _builder.Build();
        }
    }

    // Windows Forms UI
    public partial class MainForm : Form
    {
        private ComboBox cbEngine, cbWheels, cbColor;
        private TextBox txtOutput;
        private Button btnBuild;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Car Builder";
            this.Size = new System.Drawing.Size(400, 300);

            Label lblEngine = new Label { Text = "Engine:", Left = 20, Top = 20 };
            cbEngine = new ComboBox { Left = 80, Top = 20, Width = 150 };
            cbEngine.Items.AddRange(new string[] { "V6", "V8", "Electric" });

            Label lblWheels = new Label { Text = "Wheels:", Left = 20, Top = 60 };
            cbWheels = new ComboBox { Left = 80, Top = 60, Width = 150 };
            cbWheels.Items.AddRange(new string[] { "17-inch", "18-inch", "19-inch" });

            Label lblColor = new Label { Text = "Color:", Left = 20, Top = 100 };
            cbColor = new ComboBox { Left = 80, Top = 100, Width = 150 };
            cbColor.Items.AddRange(new string[] { "Red", "Blue", "Black" });

            btnBuild = new Button { Text = "Build Car", Left = 80, Top = 140, Width = 150 };
            btnBuild.Click += BtnBuild_Click;

            txtOutput = new TextBox { Left = 20, Top = 180, Width = 350, Multiline = true, Height = 60 };

            this.Controls.Add(lblEngine);
            this.Controls.Add(cbEngine);
            this.Controls.Add(lblWheels);
            this.Controls.Add(cbWheels);
            this.Controls.Add(lblColor);
            this.Controls.Add(cbColor);
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
                cbColor.SelectedItem?.ToString() ?? "Unknown Color"
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
