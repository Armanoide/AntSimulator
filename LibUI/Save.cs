using LibModel.ManageEnvironment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibModel
{
    class Save
    {
        public void SaveAs(World world)
        {
            Stream stream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.Title = "Sauvegarder la simulation";
            //saveFileDialog.ShowDialog();
            Console.WriteLine("OK SAVE");
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog.OpenFile()) != null)
                {
                    stream.Close();
                    try
                    {                        
                        using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                        {
                            JArray arrayAnthill = new JArray();
                            foreach(Anthill anthill in world.ListAnthill)
                            {
                                arrayAnthill.Add(anthill.ToJson());
                            }
                            JObject json = new JObject(
                                new JProperty("anthill", arrayAnthill)
                                );
                            sw.WriteLine(json.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }
    }
}
