using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLandingTemplate.Models;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;
using WebLandingTemplateDomainModel.Enums;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplate.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryBusiness _categoryBusiness;
        public CategoryController(CategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        private IEnumerable<SelectListItem> DataItems(int pageSize)
        {
            const int valueA = 3, valueB = 6, valueC = 9, valueD = 15;
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem() { Value = null, Text = "---Select---" });
            list.Add(new SelectListItem() { Value = valueA.ToString(), Text = valueA.ToString(), Selected = false });
            list.Add(new SelectListItem() { Value = valueB.ToString(), Text = valueB.ToString(), Selected = false });
            list.Add(new SelectListItem() { Value = valueC.ToString(), Text = valueC.ToString(), Selected = false });
            list.Add(new SelectListItem() { Value = valueD.ToString(), Text = valueD.ToString(), Selected = false });

            switch (pageSize)
            {
                case valueA:
                    list.ElementAtOrDefault(0).Selected = true;
                    break;
                case 6:
                    list.ElementAtOrDefault(1).Selected = true;
                    break;
                case 9:
                    list.ElementAtOrDefault(2).Selected = true;
                    break;
                case 15:
                    list.ElementAtOrDefault(3).Selected = true;
                    break;
                default:
                    break;
            }
                                                    
            return new SelectList(list, "Value", "Text","Selected");
        }

        private IEnumerable<SelectListItem> DataFilterType()
        {
            var istenum  = Enum.GetValues(typeof(ItemCodeTypeEnum)).Cast<ItemCodeTypeEnum>().Select(p => new SelectListItem()
            {
            Text = p.ToString(),
            Value = ((int)p).ToString()
            }).ToList();

            return new SelectList(istenum, "Value", "Text", "Selected");
        }

        // GET: Category
        public ActionResult Index(int? page, string searchString, int pageSize = 3)
        {
            ViewBag.dropdownsrc = DataItems(pageSize);
            ViewBag.typesrc = DataFilterType();

            int pageNumber = (page ?? 1);
            var listaVM = new List<CategoryVM>();
           

            if (!string.IsNullOrEmpty(searchString))
            {
                var listaDto =_categoryBusiness.GetAllCategory().Where(c => c.Name.Contains(searchString));
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }
            else
            {
                var listaDto = _categoryBusiness.GetAllCategory();
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }

            return View(listaVM.ToPagedList(pageNumber, pageSize));

        }
        
        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var itemDto = _categoryBusiness.GetCategory(id);
            var itemVM = new CategoryVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            ViewBag.ModalName = "Detalles de Categoria";
            ViewBag.GoTo = "Details";
            return PartialView("ModalCategory", itemVM);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            ViewBag.ModalName = "Crear Categoría";
            ViewBag.GoTo = "Create";
            return PartialView("ModalCategory");
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(CategoryVM itemVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var itemDto = new CategoryDto();
                    AutoMapper.Mapper.Map(itemVM, itemDto);
                    var result = _categoryBusiness.InsertCategory(itemDto);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return PartialView();
                }
            }
            else
            {
                return View("Create","_AdminLayout",itemVM);
                //return PartialView("ModalCategory", itemVM);
            }

           
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var itemDto = _categoryBusiness.GetCategory(id);
            var itemVM = new CategoryVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            ViewBag.ModalName = "Editar Categoría";
            ViewBag.GoTo = "Edit";
            //return PartialView(itemVM);
            return PartialView("ModalCategory", itemVM);
            //return View(itemVM);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryVM itemVM)
        {
            if (ModelState.IsValid) {
                try
                {
                    var itemDto = new CategoryDto();
                    AutoMapper.Mapper.Map(itemVM, itemDto);
                    var result = _categoryBusiness.UpdateCategory(itemDto);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View("Create","_AdminLayout",itemVM);
                //return PartialView("ModalCategory", itemVM);
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var itemDto = _categoryBusiness.GetCategory(id);
            var itemVM = new CategoryVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            return PartialView(itemVM);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CategoryVM itemVM)
        {
            try
            {
                var itemDto = new CategoryDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _categoryBusiness.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Category/Eliminar/5
        [HttpPost]
        public bool Eliminar(int id)
        {
            try
            {
                return true;
                var result = _categoryBusiness.DeleteCategory(id);
                if (result == "Succes")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }



    }
}