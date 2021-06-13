using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Controllers.PersonelBilgileri.Personel;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using elrincon.web.Models.Yonetim;

namespace elrincon.web.Controllers.Yonetim.DomainListesi
{
    public class DomainEditorController : DomainListeBaseController
    {
        // GET: editor
        public ActionResult Index(int id)
        {
            DomainEditorModel model = new DomainEditorModel();
            model.ListeGridModel =  FillModel(id, FillGridCustomProperties());
            model.ListeGridModel.IsWrite = false;
            model.ListeId = id;
            model.ListeAdi = "Domain Ekle";

            model.ListeAdiInputModel = new ElInputModel(InputDataType.Text);

            if(id > 0)
            {
                DataRow row = GetTableDomainListeRow(id);
                model.ListeAdi = row["liste_adi"].ToString();
                model.ListeAdiInputModel = new ElInputModel(model.ListeAdi,InputDataType.Text);
            }
            model.ListeAdiInputModel.Id = "yeni_domain_liste_adi";

            return View("~/Views/yonetim/DomainListe/DomainEditor.cshtml", model);
        }

        //editor
        public ActionResult OpenEditor(int? id , int listeId)
        {
            DomainEditorModel model = new DomainEditorModel();
            string deger = "";
            
            if(id.HasValue && id.Value > 0)
            {
                string sql = "select * from el_liste_deger where id = " + id + "";
                ElDbTable tabled = ExecuteTable(sql);
                DataRow row = tabled.Rows[0];
                deger = Convert.ToString(row["deger"]);
            }

            model.NewDomainName = new ElInputModel(deger,InputDataType.Text);
            model.NewDomainName.Id = "domain_name_input_id"; 
            model.ListeId = listeId;
            model.ChildId = id;

            return View("~/Views/yonetim/DomainListe/DomainEditorDuzenle.cshtml", model);
        }


        private CustomProperties FillGridCustomProperties()
        {
            CustomProperties customProperties = new CustomProperties();
            customProperties.Add("adi", "");
            return customProperties;
        }

        public JsonResult SaveOrUpdateChild(int? listeid, int? id, string deger)
        {
            ElResult result = ControlResult(deger);
            
            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            int lastid = 0;
            if ((id.HasValue && id.Value == 0) || !id.HasValue)
            {
                string sql = "select TOP 1 * from el_liste_deger where liste_id = " + listeid + " order by kod desc ";
                ElDbTable tabled = ExecuteTable(sql);
                if (tabled.Rows.Count == 0)
                    lastid = 0;
                else
                {
                    DataRow row = tabled.Rows[0];
                    lastid = Convert.ToInt32(row["kod"]);
                }
            }

            ElDbTable table = GetTable("el_liste_deger");
            
            try
            {
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("liste_id", CHelper.ToInt32(listeid));
                bufferModel.Add("deger", CHelper.ToString(deger));
                
                if (!id.HasValue ||id.Value == 0)
                {
                    bufferModel.Add("kod", lastid + 1);
                    table.InsertTable(bufferModel);
                }
                else
                    table.UpdateTable(bufferModel, id.Value);

                result.SetOk();
            }
            catch (Exception e)
            {
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SaveOrUpdateList(int? listeid, string listeadi)
        {
            ElResult result = ControlResultListe(listeadi);
            
            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);
             
            ElDbTable table = GetTable("el_liste"); 
            
            table.KeyField = "liste_id";
            try
            {
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("liste_adi", CHelper.ToString(listeadi));
                
                if (!listeid.HasValue || listeid.Value == 0)
                    table.InsertTable(bufferModel);
                else
                {
                    //bufferModel.Add("liste_id", CHelper.ToInt32(listeid));
                    table.UpdateTable(bufferModel, listeid.Value,"liste_id");
                }

                result.SetOk();
            }
            catch (Exception e)
            {
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private ElResult ControlResult(string deger)
        {
            ElResult result = new ElResult();
            
            if (string.IsNullOrWhiteSpace(deger))
                result.AddMessage("Domain adı Boş Olamaz.");
           
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
        private ElResult ControlResultListe(string deger)
        {
            ElResult result = new ElResult();
            
            if (string.IsNullOrWhiteSpace(deger))
                result.AddMessage("Liste adı Boş Olamaz.");
           
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }

        public JsonResult DeleteRow(int id)
        {
            ElResult result = new ElResult();
            result.SetOk();

            try
            {
                ElDbTable db = GetTable("el_liste_deger");
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