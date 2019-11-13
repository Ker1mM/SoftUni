//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.ML;
using Microsoft.ML.Trainers.LightGbm;
using MLHomeworkML.Model.DataModels;


namespace MLHomeworkML.ConsoleApp
{
    class Program
    {
        //Machine Learning model to load and use for predictions
        private const string ModelFilePath = @"MLModel.zip";

        //Dataset to use for predictions 
        private const string DataFilePath = @"D:\Projects\csharp-web\ASP.NET-Core-MVC\Resources\carsbg.csv";

        static void Main(string[] args)
        {
            if (!File.Exists(ModelFilePath))
            {
                TrainModel(DataFilePath, ModelFilePath);
            }

            Console.OutputEncoding = Encoding.UTF8;

            var testModelData = new List<ModelInput>
            {
                new ModelInput
                {
                    Make = "VW",
                    Model = "Passat",
                    Gear = "Ръчни",
                    HorsePower = 110,
                    FuelType  = "Дизел",
                    Year = "01/01/1999",
                    Range = 310000
                },

                                new ModelInput
                {
                    Make = "VW",
                    Model = "Golf",
                    Gear = "Ръчни",
                    HorsePower = 102,
                    FuelType  = "Бензин",
                    Year = "01/01/2007",
                    Range = 120000
                },
            };

            TestModel(ModelFilePath, testModelData);
        }

        // Method to load single row of data to try a single prediction
        // You can change this code and create your own sample data here (Hardcoded or from any source)
        private static ModelInput CreateSingleDataSample(MLContext mlContext, string dataFilePath)
        {
            // Read dataset to get a single row for trying a prediction          
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        .First();
            return sampleForPrediction;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        private static void TrainModel(string dataFile, string modelFile)
        {
            var mlContext = new MLContext();

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                path: dataFile,
                hasHeader: true,
                separatorChar: '\t',
                allowQuoting: true,
                allowSparse: false
                );

            var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding(
                new[]
                {
                    new InputOutputColumnPair("Make", "Make"),
                    new InputOutputColumnPair("FuelType", "FuelType"),
                    new InputOutputColumnPair("Gear", "Gear")
                })
                .Append(mlContext.Transforms.Categorical.OneHotHashEncoding(
                    new[] {
                        new InputOutputColumnPair("Model", "Model"),
                        new InputOutputColumnPair("Year", "Year")
                    })
                )
                .Append(mlContext.Transforms.Concatenate("Features",
                new[] {
                "Make", "FuelType", "Gear", "Model", "Year", "HorsePower", "Range", "CubicCapacity"
                }));

            var trainer = mlContext.Regression.Trainers.LightGbm(
                new LightGbmRegressionTrainer.Options()
                {
                    NumberOfIterations = 150,
                    LearningRate = 0.235358f,
                    NumberOfLeaves = 112,
                    MinimumExampleCountPerLeaf = 1,
                    UseCategoricalSplit = false,
                    HandleMissingValue = true,
                    MinimumExampleCountPerGroup = 200,
                    MaximumCategoricalSplitPointCount = 32,
                    CategoricalSmoothing = 20,
                    L2CategoricalRegularization = 5,
                    Booster = new GradientBooster.Options() { L2Regularization = 0.2, L1Regularization = 0 },
                    LabelColumnName = "Price",
                    FeatureColumnName = "Features"
                }
                );

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            mlContext.Model.Save(model, trainingDataView.Schema, modelFile);
        }

        private static void TestModel(string modelFile, IEnumerable<ModelInput> testModelData)
        {
            var context = new MLContext();

            var model = context.Model.Load(modelFile, out _);

            var predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

            foreach (var testData in testModelData)
            {
                var prediction = predictionEngine.Predict(testData);

                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Model: {testData.Model}");
                Console.WriteLine($"Make: {testData.Make}");
                Console.WriteLine($"Horse power: {testData.HorsePower}");
                Console.WriteLine($"Year: {testData.Year}");
                Console.WriteLine($"Range: {testData.Range}");
                Console.WriteLine($"Prediction: {prediction.Score}");
            }
        }
    }
}
