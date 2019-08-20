using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jjlbs_service.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using jjlbs_service.oracle;
using Newtonsoft.Json;

namespace jjlbs_service.Controllers
{
    public class APIController : Controller
    {
        public string Index(string type)
        {
            if (type == null)
            {
                return "404";
            }
            if (type == "all")
            {
                Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"select RAWTOHEX(VILLAGE_ID) as VILLAGE_ID, VILLAGE_NAME, VILLAGE_ADDRESS, VILLAGE_REGION, VILLAGE_TYPE, VILLAGE_LNG, VILLAGE_LAT, VILLAGE_BOUNDS from lbs_village");
                //DataSet data2 = ohp.Query($"select * from lbs_building");
                DataSet data2 = ohp.Query($"select* from lbs_building lb where lb.village_id = (select village_id from lbs_village lv where lv.village_name = '银河花都')");
                string json = JsonConvert.SerializeObject(data);
                string json2 = JsonConvert.SerializeObject(data2);
                string jsonf = "[";
                jsonf += json + "," + json2 + "]";
                return jsonf;
            }
            return "apple";
        }

        public string DetailsVillage(string id)
        {
            if (id == null)
            {
                return "404";
            }
                Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"select * from lbs_village where VILLAGE_ID = '{id}'");
                if (data.Tables.Count == 0)
                {
                    return "404";
                }
                string json = JsonConvert.SerializeObject(data);
                return json;
            }


        public string EditVillageNoBounds(string id)
        {
            if (id == null)
            {
                return "404";
            }
            string village_name = HttpContext.Request.Form["village_name"];
            string village_address = HttpContext.Request.Form["village_address"];
            string village_region = HttpContext.Request.Form["village_region"];
            string village_type = HttpContext.Request.Form["village_type"];
            List<string> errors = new List<string>();
            if (village_name.Length == 0)
            {
                errors.Add("小区名称不能为空");
            }
            if (village_address.Length == 0)
            {
                errors.Add("详细地址不能为空");
            }
            if (village_region.Length == 0)
            {
                errors.Add("区域不能为空");
            }
            if (village_type.Length == 0)
            {
                errors.Add("类型不能为空");
            }
            if (errors.Count > 0)
            {
                return JsonConvert.SerializeObject(errors);
            } else
            {
                Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"update lbs_village set village_name ='{village_name}',village_address='{village_address}',village_region='{village_region}',village_type='{village_type}' where village_id = '{id}'");
                return "[]";
            }
        }


        public string EditVillageBounds(string id)
        {
            if (id == null)
            {
                return "404";
            }
            string village_name = HttpContext.Request.Form["village_name"];
            string village_address = HttpContext.Request.Form["village_address"];
            string village_region = HttpContext.Request.Form["village_region"];
            string village_type = HttpContext.Request.Form["village_type"];
            string village_bounds = HttpContext.Request.Form["village_bounds"];
            List<string> errors = new List<string>();
            if (village_name.Length == 0)
            {
                errors.Add("小区名称不能为空");
            }
            if (village_address.Length == 0)
            {
                errors.Add("详细地址不能为空");
            }
            if (village_region.Length == 0)
            {
                errors.Add("区域不能为空");
            }
            if (village_type.Length == 0)
            {
                errors.Add("类型不能为空");
            }
            if (village_bounds.Length == 0)
            {
                errors.Add("边界不能为空");
            }
            if (errors.Count > 0)
            {
                return JsonConvert.SerializeObject(errors);
            }
            else
            {
                Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"update lbs_village set village_name ='{village_name}',village_address='{village_address}',village_region='{village_region}',village_type='{village_type}',village_bounds='{village_bounds}' where village_id = '{id}'");
                return "[]";
            }
        }
    }
    }

