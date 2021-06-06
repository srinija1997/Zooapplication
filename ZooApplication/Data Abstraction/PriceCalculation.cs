using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace ZooApplication.Data_Abstraction
{

    public class PriceCalculation : IPriceCalculation
    {
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        public PriceCalculation(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }
        public double CalculatePrice()
        {
            XmlDocument xdoc = new XmlDocument();
            string folder = "zooxml/Zoo.xml";
            double meatprice = 12.56, fruitprice = 5.60;
            string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
            xdoc.Load(serverfolder);
            string csvfolder = "zooxml/animals.csv";
            string csvserverfolder = Path.Combine(_webHostEnvironment.WebRootPath, csvfolder);
            string line;
            var sr = new StreamReader(csvserverfolder);
          
            double meattotal = 0, fruittotal = 0, bothtotal = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("meat")) // <---Variable inputted by the user
                {
                    double  tigertotal = 0,lvalue=0,tvalue=0;
                    int lioncount = 0, tigercount = 0; ;
                    if (line.Contains("Lion"))
                    {
                        XmlNodeList elemList = xdoc.GetElementsByTagName("Lion");
                        
                        for (int i = 0; i < elemList.Count; i++)
                        {
                            int attrVal = int.Parse(elemList[i].Attributes["kg"].Value);
                            lioncount += attrVal;
                        }
                        lvalue = double.Parse(line.Split(";")[1]);
                        double ltotal = lvalue * lioncount * meatprice;
                        meattotal += ltotal;
                    }
                    
                    if (line.Contains("Tiger"))
                    {
                        double liontotal = meattotal;
                        XmlNodeList elemList1 = xdoc.GetElementsByTagName("Tiger");
                        
                        for (int i = 0; i < elemList1.Count; i++)
                        {
                            int attrVal = int.Parse(elemList1[i].Attributes["kg"].Value);
                            tigercount += attrVal;
                        }
                        tvalue = double.Parse(line.Split(";")[1]);
                        tigertotal = tvalue * tigercount * meatprice;
                        meattotal = liontotal + tigertotal;
                    }
                }
                if (line.Contains("fruit")) // <---Variable inputted by the user
                {
                    double gtotal = 0, ztotal = 0;
                    if (line.Contains("Giraffe"))
                    {
                        XmlNodeList elemList = xdoc.GetElementsByTagName("Giraffe");
                       
                        int Gcount = 0;
                        
                        for (int i = 0; i < elemList.Count; i++)
                        {
                            int attrVal = int.Parse(elemList[i].Attributes["kg"].Value);
                            Gcount += attrVal;
                        }
                        double gvalue = double.Parse(line.Split(";")[1]);
                        gtotal = gvalue * Gcount * fruitprice;
                        fruittotal += gtotal;
                    }
                    if (line.Contains("Zebra"))
                    {
                        double girraffetotal = fruittotal;
                        XmlNodeList elemList1 = xdoc.GetElementsByTagName("Zebra");
                        int Zcount = 0;
                        for (int i = 0; i < elemList1.Count; i++)
                        {
                            int attrVal = int.Parse(elemList1[i].Attributes["kg"].Value);
                            Zcount += attrVal;
                        }
                        double zvalue = double.Parse(line.Split(";")[1]);
                        ztotal = zvalue * Zcount * fruitprice;
                        fruittotal = girraffetotal + ztotal;
                    }
                    
                    //Console.WriteLine("Fruit cost" + fruittotal);
                }
                if (line.Contains("both")) // <---Variable inputted by the user
                {
                    int wcount = 0;
                    double pmeat1 = 0, wvalue = 0, pmeat = 0, pfruit = 0;
                    float pcount = 0;
                    if (line.Contains("Wolf"))
                    {
                        double total=0,wmeat=0,wmeat1=0,wfruit=0;
                        XmlNodeList elemList = xdoc.GetElementsByTagName("Wolf");
                        

                        for (int i = 0; i < elemList.Count; i++)
                        {
                            int attrVal = int.Parse(elemList[i].Attributes["kg"].Value);
                            wcount += attrVal;
                        }
                        wvalue = double.Parse(line.Split(";")[1]);
                        double wpercent = double.Parse(line.Replace("%", " ").Split(";")[3]);
                        wmeat = (wvalue * wpercent) / 100;
                        wmeat1 = wmeat * meatprice;
                        wfruit = (wvalue - wmeat)* fruitprice;
                        total = wcount * (wmeat1 + wfruit);
                        bothtotal += total;
                    }

                    if (line.Contains("Piranha"))
                    {
                        double wolftotal = bothtotal;
                        XmlNodeList elemList1 = xdoc.GetElementsByTagName("Piranha");
                        for (int i = 0; i < elemList1.Count; i++)
                        {
                            float attrVal = float.Parse(elemList1[i].Attributes["kg"].Value);
                            pcount += attrVal;
                        }
                        double pvalue = double.Parse(line.Split(";")[1]);

                        double ppercent = double.Parse(line.Replace("%", " ").Split(";")[3]);

                        pmeat = (pvalue * ppercent) / 100;
                        pmeat1 = pmeat * meatprice;
                        pfruit = (pvalue - pmeat)* fruitprice;
                        double ptotal = pcount * (pmeat1 + pfruit);
                        bothtotal = wolftotal + ptotal;
                    }
                }
            }
            double totalcostfood = meattotal + fruittotal + bothtotal;
            return totalcostfood;
        }
    }
}
