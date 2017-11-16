using HtmlAgilityPack;
using LEAF.VDSC.CORE.Dao;
using LEAF.VDSC.CORE.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace LEAF.VDSC.CORE.Services
{
    public interface ITMonService
    {
        IList<Product> SelectOverProducts(int discount);
        void getDiscountProducts();
    }
    public class TMonService : BasicService, ITMonService
    {
        ITMonDao tmonDao;

        private const string basicUrl = "http://www.ticketmonster.co.kr";
        public TMonService(ITMonDao TMonDao)
        {
            tmonDao = TMonDao;
        }

        public IList<Product> SelectOverProducts(int discount)
        {
            return tmonDao.SelectOverProducts(discount);
        }

        public void getDiscountProducts()
        {
            try
            {
                IList<Product> products = new List<Product>();

                HtmlNodeCollection mainViewResults = ParseHTML(basicUrl + "/home"
                                      , "//p[contains(@class,'percent')]");

                if (mainViewResults != null)
                {
                    for (int j = 0; j < mainViewResults.Count; j++)
                    {
                        int discount = Int32.Parse(mainViewResults[j].InnerText.Split('%')[0].Trim());
                        if (discount >= discountMinimum)
                        {
                            Product product = new Product { Id = Guid.NewGuid(), Discount = discount, Detail = mainViewResults[j].ParentNode.ParentNode.InnerText };
                            products.Add(product);
                        }
                    }
                }

                tmonDao.InsertProducts(products);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
