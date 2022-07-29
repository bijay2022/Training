using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadingITRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter the path from the text file.");
        EnterPath:
            string path = Console.ReadLine();
            try
            {
                FileInfo fi = new FileInfo(path);

                bool exists = fi.Exists;
                if (exists)
                {
                    long size = fi.Length;
                    if (size == 0)
                    {
                        throw new CustomSizeException("File size is zero, There is no request.");
                    }
                    else
                    {
                        string[] lines = System.IO.File.ReadAllLines(path);
                        List<ITRequest> lstItRequest = new List<ITRequest>();
                        int j = 0;
                        foreach (string line in lines)
                        {
                            string[] columnDetails = line.Split(',');
                            if (columnDetails.Length == 8)
                            {
                                ITRequest iTRequest = new ITRequest();
                                iTRequest.RequestId = columnDetails[0].Length > 0 ? columnDetails[0] : throw new CustomColumnValueEmptyException(j + " row and" + " RequestId column value is empty.");
                                iTRequest.RequestType = columnDetails[1].Length > 0 ? columnDetails[1] : throw new CustomColumnValueEmptyException(j + " row and" + " RequestType column value is empty.");
                                iTRequest.InitiatedBy = columnDetails[2].Length > 0 ? columnDetails[2] : throw new CustomColumnValueEmptyException(j + " row and" + " InitatedBy column value is empty.");
                                iTRequest.Department = columnDetails[3].Length > 0 ? columnDetails[3] : throw new CustomColumnValueEmptyException(j + " row and" + " Department column value is empty.");
                                iTRequest.Priority = columnDetails[4].Length > 0 ? columnDetails[4] : throw new CustomColumnValueEmptyException(j + " row and" + " Priority column value is empty.");
                                iTRequest.CurrentStatus = columnDetails[5].Length > 0 ? columnDetails[5] : throw new CustomColumnValueEmptyException(j + " row and" + " currentStatus column value is empty.");
                                iTRequest.RequestDate = columnDetails[6].Length > 0 ? columnDetails[6] : throw new CustomColumnValueEmptyException(j + " row and" + " RequestDate column value is empty.");
                                iTRequest.Location = columnDetails[7].Length > 0 ? columnDetails[7] : throw new CustomColumnValueEmptyException(j + " row and" + " Location column value is empty.");
                                lstItRequest.Add(iTRequest);
                            }
                            else
                            {
                                throw new CustomSizeException("There is missing column in " + j + " row.Columnlength should be 8.");
                            }
                            j++;
                        }
                        if (lstItRequest.Count > 0)
                        {
                            Console.WriteLine("Please Enter 1:Software Installation Requests, Enter 2:Hardware Installation Requests");
                            string requestType = Console.ReadLine();

                            List<ITRequest> lstItReqResult = requestType == "1" ? lstItRequest.Where(p => p.RequestType == RequestType.SoftwareInstallation.ToString() && p.CurrentStatus == RequestStatus.Pending.ToString()).ToList()
                                : lstItRequest.Where(p => p.RequestType == RequestType.HardwareInstallation.ToString() && p.CurrentStatus == RequestStatus.Pending.ToString()).ToList();

                            if (requestType == "1") Console.WriteLine("Software Installation Requests");
                            else Console.WriteLine("Hardware Installation Requests");

                            Console.WriteLine("RequestId" + " " + "RequestType" + " " + "Initiated By" + " " + "Department" + " " + "Priority" + " " + "RequestDate" + " " + "CurrentStatus" + " " + "Location");
                            foreach (ITRequest itRequestResult in lstItReqResult)
                            {
                                Console.WriteLine(itRequestResult.RequestId + " " + itRequestResult.RequestType + " " + itRequestResult.InitiatedBy + " " + itRequestResult.Department + " " + itRequestResult.Priority + " " + itRequestResult.RequestDate + " " + itRequestResult.CurrentStatus + " " + itRequestResult.Location);
                            }
                        }

                    }
                }
                else
                {
                    Console.WriteLine("File doesn't exit. Please Enter correct file.");
                    throw new FileNotFoundException("File not found.");
                    //goto EnterPath;

                }
            }
            catch (FileNotFoundException fne)
            {
                Console.WriteLine(fne.Message);
            }
            catch (CustomSizeException cse)
            {
                Console.WriteLine(cse.Message);
            }
            catch (CustomColumnValueEmptyException ccve)
            {
                Console.WriteLine(ccve.Message);
            }
            catch (DivideByZeroException ccve)
            {
                Console.WriteLine(ccve.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.WriteLine("Finally block executed."); }
        }

    }
    
    public class CustomSizeException : Exception
    {
        public CustomSizeException(string message) : base(message)
        {
            this.HelpLink = "Visit to Microsoft for better help.";

        }

    }
    public class CustomColumnException : Exception
    {
        public CustomColumnException(string message) : base(message)
        {
            this.HelpLink = "check the file for to identigy the missing column value.";
        }

    }
    public class CustomColumnValueEmptyException : Exception
    {
        public CustomColumnValueEmptyException(string message) : base(message)
        {
            this.HelpLink = "check the file for to identify the empty column value..";
        }

    }

    enum RequestType
    {
        SoftwareInstallation,
        HardwareInstallation
    }
    enum RequestStatus
    {
        Pending,
        IInprogress,
        Closed
    }
    public class ITRequest
    {
        public string RequestId { get; set; }
        public string RequestType { get; set; }
        public string InitiatedBy { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }
        public string CurrentStatus { get; set; }
        public string RequestDate { get; set; }
        public string Location { get; set; }

    }
}
