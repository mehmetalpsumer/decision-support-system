using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using weka.core.converters;

namespace dss_assignment
{
    public partial class Form1 : Form
    {
        // Allowed classifying algorithms 
        enum Classifier
        {
            NB, // Naive bayes
            KNN, // K nearest neighbour
            ID3, // Decision tree
            ANN, // Artificial neural network
            SVM // Support vector machine
        }


        // Global variables 
        const int percentSplit = 66; // percentage to train data
        static int trainSize = -1;
        static int testSize = -1;

        static Dictionary<Classifier, double> succesRates = new Dictionary<Classifier, double>(); // success rate for each algorithm
        static KeyValuePair<Classifier, double> highestSuccessRate = new KeyValuePair<Classifier, double>(); // which method had the best ratio

        static Dictionary<Classifier, weka.classifiers.Classifier> classifiers = new Dictionary<Classifier, weka.classifiers.Classifier>(); // classifier objects returned from threads
        static List<UserInput> inputObjects = new List<UserInput>(); // keep track of input-attribute relation
        
        bool readyToTest = false; // block testing until all methods are done
        weka.core.Instances insts; // parsed data instances

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.browseFileBtn.Click += browseFileBtn_Click;
        }

        private void Classify(string path)
        {
            readyToTest = false; // initialize flag

            // Try reading file, if failed exit function
            insts = ReadFile(path);  
            if (insts == null)
            {
                // Error occured reading file, display error message
                MessageBox.Show("Instances are null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = Form.ActiveForm as Form1; // get the current form object

            // Reset UI and lists
            succesRates.Clear();
            classifiers.Clear();
            form.inputPanel.Controls.Clear();
            inputObjects.Clear();
            form.textMostSuccessful.Text = "";
            form.testResult.Text = "";

            // Place attribute inputs on UI, max 18, numeric and nominal
            int offsetV = 60;
            int offsetH = 10;
            int width = 75;
            int height = 30;
            for (int i = 0; i < insts.numAttributes()-1; i++)
            {
                // Create and place label
                Label label = new Label();
                label.Width = width;
                label.Height = height;
                label.Text = insts.attribute(i).name();
                label.Parent = form.inputPanel;
                label.Location = new Point((width * (i % 8)) + offsetH, (height * (i / 8)) + (offsetV * (i/8)));

                // NumericUpDown for numeric and ComboBox for nominal values
                if (insts.attribute(i).isNumeric())
                {
                    NumericUpDown nud = new NumericUpDown();
                    nud.Width = width - 10;
                    nud.Height = height;
                    nud.Parent = form.inputPanel;
                    nud.Location = new Point((width * (i % 8)) + offsetH, (height * (i / 8)) + (offsetV * (i / 8)) + height);
                    inputObjects.Add(new UserInput(nud));
                }
                else
                {
                    string[] values = insts.attribute(i).toString().Split('{', '}')[1].Split(',');
                    ComboBox comboBox = new ComboBox();
                    comboBox.DataSource = values;
                    comboBox.Width = width - 10;
                    comboBox.Height = height;
                    comboBox.Parent = form.inputPanel;
                    comboBox.Location = new Point((width * (i % 8)) + offsetH, (height * (i / 8)) + (offsetV * (i / 8)) + height);
                    inputObjects.Add(new UserInput(comboBox));
                }
            }
            
            // Set train and test sizes
            trainSize = insts.numInstances() * percentSplit / 100;
            testSize = insts.numInstances() - trainSize;

            // Set target attribute
            insts.setClassIndex(insts.numAttributes() - 1);

            // Randomize
            weka.filters.Filter rndFilter = new weka.filters.unsupervised.instance.Randomize();
            rndFilter.setInputFormat(insts);
            insts = weka.filters.Filter.useFilter(insts, rndFilter);


            // Start threads for each method
            Thread t_SuccessNb = new Thread(() => CalculateSuccessForNb(insts));
            t_SuccessNb.Start();

            Thread t_SuccessKn = new Thread(() => CalculateSuccessForKn(insts));
            t_SuccessKn.Start();

            Thread t_SuccessDt = new Thread(() => CalculateSuccessForDt(insts));
            t_SuccessDt.Start();

            Thread t_SuccessAnn = new Thread(() => CalculateSuccessForAnn(insts));
            t_SuccessAnn.Start();

            Thread t_SuccessSvm = new Thread(() => CalculateSuccessForSvm(insts));
            t_SuccessSvm.Start();

            // Wait for threads
            t_SuccessNb.Join();
            t_SuccessKn.Join();
            t_SuccessDt.Join();
            t_SuccessAnn.Join();
            t_SuccessSvm.Join();

            // Find out which algorithm has the best success rate
            foreach(var item in succesRates)
            {
                if (highestSuccessRate.Equals(default(KeyValuePair<Classifier, double>)) || highestSuccessRate.Value < item.Value)
                {
                    highestSuccessRate = item;
                }
            }
            form.textMostSuccessful.Text = "Most successful algorithm is " + highestSuccessRate.Key + " and it will be used for testing.";
            readyToTest = true; // switch flag
        }

        private weka.core.Instances ReadFile(string path)
        {
            try
            {
                weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader(path));
                insts.setClassIndex(insts.numAttributes() - 1);
                return insts;
            }
            catch (java.lang.Exception ex)
            {
                ex.printStackTrace();
                MessageBox.Show(ex.ToString(), "Error reading file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        #region Classifying threads
        public static void CalculateSuccessForNb(weka.core.Instances originalInsts)
        {
            try {
                var form = Form.ActiveForm as Form1;
                form.successPrcNb.Text = "Training...";
                form.successRtNb.Text = "../" + testSize;

                weka.core.Instances insts = originalInsts;

                // Pre-process     
                insts = ConvertNumericToNominal(insts);
                insts = Normalize(insts);

                // Classify
                weka.classifiers.Classifier cl = new weka.classifiers.bayes.NaiveBayes();
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);
                cl.buildClassifier(train);

                int numCorrect = 0;
                double percentage = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                    {
                        numCorrect++;
                    }

                    percentage = (double)numCorrect / (double)testSize * 100.0;
                    form.successRtNb.Text = numCorrect + "/" + testSize;
                    form.successPrcNb.Text = String.Format("{0:0.00}", percentage) + "%";
                }
                succesRates.Add(Classifier.NB, percentage);
                classifiers.Add(Classifier.NB, cl);
            }
            catch (java.lang.Exception ex)
            {
                ex.printStackTrace();
                MessageBox.Show(ex.ToString(), "Error for Navie Bayes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error for Navie Bayes", "Error for Navie Bayes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CalculateSuccessForKn(weka.core.Instances originalInsts)
        {
            try
            {
                var form = Form.ActiveForm as Form1;
                form.successPrcKn.Text = "Training...";
                form.successRtKn.Text = "../" + testSize;

                weka.core.Instances insts = originalInsts;


                // Pre-process     
                insts = ConvertNominalToNumeric(insts);
                insts = Normalize(insts);

                // Classify
                weka.classifiers.Classifier cl = new weka.classifiers.lazy.IBk();
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);
                cl.buildClassifier(train);

                int numCorrect = 0;
                double percentage = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                    {
                        numCorrect++;
                    }

                    percentage = (double)numCorrect / (double)testSize * 100.0;
                    form.successRtKn.Text = numCorrect + "/" + testSize;
                    form.successPrcKn.Text = String.Format("{0:0.00}", percentage) + "%";
                }
                succesRates.Add(Classifier.KNN, percentage);
                classifiers.Add(Classifier.KNN, cl);
            }
            catch (java.lang.Exception ex)
            {
                ex.printStackTrace();
                MessageBox.Show(ex.ToString(), "Error for KNN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error for KNN", "Error for KNN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CalculateSuccessForDt(weka.core.Instances originalInsts)
        {
            try
            {
                var form = Form.ActiveForm as Form1;
                form.successPrcDt.Text = "Training...";
                form.successRtDt.Text = "../" + testSize;

                weka.core.Instances insts = originalInsts;

                // Pre-process
                insts = ConvertNumericToNominal(insts);
                insts = Normalize(insts);

                // Classify
                weka.classifiers.Classifier cl = new weka.classifiers.trees.J48();
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);
                cl.buildClassifier(train);

                int numCorrect = 0;
                double percentage = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                    {
                        numCorrect++;
                    }

                    percentage = (double)numCorrect / (double)testSize * 100.0;
                    form.successRtDt.Text = numCorrect + "/" + testSize;
                    form.successPrcDt.Text = String.Format("{0:0.00}", percentage) + "%";
                }
                succesRates.Add(Classifier.ID3, percentage);
                classifiers.Add(Classifier.ID3, cl);
            }
            catch (java.lang.Exception ex)
            {
                ex.printStackTrace();
                MessageBox.Show(ex.ToString(), "Error for Decision Tree", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error for Decision Tree", "Error for Decision Tree", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CalculateSuccessForAnn(weka.core.Instances originalInsts)
        {
            try
            {
                var form = Form.ActiveForm as Form1;

                form.successPrcAnn.Text = "Training...";
                form.successRtAnn.Text = "../" + testSize;

                weka.core.Instances insts = originalInsts;

                // Pre-process     
                insts = ConvertNominalToNumeric(insts);
                insts = Normalize(insts);

                // Classify
                weka.classifiers.Classifier cl = new weka.classifiers.functions.MultilayerPerceptron();
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);
                cl.buildClassifier(train);

                int numCorrect = 0;
                double percentage = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                    {
                        numCorrect++;
                    }

                    percentage = (double)numCorrect / (double)testSize * 100.0;
                    form.successRtAnn.Text = numCorrect + "/" + testSize;
                    form.successPrcAnn.Text = String.Format("{0:0.00}", percentage) + "%";
                }
                succesRates.Add(Classifier.ANN, percentage);
                classifiers.Add(Classifier.ANN, cl);              
            }
            catch (java.lang.Exception ex)
            {
                ex.printStackTrace();
                MessageBox.Show(ex.ToString(), "Error for Neural Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error for  Neural Network", "Error for  Neural Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CalculateSuccessForSvm(weka.core.Instances originalInsts)
        {
            try
            {
                var form = Form.ActiveForm as Form1;
                form.successPrcSvm.Text = "Training...";
                form.successRtSvm.Text = "../" + testSize;

                weka.core.Instances insts = originalInsts;

                // Pre-process     
                insts = ConvertNominalToNumeric(insts);
                insts = Normalize(insts);

                // Classify
                weka.classifiers.Classifier cl = new weka.classifiers.functions.SMO();
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);
                cl.buildClassifier(train);

                int numCorrect = 0;
                double percentage = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                    {
                        numCorrect++;
                    }

                    percentage = (double)numCorrect / (double)testSize * 100.0;
                    form.successRtSvm.Text = numCorrect + "/" + testSize;
                    form.successPrcSvm.Text = String.Format("{0:0.00}", percentage) + "%";
                }
                succesRates.Add(Classifier.SVM, percentage);
                classifiers.Add(Classifier.SVM, cl);
            }
            catch (java.lang.Exception ex)
            {
                ex.printStackTrace();
                MessageBox.Show(ex.ToString(), "Error for SVM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Error for  SVM", "Error for SVM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Utility functions (convert, normalize etc.)
        public static weka.core.Instances ConvertNumericToNominal(weka.core.Instances insts)
        {
            weka.filters.Filter nomFilter = new weka.filters.unsupervised.attribute.NumericToNominal();
            nomFilter.setInputFormat(insts);
            return weka.filters.Filter.useFilter(insts, nomFilter);
        }

        public static weka.core.Instances ConvertNominalToNumeric(weka.core.Instances insts)
        {
            weka.filters.Filter numFilter = new weka.filters.unsupervised.attribute.Discretize();
            numFilter.setInputFormat(insts);
            return weka.filters.Filter.useFilter(insts, numFilter);
        }

        public static weka.core.Instances Normalize(weka.core.Instances insts)
        {
            weka.filters.Filter normalizeFilter = new weka.filters.unsupervised.instance.Normalize();
            normalizeFilter.setInputFormat(insts);

            weka.filters.Filter missingFilter = new weka.filters.unsupervised.attribute.ReplaceMissingValues();
            missingFilter.setInputFormat(insts);

            insts = weka.filters.Filter.useFilter(insts, normalizeFilter);
            return weka.filters.Filter.useFilter(insts, missingFilter);
        }
        #endregion

        #region Form functions
        private void openFileDialog_FileOk(object sender, CancelEventArgs e) {}

        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Data Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "arff",
                Filter = "arff files (*.arff)|*.arff",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                browseFileName.Text = openFileDialog1.FileName;
                Classify(openFileDialog1.FileName);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            var form = Form.ActiveForm as Form1;
            if (readyToTest)
            {
                weka.classifiers.Classifier cl = classifiers[highestSuccessRate.Key];
                weka.core.Instance inst = new weka.core.Instance(insts.numAttributes() - 1);
                inst.setDataset(insts);
                for (int i = 0; i < inputObjects.Count; i++)
                {
                    if (inputObjects[i].numeric)
                    {
                        inst.setValue(i, Decimal.ToDouble(inputObjects[i].num.Value));
                    }
                    else
                    {
                        inst.setValue(i, inputObjects[i].nom.SelectedItem.ToString());
                    }
                }


                try
                {
                    string[] values = insts.attribute(insts.numAttributes() - 1).toString().Split('{', '}')[1].Split(',');
                    double classOfData = cl.classifyInstance(inst);
                    int idx = Convert.ToInt32(classOfData);
                    form.testResult.Text = values[idx];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Program is not ready to test, probably needs to process data first.", "Not ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }

    public class UserInput
    {
        public NumericUpDown num { get; set; }
        public ComboBox nom { get; set; }
        public bool numeric { get; set; }

        public UserInput(NumericUpDown num)
        {
            this.num = num;
            this.numeric = true;
        }

        public UserInput(ComboBox nom)
        {
            this.nom = nom;
            this.numeric = false;
        }
    }
}
