using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Windows.Forms;

namespace projetDGM
{

    public partial class About : Form
    {
        

        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();
        public About()
        {
            InitializeComponent();
         /*   Choices choices = new Choices();
            string[] text = File.ReadAllLines(Environment.CurrentDirectory + "//grammar.txt");
            choices.Add(text);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recEngine.LoadGrammar(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngne_SpeechRecognized);

            speech.SelectVoiceByHints(VoiceGender.Male); */
        }
        

        private void recEngne_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           /* string result = e.Result.Text;
            if(result == "Hello")
            {
                result = "Hello, my name is Quetzal, how can I help you";

            }
            speech.SpeakAsync(result);
            gunaLabel31.Text = result;*/

        }


        private void gunaShadowPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaShadowPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //private void gunaCircleButton1_Click(object sender, EventArgs e)
        //{
        //    if (gunaRadioButton1.Checked == true)
        //    {
        //        speech.SelectVoice("Microsoft Zira Desktop");
        //        speech.SpeakAsync("Hello," + user.Text + ", My name is Quetzal, I have been developed by VItal TCHIRHOUZA Quetzal, I would like to welcome you to the  General Direction of Migration platform, If you have any need of assistance just let me know, I'm here to help you");
        //    }
        //    else if (gunaRadioButton2.Checked == true)
        //    {
        //        speech.SelectVoice("Microsoft Hortense Desktop");
        //        speech.SpeakAsync("Bonjour," + user.Text + " je m'appelle Quetzal, j'ai été dévelopé par Vital TCHIRHOUZA et je voudrais vous souhaitez la bienvenu sur la plateforme de la Direction Générale de migration, si vous avez besoin d'une quelconque assistance en rapport avec ce system, dites-moi, je suis là pour vous aidez");
        //    }
        //    else
        //    {

        //    }
        //}

        //private void gunaRadioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //   if (gunaRadioButton1.Checked == true)
        //    {
        //        speech.SelectVoice("Microsoft Zira Desktop");
        //        speech.SpeakAsync("Hello," + user.Text + ", My name is Quetzal, I have been developed by VItal TCHIRHOUZA Quetzal, I would like to welcome you to the  General Direction of Migration platform, If you have any need of assistance just let me know, I'm here to help you");
        //    }
        //    else if (gunaRadioButton2.Checked == true)
        //    {
        //        speech.SelectVoice("Microsoft Hortense Desktop");
        //        speech.SpeakAsync("Bonjour," + user.Text + " je m'appelle Quetzal, j'ai été dévelopé par Vital TCHIRHOUZA et je voudrais vous souhaitez la bienvenu sur la plateforme de la Direction Générale de migration, si vous avez besoin d'une quelconque assistance en rapport avec ce system, dites-moi, je suis là pour vous aidez");
        //    }
        //    else
        //    {
        //        speech.SelectVoice("Microsoft Hortense Desktop");
        //        speech.SpeakAsync("Bonjour," + user.Text + " .je m'appelle Quetzal, j'ai été dévelopé par Vital TCHIRHOUZA et je voudrais vous souhaitez la bienvenu sur la plateforme de la Direction Générale de migration, si vous avez besoin d'une quelconque assistance en rapport avec ce system, dites-moi, je suis là pour vous aidez");
        //    }
        //}

        private void About_Click(object sender, EventArgs e)
        {

        }

        private void About_Load(object sender, EventArgs e)
        {
            user.Text = PassengerDash.username;
        }

