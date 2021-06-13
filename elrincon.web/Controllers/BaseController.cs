using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.WebSockets;
using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Utils;
using Microsoft.Ajax.Utilities;

namespace elrincon.web.Controllers
{
    public class BaseController:Controller
    {
        protected UserModel ElUser;
        protected ConvertHelper CHelper = new ConvertHelper();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            if (controllerName.ToLower() != "login")
            {
                if (User == null || !User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Login/Login");
                    return;
                }
                ElUser = GetUser(User.Identity.Name);
            }
            base.OnActionExecuting(filterContext);
        }

        private UserModel GetUser(string name)
        {
            UserModel user = new UserModel();
            ElDbTable table = ExecuteTable("select * from el_kullanicilar where kullanici_adi like '" + name + "'");
            
            if (table.Rows.Count > 0)
            {
                user.Id = CHelper.ToInt32(table.Rows[0]["id"]);
                user.Username = CHelper.ToString(table.Rows[0]["kullanici_adi"]); 
                user.SubeId = CHelper.ToInt32(table.Rows[0]["subesi"]); 
            }

            return user;
        }

        //public ElDbTable GetTable(string tableName)
        //{
        //    ElDbTable tablea = new ElDbTable();

        //    return tablea.GetTable(tableName);
        //}

        public ElDbTable GetTable(string tableName)
        {
            ElDbTable cb = new ElDbTable();

            return cb.GetTable(tableName);
        }

        public ElDbTable ExecuteTable(string sql)
        {
            ElDbTable db = new ElDbTable(); 

            return db.ExecuteTable(sql);
        }
    
        public ElSelectModel GetDomainList(int ListeId)
        {

            DataRow[] tableUrunCesidi = GetTable("el_liste_deger").Select("liste_id = " + ListeId);
            ElSelectModel model = new ElSelectModel(tableUrunCesidi, "deger", "kod");
            
            return model;
        }

        /// <summary>
        /// bu kayıt bu tabloda daha önce oluşturulmuş mu
        /// </summary>
        public bool ControlSameRecord(string tableName, string fieldName, string fieldValue, int? id)
        {
            ElDbTable table = GetTable(tableName);
            
            if (fieldValue.IsNullOrWhiteSpace())
                fieldValue = "0";

            string filter = fieldName + "='" + fieldValue+"' ";
            if (id.HasValue && id.Value > 0 )
                filter += " and id <> '" + id.Value + "' ";

            DataRow[] dataRows = table.Select(filter);

            return dataRows.Length > 0;
        }

        /// <summary>
        /// personel ve hanutcu yüzdeliklerinin belirlenmesi
        /// </summary>
        /// <param name="modelEditorModelKardanOrani"></param>
        /// <param name="tip"></param>
        public void FillYuzdelikDefaultModel(List<KardanYuzdelikOranlariModel> modelEditorModelKardanOrani, string tip, int personelid )
        {
            string sql =
                "select *, muhasebe_kodu as muhasebe_kodu_oran,ROW_NUMBER() OVER (ORDER BY urunTipi ) AS RowNum " +
                "from el_kardan_yuzdelik_oranlari " +
                "where yuzdelik_tip in (" + tip + ") and personel_id = " + personelid;

            ElDbTable table = ExecuteTable(sql);

            foreach (DataRow dataRow in table.Rows)
            {
                if (!dataRow.IsNull("urunTipi"))
                    FillEmptyYuzdelikModel(modelEditorModelKardanOrani, dataRow);
            }
        }

        public void FillEmptyYuzdelikModel(List<KardanYuzdelikOranlariModel> modelEditorModelKardanOrani, DataRow dataRow)
        {
            KardanYuzdelikOranlariModel model = new KardanYuzdelikOranlariModel();

            model.YuzdelikTipi = 0;
            DataRow[] urunTipileri = GetTable("el_liste_deger").Select("liste_id = 6");
            model.UrunTipi = new ElSelectModel(urunTipileri, "deger", "kod");

            model.NarmalSatisParite1 = new ElInputModel(InputDataType.Double);
            model.NarmalSatisParite2 = new ElInputModel(InputDataType.Double);
            model.NarmalSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            model.KapiSatisParite1 = new ElInputModel(InputDataType.Double);
            model.KapiSatisParite2 = new ElInputModel(InputDataType.Double);
            model.KapiSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            model.InternetSatisParite1 = new ElInputModel(InputDataType.Double);
            model.InternetSatisParite2 = new ElInputModel(InputDataType.Double);
            model.InternetSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            model.YurtDisiSatisParite1 = new ElInputModel(InputDataType.Double);
            model.YurtDisiSatisParite2 = new ElInputModel(InputDataType.Double);
            model.YurtDisiSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            model.GenelCiroSatisParite1 = new ElInputModel(InputDataType.Double);
            model.GenelCiroSatisParite2 = new ElInputModel(InputDataType.Double);
            model.GenelCiroSatisYuzdeligi = new ElInputModel(InputDataType.Double);
            model.MuhasebeGelirKodu = new ElSelectModel(GetTableMuhasebeGiderHesabi(), "names", "id");

            if (dataRow != null && !dataRow.IsNull("urunTipi"))
            {
                model.YuzdelikTipi = !dataRow.IsNull("yuzdelik_tip") ?Convert.ToInt32(dataRow["yuzdelik_tip"]):0;
             
                model.UrunTipi.SetSelectedCode(Convert.ToInt32(dataRow["urunTipi"]));
                model.NarmalSatisParite1.Value = dataRow["normalSatisParite1"];
                model.NarmalSatisParite2.Value = dataRow["normalSatisParite2"];
                model.NarmalSatisYuzdeligi.Value = dataRow["normalSatisYuzdelik"];

                model.KapiSatisParite1.Value = dataRow["kapiSatisParite1"];
                model.KapiSatisParite2.Value = dataRow["kapiSatisParite2"];
                model.KapiSatisYuzdeligi.Value = dataRow["kapiSatisYuzdelik"];

                model.InternetSatisParite1.Value = dataRow["internetSatisParite1"];
                model.InternetSatisParite2.Value = dataRow["internetSatisParite2"];
                model.InternetSatisYuzdeligi.Value = dataRow["internetSatisYuzdelik"];

                model.YurtDisiSatisParite1.Value = dataRow["yurtDisiSatisParite1"];
                model.YurtDisiSatisParite2.Value = dataRow["yurtDisiSatisParite2"];
                model.YurtDisiSatisYuzdeligi.Value = dataRow["yurtDisiSatisYuzdelik"];

                model.GenelCiroSatisParite1.Value = dataRow["genelSatisParite1"];
                model.GenelCiroSatisParite2.Value = dataRow["genelSatisParite2"];
                model.GenelCiroSatisYuzdeligi.Value = dataRow["genelSatisYuzdelik"];

                if(!dataRow.IsNull("muhasebe_kodu_oran"))
                    model.MuhasebeGelirKodu.SetSelectedCode(Convert.ToInt32(dataRow["muhasebe_kodu_oran"])); ;
            }

            modelEditorModelKardanOrani.Add(model);
        }

        public DataRow[] GetTableMuhasebeGiderHesabi()
        {
            string sql = "select kasa_bilgi_detay_adi AS names, id " +
                         "from el_muhasebe_tanimlari_kasa_bilgi_detay " +
                         "where kasa_bilgi_detay_adi is not null ";

            ElDbTable table = ExecuteTable(sql);

            return table.Select();
        }
    
    }

}