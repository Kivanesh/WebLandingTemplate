using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLandingTemplate.Models;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;
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

        private IEnumerable<SelectListItem> DataItems()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = null, Text = "---Select---" });
            list.Add(new SelectListItem() { Value = "10", Text = "10" });
            list.Add(new SelectListItem() { Value = "25", Text = "25" });
            list.Add(new SelectListItem() { Value = "50", Text = "50" });
            list.Add(new SelectListItem() { Value = "100", Text = "100" });
                                                    
            return new SelectList(list, "Value", "Text");
        }

        // GET: Category
        public ActionResult Index(int? page, string searchString, int pageSize = 5)
        {
            ViewBag.dropdownsrc = DataItems();
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

            return PartialView(itemVM);
        }

        // GET: Product/Details/5
        public ActionResult DetailsModal(int id)
        {
            var itemDto = _categoryBusiness.GetCategory(id);
            var itemVM = new CategoryVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);

            return PartialView("ModalCategory", itemVM);
        }


        // GET: Category/Create
        public ActionResult Create()
        {
            ViewBag.ModalName = "Crear Categoría";
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
            return PartialView(itemVM);
            //return View(itemVM);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoryVM itemVM)
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






























    }
}