using HtmlAgilityPack;
using LEAF.VDSC.CORE.Dao;
using LEAF.VDSC.CORE.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEAF.VDSC.CORE.Services
{
    public interface IGmarketService
    {
        IList<Product> SelectOverProducts(int discount);
        void getDiscountProducts();
    }

    public class GmarketService : BasicService, IGmarketService
    {
        IGmarketDao gmarketDao;

        private const string basicUrl = "http://corners.gmarket.co.kr";

        public GmarketService(IGmarketDao GmarketDao)
        {
            gmarketDao = GmarketDao;
        }

        public IList<Product> SelectOverProducts(int discount)
        {
            return gmarketDao.SelectOverProducts(discount);
        }

        public void getDiscountProducts()
        {
            try
            {
                IList<Product> products = new List<Product>();

                HtmlNodeCollection superDealResults = ParseHTML(basicUrl + "/SuperDeals"
                                      , "//em[contains(@class,'sale')]");

                if (superDealResults != null)
                {
                    for (int j = 0; j < superDealResults.Count; j++)
                    {
                        int discount = Int32.Parse(superDealResults[j].InnerText.Split('%')[0].Trim());
                        if (discount >= discountMinimum)
                        {
                            Product product = new Product { Id = Guid.NewGuid(), Discount = discount, Detail = superDealResults[j].ParentNode.ParentNode.ParentNode.InnerText };
                            products.Add(product);
                        }
                    }
                }

                gmarketDao.InsertProducts(products);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
