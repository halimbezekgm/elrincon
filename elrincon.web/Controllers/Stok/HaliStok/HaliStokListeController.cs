using System;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using elrincon.web.Models.Stok.HaliStok;

namespace elrincon.web.Controllers.Stok.HaliStok
{
    public class HaliStokListeController : HaliStokBaseController
    {
        // GET: HaliStokListe
        public ActionResult Index()
        {
            HaliStokListeModel model = new HaliStokListeModel();
            model.GridModel = FillGridModel(FillGridCustomProperties());
            model.HaliStokEditorModel = FillEditorModel();

            return View("~/Views/Stok/HaliStok/HaliStokListe.cshtml", model);
        }

        private HaliStokEditorModel FillEditorModel()
        {
            return FillModel(null, "_for_query");
        }

        private CustomProperties FillGridCustomProperties()
        {
            CustomProperties customProperties = new CustomProperties();
            customProperties.Add("stok_no", "");
            customProperties.Add("tarih", "");
            customProperties.Add("sube", "");
            customProperties.Add("urun_cesidi", "");
            customProperties.Add("mensei", "");
            customProperties.Add("uretim_tipi", "");
            customProperties.Add("material", "");
            customProperties.Add("satıcı", "");

            return customProperties;
        }


        private ElDataGridModel FillGridModel(CustomProperties customProperties)
        {

            ElDbTable dataTable = GetBilgiler(customProperties);
            ElDataGridModel dataGridModel = new ElDataGridModel("hali_stok_liste_grid_id");

            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = customProperties;


            GridRow row = new GridRow("stok_no", "stok_no", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Ana Grubu", "ana_grubu", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
            
            row = new GridRow("Tarih", "tarih", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Ürün Çeşidi", "urun_cesidi_adi", "7%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Menşei", "mensei_adi", "7%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Uretim Tipi", "uretim_tipi_adi", "5%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Material", "material_adi", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Ölçu Tip", "olcu_tip_adi", "5%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
            row = new GridRow("Boy", "olcu_boy", "3%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
            row = new GridRow("En", "olcu_en", "3%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
            row = new GridRow("Mt.Kare", "olcu_mt2", "3%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Satıcı", "satici_adi", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Alış Fiyat", "adet_fiyat", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
            
            row = new GridRow("Etiket Fiyat", "etiket_fiyat", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Firma Kodu", "firma", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Şube", "sube_adi", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Konsinye Verilen", "konsinye_verilen", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Satıs Tarih", "satis_tarihi", "5%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Satış No", "satis_no", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("Düzenle", @Url.Content("~/Content/images/edit.png"), "edit_hali_stok_listesi_cliced");
            ActionButton buttonDelete = new ActionButton("Sil", @Url.Content("~/Content/images/delete.png"), "delete_hali_stok_listesi_cliced");
            //ActionButton buttonSubeDegistir = new ActionButton("Şube Değiştir", @Url.Content("~/Content/images/change_icon.png"), "sube_degistir_hali_stok_listesi_cliced");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            //dataGridModel.ActionButtons.Add(buttonSubeDegistir);
            dataGridModel.ActionButtonWith = "10%";

            dataGridModel.PageSize = 15;

            dataGridModel.DataUrl = Url.Action("GetGridBody");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler(CustomProperties customProperties)
        {
            string sql =
                "select p.id,[tarih],[stok_no],[urun_cesidi],l1.deger as urun_cesidi_adi ,[mensei],p.subesi, l2.deger as mensei_adi ,[uretim_tipi], l3.deger as uretim_tipi_adi," +
                "[material], l4.deger as material_adi ,[olcu_tip], l5.deger as olcu_tip_adi ,[dugum_tip], l7.deger as ana_grubu," +
                "[olcu_boy],[olcu_en],[olcu_mt2],[inc_olcu_boy],[inc_olcu_en],[inc_olcu_mt2],[dugum_sayisi],[renk]," +
                "[satici], l6.deger as satici_adi ,[firma],[konsinye_pesin],[toplam_maliyet],[birim_fiyat],[etiket_carpani],[adet_fiyat],[etiket_fiyat]," +
                "[tamir_yikama],[satis_carpani],[satis_fiyati],p.aciklama,l.deger as sube_adi , ' ' as satis_tarihi, '' as satis_no, " +
                "concat(pp.adi, ' ',pp.soyadi) as konsinye_verilen  " +
                "FROM  el_hali_stok p " +
                "left join el_liste_deger l on l.kod = p.subesi and l.liste_id = 2 " +
                "left join el_liste_deger l1 on l1.kod = p.urun_cesidi and l1.liste_id = 7 " +
                "left join el_liste_deger l2 on l2.kod = p.mensei and l2.liste_id = 8 " +
                "left join el_liste_deger l3 on l3.kod = p.uretim_tipi and l3.liste_id = 9 " +
                "left join el_liste_deger l4 on l4.kod = p.material and l4.liste_id = 10 " +
                "left join el_liste_deger l5 on l5.kod = p.olcu_tip and l5.liste_id = 11 " +
                "left join el_liste_deger l6 on l6.kod = p.satici and l6.liste_id = 13 " +
                "left join el_liste_deger l7 on l7.kod = p.ana_grup and l7.liste_id = 15 " +
                "left join el_personel pp on pp.id = p.konsinye_verilen_id " +
                "where 1 = 1 ";
             

            if (customProperties.ContainsKey("stok_no") && !string.IsNullOrWhiteSpace(customProperties["stok_no"]))
            {
                sql += " and stok_no like '" + customProperties["stok_no"] + "'";
            }

            if (customProperties.ContainsKey("tarih") && !string.IsNullOrWhiteSpace(customProperties["tarih"]))
            {
                sql += " and tarih like '" + customProperties["tarih"] + "'";
            }
            
            if (customProperties.ContainsKey("sube") && !string.IsNullOrWhiteSpace(customProperties["sube"]))
            {
                sql += " and subesi = " + customProperties["sube"] + "";
            }
            if (customProperties.ContainsKey("urun_cesidi") && !string.IsNullOrWhiteSpace(customProperties["urun_cesidi"]))
            {
                sql += " and urun_cesidi = " + customProperties["urun_cesidi"] + "";
            }

            if (customProperties.ContainsKey("mensei") && !string.IsNullOrWhiteSpace(customProperties["mensei"]))
            {
                sql += " and mensei = " + customProperties["mensei"] + "";
            }

            if (customProperties.ContainsKey("uretim_tipi") && !string.IsNullOrWhiteSpace(customProperties["uretim_tipi"]))
            {
                sql += " and uretim_tipi = " + customProperties["uretim_tipi"] + "";
            }

            if (customProperties.ContainsKey("material") && !string.IsNullOrWhiteSpace(customProperties["material"]))
            {
                sql += " and material = " + customProperties["material"] + "";
            }

            if (customProperties.ContainsKey("satıcı") && !string.IsNullOrWhiteSpace(customProperties["satıcı"]))
            {
                sql += " and satıcı = " + customProperties["satıcı"] + "";
            }


            ElDbTable table = ExecuteTable(sql);
            table.KeyField = "id";

            return table;
        }

        /// <summary>
        /// Grid body 
        /// </summary>
        public ActionResult GetGridBody(GridParameter parameter)
        {
            ElDataGridModel model = FillGridModel(parameter.CustomProperties);
            // model.SetParameter(parameter);

            if (!string.IsNullOrWhiteSpace(parameter.OrderBy))
            {
                string orderby = parameter.OrderBy;
                orderby = orderby.Remove(orderby.Length - 2, 2);

                model.GridTable.GridTable.Orderby = orderby;
            }
            //else
            //    model.GridTable.GridTable.Orderby = " order by id asc";

            //model.GridTable.GridTable.OffsetFetch = Convert.ToInt32(parameter.PageSize);
            //model.GridTable.GridTable.Offset = (Convert.ToInt32(parameter.PageNumber)-1) * Convert.ToInt32(parameter.PageSize);

            return PartialView("ElGrid/ElDataGridBody", model);
        }

        public JsonResult DeleteRow(int id)
        {
            ElResult result = new ElResult();
            result.SetOk();

            try
            {
                ElDbTable db = GetTable("el_hali_stok");
                db.Delete("id", id);
            }
            catch (Exception e)
            {
                result.SetError();
                result.AddErrorMessage(e.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}