        private void gunaRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //record();
        }

        private void gunaTileButton1_Click(object sender, EventArgs e)
        {
            record();
        }
        private void record()
        {
            if (gunaRadioButton1.Checked == false && gunaRadioButton2.Checked == false)
            {

            }
            else if (gunaRadioButton1.Checked == true)
            {
                speech.SelectVoice("Microsoft Zira Desktop");
                speech.SpeakAsync("hello, To record information in the system, you have to follow the steps bellow:; " +
                    "- click on new." +
                    "- fill all the text fields and select in the differents combobox according to what you need. " +
                    "- if you try to type text into a text box but nothing happen, this means that it is a read only textbox, it content will be generated automatically." +
                    "- check if all the information are ok and press on the save button.");
            }
            else if (gunaRadioButton2.Checked == true)
            {

                speech.SelectVoice("Microsoft Hortense Desktop");
                speech.SpeakAsync("Bonjour, pour enregistrer les informations, vous devez suivre les étapes suivantes:; " +
                    "- cliquer sur nouveau." +
                    "- complétez tous les champs de saisie et selectionnez dans les zones de selection selon votre besoin." +
                    "- s'il arrive que vous essayez de taper un text dans une zone de saisie et que rien ne se produit, ce que c'est une zone à lecture seule, son contenu sera générer automatiquement." +
                    "- vérifiez si toutes les informations sont correctes et cliquez sur le boutton enregistrer.");
            }
            else
            {

            }
        }
        private void docmaker()
        {
            if (gunaRadioButton1.Checked == false && gunaRadioButton2.Checked == false)
            {

            }
            else if (gunaRadioButton1.Checked == true)
            {
                speech.SelectVoice("Microsoft Zira Desktop");
                speech.SpeakAsync("Good morning, following are the steps to produce a document throught this system:; " +
                    "- Be connected as a document producer and click on the star button in the dashboard;" +
                    "- the panel of creation of document will open;" +
                    "- click on new in order to create a new document and the system will generate it number;" +
                    "- fill all the text fields and select in the differents combobox according to what you need; " +
                    "- if you try to type text into a text box but nothing happen, this means that it is a read only textbox, it content will be generated automatically;" +
                    "- click on generate QR code in order to get the document encryption code;" +
                    "- select the type of your document in the type combobox;" +
                    "- check if all the information are ok and press on the save button." +
                    "- click on view document;" +
                    "- a window is going to be open; " +
                    "- click at generate and you are going to see the document in it original version;" +
                    "- you can print it or extract the softcopy of it as a pdf file or microsoft file.");
            }
            else if (gunaRadioButton2.Checked == true)
            {

                speech.SelectVoice("Microsoft Hortense Desktop");
                speech.SpeakAsync("Bonjour, ci-dessous sont les étapes à suivre pour produire un document :;" +
                    ", connectez-vous en tant que producteur de document et cliquez sur le boutton commencer dans l'interface d'accueil;" +
                    ", l'interface de création des documents va s'ouvrir; " +
                    ", cliquer sur nouveau pour créer un nouveau document et générer son numéro;" +
                    ", complétez tous les champs de saisie et selectionnez dans les zones de selection selon votre besoin;" +
                    ", s'il arrive que vous essayez de taper un text dans une zone de saisie et que rien ne se produit, ce que c'est une zone à lecture seule, son contenu sera générer automatiquement;" +
                    ", cliquez sur le bouton générer QR code pour obtenir le code de cryptage du document;" +
                    ", selectionez le type de votre document dans la zone de selection type;" +
                    ", vérifiez si toutes les informations sont correctes et cliquez sur le boutton enregistrer;" +
                    ", cliquez sur voir le document;" +
                    ", une nouvelle fenetre va s'ouvrir;" +
                    ", cliquer sur générer et vous allez devoir obtenir la version électronique de votre document;" +
                    ", vous pouvez imprimer ou extraire votre document en version électronique sur format pdf ou comme fichier microsoft." +
                    ", Merci et à la prochaine.");
            }
            else
            {

            }
        }
        private void gunaTileButton2_Click(object sender, EventArgs e)
        {
            docmaker();
        }

        private void stat()
        {
            if (gunaRadioButton1.Checked == false && gunaRadioButton2.Checked == false)
            {

            }
            else if (gunaRadioButton1.Checked == true)
            {
                speech.SelectVoice("Microsoft Zira Desktop");
                speech.SpeakAsync("Good morning, within this system, only border chief are authorized to check statistics, and following are the steps to generate them:; " +
                    "- Be connected as a Border chief and click on the start button in the dashboard;" +
                    "- the main panel of the border chief will open;" +
                    "- click on tasks management and go to the statistical form;" +
                    "- select the kind of statistic you are in need of;" +
                    "- and get the statistical change at real-time; " +
                    "- thank you.");
            }
            else if (gunaRadioButton2.Checked == true)
            {

                speech.SelectVoice("Microsoft Hortense Desktop");
                speech.SpeakAsync("Bonjour, ce système a été developper de sorte que seuls les chefs de postes aient accès aux données statistics, pour accéder à celles-ci il faut :;" +
                    ", connectez-vous en tant que chef de poste et cliquez sur le boutton commencer dans l'interface d'accueil;" +
                    ", la fenetre principale de trvail du chef de poste va s'afficher; " +
                    ", cliquez sur gestion des taches;" +
                    ", selectionez le paneau de statistique;" +
                    ", sélectionnez le type de statistique dont vous avez besoin;" +
                    ", Obtenez les resultats ainsi que leurs diagrammes;" +
                    ", Merci et à la prochaine.");
            }
            else
            {

            }
        }

        private void gunaTileButton4_Click(object sender, EventArgs e)
        {
            stat();
        }

        private void Greport()
        {
            if (gunaRadioButton1.Checked == false && gunaRadioButton2.Checked == false)
            {

            }
            else if (gunaRadioButton1.Checked == true)
            {
                speech.SelectVoice("Microsoft Zira Desktop");
                speech.SpeakAsync("Good morning, within this system, following are the steps to generate reports:; " +
                    "-enter into your work panel" +
                    "- click on report;" +
                    "-go to generate;" +
                    "- setup the page configuration and format;" +
                    "- print your report or extract it into pdf format of microsoft file format; " +
                    "- thank you.");
            }
            else if (gunaRadioButton2.Checked == true)
            {

                speech.SelectVoice("Microsoft Hortense Desktop");
                speech.SpeakAsync("Bonjour, dans ce système, les étapes à suivre pour générer un rapport sont:;" +
                    ", entrer dans son interface de travail;" +
                    ", cliquer sur rapport; " +
                    ", aller ur générer;" +
                    ", configurer le format de la page et sa disposition;" +
                    ", imprimer votre rapport ou alors générez-le sous format pdf ou microsoft office;" +
                    ", Merci et à la prochaine.");
            }
            else
            {

            }
        }

        private void gunaTileButton3_Click(object sender, EventArgs e)
        {
            Greport();
        }
    }
}
