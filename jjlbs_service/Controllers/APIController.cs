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
                DataSet data = ohp.Query($"select RAWTOHEX(VILLAGE_ID) as VILLAGE_ID, VILLAGE_NAME, VILLAGE_ADDRESS, VILLAGE_REGION, VILLAGE_TYPE, VILLAGE_LNG, VILLAGE_LAT, VILLAGE_BOUNDS from lbs_village order by create_date desc");
                //DataSet data2 = ohp.Query($"select * from lbs_building");
                //DataSet data2 = ohp.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from lbs_building lb where lb.village_id = (select village_id from lbs_village lv where lv.village_name = '元电职工住宅A区')");
                DataSet data2 = ohp.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from lbs_building lb");
                DataSet data3 = ohp.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from lbs_building lb where lb.village_id = (select village_id from lbs_village lv where lv.village_name = '元电职工住宅A区')");

                string json = JsonConvert.SerializeObject(data);
                string json2 = JsonConvert.SerializeObject(data2);
                string json3 = JsonConvert.SerializeObject(data3);

                string jsonf = "[";
                jsonf += json + "," + json2 + "," + json3 + "]";
                return jsonf;
            }
            return "apple";
        }

        public string IndexByRegion(string type)
        {
            if (type == null)
            {
                return "404";
            }
            string region = "";
            string village = "";
            Oraclehp ohpAll = new Oraclehp();
            DataSet dataAll = ohpAll.Query($"select RAWTOHEX(VILLAGE_ID) as VILLAGE_ID, VILLAGE_NAME, VILLAGE_ADDRESS, VILLAGE_REGION, VILLAGE_TYPE, VILLAGE_LNG, VILLAGE_LAT, VILLAGE_BOUNDS from lbs_village order by create_date desc");
            //DataSet data2 = ohp.Query($"select * from lbs_building");
            DataSet data2All = ohpAll.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from lbs_building lb");

            DataSet data3All = ohpAll.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from lbs_building lb where lb.village_id = (select village_id from lbs_village lv where lv.village_name = '元电职工住宅A区')");

            string jsonAll = JsonConvert.SerializeObject(dataAll);
            string json2All = JsonConvert.SerializeObject(data2All);
            string json3All = JsonConvert.SerializeObject(data3All);

            string jsonfAll = "[";
            jsonfAll += jsonAll + "," + json2All + "," + json3All + "]";
            if (type == "all")
            {
                return jsonfAll;
            }
            if (type == "hsq")
            {
                region = "内蒙古自治区/赤峰市/红山区";
                village = "怡康家园(五道街)";
            }
            else if (type == "ybsq")
            {
                region = "内蒙古自治区/赤峰市/元宝山区";
                village = "锦绣花苑";
            }
            else if (type == "ssq")
            {
                region = "内蒙古自治区/赤峰市/松山区";
                village = "水利局家属楼(银河路)";
            }
            else if (type == "alkexq")
            {
                region = "内蒙古自治区/赤峰市/阿鲁科尔沁旗";
                village = "尚景花园";
            }
            else if (type == "blzq")
            {
                region = "内蒙古自治区/赤峰市/巴林左旗";
                village = "上京小区";
            }
            else if (type == "blyq")
            {
                region = "内蒙古自治区/赤峰市/巴林右旗";
                village = "珠峰骏景";
            }
            else if (type == "lxx")
            {
                region = "内蒙古自治区/赤峰市/林西县";
                village = "中昊小区";
            }
            else if (type == "ksktq")
            {
                region = "内蒙古自治区/赤峰市/克什克腾旗";
                village = "如意家园(如意家园B区南)";
            }
            else if (type == "wntq")
            {
                region = "内蒙古自治区/赤峰市/翁牛特旗";
                village = "税务A区";
            }
            else if (type == "klxq")
            {
                region = "内蒙古自治区/赤峰市/喀喇沁旗";
                village = "河北街道向阳社区";
            }
            else if (type == "ncx")
            {
                region = "内蒙古自治区/赤峰市/宁城县";
                village = "和谐家园(昌盛街)";
            }
            else if (type == "ahq")
            {
                region = "内蒙古自治区/赤峰市/敖汉旗";
                village = "锦绣花园";
            }
            Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"select RAWTOHEX(VILLAGE_ID) as VILLAGE_ID, VILLAGE_NAME, VILLAGE_ADDRESS, VILLAGE_REGION, VILLAGE_TYPE, VILLAGE_LNG, VILLAGE_LAT, VILLAGE_BOUNDS from lbs_village where village_region='{region}' order by create_date desc");
            //DataSet data2 = ohp.Query($"select * from lbs_building");
            DataSet data2 = ohp.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from lbs_building lb where region='{region}'");

            DataSet data3 = ohp.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from lbs_building lb where lb.village_id = (select village_id from lbs_village lv where lv.village_name = '{village}')");
                string json = JsonConvert.SerializeObject(data);
                string json2 = JsonConvert.SerializeObject(data2);
            string json3 = JsonConvert.SerializeObject(data3);

            string jsonf = "[";
            jsonf += json + "," + json2 + "," + json3 + "]";
            return jsonf;


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

        public string DetailsBuilding(string id)
        {
            if (id == null)
            {
                return "404";
            }
            Oraclehp ohp = new Oraclehp();
            DataSet data = ohp.Query($"select * from lbs_building where BUILDING_ID = '{id}'");
            if (data.Tables.Count == 0)
            {
                return "404";
            }
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        public string VillageBuildings(string id)
        {
            if (id == null)
            {
                return "404";
            }
            Oraclehp ohp = new Oraclehp();
            DataSet data = ohp.Query($"select RAWTOHEX(BUILDING_ID) as BUILDING_ID, BUILDING_NAME, BUILDING_ADDRESS, LNG, LAT from LBS_BUILDING t where t.village_id = '{id}'");
            if (data.Tables.Count == 0)
            {
                return "404";
            }
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        public string CreateVillage()
        {
            string village_name = HttpContext.Request.Form["village_name"];
            string village_address = HttpContext.Request.Form["village_address"];
            string village_region = HttpContext.Request.Form["village_region"];
            string village_type = HttpContext.Request.Form["village_type"];
            string village_bounds = HttpContext.Request.Form["village_bounds"];
            string village_x = HttpContext.Request.Form["village_x"];
            string village_y = HttpContext.Request.Form["village_y"];
            string village_lng = HttpContext.Request.Form["village_lng"];
            string village_lat = HttpContext.Request.Form["village_lat"];
            string source = HttpContext.Request.Form["source"];
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
                errors.Add("小区边界不能为空");
            }
            if (village_x.Length == 0)
            {
                errors.Add("小区经度不能为空");
            }
            if (village_y.Length == 0)
            {
                errors.Add("小区纬度不能为空");
            }
            if (village_lng.Length == 0)
            {
                errors.Add("小区经度不能为空");
            }
            if (village_lng.Length == 0)
            {
                errors.Add("小区纬度不能为空");
            }
            if (source.Length == 0)
            {
                errors.Add("小区来源不能为空");
            }
            if (errors.Count > 0)
            {
                return JsonConvert.SerializeObject(errors);
            }
            else
            {
                Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"begin insert into lbs_village (village_name, village_address, village_region, village_type, village_bounds, village_x, village_y, village_lng, village_lat, source) values ('{village_name}','{village_address}','{village_region}','{village_type}', '{village_bounds}','{village_x}','{village_y}','{village_lat}','{village_lng}','{source}');commit;end;");
                return "[]";
            }
        }

        public string CreateBuilding()
        {
            string building_number = HttpContext.Request.Form["building_number"];
            string building_name = HttpContext.Request.Form["building_name"];
            string building_address = HttpContext.Request.Form["building_address"];
            string building_region = HttpContext.Request.Form["building_region"];
            string building_type = HttpContext.Request.Form["building_type"];
            string building_bounds = HttpContext.Request.Form["building_bounds"];
            string building_x = HttpContext.Request.Form["building_x"];
            string building_y = HttpContext.Request.Form["building_y"];
            string building_lng = HttpContext.Request.Form["building_lng"];
            string building_lat = HttpContext.Request.Form["building_lat"];
            string source = HttpContext.Request.Form["source"];
            List<string> errors = new List<string>();
            if (building_number.Length == 0)
            {
                errors.Add("楼宇号码不能为空");
            }
            if (building_name.Length == 0)
            {
                errors.Add("楼宇名称不能为空");
            }
            if (building_address.Length == 0)
            {
                errors.Add("详细地址不能为空");
            }
            if (building_region.Length == 0)
            {
                errors.Add("区域不能为空");
            }
            if (building_type.Length == 0)
            {
                errors.Add("类型不能为空");
            }
            if (building_bounds.Length == 0)
            {
                errors.Add("楼宇边界不能为空");
            }
            if (building_x.Length == 0)
            {
                errors.Add("楼宇经度不能为空");
            }
            if (building_y.Length == 0)
            {
                errors.Add("楼宇纬度不能为空");
            }
            if (building_lng.Length == 0)
            {
                errors.Add("楼宇经度不能为空");
            }
            if (building_lng.Length == 0)
            {
                errors.Add("楼宇纬度不能为空");
            }
            if (source.Length == 0)
            {
                errors.Add("楼宇来源不能为空");
            }
            if (errors.Count > 0)
            {
                return JsonConvert.SerializeObject(errors);
            }
            else
            {
                Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"begin insert into lbs_building (building_number, building_name, building_address, region, type, bounds, x, y, lng, lat, source) values ('{building_number}','{building_name}','{building_address}','{building_region}','{building_type}', '{building_bounds}','{building_x}','{building_y}','{building_lat}','{building_lng}','{source}');commit;end;");
                return "[]";
            }
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
                DataSet data = ohp.Query($"begin update lbs_village set village_name ='{village_name}',village_address='{village_address}',village_region='{village_region}',village_type='{village_type}' where village_id = '{id}';commit;end;");
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
                DataSet data = ohp.Query($"begin update lbs_village set village_name ='{village_name}',village_address='{village_address}',village_region='{village_region}',village_type='{village_type}',village_bounds='{village_bounds}' where village_id = '{id}';commit;end;");
                return "[]";
            }
        }

        public string EditBuilding(string id)
        {
            if (id == null)
            {
                return "404";
            }
            string building_name = HttpContext.Request.Form["building_name"];
            string building_address = HttpContext.Request.Form["building_address"];
            List<string> errors = new List<string>();
            if (building_name.Length == 0)
            {
                errors.Add("楼宇名称不能为空");
            }
            if (building_address.Length == 0)
            {
                errors.Add("详细地址不能为空");
            }
            if (errors.Count > 0)
            {
                return JsonConvert.SerializeObject(errors);
            }
            else
            {
                Oraclehp ohp = new Oraclehp();
                DataSet data = ohp.Query($"begin update lbs_building set building_name ='{building_name}',building_address='{building_address}' where building_id = '{id}';commit;end;");
                return "[]";
            }
        }

        public string DeleteVillage(string id)
        {
            if (id == null)
            {
                return "404";
            }
            Oraclehp ohp = new Oraclehp();
            DataSet data = ohp.Query($"begin delete from lbs_village where village_id = '{id}';commit;end;");
            return "[]";
        }

        public string DeleteBuilding(string id)
        {
            if (id == null)
            {
                return "404";
            }
            Oraclehp ohp = new Oraclehp();
            DataSet data = ohp.Query($"begin delete from lbs_building where building_id = '{id}';commit;end;");
            return "[]";
        }
    }
    }

