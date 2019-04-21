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
        const int percentSplit = 66;
        static int trainSize = -1;
        static int testSize = -1;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.browseFileBtn.Click += browseFileBtn_Click;
            
        }

        private void Classify(string path)
        {
            // Try reading file, if failed exit function
            weka.core.Instances insts = ReadFile(path);

            if (insts == null)
            {
                // Error occured reading file, display error message
                return;
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

            /*
            t_SuccessNb.Join();
            t_SuccessKn.Join();
            t_SuccessDt.Join();
            t_SuccessAnn.Join();
            t_SuccessSvm.Join();*/

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
                return null;
            }
        }

        public static void CalculateSuccessForNb(weka.core.Instances originalInsts)
        {
            var form = Form.ActiveForm as Form1;
            form.successPrcNb.Text = "Training...";
            form.successRtNb.Text = "../" + testSize;

            weka.core.Instances insts = originalInsts;

            // Pre-process     
            insts = ConvertNumericToNominal(insts);

            // Classify
            weka.classifiers.Classifier cl = new weka.classifiers.bayes.NaiveBayes();
            weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);
            cl.buildClassifier(train);

            int numCorrect = 0;
            for (int i = trainSize; i < insts.numInstances(); i++)
            {
                weka.core.Instance currentInst = insts.instance(i);
                double predictedClass = cl.classifyInstance(currentInst);
                if (predictedClass == insts.instance(i).classValue())
                {
                    numCorrect++;
                }

                form.successRtNb.Text = numCorrect + "/" + testSize;
                form.successPrcNb.Text = String.Format("{0:0.00}", (double)((double)numCorrect / (double)testSize * 100.0)) + "%";
            }
            
        }

        public static void CalculateSuccessForKn(weka.core.Instances originalInsts)
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
            for (int i = trainSize; i < insts.numInstances(); i++)
            {
                weka.core.Instance currentInst = insts.instance(i);
                double predictedClass = cl.classifyInstance(currentInst);
                if (predictedClass == insts.instance(i).classValue())
                {
                    numCorrect++;
                }
                form.successRtKn.Text = numCorrect + "/" + testSize;
                form.successPrcKn.Text = String.Format("{0:0.00}", (double)((double)numCorrect / (double)testSize * 100.0)) + "%";
            }
            
        }

        public static void CalculateSuccessForDt(weka.core.Instances originalInsts)
        {
            var form = Form.ActiveForm as Form1;
            form.successPrcDt.Text = "Training...";
            form.successRtDt.Text = "../" + testSize;

            weka.core.Instances insts = originalInsts;

            // Pre-process


            // Classify
            weka.classifiers.Classifier cl = new weka.classifiers.trees.J48();
            weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);
            cl.buildClassifier(train);

            int numCorrect = 0;
            for (int i = trainSize; i < insts.numInstances(); i++)
            {
                weka.core.Instance currentInst = insts.instance(i);
                double predictedClass = cl.classifyInstance(currentInst);
                if (predictedClass == insts.instance(i).classValue())
                {
                    numCorrect++;
                }
                form.successRtDt.Text = numCorrect + "/" + testSize;
                form.successPrcDt.Text = String.Format("{0:0.00}", (double)((double)numCorrect / (double)testSize * 100.0)) + "%";
            }
  
        }

        public static void CalculateSuccessForAnn(weka.core.Instances originalInsts)
        {
            var form = Form.ActiveForm as Form1;
            try
            {
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
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                    {
                        numCorrect++;
                    }
                    form.successRtAnn.Text = numCorrect + "/" + testSize;
                    form.successPrcAnn.Text = String.Format("{0:0.00}", (double)((double)numCorrect / (double)testSize * 100.0)) + "%";
                }
            } catch(Exception e)
            {
                form.successRtAnn.Text = "Occured!";
                form.successPrcAnn.Text = "An Error";
                Console.WriteLine(e);
            } 
        }

        public static void CalculateSuccessForSvm(weka.core.Instances originalInsts)
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
            for (int i = trainSize; i < insts.numInstances(); i++)
            {
                weka.core.Instance currentInst = insts.instance(i);
                double predictedClass = cl.classifyInstance(currentInst);
                if (predictedClass == insts.instance(i).classValue())
                {
                    numCorrect++;
                }
                form.successRtSvm.Text = numCorrect + "/" + testSize;
                form.successPrcSvm.Text = String.Format("{0:0.00}", (double)((double)numCorrect / (double)testSize * 100.0)) + "%";
            }
            
        }


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
            return weka.filters.Filter.useFilter(insts, normalizeFilter);
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

        private void successTitleKn_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
