using System.IO;
using System;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Attachments;
using Com.Zoho.Crm.API.Layouts;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Tags;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.Record.RecordOperations;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;
using Info = Com.Zoho.Crm.API.Record.Info;
using ResponseHandler = Com.Zoho.Crm.API.Record.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Record.ResponseWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using System.Collections.Generic;



namespace SolZoho
{
    public class Record
    {
        /// 

        /// This method is used to create records of a module and print the response.
        /// 

        public static void CreateRecords(string moduleAPIName)
        {
            //API Name of the module to create records
            //string moduleAPIName = "Leads";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            //Get instance of BodyWrapper Class that will contain the request body
            BodyWrapper bodyWrapper = new BodyWrapper();

            //List of Record instances
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

            //Get instance of Record Class
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

            /*
            * Call addFieldValue method that takes two arguments
            * 1 -> Call Field "." and choose the module from the displayed list and press "." and choose the field name from the displayed list.
            * 2 -> Value
            */
            record1.AddFieldValue(Leads.CITY, "City");

            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");

            record1.AddFieldValue(Leads.FIRST_NAME, "First Name");

            record1.AddFieldValue(Leads.COMPANY, "KKRNP");

            /*
            * Call addKeyValue method that takes two arguments
            * 1 -> A string that is the Field's API Name
            * 2 -> Value
            */

            record1.AddKeyValue("Custom_field", "Value");

            record1.AddKeyValue("External", "TestExternal12321345");

            record1.AddKeyValue("Custom_field_2", "value");

            record1.AddKeyValue("Date_Time_2", new DateTimeOffset(new DateTime(2020, 05, 15, 12, 0, 0, DateTimeKind.Local)));

            record1.AddKeyValue("Date_1", new DateTime(2021, 1, 13).Date);

            record1.AddKeyValue("Subject", "AutomatedSDK");

            List<FileDetails> fileDetails = new List<FileDetails>();

            FileDetails fileDetail1 = new FileDetails();

            fileDetail1.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c32537b7c2400dadca8ff55f620c65357da";

            fileDetails.Add(fileDetail1);

            FileDetails fileDetail2 = new FileDetails();

            fileDetail2.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c32e0063e7321b5b4ca878a934519e6cdb2";

            fileDetails.Add(fileDetail2);

            FileDetails fileDetail3 = new FileDetails();

            fileDetail3.FileId = "ae9c7cefa418aec1d6a5cc2d9ab35c323daf4780bfe0058133556f155795981f";

            fileDetails.Add(fileDetail3);

            record1.AddKeyValue("File_Upload", fileDetails);

            //Products

            record1.AddFieldValue(Products.PRODUCT_NAME, "Product_Name");

            Com.Zoho.Crm.API.Record.Record vendorName = new Com.Zoho.Crm.API.Record.Record();

            vendorName.AddFieldValue(Products.ID, 34770614996051);

            record1.AddFieldValue(Products.VENDOR_NAME, vendorName);

            record1.AddFieldValue(Products.PRODUCT_CATEGORY, new Choice<string>("Software"));

            record1.AddFieldValue(Products.PRODUCT_ACTIVE, true);

            record1.AddFieldValue(Products.SALES_START_DATE, new DateTime(2020, 10, 12));

            record1.AddFieldValue(Products.SALES_END_DATE, new DateTime(2020, 12, 12));

            record1.AddFieldValue(Products.TAXABLE, true);

            List<Choice<string>> taxes = new List<Choice<string>>();

            taxes.Add(new Choice<string>("MyTax22"));

            taxes.Add(new Choice<string>("Sales Tax"));

            taxes.Add(new Choice<string>("MyTax11"));

            taxes.Add(new Choice<string>("Vat"));

            taxes.Add(new Choice<string>("MyTax23"));

            //record1.AddFieldValue(Products.TAX, taxes);  // AddFieldValue(Products.TAX, taxes);



            ///** Following methods are being used only by Inventory modules */

            //Com.Zoho.Crm.API.Record.Record dealName = new Com.Zoho.Crm.API.Record.Record();

            //dealName.AddFieldValue(Deals.ID, 347706110668016);

            //record1.AddFieldValue(Sales_Orders.DEAL_NAME, dealName);

            //Com.Zoho.Crm.API.Record.Record contactName = new Com.Zoho.Crm.API.Record.Record();

            //contactName.AddFieldValue(Contacts.ID, 347706110668012);

            //record1.AddFieldValue(Purchase_Orders.CONTACT_NAME, contactName);

            //Com.Zoho.Crm.API.Record.Record accountName = new Com.Zoho.Crm.API.Record.Record();

            //accountName.AddKeyValue("name", "automatedAccount");

            //record1.AddFieldValue(Quotes.ACCOUNT_NAME, accountName);



            //List<InventoryLineItems> inventoryLineItemList = new List<InventoryLineItems>();

            //InventoryLineItems inventoryLineItem = new InventoryLineItems();

            //LineItemProduct lineItemProduct = new LineItemProduct();

            //lineItemProduct.Id = 347706110725009;

            //inventoryLineItem.Product = lineItemProduct;

            //inventoryLineItem.Quantity = 1.5;

            //inventoryLineItem.ProductDescription = "productDescription";

            //inventoryLineItem.ListPrice = 10.0;

            //inventoryLineItem.Discount = "5.0";

            //inventoryLineItem.Discount = "5.25%";

            //List<LineTax> productLineTaxes = new List<LineTax>();

            //LineTax productLineTax = new LineTax();

            //productLineTax.Name = "MyTax22";

            //productLineTax.Percentage = 20.0;

            //productLineTaxes.Add(productLineTax);

            //inventoryLineItem.LineTax = productLineTaxes;

            //inventoryLineItemList.Add(inventoryLineItem);

            //record1.AddKeyValue("Product_Details", inventoryLineItemList);

            //List<LineTax> lineTaxes = new List<LineTax>();

            //LineTax lineTax = new LineTax();

            //lineTax.Name = "MyTax22";

            //lineTax.Percentage = 20.0;

            //lineTaxes.Add(lineTax);

            //record1.AddKeyValue("$line_tax", lineTaxes);

            ///** End Inventory **/

            ///** Following methods are being used only by Activity modules */

            ////Tasks

            //record1.AddFieldValue(Tasks.DESCRIPTION, "Test Task");

            //record1.AddKeyValue("Currency", new Choice<string>("INR"));

            //RemindAt remindAt = new RemindAt();

            //remindAt.Alarm = "FREQ=NONE;ACTION=EMAILANDPOPUP;TRIGGER=DATE-TIME:2020-09-03T12:30:00+05:30";

            //record1.AddFieldValue(Tasks.REMIND_AT, remindAt);

            //Com.Zoho.Crm.API.Record.Record whoId = new Com.Zoho.Crm.API.Record.Record();

            //whoId.Id = 34770614977055;

            //record1.AddFieldValue(Tasks.WHO_ID, whoId);

            //record1.AddFieldValue(Tasks.STATUS, new Choice<string>("Waiting for input"));

            //record1.AddFieldValue(Tasks.DUE_DATE, new DateTime(2021, 1, 13));

            //record1.AddFieldValue(Tasks.PRIORITY, new Choice<string>("High"));

            //record1.AddKeyValue("$se_module", "Accounts");

            //record1.AddFieldValue(Tasks.SUBJECT, "Email1");

            //Com.Zoho.Crm.API.Record.Record whatId = new Com.Zoho.Crm.API.Record.Record();

            //whatId.Id = 34770610207118;

            //record1.AddFieldValue(Tasks.WHAT_ID, whatId);


            ///** Recurring Activity can be provided in any activity module*/

            //RecurringActivity recurringActivity = new RecurringActivity();

            //recurringActivity.Rrule = "FREQ=DAILY;INTERVAL=10;UNTIL=2020-08-14;DTSTART=2020-07-03";

            //record1.AddFieldValue(Events.RECURRING_ACTIVITY, recurringActivity);


            ///** Following methods are being used only by Price_Books modules */

            //List<PricingDetails> pricingDetails = new List<PricingDetails>();

            //PricingDetails pricingDetail1 = new PricingDetails();

            //pricingDetail1.FromRange = 1.0;

            //pricingDetail1.ToRange = 5.0;

            //pricingDetail1.Discount = 2.0;

            //pricingDetails.Add(pricingDetail1);

            //PricingDetails pricingDetail2 = new PricingDetails();

            //pricingDetail2.AddKeyValue("from_range", 6.0);

            //pricingDetail2.AddKeyValue("to_range", 11.0);

            //pricingDetail2.AddKeyValue("discount", 3.0);

            //pricingDetails.Add(pricingDetail2);

            //record1.AddFieldValue(Price_Books.PRICING_DETAILS, pricingDetails);

            //record1.AddKeyValue("Email", "abc@zoho.com");

            //record1.AddFieldValue(Price_Books.DESCRIPTION, "TEST");

            //record1.AddFieldValue(Price_Books.PRICE_BOOK_NAME, "book_name");

            //record1.AddFieldValue(Price_Books.PRICING_MODEL, new Choice<string>("Flat1"));

            //List<Tag> tagList = new List<Tag>();

            //Tag tag = new Tag();

            //tag.Name = "Testtask";

            //tagList.Add(tag);

            //record1.Tag = tagList;

            ////Add Record instance to the list
            //records.Add(record1);

            ////Set the list to Records in BodyWrapper instance
            //bodyWrapper.Data = records;

            //List<string> trigger = new List<string>();

            //trigger.Add("approval");

            //trigger.Add("workflow");

            //trigger.Add("blueprint");

            //bodyWrapper.Trigger = trigger;

            ////bodyWrapper.LarId = "34770610087515";


            //HeaderMap headerInstance = new HeaderMap();

            ////headerInstance.Add(GetRecordsHeader.X_EXTERNAL, "Leads.External");

            ////Call CreateRecords method that takes moduleAPIName, BodyWrapper and headerInstance instance as parameter.
            //APIResponse<ActionHandler> response = recordOperations.CreateRecords(moduleAPIName, bodyWrapper, headerInstance);

            //if (response != null)
            //{
            //    //Get the status code from response
            //    Console.WriteLine("Status Code: " + response.StatusCode);

            //    //Check if expected response is received
            //    if (response.IsExpected)
            //    {
            //        //Get object from response
            //        ActionHandler actionHandler = response.Object;

            //        if (actionHandler is ActionWrapper)
            //        {
            //            //Get the received ActionWrapper instance
            //            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

            //            //Get the list of obtained ActionResponse instances
            //            List<ActionResponse> actionResponses = actionWrapper.Data;

            //            foreach (ActionResponse actionResponse in actionResponses)
            //            {
            //                //Check if the request is successful
            //                if (actionResponse is SuccessResponse)
            //                {
            //                    //Get the received SuccessResponse instance
            //                    SuccessResponse successResponse = (SuccessResponse)actionResponse;

            //                    //Get the Status
            //                    Console.WriteLine("Status: " + successResponse.Status.Value);

            //                    //Get the Code
            //                    Console.WriteLine("Code: " + successResponse.Code.Value);

            //                    Console.WriteLine("Details: ");

            //                    //Get the details map

            //                    //Get the details map
            //                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
            //                    {
            //                        //Get each value in the map
            //                        Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
            //                    }

            //                    //Get the Message
            //                    Console.WriteLine("Message: " + successResponse.Message.Value);
            //                }
            //                //Check if the request returned an exception
            //                else if (actionResponse is APIException)
            //                {
            //                    //Get the received APIException instance
            //                    APIException exception = (APIException)actionResponse;

            //                    //Get the Status
            //                    Console.WriteLine("Status: " + exception.Status.Value);

            //                    //Get the Code
            //                    Console.WriteLine("Code: " + exception.Code.Value);

            //                    Console.WriteLine("Details: ");

            //                    //Get the details map
            //                    foreach (KeyValuePair<string, object> entry in exception.Details)
            //                    {
            //                        //Get each value in the map
            //                        Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
            //                    }

            //                    //Get the Message
            //                    Console.WriteLine("Message: " + exception.Message.Value);
            //                }
            //            }
            //        }
            //        //Check if the request returned an exception
            //        else if (actionHandler is APIException)
            //        {
            //            //Get the received APIException instance
            //            APIException exception = (APIException)actionHandler;

            //            //Get the Status
            //            Console.WriteLine("Status: " + exception.Status.Value);

            //            //Get the Code
            //            Console.WriteLine("Code: " + exception.Code.Value);

            //            Console.WriteLine("Details: ");

            //            //Get the details map
            //            foreach (KeyValuePair<string, object> entry in exception.Details)
            //            {
            //                //Get each value in the map
            //                Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
            //            }

            //            //Get the Message
            //            Console.WriteLine("Message: " + exception.Message.Value);

            //        }
            //    }
            //    else
            //    {  //If response is not as expected

            //        //Get model object from response
            //        Model responseObject = response.Model;

            //        //Get the response object's class
            //        Type type = responseObject.GetType();

            //        //Get all declared fields of the response class
            //        Console.WriteLine("Type is: {0}", type.Name);

            //        PropertyInfo[] props = type.GetProperties();

            //        Console.WriteLine("Properties (N = {0}):", props.Length);

            //        foreach (var prop in props)
            //        {
            //            if (prop.GetIndexParameters().Length == 0)
            //            {
            //                Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
            //            }
            //            else
            //            {
            //                Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
            //            }
            //        }

            //    }


            //}

        }

    }
}
