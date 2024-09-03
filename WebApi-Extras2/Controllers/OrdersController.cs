using CLExtras2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace WebApi_Extras2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        Extras2Context db = new Extras2Context();
        SqlConnection con = new SqlConnection("Server=media.ruppin.ac.il;Database=bgroup33_test2;User ID=bgroup33;Password=bgroup33_99691;Encrypt=false");

        [HttpGet]
        [Route("ShowOrders/{CustomerId}")]
        public dynamic ShowOrders(int CustomerId)
        {
            try
            {
                string query = "select top 1 BK.keyword_id, keyword_name, count(*) as amount from Orders O inner join Box_Keywords BK on O.box_id=BK.box_id inner join Keywords K on BK.keyword_id = K.keyword_id where O.box_id is not null and customer_id = @userId group by BK.keyword_id, keyword_name order by amount desc";

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@userId", CustomerId);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "keywords");
                DataTable dt = ds.Tables["keywords"];

                if (dt.Rows.Count == 1)
                {
                    string keywordName = dt.Rows[0]["keyword_name"].ToString();
                    int keywordId = (int)dt.Rows[0]["keyword_id"];

                    query = "select * from Boxes B left join Box_Keywords BK on B.box_id=BK.box_id where (keyword_id=@keyId or box_description like '%@keyname%') and B.quantity_available > 0 and B.box_id not in (select distinct box_id from Orders where customer_id=@user_id)";
                    SqlDataAdapter adapter2 = new SqlDataAdapter(query, con);
                    adapter2.SelectCommand.Parameters.AddWithValue("@user_id", CustomerId);
                    adapter2.SelectCommand.Parameters.AddWithValue("@keyname", keywordName);
                    adapter2.SelectCommand.Parameters.AddWithValue("@keyId", keywordId);
                    adapter2.Fill(ds, "boxes");
                    DataTable dt2 = ds.Tables["boxes"];
                    /*List<Dictionary<string, object>> data = DataTableToDictionaryList(dt2);*/
                    string json = JsonConvert.SerializeObject(dt2);
                    return Content(json, "application/json");

                }
                return Ok(dt.Rows.Count);


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
