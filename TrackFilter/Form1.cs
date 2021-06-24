using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackFilter
{
    public partial class Form1 : Form
    {

        string[] fileList;
        string selectedPath;
        WorkerInput wInput;
        public Form1()
        {
            InitializeComponent();
            fileList = null;
            selectedPath = null;
            wInput = new WorkerInput();
        }

        private void FilterData_Click(object sender, EventArgs e)
        {
            if(!backgroundWorker.IsBusy && selectedPath != null && fileList != null && fileList.Length > 0)
            {
                wInput.selectedPath = selectedPath;
                wInput.fileList = fileList;
                wInput.totalFrames = Convert.ToInt32(TotalFrames.Value);
                wInput.minFrames = Convert.ToInt32(MinFrames.Value);
                wInput.distance = Convert.ToDouble(MaxDistance.Value);
                backgroundWorker.RunWorkerAsync(wInput);
            }            
        }

        private class CellTrack
        {
            //Name of the Cell Track
            public string cName;

            //Column Index from CSV files
            public int cColumnIndex;

            //Distances recorded at each Frame (set as string to account for NULL measurements)
            public Dictionary<int,string> distances;
            
            public CellTrack(string pName, int pColumnIndex)
            {
                cName = pName;
                cColumnIndex = pColumnIndex;
                distances = new Dictionary<int, string>();
            }
        }

        private class WorkerInput
        {
            public string[] fileList;
            public string selectedPath;
            public int totalFrames;
            public int minFrames;
            public double distance;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                backgroundWorker.CancelAsync();
        }

        private void SelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = ofd.SelectedPath;
                    fileList = Directory.GetFiles(selectedPath);
                    SelectedPathLbl.Text = selectedPath;
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            ProgressLbl.Text = string.Format("Processing ... {0}%", e.ProgressPercentage);
        } 

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Initialize the selected file path, list of files in that directory
            string selectedPath = ((WorkerInput)e.Argument).selectedPath;
            string[] fileList = ((WorkerInput)e.Argument).fileList;

            //Set Maximum and minimum number of frames per track
            int totalFrames = ((WorkerInput)e.Argument).totalFrames;
            int minFrames = ((WorkerInput)e.Argument).minFrames;

            //Set the maximum distance measurement to filter by
            double distance = ((WorkerInput)e.Argument).distance;

            //Initialize progress bar values
            int totalTime = fileList.Length + totalFrames;
            int index = 1;
            backgroundWorker.ReportProgress(index * 100/ totalTime);

            try
            {
                List<CellTrack> cTracks = new List<CellTrack>();
                int frameCount = 0;
                //Read in all files and convert to CellTrack Object
                foreach (string file in fileList)
                {
                    if (backgroundWorker.CancellationPending)
                        throw new Exception("Filter Cancelled.");
                    frameCount = 0;
                    List<CellTrack> fileTracks = new List<CellTrack>();
                    using (StreamReader reader = new StreamReader(file))
                    {
                        bool headerfound = false;
                        while (!reader.EndOfStream && frameCount < totalFrames)
                        {
                            if (backgroundWorker.CancellationPending)
                                throw new Exception("Filter Cancelled.");
                            string row = reader.ReadLine();
                            //Look for Header Row and use it to name all the tracks,
                            //Generate a new CellTrack object for each column in CSV
                            if (row.Contains("Time [s]"))
                            {
                                headerfound = true;
                                string[] trackList = row.Split(',');
                                for (int i = 1; i < trackList.Length; i++)
                                {
                                    if (!string.IsNullOrEmpty(trackList[i]))
                                    {
                                        CellTrack newTrack = new CellTrack(trackList[i], i);
                                        fileTracks.Add(newTrack);
                                    }
                                }
                                row = reader.ReadLine();
                            }
                            if (headerfound)
                            {
                                //Split each row into columns and add the values to the respective CellTrack
                                string[] rowDistances = row.Split(',');
                                for (int i = 1; i <= fileTracks.Count; i++)
                                {
                                    foreach (CellTrack track in fileTracks)
                                    {
                                        if (i == track.cColumnIndex)
                                        {
                                            if (i < rowDistances.Length)
                                                track.distances.Add(frameCount, rowDistances[i]);
                                            else
                                                track.distances.Add(frameCount, "");
                                            break;
                                        }
                                    }
                                }
                                frameCount++;
                            }
                        }
                        reader.Close();
                    }
                    List<CellTrack> finalTracks = new List<CellTrack>();
                    //Check for consecutive measurements in each track that are inside the target distance
                    //Remove any Tracks that have less than the minimum number of Frames
                    //Remove any Tracks that have more than one set of consecutive positive results
                    foreach (CellTrack track in fileTracks)
                    {
                        if (backgroundWorker.CancellationPending)
                            throw new Exception("Filter Cancelled.");
                        bool targetFound = false;
                        bool targetBreak = false;
                        bool secondTarget = false;
                        int dataCount = 0;
                        for (int i = 0; i < frameCount; i++)
                        {
                            if (backgroundWorker.CancellationPending)
                                throw new Exception("Filter Cancelled.");
                            string measureString = "";
                            //Set default > maximum allowable value for distance to treat all null measurements as breaks
                            double measurement = 1.1;
                            track.distances.TryGetValue(i, out measureString);
                            if (!string.IsNullOrEmpty(measureString))
                            {
                                double.TryParse(measureString, out measurement);
                                dataCount++;
                            }
                            if (measurement <= distance)
                            {
                                if (!targetFound)
                                    targetFound = true;
                                else if (targetBreak)
                                {
                                    secondTarget = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (targetFound)
                                    targetBreak = true;
                            }
                        }
                        if (targetFound && !secondTarget && dataCount >= minFrames)
                            finalTracks.Add(track);
                    }
                    if (backgroundWorker.CancellationPending)
                        throw new Exception("Filter Cancelled.");
                    //Add all remaining tracks to total track list
                    foreach (CellTrack track in finalTracks)
                        cTracks.Add(track);
                    backgroundWorker.ReportProgress(index * 100 / totalTime);
                }
                if (backgroundWorker.CancellationPending)
                    throw new Exception("Filter Cancelled.");
                //Write all values to output string for output to CSV
                string output = "Frame";
                foreach (CellTrack track in cTracks)
                    output += "," + track.cName;
                output += "\n";
                for (int i = 0; i <= totalFrames; i++)
                {
                    if (backgroundWorker.CancellationPending)
                        throw new Exception("Filter Cancelled.");
                    output += i.ToString();
                    foreach (CellTrack track in cTracks)
                    {
                        string distString = "";
                        track.distances.TryGetValue(i, out distString);
                        output += "," + distString;
                    }
                    output += "\n";
                    backgroundWorker.ReportProgress(index++ * 100 / totalTime);
                }
                if (backgroundWorker.CancellationPending)
                    throw new Exception("Filter Cancelled.");
                //Write output string to CSV
                using (StreamWriter writer = new StreamWriter(selectedPath + ".csv"))
                {
                    writer.Write(output);
                }
            }
            catch(Exception ex)
            {
                backgroundWorker.CancelAsync();
                MessageBox.Show(ex.Message, "Error Filtering Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ProgressLbl.Text = "Filter Cancelled";
                return;
            }

            progressBar.Value = progressBar.Maximum;
            ProgressLbl.Text = "Filter Complete";
            MessageBox.Show("Filtered Data saved to " + selectedPath + ".csv", "Filter Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //using (FolderBrowserDialog ofd = new FolderBrowserDialog())
        //{
        //    if (ofd.ShowDialog() == DialogResult.OK)
        //    {
        //        ProgressLbl.Text = "Working...";
        //        string[] fileList = Directory.GetFiles(ofd.SelectedPath);
        //        List<CellTrack> cTracks = new List<CellTrack>();
        //        int frameCount = 0;
        //        foreach (string file in fileList)
        //        {
        //            frameCount = 0;
        //            List<CellTrack> fileTracks = new List<CellTrack>();
        //            using (StreamReader reader = new StreamReader(file))
        //            {
        //                bool headerfound = false;
        //                while(!reader.EndOfStream)
        //                {
        //                    string row = reader.ReadLine();
        //                    if(row.Contains("Time [s]"))
        //                    {
        //                        headerfound = true;
        //                        string[] trackList = row.Split(',');
        //                        for(int i = 1; i < trackList.Length; i++)
        //                        {
        //                            if(!string.IsNullOrEmpty(trackList[i]))
        //                            {
        //                                CellTrack newTrack = new CellTrack(trackList[i], i);
        //                                fileTracks.Add(newTrack);
        //                            }
        //                        }
        //                        row = reader.ReadLine();
        //                    }
        //                    if(headerfound)
        //                    {
        //                        string[] rowDistances = row.Split(',');
        //                        for(int i = 1; i <= fileTracks.Count; i++)
        //                        {
        //                            foreach(CellTrack track in fileTracks)
        //                            {
        //                                if(i == track.cColumnIndex)
        //                                {
        //                                    if (i < rowDistances.Length)
        //                                        track.distances.Add(frameCount, rowDistances[i]);
        //                                    else
        //                                        track.distances.Add(frameCount, "");
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                        frameCount++;
        //                    }
        //                }
        //                reader.Close();
        //            }
        //            List<CellTrack> finalTracks = new List<CellTrack>();
        //            foreach (CellTrack track in fileTracks)
        //            {
        //                bool targetFound = false;
        //                bool targetBreak = false;
        //                bool secondTarget = false;
        //                int dataCount = 0;
        //                for(int i = 0; i < frameCount; i++)
        //                {
        //                    string measureString = "";
        //                    double measurement = 1;
        //                    track.distances.TryGetValue(i, out measureString);
        //                    if(!string.IsNullOrEmpty(measureString))
        //                    {
        //                        double.TryParse(measureString, out measurement);
        //                        dataCount++;
        //                    }
        //                    if (measurement <= 0.25)
        //                    {
        //                        if(!targetFound)
        //                            targetFound = true;
        //                        else if(targetBreak)
        //                        {
        //                            secondTarget = true;
        //                            break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (targetFound)
        //                            targetBreak = true;
        //                    }
        //                }
        //                if (targetFound && !secondTarget && dataCount >= 5)
        //                    finalTracks.Add(track);
        //            }
        //            foreach (CellTrack track in finalTracks)
        //                cTracks.Add(track);
        //        }
        //        string output = "Frame";
        //        foreach (CellTrack track in cTracks)
        //            output += "," + track.cName;
        //        output += "\n";
        //        for(int i = 0; i <= 80; i++)
        //        {
        //            output += i.ToString();
        //            foreach (CellTrack track in cTracks)
        //            {
        //                string distString = "";
        //                track.distances.TryGetValue(i, out distString);
        //                output += "," + distString;
        //            }
        //            output += "\n";
        //        }

        //        using (StreamWriter writer = new StreamWriter(ofd.SelectedPath + ".csv"))
        //        {
        //            writer.Write(output);
        //        }
        //        ProgressLbl.Text = "Done.";
        //    }
        //}

    }
}
