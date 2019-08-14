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

public partial class MasterSchedulePage : System.Web.UI.Page
{

    public class Master_Schedule
    {

        public ObjectId Id { get; set; }
        public DateTime CraneDate { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public string Unit { get; set; }
        public string Method { get; set; }
        public string CraneCrew { get; set; }
        public string FloorArea { get; set; }
        public string WallArea { get; set; }
        public string TotalSQFT { get; set; }
        public string Comment { get; set; }


        [BsonSerializer(typeof(TestingObjectTypeSerializer))]
        public string __v1 { get; set; }

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

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public static string[,] jobdetailDB = new string[1000, 15];
    protected  async void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Array.Clear(jobdetailDB, 0, jobdetailDB.Length);
        ListBox1.Items.Clear();
        DateTime currentdate = Calendar1.SelectedDate;

        var client = new MongoClient("mongodb+srv://sadiqa:Canada2005@acqbuilt-kkofs.mongodb.net/test?retryWrites=true&w=majority");
        var database = client.GetDatabase("Schedule");

        var masterSchedule = database.GetCollection<Master_Schedule>("Master Schedule");

        var list= await masterSchedule.Find(x => x.CraneDate == currentdate).ToListAsync();

        int i = 0;

        foreach (var Jobdetails in list)
        {

            jobdetailDB[i, 0] = Convert.ToString(Jobdetails.Job);
            jobdetailDB[i, 1] = Convert.ToString(Jobdetails.Address);
            jobdetailDB[i, 2] = Convert.ToString(Jobdetails.Method);
            jobdetailDB[i, 3] = Convert.ToString(Jobdetails.Unit);
            jobdetailDB[i, 4] = Convert.ToString(Jobdetails.FloorArea);
            jobdetailDB[i, 5] = Convert.ToString(Jobdetails.WallArea);
            jobdetailDB[i, 6] = Convert.ToString(Jobdetails.TotalSQFT);
            jobdetailDB[i, 7] = Convert.ToString(Jobdetails.Comment);

            ListBox1.Items.Add(Jobdetails.Job);

            i++;
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
        TextBox8.Text = jobdetailDB[i, 7];

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}

