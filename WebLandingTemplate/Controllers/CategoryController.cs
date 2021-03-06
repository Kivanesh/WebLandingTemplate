﻿using PagedList;
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
        IProductBusiness _productBusiness;
        IServiceCorpBusiness _svcBusiness;

        public CategoryController(ProductBusiness productBusiness, ServiceCorpBusiness svcBusiness, CategoryBusiness categoryBusiness)
        {
            _productBusiness = productBusiness;
            _svcBusiness = svcBusiness;
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
            list.Insert(0, new SelectListItem() { Value = null, Text = "--Seleciona--", Selected = false });

            switch (pageSize)
            {
                case valueA:
                    list.ElementAtOrDefault(0).Selected = true;
                    break;
                case 6:
                    list.ElementAtOrDefault(1).Selected = true;
                    list.ElementAtOrDefault(1).Text = "6";
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
            istenum.Insert(0,new SelectListItem() { Value = null, Text = "--Seleciona--", Selected = false });

            return new SelectList(istenum, "Value", "Text", "Selected");
        }

        // GET: Category
        public ActionResult Index(int? page, string searchString, int pageSize = 5, int? filterType = null)
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

            if (filterType != null)
            {
                listaVM = listaVM.Where(s => s.ItemCodeType == filterType).ToList();
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
                //return true;



                var categoryObj = _categoryBusiness.GetCategory(id);
                switch (categoryObj.ItemCodeType)
                {
                    case 1:
                        var itemLstP = _productBusiness.GetAllProducts().Where(c => c.ProductType == id).ToList();
                        foreach (ProductDto i in itemLstP)
                        {
                            i.ProductType = 16;
                            var res = _productBusiness.UpdateProduct(i);
                        }
                        break;
                    case 2:
                        var itemLstS = _svcBusiness.GetAllService().Where(c => c.ServiceType == id).ToList();
                        foreach (ServiceCorpDto i in itemLstS)
                        {
                            i.ServiceType = 17;
                            var res = _svcBusiness.UpdateService(i);
                        }
                        break;

                }
                var result = _categoryBusiness.DeleteCategory(id);

                if (result == "Succes")
                {
                    
                    


                    return true;
                }
                else
                {
                    switch(categoryObj.ItemCodeType)
                    {
                        case 1:
                            var itemLstP = _productBusiness.GetAllProducts().Where(c => c.ProductType == id).ToList();
                        foreach (ProductDto i in itemLstP)
                        {
                            i.ProductType = id;
                            var res = _productBusiness.UpdateProduct(i);
                        }
                        break;
                        case 2:
                            var itemLstS = _svcBusiness.GetAllService().Where(c => c.ServiceType == id).ToList();
                        foreach (ServiceCorpDto i in itemLstS)
                        {
                            i.ServiceType = id;
                            var res = _svcBusiness.UpdateService(i);
                        }
                        break;

                    }
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