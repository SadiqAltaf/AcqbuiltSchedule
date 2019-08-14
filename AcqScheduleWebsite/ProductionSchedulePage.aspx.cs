using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public partial class ProductionSchedulePage : System.Web.UI.Page
{
    public class Production_Schedule
    {

        public ObjectId Id { get; set; }
        public DateTime Startdate { get; set; }
        public string Job { get; set; }
        public string Operation { get; set; }
        public string Instruction { get; set; }
        public string Comment { get; set; }
        public string AllocatedQty { get; set; }
        public string Duedate { get; set; }
        public string Change { get; set; }
        public string CraneDateRef { get; set; }
        public string Resourcegroup { get; set; }
        public string UOM { get; set; }
        public string BuildOrder { get; set; }
        public string CrewName { get; set; }


        [BsonSerializer(typeof(TestingObjectTypeSerializer))]
        public string __v1 { get; set; }

    }
    public static string[,] jobdetailDB = new string[1000, 15];
    protected async void Page_Load(object sender, EventArgs e)
    {
        var client = new MongoClient("mongodb+srv://sadiqa:Canada2005@acqbuilt-kkofs.mongodb.net/test?retryWrites=true&w=majority");
        var database = client.GetDatabase("Schedule");

        var ProductionSchedule = database.GetCollection<Production_Schedule>("Production Schedule");

        var list = await ProductionSchedule.Find(_ => true).ToListAsync();
        var result = from c in list where c.Resourcegroup != null select c.Resourcegroup;

        result = result.Distinct().ToList();

        foreach (var resource in result)
        {
            DropDownList1.Items.Add(resource);
        }
    }

    public class TestingObjectTypeSerializer : IBsonSerializer
    {
        public Type ValueType { get; } = typeof(string);

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context.Reader.CurrentBsonType == BsonType.Int32) return GetNumberValue(context);

            return context.Reader.ReadString();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            context.Writer.WriteString(value as string);
        }

        private static object GetNumberValue(BsonDeserializationContext context)
        {
            var value = context.Reader.ReadInt32();

            switch (value)
            {
                case 1:
                    return "one";
                case 2:
                    return "two";
                case 3:
                    return "three";
                default:
                    return "BadType";
            }
        }
    }

    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = ListBox1.SelectedIndex;





        TextBox2.Text = jobdetailDB[i, 1];
        TextBox3.Text = jobdetailDB[i, 2];
        TextBox4.Text = jobdetailDB[i, 3];
        TextBox10.Text = jobdetailDB[i, 4];
        TextBox6.Text = jobdetailDB[i, 5];
        TextBox9.Text = jobdetailDB[i, 6];
    }

    protected async void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Array.Clear(jobdetailDB, 0, jobdetailDB.Length);
        ListBox1.Items.Clear();
        DateTime currentdate = Calendar1.SelectedDate;
        string resource = DropDownList1.Text;

        var client = new MongoClient("mongodb+srv://sadiqa:Canada2005@acqbuilt-kkofs.mongodb.net/test?retryWrites=true&w=majority");
        var database = client.GetDatabase("Schedule");

        var ProductionSchedule = database.GetCollection<Production_Schedule>("Production Schedule");

        var list = await ProductionSchedule.Find(x => x.Startdate == currentdate && x.Resourcegroup== resource).ToListAsync();

        int i = 0;

        foreach (var Jobdetails in list)
        {

            jobdetailDB[i, 0] = Convert.ToString(Jobdetails.Job);
            jobdetailDB[i, 1] = Convert.ToString(Jobdetails.Operation);
            jobdetailDB[i, 2] = Convert.ToString(Jobdetails.Instruction);
            jobdetailDB[i, 3] = Convert.ToString(Jobdetails.CrewName);
            jobdetailDB[i, 4] = Convert.ToString(Jobdetails.AllocatedQty);
            jobdetailDB[i, 5] = Convert.ToString(Jobdetails.UOM);
            jobdetailDB[i, 6] = Convert.ToString(Jobdetails.Comment);

            ListBox1.Items.Add(Jobdetails.Job);

            i++;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}