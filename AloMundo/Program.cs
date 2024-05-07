using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;


namespace AloMundo
{ internal class Program
  {
        //inicio da imagem
        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            // Cria um novo Bitmap com a largura e altura desejadas
            Bitmap resizedImage = new Bitmap(width, height);

            // Desenha a imagem original no novo Bitmap usando as dimensões desejadas
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static string ConvertToAscii(Bitmap image)
        {
            // Caracteres ASCII usados para representar a imagem
            char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '@' };

            StringBuilder asciiArt = new StringBuilder();

            // Percorre os pixels da imagem e converte cada um em um caractere ASCII correspondente
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int asciiIndex = grayScale * (asciiChars.Length - 1) / 255;
                    char asciiChar = asciiChars[asciiIndex];
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

            return asciiArt.ToString();
        }

        static void ExibirImagem(string imagePath, int width, int height)
        {
            // Caminho para a imagem que deseja exibir
            //string imagePath = @"C:\Users\Danilo Filitto\Downloads\Panda.jpg";

            // Carrega a imagem
            Bitmap image = new Bitmap(imagePath);

            // Redimensiona a imagem para a largura e altura desejadas
            int consoleWidth = width;
            int consoleHeight = height;
            Bitmap resizedImage = ResizeImage(image, consoleWidth, consoleHeight);

            // Converte a imagem em texto ASCII
            string asciiArt = ConvertToAscii(resizedImage);

            // Exibe o texto ASCII no console
            Console.WriteLine(asciiArt);


        }
        //fim da imagem

        static void GravarArquivoTexto(string nome, string nomeDono,  float alimentado,  float limpo,  float feliz)
        {
            string fileContent = nome + Environment.NewLine;
            fileContent += nomeDono + Environment.NewLine;
            fileContent += alimentado + Environment.NewLine;
            fileContent += limpo + Environment.NewLine;
            fileContent += feliz + Environment.NewLine;
            string dir = Environment.CurrentDirectory + "\\";
            string file = dir + nome + nomeDono + ".txt";
            File.WriteAllText(file, fileContent);
        }
        static void LerArquivoTexto(string nome, string nomeDono, ref float alimentado, ref float limpo, ref float feliz);

        static void AtualizarStatus(ref float alimentado, ref float limpo, ref float feliz)
        {
            Random rand = new Random();
            int caracteristica = 0;
            caracteristica = rand.Next(3);
            switch (caracteristica)
            {
                case 0: alimentado -= rand.Next(40); break;
                case 1: limpo -= rand.Next(40); break;
                case 2: feliz -= rand.Next(40); break;
            }
        }

        static string Falar()
        {
            // falas do bichinho
            string[] frases = new string[3];
            frases[0] = "nossa o dia foi muito legal, comi o sofa";
            frases[1] = "que saudades, passei o dia todo esperando voce chegar";
            frases[2] = "hoje assisti varios desenhos";

            Random rand = new Random();
            return frases[rand.Next(frases.Length)];
        }
        static void Main(string[] args)
        {
            String nome = ""; // nome do bichinho
            string nomeDono = ""; // nome do jogador
            string entrada = ""; // entrada de dados

            //status 
            float alimentado = 100;
            float limpo = 100;
            float feliz = 100;

            string Foto = Environment.CurrentDirectory + "\\gato.jpg";



            Console.WriteLine("Meu bichinho virtual");
            ExibirImagem(Foto ,20, 15);

            //coleta de dados e entrada de dados
            if (args.Length > 0) {
                nome = args[0];
            } else
            {
                Console.Write("qual o nome do seu bichinho:");
                nome = Console.ReadLine();
            }
            Console.Write("qual o nome do meu dono?");
            nomeDono = Console.ReadLine();
            Console.WriteLine("legal estava com muita saudade de voce {0}", nomeDono);

            // coletar os dados do animal no arquivo texto
            string dir = Environment.CurrentDirectory+"\\";
            string file = dir + nome + nomeDono + ".txt";
            if (File.Exists(file))
            {
                LerArquivoTexto(nome, nomeDono, ref alimentado, ref limpo, ref feliz);
             
                while (entrada.ToLower() != "nada" && alimentado > 0 && limpo > 0 && feliz > 0)
                {
                    //alterar os status 
                    // 0 = alimento, 1 = limpo, 2 = feliz
                    AtualizarStatus(ref alimentado, ref limpo, ref feliz);
                    Console.Clear();
                    Console.WriteLine("Alimentado: {0}", alimentado);
                    Console.WriteLine("limpo: {0}", limpo);
                    Console.WriteLine("feliz: {0}", feliz);

                    if (alimentado > 40 && alimentado < 60)
                    {
                        Console.WriteLine("eu estou faminto");
                        Console.WriteLine("quero comida");
                    }
                    if (limpo > 40 && limpo < 60)
                    {
                        Console.WriteLine("eu estou suja");
                        Console.WriteLine("quero tomar banho");
                    }
                    if (feliz > 40 && feliz < 60)
                    {
                        Console.WriteLine("eu estou triste");
                        Console.WriteLine("vamos brincar");
                    }

                    Console.WriteLine(Falar());  
                    Thread.Sleep(3000);
                    Console.Clear();

                
                    Console.WriteLine("{0} o que vamos dazer hoje?", nomeDono);
                    Console.Write("brincar/comer/banho/nada:");
                    entrada = Console.ReadLine().ToLower();

                    switch (entrada)
                    {
                        case "brincar": feliz += rand.Next(20); break;
                        case "comer": alimentado += rand.Next(20); break;
                        case "banho": limpo += rand.Next(20); break;

                    }
                    if (feliz > 100) feliz = 100;
                    if (alimentado > 100) alimentado = 100;
                    if (limpo > 100) limpo = 100;

                }

                // fala de morte e de saida
                Console.Clear();
                if (alimentado <= 0 || limpo <= 0 || feliz <= 0)
                {
                    Console.WriteLine("{0} que descuido... seu bichinho desmaiou!", nomeDono);
                    Console.WriteLine("por que voce deixou ele assim?");
                }
                else
                {
                    Console.WriteLine("{0}, nao vejo a hora de brincar com voce de novo", nomeDono);
                    Console.WriteLine("volte logo!");
                }

                // armazenar os dados


                GravarArquivoTexto(nome, nomeDono, alimentado,  limpo,  feliz);
                Console.ReadKey();
        
       
            }
        }
}
