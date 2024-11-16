using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using Microsoft.Maui.Storage;
using System.Formats.Tar;

namespace BlazorHybridApp.Data
{
    

    internal class ReservationManager
    {
        private readonly Dictionary<string, FltRsv> FltDict;
        private FltRsv _screen_flt;
        private readonly string rsv_output_filename = Path.Combine(FileSystem.Current.AppDataDirectory, "T_T.csv");


        public ReservationManager()
        {
            this.FltDict = new Dictionary<string, FltRsv>();
            Init();
        }

        public async Task Init()
        {
            if (!File.Exists(rsv_output_filename))
            {

                using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync("reservation.csv");


                using FileStream outputStream = File.Create(rsv_output_filename);
                await inputStream.CopyToAsync(outputStream);
            }
            
            using (var reader = new StreamReader(rsv_output_filename))
            {
                string? tmp;
                while (reader.Peek() >= 0)
                {
                    tmp = reader.ReadLine() ?? throw new Exception("Nathan find out a line is null!");
                    
                    this.FltDict.Add(tmp.Split(',')[0], new FltRsv(tmp));
                    if (tmp[^7..].Equals(",Active"))
                    {
                        _screen_flt = new FltRsv(tmp);
                    }
                }
            }
        }

        public async Task SaveResv()
        {
            Debug.WriteLine("##: Calling SaveRsv! ~~~~~~~");

            using (StreamWriter writer = new StreamWriter(rsv_output_filename))
            {
                Debug.WriteLine("##: ##: Using StreamWriter! Ready to write to: " + rsv_output_filename);
                foreach (FltRsv _flt in FltDict.Values.ToList())
                {
                    writer.WriteLine(
                        _flt.Flt_rsv_code + "," +
                        _flt.Flt_dtl_code + "," +
                        _flt.Flt_dtl_line + "," +
                        _flt.Flt_dtl_date + "," +
                        _flt.Flt_dtl_cost + "," +
                        _flt.Flt_dtl_name + "," +
                        _flt.Flt_dtl_ctzn + "," +
                        _flt.Flt_dtl_stat);
                    Debug.WriteLine(_flt.Flt_dtl_name);
                }
                Debug.WriteLine("##: Done writing!");
            }
            
        }

     

        public Dictionary<string, FltRsv> getDict()
        {
            return this.FltDict;
        }

        public FltRsv getActiveFlt()
        {
            return this._screen_flt;
        }
    }

    internal class FltRsv
    {
        public string Flt_rsv_code { get; set; }
        public string Flt_dtl_code { get; set; }
        public string Flt_dtl_line { get; set; }
        public string Flt_dtl_date { get; set; }
        public string Flt_dtl_cost { get; set; }
        public string Flt_dtl_name { get; set; }
        public string Flt_dtl_ctzn { get; set; }
        public string Flt_dtl_stat { get; set; }
        public string Flt_or_text { get; set; }

        public FltRsv(string _flt)
        {
            Flt_rsv_code = _flt.Split(',')[0];
            Flt_dtl_code = _flt.Split(',')[1];
            Flt_dtl_line = _flt.Split(',')[2];
            Flt_dtl_date = _flt.Split(',')[3];
            Flt_dtl_cost = _flt.Split(',')[4];
            Flt_dtl_name = _flt.Split(',')[5];
            Flt_dtl_ctzn = _flt.Split(',')[6];
            Flt_dtl_stat = _flt.Split(',')[7];
            Flt_or_text  = _flt;
        }
    }
}
