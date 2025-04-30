using NeuroGenesisAI.Core.Entities;
using NeuroGenesisAI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NeuroGenesisAI.VisualizerWPF
{
    public partial class MainWindow : Window
    {
        private readonly Brain _brain;
        private readonly Trainer _trainer;
        private readonly Dictionary<string, (Ellipse, Point)> _neuronVisuals = new();
        private readonly Random _random = new();
        private readonly DispatcherTimer _timer;
        private Point _lastMousePosition;
        private bool _isDragging = false;

        public MainWindow()
        {
            InitializeComponent();

            _brain = new Brain();       // Instancia o cérebro artificial
            _trainer = new Trainer(_brain); // Instancia o treinador

            _ = StartTraining(); // Inicia treinamento assíncrono sem travar a UI

            // Timer que atualiza o estado do cérebro
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500) // Atualiza a cada 1 segundo
            };
            _timer.Tick += UpdateBrain;
            _timer.Start();
        }

        /// <summary>
        /// Inicia a sequência de treinamento do cérebro.
        /// </summary>
        private async Task StartTraining()
        {
            Console.WriteLine("\n🚀 Iniciando Treinamento Básico...");
            await _trainer.RunBasicTraining(rounds: 5, delayMilliseconds: 2000);

            Console.WriteLine("\n🚀 Iniciando Desafios Cognitivos...");
            await _trainer.RunChallengeTraining(rounds: 5, delayMilliseconds: 1500);
        }

        /// <summary>
        /// Atualiza o estado do cérebro e redesenha no canvas.
        /// </summary>
        private void UpdateBrain(object sender, EventArgs e)
        {
            _brain.Cycle(); // Evolui a rede neural

            BrainCanvas.Children.Clear(); // Limpa o canvas para redesenhar

            // Atualiza neurônios e posições
            foreach (var neuron in _brain.neurons)
            {
                if (!_neuronVisuals.ContainsKey(neuron.Id))
                {
                    var position = new Point(
                        _random.Next(50, (int)BrainCanvas.ActualWidth - 50),
                        _random.Next(50, (int)BrainCanvas.ActualHeight - 50)
                    );

                    var ellipse = new Ellipse
                    {
                        Width = 5 + neuron.MaturityLevel,
                        Height = 5 + neuron.MaturityLevel,
                        Fill = GetBrushForNeuron(neuron)
                    };

                    _neuronVisuals[neuron.Id] = (ellipse, position);
                }
            }

            // Desenha conexões primeiro
            foreach (var neuron in _brain.neurons)
            {
                if (neuron is Neuron concreteNeuron)
                {
                    foreach (var connection in concreteNeuron.Connections)
                    {
                        if (_neuronVisuals.ContainsKey(connection.Source.Id) &&
                            _neuronVisuals.ContainsKey(connection.Target.Id))
                        {
                            var sourcePos = _neuronVisuals[connection.Source.Id].Item2;
                            var targetPos = _neuronVisuals[connection.Target.Id].Item2;

                            var line = new Line
                            {
                                X1 = sourcePos.X,
                                Y1 = sourcePos.Y,
                                X2 = targetPos.X,
                                Y2 = targetPos.Y,
                                Stroke = new SolidColorBrush(ColorFromStrength(connection.Strength)),
                                StrokeThickness = 1
                            };

                            BrainCanvas.Children.Add(line);
                        }
                    }
                }
            }

            // Desenha neurônios acima das conexões
            foreach (var (ellipse, position) in _neuronVisuals.Values)
            {
                Canvas.SetLeft(ellipse, position.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, position.Y - ellipse.Height / 2);
                BrainCanvas.Children.Add(ellipse);
            }
        }

        /// <summary>
        /// Retorna a cor do neurônio baseado no cluster ao qual pertence.
        /// </summary>
        private SolidColorBrush GetBrushForNeuron(INeuron neuron)
        {
            foreach (var cluster in _brain.clusters)
            {
                if (cluster.Neurons.Contains(neuron))
                {
                    return GetColorForCluster(cluster.Name);
                }
            }
            return Brushes.White;
        }

        /// <summary>
        /// Define a cor baseada no nome do cluster.
        /// </summary>
        private SolidColorBrush GetColorForCluster(string clusterName)
        {
            return clusterName switch
            {
                "Visão" => Brushes.LightBlue,
                "Audição" => Brushes.LightGreen,
                "Memória" => Brushes.LightYellow,
                "Movimento" => Brushes.LightCoral,
                _ => Brushes.White
            };
        }

        /// <summary>
        /// Define cor de conexão baseada na força da conexão.
        /// </summary>
        private Color ColorFromStrength(double strength)
        {
            byte intensity = (byte)Math.Min(255, strength * 255);
            return Color.FromRgb(intensity, intensity, intensity);
        }

        // ==== Controle de Zoom e Pan ====

        private void BrainCanvas_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            double zoomFactor = e.Delta > 0 ? 1.1 : 0.9;

            BrainScale.ScaleX *= zoomFactor;
            BrainScale.ScaleY *= zoomFactor;
        }

        private void BrainCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _lastMousePosition = e.GetPosition(this);
            _isDragging = true;
            BrainCanvas.CaptureMouse();
        }

        private void BrainCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isDragging = false;
            BrainCanvas.ReleaseMouseCapture();
        }

        private void BrainCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                var deltaX = currentPosition.X - _lastMousePosition.X;
                var deltaY = currentPosition.Y - _lastMousePosition.Y;

                BrainTranslate.X += deltaX;
                BrainTranslate.Y += deltaY;

                _lastMousePosition = currentPosition;
            }
        }
    }
}
